namespace EgSlackInvite.Models.Requests
{
    using System;
    using System.Collections.Generic;

    public class MeetingInvite
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Body { get; set; }
        public List<string> Attendees { get; set; }
    }
}
