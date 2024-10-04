namespace EgSlackInvite.Services.Concrete.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Abstract;
    using Abstract.Client;
    using Abstract.Service;
    using Models.Responses;

    public class SlackChatClientUserService: IChatClientUserService
    {
        private readonly ISlackUserApiClient _apiClient;

        public SlackChatClientUserService(ISlackUserApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Task<string[]> GetEmailAddressesForUsers(IList<string> userIds)
        {
            var emails = userIds
                .Select(_apiClient.GetEmailAddressForUserId)
                .ToList();

            return Task.WhenAll(emails);
        }

        /// <inheritdoc cref="IChatClientUiService"/>
        public async Task<IList<ChatClientUserProfile>> GetChatClientUserProfiles(ChatClientChannelInfo channelInfo)
        {
            var users = channelInfo.Members.Select(_apiClient.GetProfileForUserId).ToList();

            return await Task.WhenAll(users);
        }
    }
}
