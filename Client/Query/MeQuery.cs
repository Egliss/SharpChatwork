using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query
{
	internal class MeQuery : ClientQuery, IMeQuery
	{
		public MeQuery(IChatworkClient client) : base(client)
		{
		}

		public async ValueTask<Status> GetMyStatusAsync()
		{
            return await this.chatworkClient.QueryAsync<Status>(EndPoints.MyStatus, HttpMethod.Get, new Dictionary<string, string>());
        }

        public async ValueTask<List<UserTask>> GetMyTasksAsync()
		{
            return await this.chatworkClient.QueryAsync<List<UserTask>>(EndPoints.MyTasks, HttpMethod.Get, new Dictionary<string, string>());
        }

        public async ValueTask<User> GetUserAsync()
		{
            return await this.chatworkClient.QueryAsync<User>(EndPoints.Me, HttpMethod.Get, new Dictionary<string, string>());
        }
    }
}
