namespace EgSlackInvite.Services.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Abstract;
    using Infrastructure.Abstract;
    using Infrastructure.Configuration;
    using Models.Requests;
    using Newtonsoft.Json;
    using Utilities;

    public class SlackChatClientUiService : IChatClientUiService
    {

        private readonly IHttpHandler _httpHandler;

        public SlackChatClientUiService(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }

        /// <summary> Creates a dialog in the Slack Client </summary>
        /// <param name="triggerId">The <see cref="string"/> trigger Id to respond to.</param>
        /// <returns>A <see cref="Task"/></returns>
        public async Task CreateDialog(string triggerId)
        {
            var oAuthToken = SlackApiConfig.SlackOAuthToken;
            var endpoint = new Uri(SlackApiConfig.SlackOpenDialogEndpoint);

            var slackDialog = CreateSlackDialog();

            var postRequest = new SlackDialogPostRequest
            {
                TriggerId = triggerId,
                Dialog = slackDialog
            };

            var serialized = JsonConvert.SerializeObject(postRequest);

            await _httpHandler.PostWithAuthorizationAsJsonAsync(oAuthToken, endpoint, serialized);
        }

        /// <summary>
        /// Creates a new meeting invite dialog.
        /// </summary>
        /// <returns>An instance of <see cref="ChatClientDialog"/></returns>
        private ChatClientDialog CreateSlackDialog() => new ChatClientDialog
        {
            CallBackId = $"meet={Guid.NewGuid()}",
            Title = "Create A Meeting Invite",
            SubmitLabel = "Submit",
            State = "Unsent",
            Elements = new List<Element>
                    {
                        new Element
                        {
                            Type = "text",
                            Label = "Title",
                            Placeholder = "The name of the meeting",
                            Hint = "Enter the title of the meeting or event.",
                            Name = "meeting_name",
                            MaxLength = 50
                        },
                        new Element
                        {
                            Type = "select",
                            Label = "Meeting Date",
                            Name = "meeting_date",
                            Hint = "Select a date to book the meeting.",
                            Options = DateTimeUtilities.GetDateTextValues(2).Select(x => new DropDownOption
                            {
                                Label = x,
                                Value = x

                            }).ToList()
                        },
                        new Element
                        {
                            Type = "text",
                            Name = "start_time",
                            Label = "Start Time",
                            Placeholder = "12:00 am/pm",
                            Hint = "Enter time in 12 hour format suffixed by am/pm",
                            MinLength = 3,
                            MaxLength = 10
                        },
                        new Element
                        {
                            Type = "text",
                            Name = "end_time",
                            Label = "End Time",
                            Placeholder = "12:00 am/pm",
                            Hint = "Enter time in 12 hour format suffixed by am/pm",
                            MinLength = 3,
                            MaxLength = 10
                        },
                        new Element
                        {
                            Type = "textarea",
                            Name = "message_body",
                            Label = "Meeting Description",
                            MaxLength = 300
                        }
                    }
        };
    }
}
