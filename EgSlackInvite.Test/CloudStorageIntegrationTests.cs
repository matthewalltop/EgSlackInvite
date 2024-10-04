namespace EgSlackInvite.Test
{
    using System.Linq;
    using System.Threading.Tasks;
    using CloudProvider.Concrete;
    using CloudProvider.Entities;
    using FluentAssertions;
    using Xunit;

    public class CloudStorageIntegrationTests: TestBase
    {

        [Fact(Skip = "Integration"), Trait("Category", "Integration")]
        public async Task CanAddUserToCloudStorage()
        {
            // Arrange
            const string userId = "1234567890";
            const string userEmail = "test.account@fake_email_domain.com";
            var systemUnderTest = new UserSettingsClient();
            var whiteListEntity = new UserSettingsEntity
            {
                RowKey = userId,
                Email = userEmail
            };

            // Act
            await systemUnderTest.AddUserSettingsEntityAsync(whiteListEntity);
            var result = await systemUnderTest.GetUserSettingsAsync(false);

            // Assert
            var enumerable = result.ToList();
            enumerable.Count().Should().BeGreaterThan(0);
            enumerable.FirstOrDefault(x => x.RowKey == userId).Should().NotBeNull(because: "The Id was recovered from the cloud service.");
        }

        [Fact(Skip = "Integration"), Trait("Category", "Integration")]
        public async Task CanRemoveUserFromCloudStorage()
        {
            // Arrange
            const string userId = "1234567890";
            const string userEmail = "test.account@fake_email_domain.com";
            var systemUnderTest = new UserSettingsClient();

            // Act
            await systemUnderTest.RemoveUserSettingsAsync(userId, userEmail);
            var result = await systemUnderTest.GetUserSettingsAsync(false);


            // Assert
            result.Count().Should().Be(0);
        }
    }
}
