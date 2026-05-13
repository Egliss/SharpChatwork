using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query;

public interface IRoomTaskQuery
{
    public ValueTask<IEnumerable<UserTask>> GetAllAsync(long roomId, long? accountId = null, long? assignedByAccountId = null, TaskStateType? status = null, CancellationToken cancellation = default);
    public ValueTask<TaskIds> CreateAsync(long roomId, string taskText, IEnumerable<long> toIds, long limit, TaskLimitType limitType, CancellationToken cancellation = default);
    public ValueTask<UserTask> GetAsync(long roomId, long taskId, CancellationToken cancellation = default);
    public ValueTask<TaskId> UpdateAsync(long roomId, long taskId, TaskStateType state, CancellationToken cancellation = default);
}
