namespace SlackApiClient.Abstract.Service
{
    using System.Threading.Tasks;
    using Models.Responses;

    public interface IChatClientChannelService
    {
        /// <summary>
        /// Returns the channel info for the given Id-
        /// </summary>
        /// <param name="channelDtoData">The <see cref="ChannelDto"/> containing the name and Id for the channel.</param>
        /// <param name="isPrivate">Indicates if the channel is private or not</param>
        /// <returns>A <see cref="Task{T}"/></returns>
        Task<ChatClientChannelInfo> GetChannel(ChannelDto channelDtoData, bool isPrivate = false);
    }
}
