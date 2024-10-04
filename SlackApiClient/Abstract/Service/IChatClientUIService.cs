namespace SlackApiClient.Abstract.Service
{
    using System.Threading.Tasks;

    public interface IChatClientUiService
    {
        Task CreateDialog(string triggerId);
    }
}
