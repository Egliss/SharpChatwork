using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query;

public interface IRoomMessageQuery
{
    public ValueTask<MessageId> SendAsync(long roomId, string message, bool isSelfUnread, CancellationToken cancellation = default);

    public ValueTask<MessageReadUnread> ReadAsync(long roomId, long messageId, CancellationToken cancellation = default);
    public ValueTask<MessageReadUnread> UnReadAsync(long roomId, long messageId, CancellationToken cancellation = default);

    public ValueTask<IEnumerable<UserMessage>> GetAllAsync(long roomId, bool isForceMode = false, CancellationToken cancellation = default);
    public ValueTask<UserMessage> GetAsync(long roomId, long messageId, CancellationToken cancellation = default);

    public ValueTask<MessageId> UpdateAsync(long roomId, long messageId, string message, CancellationToken cancellation = default);
    public ValueTask<MessageId> RemoveAsync(long roomId, long messageId, CancellationToken cancellation = default);
}
