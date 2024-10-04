﻿namespace EgSlackInvite.Services.Concrete.Strategy
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure.Abstract;
    using Infrastructure.Configuration;
    using Models.Responses;
    using Newtonsoft.Json;
    using Utilities;

    public class SlackPrivateChannelStrategy : ISlackChannelStrategy
    {
        private readonly string _oAuthToken = SlackApiConfig.SlackOAuthToken;

        private const string SELECTED_JSON_PROPERTY = "group";

        private readonly IHttpHandler _handler;
        public SlackPrivateChannelStrategy(IHttpHandler handler)
        {
            _handler = handler;
        }

        /// <inheritdoc cref="ISlackChannelStrategy"/>
        public async Task<ChatClientChannelInfo> GetSlackChannelInfo(ChannelDto channelDtoData)
        {
            var endPoint = new Uri(SlackApiConfig.SlackPrivateChannelUri(channelDtoData.Id));
            var httpResult = await _handler.GetWithAuthorizationAsync(_oAuthToken, endPoint);
            var content = await httpResult.Content.ReadAsStringAsync();

            
            var channel = content.ExtractJsonPropertyFromString(SELECTED_JSON_PROPERTY);
            var chatClientChannelInfo = JsonConvert.DeserializeObject<ChatClientChannelInfo>(channel.ToString());

            return chatClientChannelInfo;
        }
    }
}
