using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Query
{
    public interface IChatworkQueryResult
    {
        ChatworkQueryInfo info { get; set; }
    }
}
