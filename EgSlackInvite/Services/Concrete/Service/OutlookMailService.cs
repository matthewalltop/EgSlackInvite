namespace EgSlackInvite.Services.Concrete.Service
{
    using System.Linq;
    using Abstract;
    using Microsoft.Exchange.WebServices.Data;
    using Models.Requests;

    /// <summary>
    /// This class is a facade over the ExchangeService in the Microsoft.Exchange.WebServices package.
    /// </summary>
    public class OutlookMailService: OutlookMailServiceBase, IMailService
    {
        public async System.Threading.Tasks.Task SendMeetingInvite(MeetingInvite invite)
        {
            var app = new Appointment(Service)
            {
                Subject = invite.Name,
                Body = invite.Body,
                Start = invite.Start,
                End = invite.End,
                Location = invite.Location
            };

            invite.Attendees.ToList()
                            .ForEach(x => app.RequiredAttendees.Add(x));

            await app.Save(SendInvitationsMode.SendToAllAndSaveCopy);

            // Verify meeting was created.
            await Item.Bind(Service, app.Id, new PropertySet(ItemSchema.Subject));
        }

    }
}
