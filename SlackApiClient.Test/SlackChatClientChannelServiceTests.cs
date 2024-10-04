namespace SlackApiClient.Test
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Concrete.Service;
    using Concrete.Strategy;
    using FluentAssertions;
    using Models.Responses;
    using Moq;
    using Xunit;

    public class SlackChatClientChannelServiceTests : TestBase
    {
        private readonly SlackChatClientChannelService _systemUnderTest;
        private readonly Mock<ISlackStrategyFactory> _strategyFactory;
        private readonly Mock<ISlackChannelStrategy> _channelStrategy;
        
        public SlackChatClientChannelServiceTests()
        {
            _channelStrategy = new Mock<ISlackChannelStrategy>();
            _strategyFactory = new Mock<ISlackStrategyFactory>();
            _strategyFactory.Setup(x => x.GetStrategy(true)).Returns(_channelStrategy.Object);
            

            _systemUnderTest = new SlackChatClientChannelService(_strategyFactory.Object);
        }

        [Fact]
        [Category("Integration")]
        public async Task ServiceGetsChannelInfo()
        {
            // Arrange
            var mockChannel = new ChannelDto
            {
                Id = "GD7EB0RU7",
                Name = "privategroup"
            };

            _channelStrategy.Setup(x => x.GetSlackChannelInfo(It.IsAny<ChannelDto>()))
                .Returns(Task.FromResult(new ChatClientChannelInfo{ Id=mockChannel.Id, Name = mockChannel.Name}));

            // Act
            var result = await _systemUnderTest.GetChannel(mockChannel, true);


            // Assert
            result.Should().BeOfType<ChatClientChannelInfo>(because: "An object was successfully returned and deserialized");
            result.Id.Should().BeEquivalentTo(mockChannel.Id, because: "The correct channel was returned");
        }
    }
}