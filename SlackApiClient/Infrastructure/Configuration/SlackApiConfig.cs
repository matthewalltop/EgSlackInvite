namespace SlackApiClient.Infrastructure.Configuration
{
    using System;

    public static class SlackApiConfig
    {
        public static string SlackOAuthToken
            => Environment.GetEnvironmentVariable("slackoauthToken");

        public static string SlackBaseApiEndpoint
            => Environment.GetEnvironmentVariable("slackBaseApiUri");

        public static string SlackOpenDialogEndpoint
            => "dialog.open";

        public static string SlackDefaultPrivateChannelName
            => Environment.GetEnvironmentVariable("slackDefaultPrivateChannelName");

        public static string SlackPrivateChannelUri
            => $"groups.info";

        public static string SlackPublicChannelUri
            => $"channels.info";

        public static string SlackUserDataUri
            => $"users.info";

    }
}
