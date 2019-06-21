using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Client.Query
{
    public interface IQueryScope
    {
        string scope { get; }
        ScopeType scopeType { set; }
    }
}
