using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query;


namespace SharpChatwork
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
