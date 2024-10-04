namespace SlackApiClient.Abstract.Client
{
    using Infrastructure.Configuration;
    using RestSharp;

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
