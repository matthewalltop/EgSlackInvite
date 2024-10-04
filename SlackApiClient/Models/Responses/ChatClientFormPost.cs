namespace SlackApiClient.Models.Responses
{
    using System;
    using Newtonsoft.Json;

    public class ChatClientFormPost
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("action_ts")]
        public string ActionTs { get; set; }

        [JsonProperty("team")]
        public TeamDto TeamDto { get; set; }

        [JsonProperty("user")]
        public UserDto UserDto { get; set; }

        [JsonProperty("channel")]
        public ChannelDto ChannelDto { get; set; }

        [JsonProperty("submission")]
        public SubmissionDto SubmissionDto { get; set; }
    }

    public class SubmissionDto
    {
        [JsonProperty("meeting_name")]
        public string MeetingName { get; set; }

        [JsonProperty("message_body")]
        public string MessageBody { get; set; }

        [JsonProperty("meeting_date")]
        public DateTime MeetingDate { get; set; }

        [JsonProperty("start_time")]
        public string StartTime { get; set; }

        [JsonProperty("end_time")]
        public string EndTime { get; set; }
    }

    public class ChannelDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class UserDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class TeamDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("domain")]
        public string Domain { get; set; }
    }
}
