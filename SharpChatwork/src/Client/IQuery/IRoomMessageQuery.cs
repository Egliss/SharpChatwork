using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    public interface IRoomMessageQuery
    {
        public ValueTask<ElementId> SendAsync(long roomId, string message, bool isSelfUnread, CancellationToken cancellation = default);

        public ValueTask<MessageReadUnread> ReadAsync(long roomId, long messageId, CancellationToken cancellation = default);
        public ValueTask<MessageReadUnread> UnReadAsync(long roomId, long messageId, CancellationToken cancellation = default);

        public ValueTask<IEnumerable<UserMessage>> GetAllAsync(long roomId, bool isForceMode = false, CancellationToken cancellation = default);
        public ValueTask<UserMessage> GetAsync(long roomId, long messageId, CancellationToken cancellation = default);

        public ValueTask<ElementId> UpdateAsync(long roomId, long messageId, string message, CancellationToken cancellation = default);
        public ValueTask<ElementId> RemoveAsync(long roomId, long messageId, CancellationToken cancellation = default);
    }
}
