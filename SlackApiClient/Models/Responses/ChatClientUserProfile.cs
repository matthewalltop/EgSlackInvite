namespace SlackApiClient.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Classes used for the serialization of Slack User Response.
    /// </summary>
    public class ChatClientUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("profile")]
        public ChatClientUserProfile Profile { get; set; }
    }

    public class SlackApiUserResponse
    {
        [JsonProperty("ok")]
        public string Ok { get; set; }

        [JsonProperty("user")]
        public ChatClientUser User { get; set; }
    }

    public class SlackApiUserListResponse
    {
        [JsonProperty("ok")]
        public string Ok { get; set; }

        [JsonProperty("members")]
        public IEnumerable<ChatClientUser> Members { get; set; }
    }

    public class ChatClientUserProfile
    {
        [JsonProperty("avatar_hash")]
        public string AvatarHash { get; set; }

        [JsonProperty("status_text")]
        public string StatusText { get; set; }

        [JsonProperty("status_emoji")]
        public string StatusEmoji { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("real_name_normalized")]
        public string RealNameNormalized { get; set; }

        [JsonProperty("display_name_normalized")]
        public string DisplayNameNormalized { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }
    }
}
