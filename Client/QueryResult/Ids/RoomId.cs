using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Query.Types.Ids
{
    public class RoomId : ElementId
    {
        public long room_id
        {
            get => this.id;
            set => this.id = value;
        }
    }
}
