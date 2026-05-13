using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.Query;

public interface IIncomingRequestQuery
{
    public ValueTask<IEnumerable<IncomingRequest>> GetAllAsync(CancellationToken token = default);
    public ValueTask<IncomingRequest> AcceptAsync(long requestId, CancellationToken token = default);
    public ValueTask CancelAsync(long requestId, CancellationToken token = default);
}
