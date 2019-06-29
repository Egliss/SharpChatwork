using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Client.Query
{
	internal class RoomMessageQuery : ClientQuery, IRoomMessageQuery
	{
		public RoomMessageQuery(IChatworkClient client) : base(client)
		{
		}

		public ValueTask<List<UserMessage>> GetAllAsync(long roomId, bool isForceMode = false)
		{
			throw new NotImplementedException();
		}

		public ValueTask<UserMessage> GetAsync(long roomId, long messageId)
		{
			throw new NotImplementedException();
		}

		public ValueTask<MessageReadUnread> ReadAsync(long roomId, long messageId)
		{
			throw new NotImplementedException();
		}

		public ValueTask<ElementId> RemoveAsync(long roomId, long messageId)
		{
			throw new NotImplementedException();
		}

		public ValueTask<ElementId> SendAsync(long roomId, string message, bool isSelfUnread)
		{
			throw new NotImplementedException();
		}

		public ValueTask<MessageReadUnread> UnReadAsync(long roomId, long messageId)
		{
			throw new NotImplementedException();
		}

		public ValueTask<ElementId> UpdateAsync(long roomId, long messageId, string message)
		{
			throw new NotImplementedException();
		}
	}
}
