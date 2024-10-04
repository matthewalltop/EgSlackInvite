namespace EgSlackInvite.Services.Abstract.Client
{
    using System.Collections.Generic;
    using Microsoft.Exchange.WebServices.Data;

    public interface IMailboxClient
    {
        /// <summary> Returns a collection of calendar events for a given channel </summary>
        /// <returns>An <see cref="IEnumerable{T}"/></returns>
        IEnumerable<CalendarEvent> GetCalendarEventsForChannel();

        /// <summary> Returns a given calendar event </summary>
        /// <returns> An <see cref="IEnumerable{T}"/></returns>
        CalendarEvent GetCalendarEvent();
    }
}
