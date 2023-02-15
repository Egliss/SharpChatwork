using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    public interface IRoomMemberQuery
    {
        ValueTask<IEnumerable<User>> GetAllAsync(long roomId);
        ValueTask<RoomMember> UpdateAsync(long roomId, IEnumerable<long> adminsMembers, IEnumerable<long> normalMembers, IEnumerable<long> readonlyMembers);
    }
}
