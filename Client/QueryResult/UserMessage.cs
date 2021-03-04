namespace SharpChatwork.Query.Types
{
    public class UserMessage
    {
        public string message_id { get; set; }
        public User account { get; set; }
        public string body { get; set; }
        public int send_time { get; set; }
        public int update_time { get; set; }
    }
}
