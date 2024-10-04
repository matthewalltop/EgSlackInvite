namespace EgSlackInvite.Services.Concrete.Service
{
    using System.Threading.Tasks;
    using Abstract;
    using Models.Responses;
    using Strategy;

    public class SlackChatClientChannelService : IChatClientChannelService
    {
        private readonly ISlackStrategyFactory _strategyFactory;
        public SlackChatClientChannelService(ISlackStrategyFactory strategyFactory)
        {
            _strategyFactory = strategyFactory;
        }

        /// <summary>
        /// Returns a formatted channel object from the Slack Api.
        /// </summary>
        /// <param name="channelDtoData">A <see cref="ChannelDto"/> object used to fetch the channel from Slack</param>
        /// /// <param name="isPrivate">Indicates if the channel is private or not.</param>
        /// <returns></returns>
        public async Task<ChatClientChannelInfo> GetChannel(ChannelDto channelDtoData, bool isPrivate)
        {
            var strategy = this._strategyFactory.GetStrategy(isPrivate);
            return await strategy.GetSlackChannelInfo(channelDtoData);
        }
    }
}
