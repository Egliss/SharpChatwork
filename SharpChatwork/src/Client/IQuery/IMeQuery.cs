using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    public interface IMeQuery
    {
        public ValueTask<Status> GetMyStatusAsync(CancellationToken cancellation = default);
        public ValueTask<IEnumerable<UserTask>> GetMyTasksAsync(CancellationToken cancellation = default);
        public ValueTask<User> GetUserAsync(CancellationToken cancellation = default);
    }
}
