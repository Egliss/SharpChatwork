using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Client
{
    public class AccessTokenBaseClient : IChatworkClient
    {
        public IChatworkQueryResult Query(IChatworkQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<IChatworkQueryResult> QueryAsync(IChatworkQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
