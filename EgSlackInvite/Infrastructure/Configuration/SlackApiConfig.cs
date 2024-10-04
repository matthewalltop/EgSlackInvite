namespace EgSlackInvite.Infrastructure.Configuration
{
    using System;

    public static class SlackApiConfig
    {
        public static string SlackOAuthToken
            => Environment.GetEnvironmentVariable("slackoauthToken");

        public static string SlackBaseApiEndpoint
            => Environment.GetEnvironmentVariable("slackBaseApiUri");

        public static string SlackOpenDialogEndpoint
            => $"{SlackBaseApiEndpoint}/dialog.open";

        public static string SlackDefaultPrivateChannelName
            => Environment.GetEnvironmentVariable("slackDefaultPrivateChannelName");

        public static string SlackPrivateChannelUri(string channelId)
            => $"{SlackBaseApiEndpoint}/groups.info?channel={channelId}";

        public static string SlackPublicChannelUri(string channelId)
            => $"{SlackBaseApiEndpoint}/channels.info?channel={channelId}";

        public static string SlackUserDataUri(string userId)
            => $"{SlackBaseApiEndpoint}/users.info?user={userId}";

    }
}
