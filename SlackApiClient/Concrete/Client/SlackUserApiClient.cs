namespace SlackApiClient.Concrete.Client
{
    using System.Threading.Tasks;

    using RestSharp;

    using Abstract.Client;
    using Infrastructure.Configuration;
    using Models.Responses;

    public class SlackUserApiClient : BaseSlackApiClient, ISlackUserApiClient
    {
        public SlackUserApiClient() : base(SlackApiConfig.SlackUserDataUri)
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
}