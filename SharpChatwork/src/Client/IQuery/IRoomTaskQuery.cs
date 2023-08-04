using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    public interface IRoomTaskQuery
    {
        public ValueTask<IEnumerable<UserTask>> GetllAsync(long roomId, long accountId, long autherId, bool isDone = false, CancellationToken cancellation = default);
        public ValueTask<ElementId> CreateAsync(long roomId, string taskText, long limit, CancellationToken cancellation = default);
        public ValueTask<UserTask> GetAsync(long roomId, long taskId, CancellationToken cancellation = default);
        public ValueTask<ElementId> UpdateAsync(long roomId, long taskId, TaskStateType state, CancellationToken cancellation = default);
    }
}
