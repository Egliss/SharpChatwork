using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Query.Types
{
    public class RoomMember
    {
        public List<int> admin { get; set; } = new List<int>();
        public List<int> member { get; set; } = new List<int>();
        public List<int> @readonly { get; set; } = new List<int>();
    }
}
