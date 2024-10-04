namespace SlackApiClient.Utilities
{
    using Newtonsoft.Json.Linq;

    public static class JsonExtensions
    {
        /// <summary>
        /// Extracts a value from a JSON object, given the key name.
        /// </summary>
        /// <param name="json">The JSON to search</param>
        /// <param name="sectionName">The key to search for</param>
        /// <returns>A <see cref="JToken"/></returns>
        public static JToken ExtractJsonPropertyFromString(this string json, string sectionName)
        {
            var parsed = JObject.Parse(json);
            var selection = parsed[sectionName];

            return selection;
        }

    }
}
