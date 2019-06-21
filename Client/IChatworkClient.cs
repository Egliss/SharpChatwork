using SharpChatwork.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork
{
    public interface IChatworkClient
    {
        IChatworkQueryResult Query(IChatworkQuery query);
        Task<IChatworkQueryResult> QueryAsync(IChatworkQuery query);

    }
}
