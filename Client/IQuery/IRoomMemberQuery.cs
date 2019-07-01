using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
	public interface IRoomMemberQuery
	{
		ValueTask<List<User>> GetAllAsync(long roomId);
		ValueTask<RoomMember> UpdateAsync(long roomId, IEnumerable<long> adminsMembers, IEnumerable<long> normalMembers, IEnumerable<long> readonlyMembers);
	}
}
