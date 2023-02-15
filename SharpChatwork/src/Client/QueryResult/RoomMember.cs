using System.Collections.Generic;

namespace SharpChatwork.Query.Types
{
    public class RoomMember
    {
        public List<long> admin { get; set; } = new List<long>();
        public List<long> member { get; set; } = new List<long>();
        public List<long> @readonly { get; set; } = new List<long>();
    }
}
