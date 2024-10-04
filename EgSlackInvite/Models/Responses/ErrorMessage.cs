namespace EgSlackInvite.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ErrorList
    {
        [JsonProperty("errors")]
        public List<ErrorMessage> Errors{ get; set; } = new List<ErrorMessage>();
    }

    public class ErrorMessage
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
