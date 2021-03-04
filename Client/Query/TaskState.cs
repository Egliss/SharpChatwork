using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Query
{
    public enum TaskStateType
    {
        [EnumAlias("open")]
        Open,
        [EnumAlias("done")]
        Done
    }
}
