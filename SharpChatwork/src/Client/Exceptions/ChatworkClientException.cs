using System;
using System.Runtime.Serialization;

namespace SharpChatwork.Client.Exceptions
{
    public class ChatworkClientException : Exception
    {
        public ChatworkClientException()
        {
        }

        public ChatworkClientException(string message) : base(message)
        {
        }

        public ChatworkClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ChatworkClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
