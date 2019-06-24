using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Client
{
    public interface IChatworkQueryResult
    {
        ChatworkQueryInfo info { get; set; }
    }
}
