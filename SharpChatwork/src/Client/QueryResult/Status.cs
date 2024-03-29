#pragma warning disable CA1707 // Underscore

namespace SharpChatwork.Query.Types
{
    public class Status
    {
        public int unread_room_num { get; set; }
        public int mention_room_num { get; set; }
        public int mytask_room_num { get; set; }
        public int unread_num { get; set; }
        public int mention_num { get; set; }
        public int mytask_num { get; set; }
    }
}
