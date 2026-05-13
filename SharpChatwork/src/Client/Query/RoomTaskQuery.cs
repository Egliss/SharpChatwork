using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query;

internal sealed class RoomTaskQuery(IChatworkClient client) : ClientQuery(client), IRoomTaskQuery
{
    public async ValueTask<TaskIds> CreateAsync(long roomId, string taskText, IEnumerable<long> toIds, long limit, TaskLimitType limitType, CancellationToken token = default)
    {
        var data = new Dictionary<string, string>
        {
            {"body", taskText},
            {"to_ids", JoinIds(toIds)},
            {"limit", limit.ToString(CultureInfo.InvariantCulture)},
            {"limit_type", limitType.ToAliasOrDefault()},
        };
        return await this.chatworkClient.QueryAsync<TaskIds>(EndPoints.RoomTasks(roomId), HttpMethod.Post, data, token);
    }

    private static string JoinIds(IEnumerable<long> ids)
    {
        return string.Join(",", ids.Select(id => id.ToString(CultureInfo.InvariantCulture)));
    }

    public async ValueTask<UserTask> GetAsync(long roomId, long taskId, CancellationToken token = default)
    {
        return await this.chatworkClient.QueryAsync<UserTask>(EndPoints.RoomTasksOf(roomId, taskId), HttpMethod.Get, new Dictionary<string, string>(), token);
    }

    public async ValueTask<IEnumerable<UserTask>> GetAllAsync(long roomId, long accountId, long autherId, bool isDone = false, CancellationToken token = default)
    {
        var doneString = isDone ? "done" : "open";
        var uri = $"{EndPoints.RoomTasks(roomId)}"
            + $"?account_id={accountId.ToString(CultureInfo.InvariantCulture)}"
            + $"&assigned_by_account_id={autherId.ToString(CultureInfo.InvariantCulture)}"
            + $"&status={doneString}";
        return await this.chatworkClient.QueryAsync<List<UserTask>>(new Uri(uri), HttpMethod.Get, new Dictionary<string, string>(), token);
    }

    public async ValueTask<TaskId> UpdateAsync(long roomId, long taskId, TaskStateType state, CancellationToken token = default)
    {
        var data = new Dictionary<string, string>
        {
            {"body", state.ToAliasOrDefault()},
        };
        return await this.chatworkClient.QueryAsync<TaskId>(EndPoints.RoomTasksOfStatus(roomId, taskId), HttpMethod.Put, data, token);
    }
}
