using System;
using System.Runtime.Serialization;

namespace RetryPattern.Exceptions
{
    public class RetryExceededException : Exception
    {
        public RetryExceededException()
        {
        }

        public RetryExceededException(string message) : base(message)
        {
        }

        public RetryExceededException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RetryExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
