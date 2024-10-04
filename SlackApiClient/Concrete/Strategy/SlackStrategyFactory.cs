namespace SlackApiClient.Concrete.Strategy
{
    using Autofac.Features.Indexed;

    public class SlackStrategyFactory : ISlackStrategyFactory
    {
        private readonly IIndex<SlackChannelAccess, ISlackChannelStrategy> _classes;

        public SlackStrategyFactory(IIndex<SlackChannelAccess, ISlackChannelStrategy> classes)
        {
            this._classes = classes;
        }

        public ISlackChannelStrategy GetStrategy(bool isPrivate)
        {
            return isPrivate 
                ? this._classes[SlackChannelAccess.Private] 
                : this._classes[SlackChannelAccess.Public];
        }
        
    }
}
