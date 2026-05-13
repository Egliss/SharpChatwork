using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query;

internal sealed class RoomMemberQuery(IChatworkClient client) : ClientQuery(client), IRoomMemberQuery
{
    public async ValueTask<IEnumerable<User>> GetAllAsync(long roomId, CancellationToken token = default)
    {
        return await this.chatworkClient.QueryAsync<List<User>>(EndPoints.RoomMember(roomId), HttpMethod.Get, new Dictionary<string, string>(), token);
    }

    public async ValueTask<RoomMember> UpdateAsync(long roomId, IEnumerable<long> adminsMembers, IEnumerable<long> normalMembers, IEnumerable<long> readonlyMembers, CancellationToken token = default)
    {
        var data = new Dictionary<string, string>
        {
            {"members_admin_ids", JoinIds(adminsMembers)},
            {"members_member_ids", JoinIds(normalMembers)},
            {"members_readonly_ids", JoinIds(readonlyMembers)},
        };
        return await this.chatworkClient.QueryAsync<RoomMember>(EndPoints.RoomMember(roomId), HttpMethod.Put, data, token);
    }

    private static string JoinIds(IEnumerable<long> ids)
    {
        return string.Join(",", ids.Select(id => id.ToString(CultureInfo.InvariantCulture)));
    }
}
