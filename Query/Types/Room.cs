using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Query.Types
{
    public class Room
    {
        public int room_id { get; set; } = -1;
        public string name { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
        public bool sticky { get; set; } = false;
        public int unread_num { get; set; } = 0;
        public int mention_num { get; set; } = 0;
        public int mytask_num { get; set; } = 0;
        public int message_num { get; set; } = 0;
        public int file_num { get; set; } = 0;
        public int task_num { get; set; } = 0;
        public string icon_path { get; set; } = string.Empty;
        public int last_update_time { get; set; } = 0;
    }
}
