namespace EgSlackInvite.Services.Abstract
{
    using System.Threading.Tasks;
    using Models.Requests;

    public interface IMailService
    {
        Task SendMeetingInvite(MeetingInvite invite);
    }
}
