using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query;

internal sealed class IncomingRequestQuery(IChatworkClient client) : ClientQuery(client), IIncomingRequestQuery
{
    public async ValueTask<IncomingRequest> AcceptAsync(long requestId, CancellationToken token = default)
    {
        return await this.chatworkClient.QueryAsync<IncomingRequest>(EndPoints.IncomingRequestsOf(requestId), HttpMethod.Put, new Dictionary<string, string>(), token);
    }

    public async ValueTask CancelAsync(long requestId, CancellationToken token = default)
    {
        await this.chatworkClient.QueryAsync(EndPoints.IncomingRequestsOf(requestId), HttpMethod.Delete, new Dictionary<string, string>(), token);
    }

    public async ValueTask<IEnumerable<IncomingRequest>> GetAllAsync(CancellationToken token = default)
    {
        return await this.chatworkClient.QueryAsync<List<IncomingRequest>>(EndPoints.IncomingRequests, HttpMethod.Get, new Dictionary<string, string>(), token);
    }
}
