namespace SlackApiClient.Concrete.Strategy
{
    using System.Threading.Tasks;
    using Models.Responses;

    public interface ISlackChannelStrategy
    {
        Task<ChatClientChannelInfo> GetSlackChannelInfo(ChannelDto channelDtoData);
    }
}
