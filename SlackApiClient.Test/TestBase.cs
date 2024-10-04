namespace SlackApiClient.Test
{
    using System;

    public abstract class TestBase
    {
        protected TestBase()
        {
            Environment.SetEnvironmentVariable("slackoauthToken", "");
            Environment.SetEnvironmentVariable("slackBaseApiUri", "https://slack.com/api");
            Environment.SetEnvironmentVariable("slackDialogApiUri", "https://slack.com/api/dialog.open");
            Environment.SetEnvironmentVariable("slackDefaultPrivateChannelName", "privategroup");
            Environment.SetEnvironmentVariable("slackGroupApiUri", "https://slack.com/api/groups.info?channel=");
            Environment.SetEnvironmentVariable("slackChannelApiUri", "https://slack.com/api/channels.info?channel=");
            Environment.SetEnvironmentVariable("slackUserApiUri", "https://slack.com/api/users.info?user=");
            Environment.SetEnvironmentVariable("AzureStorageConnection", "");
        }
    }
}
