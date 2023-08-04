using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    public interface IIncomingRequestQuery
    {
        ValueTask<IEnumerable<IncomingRequest>> GetAllAsync(CancellationToken token = default);
        ValueTask<IncomingRequest> AcceptAsync(long requestId, CancellationToken token = default);
        ValueTask CancelAsync(long requestId, CancellationToken token = default);
    }
}
