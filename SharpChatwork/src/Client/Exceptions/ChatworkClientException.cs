using System;

namespace SharpChatwork.Client.Exceptions;

public class ChatworkClientException(ResponseWrapper response) : Exception
{
    public readonly ResponseWrapper Response = response;

    public override string ToString()
    {
        return base.ToString() + $" --> ChatworkClientException: {this.Response.statusCode} {this.Response.content}";
    }
}
