using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Query.Types
{
    public class MessageReadUnread
    {
        public int unread_num { get; set; }
        public int mention_num { get; set; }
    }
}
