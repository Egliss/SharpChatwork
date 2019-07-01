using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
	public interface IRoomMessageQuery
	{
		ValueTask<ElementId> SendAsync(long roomId, string message, bool isSelfUnread);

		ValueTask<MessageReadUnread> ReadAsync(long roomId, long messageId);
		ValueTask<MessageReadUnread> UnReadAsync(long roomId, long messageId);

		ValueTask<List<UserMessage>> GetAllAsync(long roomId, bool isForceMode = false);
		ValueTask<UserMessage> GetAsync(long roomId, long messageId);

		ValueTask<ElementId> UpdateAsync(long roomId, long messageId, string message);
		ValueTask<ElementId> RemoveAsync(long roomId, long messageId);
	}
}
