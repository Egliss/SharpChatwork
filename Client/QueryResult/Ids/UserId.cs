using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Query.Types
{
    public class UserId : ElementId
    {
        public long user_id
        {
            get => this.id;
            set => this.id = value;
        }
    }
}
