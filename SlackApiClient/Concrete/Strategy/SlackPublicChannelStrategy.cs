namespace SlackApiClient.Concrete.Strategy
{
    using System.Threading.Tasks;
    using Abstract.Client;
    using Infrastructure.Configuration;
    using Models.Responses;
    using Newtonsoft.Json;
    using RestSharp;
    using Utilities;

    public class SlackPublicChannelStrategy : BaseSlackApiClient, ISlackChannelStrategy
    {
        private const string SELECTED_JSON_PROPERTY = "channel";

        public SlackPublicChannelStrategy(): base(SlackApiConfig.SlackPublicChannelUri)
        {
        }

        /// <inheritdoc cref="ISlackChannelStrategy"/>
        public async Task<ChatClientChannelInfo> GetSlackChannelInfo(ChannelDto channelDtoData)
        {

            var request = new RestRequest();
            request.AddQueryParameter("channel", channelDtoData.Id);

            var result = await RestClient.ExecuteGetTaskAsync(request);

            var content = result.Content;

            var channel = content.ExtractJsonPropertyFromString(SELECTED_JSON_PROPERTY);
            var chatClientChannelInfo = JsonConvert.DeserializeObject<ChatClientChannelInfo>(channel.ToString());

            return chatClientChannelInfo;
        }
    }
}
