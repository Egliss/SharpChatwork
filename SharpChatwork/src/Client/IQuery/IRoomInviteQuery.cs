using System.Threading;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query;

public interface IRoomInviteQuery
{
    public ValueTask<InviteLink> GetAsync(long roomId, CancellationToken cancellation = default);
    public ValueTask<InviteLink> CreateAsync(long roomId, string uniqueName, string description, bool requireAcceptance, CancellationToken cancellation = default);
    public ValueTask<InviteLink> UpdateAsync(long roomId, string uniqueName, string description, bool requireAcceptance, CancellationToken cancellation = default);
    public ValueTask DestroyAsync(long roomId, CancellationToken cancellation = default);
}
