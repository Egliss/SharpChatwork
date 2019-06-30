using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Client.Query
{
	public interface IRoomTaskQuery 
	{
		ValueTask<List<UserTask>> GetllAsync(long roomId, long accountId, long autherId, bool isDone = false);
		ValueTask<ElementId> CreateAsync(long roomId, string taskText, long limit);
		ValueTask<UserTask> GetAsync(long roomId, long taskId);
		ValueTask<ElementId> UpdateAsync(long roomId, long taskId, TaskStateType state);
	}
}
