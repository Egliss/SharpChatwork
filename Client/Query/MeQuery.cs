using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Client.Query
{
	internal class MeQuery : ClientQuery, IMeQuery
	{
		public MeQuery(IChatworkClient client) : base(client)
		{
		}

		public ValueTask<Status> GetMyStatusAsync()
		{
			throw new NotImplementedException();
		}

		public ValueTask<List<UserTask>> GetMyTasksAsync()
		{
			throw new NotImplementedException();
		}

		public ValueTask<User> GetUserAsync()
		{
			throw new NotImplementedException();
		}
	}
}
