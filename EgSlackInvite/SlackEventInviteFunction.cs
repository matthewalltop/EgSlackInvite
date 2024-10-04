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
    using Infrastructure;
    using SlackApiClient.Abstract.Service;

    /// <summary>
    /// Handles Slash command from Slack and responds with a Create a Meeting Dialog.
    /// </summary>
    public static class SlackEventInviteFunction
    {
        [FunctionName("SlackEventInviteFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req, ILogger log)
        {

            if (!req.Form.TryGetValue("user_id", out var userId))
            {
                log.LogInformation("Unable to get user Id from request");
                return new BadRequestResult();
            }

            // The trigger id of the action must be attached to the request to the Slack API in order to show the dialog.
            if (!req.Form.TryGetValue("trigger_id", out var triggerId))
            {
                log.LogInformation("Unable to get trigger Id from request.");
                return new BadRequestResult();
            }

            var container = IocRegistration.Container;
            var chatService = container.Resolve<IChatClientUiService>();
            var cloudService = container.Resolve<IUserSettingsClient>();

            var whiteList = await cloudService.GetUserSettingsAsync();

            var user = whiteList.FirstOrDefault(x => x.RowKey == userId);

            if (!user.IsWhiteListed)
              return new UnauthorizedResult();

            await chatService.CreateDialog(triggerId);
            return new OkResult();
        }

    }
}
