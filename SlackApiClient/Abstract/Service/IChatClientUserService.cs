namespace SlackApiClient.Abstract.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Responses;

    public interface IChatClientUserService
    {
        /// <summary>
        /// Returns a collection of users from the chat client.
        /// </summary>
        /// <param name="channelInfo">The <see cref="ChatClientChannelInfo"/> object returned from the Client</param>
        /// <returns>An <see cref="IEnumerable{T}"/></returns>
        Task<IList<ChatClientUserProfile>> GetChatClientUserProfiles(ChatClientChannelInfo channelInfo);


        /// <summary>
        /// Returns emails for given user Ids
        /// </summary>
        /// <param name="userIds">An <see cref="IList{T}"/> of user Ids</param>
        /// <returns>A <see cref="Task{T}"/></returns>
        Task<string[]> GetEmailAddressesForUsers(IList<string> userIds);
    }
}
