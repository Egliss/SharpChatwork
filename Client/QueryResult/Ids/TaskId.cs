using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Query.Types.Ids
{
    public class TaskId : ElementId
    {
        public long task_id
        {
            get => this.id;
            set => this.id = value;
        }
    }
}
