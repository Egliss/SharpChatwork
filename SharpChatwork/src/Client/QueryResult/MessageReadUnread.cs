#pragma warning disable CA1707 // Underscore

namespace SharpChatwork.Query.Types
{
    public class MessageReadUnread
    {
        public int unread_num { get; set; }
        public int mention_num { get; set; }
    }
}
