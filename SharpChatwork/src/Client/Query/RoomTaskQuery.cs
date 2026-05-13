using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query;

internal sealed class RoomTaskQuery(IChatworkClient client) : ClientQuery(client), IRoomTaskQuery
{
    public ValueTask<TaskId> CreateAsync(long roomId, string taskText, long limit, CancellationToken token = default)
    {
        //var data = new Dictionary<string, string>()
        //{
        //    { "body" , taskText },
        //    { "limit" , limit},
        //    { "limit_type" , doneString},
        //    { "to_ids" , doneString},
        //};
        //return await this.QueryAsync<List<UserTask>>(EndPoints.RoomTasks(roomId), HttpMethod.Get, data);
        throw new NotImplementedException();
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
