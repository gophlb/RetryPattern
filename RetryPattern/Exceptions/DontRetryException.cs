using System;
using System.Runtime.Serialization;

namespace RetryPattern.Exceptions
{
    public class DontRetryException : Exception
    {
        public DontRetryException()
        {
        }

        public DontRetryException(string message) : base(message)
        {
        }

        public DontRetryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DontRetryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
