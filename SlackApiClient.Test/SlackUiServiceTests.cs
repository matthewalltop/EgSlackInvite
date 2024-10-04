namespace SlackApiClient.Test
{
    using System;
    using System.Threading.Tasks;
    using Concrete.Service;
    using Xunit;

    public class SlackUiServiceTests: TestBase
    {

        private readonly SlackChatClientUiService _systemUnderTest;

        public SlackUiServiceTests()
        {
            _systemUnderTest = new SlackChatClientUiService();
        }

        [Fact]
        public async Task ServicePostsDialog()
        {
            // Arrange
            var triggerId = Guid.NewGuid().ToString();

            // Act
            await _systemUnderTest.CreateDialog(triggerId);

        }
    }
}