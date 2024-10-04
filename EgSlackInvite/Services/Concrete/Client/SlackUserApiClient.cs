namespace EgSlackInvite.Services.Concrete.Client
{
    using System.Threading.Tasks;
    using Abstract.Client;
    using RestSharp;

    using Infrastructure.Configuration;
    using Models.Responses;



    public class SlackUserApiClient : BaseSlackApiClient, ISlackUserApiClient
    {
        public SlackUserApiClient() : base("users.info")
        {
        }
       
        public async Task<string> GetEmailAddressForUserId(string userId)
        {
            // looks like this -> https://slack.com/api/users.info?user={userId}
            var request = new RestRequest();
            request.AddQueryParameter("user", userId);

            var response = await RestClient.ExecuteGetTaskAsync<SlackApiUserResponse>(request);
            
            return response?.Data?.User?.Profile?.Email;
        }

        public async Task<ChatClientUserProfile> GetProfileForUserId(string userId)
        {
            // looks like this -> https://slack.com/api/users.info?user={userId}
            var request = new RestRequest();
            request.AddQueryParameter("user", userId);

            var response = await RestClient.ExecuteGetTaskAsync<SlackApiUserResponse>(request);

            return response?.Data?.User?.Profile;
        }
    }
    
    public abstract class BaseSlackApiClient
    {
        protected readonly RestClient RestClient;

        protected BaseSlackApiClient(string resource)
        {
            RestClient = new RestClient($"{SlackApiConfig.SlackBaseApiEndpoint}/{resource}");
            RestClient.AddDefaultHeader("Authorization", $"Bearer {SlackApiConfig.SlackOAuthToken}");
        }
    }
}