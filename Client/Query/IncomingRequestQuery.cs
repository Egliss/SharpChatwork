using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query
{
	internal class IncomingRequestQuery : ClientQuery, IIncomingRequestQuery
	{
		public IncomingRequestQuery(IChatworkClient client) : base(client)
		{
		}

        public async ValueTask<IncomingRequest> AcceptAsync(long requestId)
        {
            return await this.chatworkClient.QueryAsync<IncomingRequest>(EndPoints.IncomingRequestsOf(requestId), HttpMethod.Post,new Dictionary<string, string>());
        }

        public async ValueTask CancelAsync(long requestId)
        {
            await this.chatworkClient.QueryAsync(EndPoints.IncomingRequestsOf(requestId), HttpMethod.Delete, new Dictionary<string, string>());
        }

        public async ValueTask<IEnumerable<IncomingRequest>> GetAllAsync()
        {
            return await this.chatworkClient.QueryAsync<List<IncomingRequest>>(EndPoints.IncomingRequests, HttpMethod.Get, new Dictionary<string, string>());
        }
    }
}
