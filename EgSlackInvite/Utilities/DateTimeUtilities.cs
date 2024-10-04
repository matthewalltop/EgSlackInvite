namespace EgSlackInvite.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public static class DateTimeUtilities
    {
        /// <summary>
        /// Returns a long date string of all days between today and specified months in the future.
        /// </summary>
        /// <param name="numberOfMonths"></param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="string"/></returns>
        public static IEnumerable<string> GetDateTextValues(int numberOfMonths)
        {
            var today = DateTime.Now;
            var endDate = DateTime.Now.AddMonths(numberOfMonths);

            var selectedDates = Enumerable.Range(0, endDate.Subtract(today).Days + 1)
                .Select(index => today.AddDays(index))
                .ToList().Select(x => x.ToLongDateString());
            return selectedDates;
        }

        /// <summary> Returns a properly formatted <see cref="DateTime"/> given a date and a timestamp string.</summary>
        /// <param name="baseDate">The <see cref="DateTime"/> to set a timestamp on.</param>
        /// <param name="timeValue">The <see cref="string"/> time value to append to the <see cref="DateTime"/></param>
        /// <returns>A new instance of <see cref="DateTime"/></returns>
        public static DateTime FormatTimeForEmail(DateTime baseDate, string timeValue)
        {
            DateTime result;

            if (ParseMultipleFormats(timeValue, out var parsedTime))
                result = baseDate + parsedTime;
            else
                throw new InvalidTimeFormatException($"{timeValue} cannot be parsed into a valid timestamp.");
            

            return result;
        }

        /// <summary>
        /// Parses a time value against multiple formats and returns the result.
        /// </summary>
        /// <param name="timeValue">The <see cref="string"/> representation of the time</param>
        /// <param name="parsedTime">The <see cref="TimeSpan"/> to be returned.</param>
        /// <returns>A <see cref="bool"/></returns>
        public static bool ParseMultipleFormats(string timeValue, out TimeSpan parsedTime)
        {
            var formats = new[] {"hh:mm tt", "h:mm tt", "hh:mmtt", "h:mmtt"};

            if (!DateTime.TryParseExact(timeValue.ToUpperInvariant(), formats,
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var successfulParse)) return false;

            parsedTime = successfulParse.TimeOfDay;
            return true;
        }
    }
}
