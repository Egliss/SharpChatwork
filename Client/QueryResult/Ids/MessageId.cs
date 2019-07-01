using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Query.Types.Ids
{
    public class MessageId : ElementId
    {
        public long message_id
        {
            get => this.id;
            set => this.id = value;
        }
    }
}
