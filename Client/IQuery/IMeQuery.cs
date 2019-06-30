using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Client.Query
{
	public interface IMeQuery : IClientQuery
	{
		ValueTask<Status> GetMyStatusAsync();
		ValueTask<List<UserTask>> GetMyTasksAsync();
		ValueTask<User> GetUserAsync();
	}
}
