namespace SlackApiClient.Abstract.Client
{
    using System.Threading.Tasks;
    using Models.Responses;

    public interface ISlackUserApiClient
    {
        Task<ChatClientUserProfile> GetProfileForUserId(string userId);

        Task<string> GetEmailAddressForUserId(string userId);
    }
}
