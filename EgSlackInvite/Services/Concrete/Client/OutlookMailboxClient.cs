namespace EgSlackInvite.Services.Concrete.Client
{
    using System;
    using System.Collections.Generic;
    using Abstract;
    using Abstract.Client;
    using Microsoft.Exchange.WebServices.Data;

    public class OutlookMailboxClient : OutlookMailServiceBase, IMailboxClient
    {
        public IEnumerable<CalendarEvent> GetCalendarEventsForChannel()
        {
            throw new NotImplementedException();
        }

        public CalendarEvent GetCalendarEvent()
        {
            throw new NotImplementedException();
        }
    }
}
