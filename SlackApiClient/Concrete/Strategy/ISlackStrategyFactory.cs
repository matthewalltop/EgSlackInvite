namespace SlackApiClient.Concrete.Strategy
{
    public interface ISlackStrategyFactory
    {
        ISlackChannelStrategy GetStrategy(bool isPrivate);
    }
}