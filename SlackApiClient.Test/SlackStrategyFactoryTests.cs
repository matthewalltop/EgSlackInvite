namespace SlackApiClient.Test
{
    using System.Collections.Generic;
    using Autofac.Features.Indexed;
    using Concrete.Strategy;
    using FluentAssertions;
    using Xunit;

    public class SlackStrategyFactoryTests: TestBase
    {
        private readonly SlackStrategyFactory _systemUnderTest;

        public SlackStrategyFactoryTests()
        {
            // Arrange
            IIndex<SlackChannelAccess, ISlackChannelStrategy> classes = new MockIndex<SlackChannelAccess, ISlackChannelStrategy>
            {
                {SlackChannelAccess.Public, new SlackPublicChannelStrategy()},
                {SlackChannelAccess.Private, new SlackPrivateChannelStrategy()}
            };
            _systemUnderTest = new SlackStrategyFactory(classes);
        }

        [Fact]
        public void FactorySelectsPublicStrategyProvidedFalseValue()
        {
            // Act
            var result = _systemUnderTest.GetStrategy(false);

            // Assert
            result.Should()
                .BeOfType<SlackPublicChannelStrategy>(because: "The correct strategy was returned from the factory");
        }

        [Fact]
        public void FactorySelectsPrivateStrategyProvidedTrueValue()
        {
            // Act
            var result = _systemUnderTest.GetStrategy(true);

            // Assert
            result.Should()
                .BeOfType<SlackPrivateChannelStrategy>(because: "The correct strategy was returned from the factory");
        }


    }

    internal class MockIndex<T, TR> : Dictionary<T, TR>, IIndex<T, TR>
    {
    }
}
