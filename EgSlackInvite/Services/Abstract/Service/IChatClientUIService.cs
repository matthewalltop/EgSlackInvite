namespace EgSlackInvite.Services.Abstract
{
    using System.Threading.Tasks;

    public interface IChatClientUiService
    {
        Task CreateDialog(string triggerId);
    }
}
