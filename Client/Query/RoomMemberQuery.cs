using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Client.Query
{
	internal class RoomMemberQuery : ClientQuery, IRoomMemberQuery
	{
		public RoomMemberQuery(IChatworkClient client) : base(client)
		{
		}

		public ValueTask<List<User>> GetAllAsync(long roomId)
		{
			throw new NotImplementedException();
		}

		public ValueTask<RoomMember> UpdateAsync(long roomId, IEnumerable<long> adminsMembers, IEnumerable<long> normalMembers, IEnumerable<long> readonlyMembers)
		{
			throw new NotImplementedException();
		}
	}
}
