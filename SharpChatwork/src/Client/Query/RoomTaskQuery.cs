using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace SharpChatwork.Query
{
    internal sealed class RoomTaskQuery : ClientQuery, IRoomTaskQuery
    {
        public RoomTaskQuery(IChatworkClient client) : base(client)
        {
        }

        public ValueTask<ElementId> CreateAsync(long roomId, string taskText, long limit, CancellationToken token = default)
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

        public async ValueTask<IEnumerable<UserTask>> GetllAsync(long roomId, long accountId, long autherId, bool isDone = false, CancellationToken token = default)
        {
            var doneString = "done";
            if(!isDone)
                doneString = "open";
            var data = new Dictionary<string, string>()
            {
                { "account_id" , accountId.ToString() },
                { "assigned_by_account_id" , autherId.ToString()},
                { "status" , doneString},
            };
            return await this.chatworkClient.QueryAsync<List<UserTask>>(EndPoints.RoomTasks(roomId), HttpMethod.Get, data, token);
        }

        public async ValueTask<ElementId> UpdateAsync(long roomId, long taskId, TaskStateType state, CancellationToken token = default)
        {
            var uri = $"{EndPoints.RoomTasksOf(roomId, taskId)}?body={state.ToAliasOrDefault()}";
            return await this.chatworkClient.QueryAsync<TaskId>(new Uri(uri), HttpMethod.Post, new Dictionary<string, string>(), token);
        }
    }
}
