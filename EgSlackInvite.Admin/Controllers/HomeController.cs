namespace EgSlackInvite.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudProvider.Abstract;
    using CloudProvider.Entities;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    using Models;
    using RestSharp;
    using SlackApiClient.Models.Responses;

    [Microsoft.AspNetCore.Authorization.Authorize]

    public class HomeController : Controller
    {

        public RestClient RestClient { get; set; }
        private readonly IUserSettingsClient _cloudClient;
        public HomeController(IUserSettingsClient cloudClient)
        {
            _cloudClient = cloudClient;
            RestClient = new RestClient($"https://slack.com/api/users.list");
            RestClient.AddDefaultHeader("Authorization", $"Bearer {Environment.GetEnvironmentVariable("slackoauthToken")}");
        }

        [HttpGet]
        [RequestFormSizeLimit(Int32.MaxValue, Order = 1)]
        public async Task<IActionResult> Index()
        {
            // TODO: All of this needs to be handled in a service layer.
            // 1. Users should always be retrieved from the datatable.
            // 2. Seed should be performed well before it reaches this point.
            var userModels = new List<UserModel>();
            var users = await _cloudClient.GetUserSettingsAsync();

            if (users.Any())
            {
                userModels.AddRange(users.Select(x => new UserModel
                {
                    Id = x.RowKey,
                    Email = x.Email,
                    UserName = x.FullName,
                    IsWhiteListed = x.IsWhiteListed,
                    ReceivesEmail = x.ReceivesEmails
                }));

                return View("Index", userModels);
            }

            var request = new RestRequest();
            var response = await RestClient.ExecuteGetTaskAsync<SlackApiUserListResponse>(request);
            var members = response.Data.Members;

            userModels = members.Where(x => !string.IsNullOrEmpty(x.Profile.Email)
                                                && x.Profile.Email.Contains("e-gineering"))
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    UserName = x.Profile.RealName,
                    Email = x.Profile.Email,
                    IsWhiteListed = false, // DEFAULT
                    ReceivesEmail = true // DEFAULT
                }).ToList();


            var whiteListModels = members.Where(x => !string.IsNullOrEmpty(x.Profile.Email)
                                                     && x.Profile.Email.Contains("e-gineering"))
                .Select(x => new UserSettingsEntity(x.Id, x.Profile.Email, x.Profile.RealName, false, true));

            var taskList = whiteListModels.Select(model => _cloudClient.AddUserSettingsEntityAsync(model)).ToList();
            await Task.WhenAll(taskList);

            return View("Index", userModels);
        }

        [HttpPost]
        [RequestFormSizeLimit(Int32.MaxValue, Order = 1)]
        [ValidateAntiForgeryToken(Order = 2)]
        public async Task<IActionResult> UpdateUsers(IEnumerable<UserModel> model)
        {
            ModelState.Clear();
            var entities = model.Select(x => new UserSettingsEntity(x.Id, x.Email, x.UserName, x.IsWhiteListed, x.ReceivesEmail));
            var taskList = entities.Select(x => _cloudClient.AddUserSettingsEntityAsync(x)).ToList();
            await Task.WhenAll(taskList);

            return Content(ModelState.IsValid ? "Posted" : "Not posted");
        }
    }
}
