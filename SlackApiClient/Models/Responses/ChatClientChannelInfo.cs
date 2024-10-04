namespace SlackApiClient.Models.Responses
{
    using Newtonsoft.Json;

    public class ChatClientChannelInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("is_group")]
        public bool IsGroup { get; set; }
        [JsonProperty("created")]
        public int Created { get; set; }
        [JsonProperty("creator")]
        public string Creator { get; set; }
        [JsonProperty("members")]
        public string[] Members { get; set; }
    }
}
