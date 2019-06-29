using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Client.Query
{
	internal class RoomTaskQuery : ClientQuery, IRoomTaskQuery
	{
		public RoomTaskQuery(IChatworkClient client) : base(client)
		{
		}

		public ValueTask<ElementId> CreateAsync(long roomId, string taskText, long limit)
		{
			throw new NotImplementedException();
		}

		public ValueTask<UserTask> GetAsync(long roomId, long taskId)
		{
			throw new NotImplementedException();
		}

		public ValueTask<List<UserTask>> GetllAsync(long roomId, long accountId, long autherId, bool isDone = false)
		{
			throw new NotImplementedException();
		}

		public ValueTask<ElementId> UpdateAsync(long roomId, long taskId, TaskStateType state)
		{
			throw new NotImplementedException();
		}
	}
}
