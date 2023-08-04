using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    internal sealed class MeQuery : ClientQuery, IMeQuery
    {
        public MeQuery(IChatworkClient client) : base(client)
        {
        }

        public async ValueTask<Status> GetMyStatusAsync(CancellationToken token = default)
        {
            return await this.chatworkClient.QueryAsync<Status>(EndPoints.MyStatus, HttpMethod.Get, new Dictionary<string, string>(), token);
        }

        public async ValueTask<IEnumerable<UserTask>> GetMyTasksAsync(CancellationToken token = default)
        {
            return await this.chatworkClient.QueryAsync<List<UserTask>>(EndPoints.MyTasks, HttpMethod.Get, new Dictionary<string, string>(), token);
        }

        public async ValueTask<User> GetUserAsync(CancellationToken token = default)
        {
            return await this.chatworkClient.QueryAsync<User>(EndPoints.Me, HttpMethod.Get, new Dictionary<string, string>(), token);
        }
    }
}
