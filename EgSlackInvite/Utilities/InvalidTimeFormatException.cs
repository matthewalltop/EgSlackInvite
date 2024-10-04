namespace EgSlackInvite.Utilities
{
    using System;
    using System.Runtime.Serialization;

    public class InvalidTimeFormatException : Exception
    {
        public InvalidTimeFormatException()
        {
        }

        public InvalidTimeFormatException(string message) : base(message)
        {
        }

        public InvalidTimeFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidTimeFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
