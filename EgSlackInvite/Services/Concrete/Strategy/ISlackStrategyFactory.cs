namespace EgSlackInvite.Services.Concrete.Strategy
{
    public interface ISlackStrategyFactory
    {
        ISlackChannelStrategy GetStrategy(bool isPrivate);
    }
}