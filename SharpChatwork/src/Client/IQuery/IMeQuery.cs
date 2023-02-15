using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    public interface IMeQuery
    {
        ValueTask<Status> GetMyStatusAsync();
        ValueTask<IEnumerable<UserTask>> GetMyTasksAsync();
        ValueTask<User> GetUserAsync();
    }
}
