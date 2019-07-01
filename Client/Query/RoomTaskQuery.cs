using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;


namespace SharpChatwork.Query
{
	internal class RoomTaskQuery : ClientQuery, IRoomTaskQuery
	{
		public RoomTaskQuery(IChatworkClient client) : base(client)
		{
		}

		public async ValueTask<ElementId> CreateAsync(long roomId, string taskText, long limit)
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

		public async ValueTask<UserTask> GetAsync(long roomId, long taskId)
		{
            return await this.chatworkClient.QueryAsync<UserTask>(EndPoints.RoomTasksOf(roomId, taskId), HttpMethod.Get,new Dictionary<string, string>());
        }

        public async ValueTask<List<UserTask>> GetllAsync(long roomId, long accountId, long autherId, bool isDone = false)
        {
            var doneString = "done";
            if (!isDone)
                doneString = "open";
            var data = new Dictionary<string, string>()
            {
                { "account_id" , accountId.ToString() },
                { "assigned_by_account_id" , autherId.ToString()},
                { "status" , doneString},
            };
            return await this.chatworkClient.QueryAsync<List<UserTask>>(EndPoints.RoomTasks(roomId), HttpMethod.Get, data);
        }

		public async ValueTask<ElementId> UpdateAsync(long roomId, long taskId, TaskStateType state)
		{
            var uri = $"{EndPoints.RoomTasksOf(roomId, taskId)}?body={state.ToAliasOrDefault()}";
            return await this.chatworkClient.QueryAsync<TaskId>(new Uri(uri), HttpMethod.Post,new Dictionary<string, string>());
        }
	}
}
