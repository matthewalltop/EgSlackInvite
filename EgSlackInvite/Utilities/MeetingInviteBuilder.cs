namespace EgSlackInvite.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CloudProvider.Entities;
    using Models.Requests;
    using Models.Responses;
    using Newtonsoft.Json;
    using SlackApiClient.Models.Responses;

    public class MeetingInviteBuilder
    {
        private readonly MeetingInvite _meetingInvite;
        private readonly ErrorList _errorList = new ErrorList();

        public MeetingInviteBuilder(SubmissionDto submission, 
                                    IEnumerable<string> userEmails, 
                                    IEnumerable<UserSettingsEntity> userSettings)
        {

            var usersToInvite = userEmails.Where(x => userSettings.Any(y => y.Email == x && y.ReceivesEmails));

            _meetingInvite = new MeetingInvite
            {
                Name = submission.MeetingName,
                Location = "Annex", // TODO: Add room options to rawInviteData.
                Attendees = usersToInvite.ToList(),
                Body = submission.MessageBody,
            };


            // TODO: Refactor this.
            try
            {
                SetStartTime(submission.MeetingDate, submission.StartTime);
                SetEndTime(submission.MeetingDate, submission.EndTime);


                // TODO: Not sure if this is how this should be handled.
                if (_meetingInvite.End < _meetingInvite.Start)
                    _meetingInvite.End = _meetingInvite.End.AddDays(1);
            }
            finally
            {

            }
        }

        public MeetingInvite Build()
        {
            return this._meetingInvite;
        }
 
        public void SetStartTime(DateTime dateBase, string timeStamp)
        {
            try
            {
                _meetingInvite.Start =
                    DateTimeUtilities.FormatTimeForEmail(dateBase, timeStamp);
            }
            catch (InvalidTimeFormatException)
            {
                _errorList.Errors.Add(new ErrorMessage
                {
                    Name = "start_time",
                    Error = "Start Time could not be parsed. Please enter in 00:00 am/pm format"
                });
                throw;
            }
        }

        public void SetEndTime(DateTime dateBase, string timeStamp)
        {
            try
            {
                _meetingInvite.End =
                    DateTimeUtilities.FormatTimeForEmail(dateBase, timeStamp);
            }
            catch (InvalidTimeFormatException)
            {
                _errorList.Errors.Add(new ErrorMessage
                {
                    Name = "end_time",
                    Error = "End Time could not be parsed. Please enter in 00:00 am/pm format"
                });
                throw;
            }
        }

        public bool HasErrors()
        {
            return _errorList.Errors.Any();
        }

        public string GetErrors()
        {
            var serializedResponse = JsonConvert.SerializeObject(_errorList);
            return serializedResponse;
        }
            
        
    }
}
