namespace EgSlackInvite
{
    using System.Linq;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    using Autofac;
    using CloudProvider.Abstract;
    using Newtonsoft.Json;

    using Infrastructure;
    using Infrastructure.Configuration;
    using Services.Abstract;
    using Utilities;

    using SlackApiClient.Models.Responses;
    using SlackApiClient.Abstract.Service;

    /// <summary>
    /// Handles the payload from Slack dialog submission.
    /// </summary>
    public static class SlackMeetingRequestFunction
    {
        [FunctionName("SlackMeetingRequestFunction")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            var container = IocRegistration.Container;

            log.LogInformation("C# HTTP trigger function processed a request.");

            // Channel Id is used to obtain channel information + user Ids from Slack Apis
            if (!req.Form.TryGetValue("payload", out var payload))
            {
                log.LogInformation("Unable to get payload from request.");
                return new BadRequestResult();
            }

            var messagePayload = JsonConvert.DeserializeObject<ChatClientFormPost>(payload);
          
            // Slack channels are listed as 'privategroup'. This is important in determining which API endpoint to request the Channel Data from.
            var isPrivateChannel = messagePayload.ChannelDto.Name == SlackApiConfig.SlackDefaultPrivateChannelName;

            var channelService = container.Resolve<IChatClientChannelService>();
            var userService = container.Resolve<IChatClientUserService>();
            var userSettingsClient = container.Resolve<IUserSettingsClient>();

            log.LogInformation("Attemping to get channel information from Slack...");
            var channelData = await channelService.GetChannel(messagePayload.ChannelDto, isPrivateChannel);

            log.LogInformation("Attempting to get channel member data from Slack...");
            var userEmails = await userService.GetEmailAddressesForUsers(channelData.Members);

            log.LogInformation("Acquiring user settings");
            var userSettings = await userSettingsClient.GetUserSettingsAsync();

            if (!userEmails.Any())
            {
                log.LogInformation("No users were returned for this channel.");
                return new BadRequestResult();
            }

            log.LogInformation("Building event from form submission");

            // TODO: I'm not super happy with this. I think there is a better way to go about putting together the meeting; though this is cleaner.
            var meetingBuilder = new MeetingInviteBuilder(messagePayload.SubmissionDto, userEmails, userSettings);

            if (meetingBuilder.HasErrors())
            {
                log.LogInformation("Unable to format a meeting invite with provided form data.");
                return new OkObjectResult(meetingBuilder.GetErrors());
            }

            log.LogInformation("Connecting to Outlook");
            var mailService = container.Resolve<IMailService>();
            await mailService.SendMeetingInvite(meetingBuilder.Build());
            

            log.LogInformation("Meeting request sent to:\n");
            foreach (var user in userEmails)
                log.LogInformation($"{user}\n");

            return new OkResult();
        }

    }
}
