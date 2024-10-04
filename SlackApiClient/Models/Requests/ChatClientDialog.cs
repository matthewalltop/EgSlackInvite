namespace SlackApiClient.Models.Requests
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SlackDialogPostRequest
    {
        [JsonProperty("trigger_id")]
        public string TriggerId { get; set; }

        [JsonProperty("dialog")]
        public ChatClientDialog Dialog { get; set; }
    }


    public class ChatClientDialog
    {
        [JsonProperty("callback_id")]
        public string CallBackId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("submit_label")]
        public string SubmitLabel { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("attachment_type")]
        public string AttachmentType { get; set; }

        [JsonProperty("elements")]
        public IList<Element> Elements { get; set; }
    }

    public class Element
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
   
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("max_length")]
        public int? MaxLength { get; set; }

        [JsonProperty("min_length")]
        public int? MinLength { get; set; }

        [JsonProperty("hint")]
        public string Hint { get; set; }

        [JsonProperty("options")]
        public IList<DropDownOption> Options { get; set; }

        [JsonProperty("placeholder")]
        public string Placeholder { get; set; }

    }

    public class DropDownOption
    {
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
