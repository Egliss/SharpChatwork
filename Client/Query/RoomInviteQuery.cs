using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Client.Query
{
	internal class RoomInviteQuery : ClientQuery, IRoomInviteQuery
	{
		public RoomInviteQuery(IChatworkClient client) : base(client)
		{
		}

		public ValueTask<InviteLink> CreateAsync(long roomId, string uniqueName, string description, bool requireAcceptance)
		{
			throw new NotImplementedException();
		}

		public ValueTask DestroyAsync(long roomId)
		{
			throw new NotImplementedException();
		}

		public ValueTask<InviteLink> GetAsync(long roomId)
		{
			throw new NotImplementedException();
		}

		public ValueTask<InviteLink> UpdateAsync(long roomId, string uniqueName, string description, bool requireAcceptance)
		{
			throw new NotImplementedException();
		}
	}
}
