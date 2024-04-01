using System;
using System.Runtime.Serialization;

namespace SharpChatwork.Client.Exceptions
{
    public class ChatworkClientException(ResponseWrapper response) : Exception
    {
        public readonly ResponseWrapper response = response;
    }
}
