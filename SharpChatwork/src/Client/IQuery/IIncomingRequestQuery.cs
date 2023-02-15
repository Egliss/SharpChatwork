using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    public interface IIncomingRequestQuery
    {
        ValueTask<IEnumerable<IncomingRequest>> GetAllAsync();
        ValueTask<IncomingRequest> AcceptAsync(long requestId);
        ValueTask CancelAsync(long requestId);
    }
}
