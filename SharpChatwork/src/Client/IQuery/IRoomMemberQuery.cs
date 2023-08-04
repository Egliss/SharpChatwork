using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    public interface IRoomMemberQuery
    {
        public ValueTask<IEnumerable<User>> GetAllAsync(long roomId, CancellationToken cancellation = default);
        public ValueTask<RoomMember> UpdateAsync(long roomId, IEnumerable<long> adminsMembers, IEnumerable<long> normalMembers, IEnumerable<long> readonlyMembers, CancellationToken cancellation = default);
    }
}
