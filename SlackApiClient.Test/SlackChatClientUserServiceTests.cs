namespace SlackApiClient.Test
{
    using System.Linq;
    using System.Threading.Tasks;
    using Abstract.Client;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Concrete.Client;
    using Concrete.Service;
    using FluentAssertions;
    using Models.Responses;
    using Moq;
    using Xunit;

    public class SlackChatClientUserServiceTests : TestBase
    {
        private readonly IFixture _fixture;


        public SlackChatClientUserServiceTests()
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
        }

        
        private void BuildSlackUserApiClientMock(Mock<ISlackUserApiClient> httpClientMock, string id)
        {
            var profile = new ChatClientUserProfile
            {
                DisplayName = id,
                Email = $"{id}@email.com",
            };

            httpClientMock.Setup(x => x.GetProfileForUserId(id))
                .Returns(Task.FromResult(profile));
        }
        
        [Fact]
        public async Task ShouldMapUserInfoFromProfileJson()
        {
            var channelInfo = new ChatClientChannelInfo
            {
                Members = new[] {"test"}
            };

            var mockClient = _fixture.Freeze<Mock<ISlackUserApiClient>>();
            foreach (var member in channelInfo.Members)
            {
                BuildSlackUserApiClientMock(mockClient, member);
            }

            var sut = _fixture.Create<SlackChatClientUserService>();
            
            //Act
            var response = await sut.GetChatClientUserProfiles(channelInfo);

            var result = response.First();

            // Assert
            mockClient.VerifyAll();

            result.Should().NotBeNull();
            result.DisplayName.Should().BeEquivalentTo("test");
            result.Email.Should().BeEquivalentTo("test@email.com");
        }

        [Fact]
        public async Task ShouldCallHttpWithEachMember()
        {
            var channelInfo = new ChatClientChannelInfo
            {
                Members = new[] {"test", "test2"}
            };

            var mockClient = _fixture.Freeze<Mock<ISlackUserApiClient>>();
            foreach (var member in channelInfo.Members)
            {
                BuildSlackUserApiClientMock(mockClient, member);
            }

            var sut = _fixture.Create<SlackChatClientUserService>();
            //Act
            var response = await sut.GetChatClientUserProfiles(channelInfo);

            // Assert
            mockClient.VerifyAll();

            var firstOne = response.First();
            firstOne.Should().NotBeNull();
            firstOne.DisplayName.Should().BeEquivalentTo("test");
            firstOne.Email.Should().BeEquivalentTo("test@email.com");
            
            var lastOne = response.Last();
            lastOne.Should().NotBeNull();
            lastOne.DisplayName.Should().BeEquivalentTo("test2");
            lastOne.Email.Should().BeEquivalentTo("test2@email.com");
        }
        
        [Fact]
        public async Task ShouldCallSlackUserApi()
        {
            var memberIds = new[] {"test", "test2"};

            // mock calls to slack api
            var mockClient = _fixture.Freeze<Mock<ISlackUserApiClient>>();
            foreach (var member in memberIds)
            {
                mockClient.Setup(x => x.GetEmailAddressForUserId(member))
                    .Returns(Task.FromResult($"{member}@email.com"));
            }
            
            var sut = _fixture.Create<SlackChatClientUserService>();

            //Act
            var actual = await sut.GetEmailAddressesForUsers(memberIds);
            
            //Assert

            actual.Should().Contain(email => email == "test@email.com");
            actual.Should().Contain(email => email == "test2@email.com");
        }
        
        [Fact(Skip = "Integration"), Trait("Category","Integration")]
        public async Task GetEmailForUserIT()
        {
            var apiClient = new SlackUserApiClient();
            var userEmail =  await apiClient.GetEmailAddressForUserId("U03LQDC0K");

            userEmail.Should().Be("brad.stayte@e-gineering.com");
        }
    }
}