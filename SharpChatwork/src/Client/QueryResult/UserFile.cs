#pragma warning disable CA1707 // Underscore

namespace SharpChatwork.Query.Types
{
    public class UserFile
    {
        public int file_id { get; set; }
        public User account { get; set; }
        public string message_id { get; set; }
        public string filename { get; set; }
        public int filesize { get; set; }
        public int upload_time { get; set; }
    }
}
