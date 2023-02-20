namespace SharpChatwork.Client.Exceptions
{
    public class ChatworkRequestException : ChatworkClientException
    {
        public int errorCode { get; }
        public string message { get; }

        public ChatworkRequestException(int errorCode, string message) : base($"Code: {errorCode}, Message: {message}")
        {
            this.errorCode = errorCode;
            this.message = message;
        }
    }
}
