using System;
using System.Collections.Generic;
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

    public ValueTask<RoomMember> UpdateAsync(long roomId, IEnumerable<long> adminsMembers, IEnumerable<long> normalMembers, IEnumerable<long> readonlyMembers, CancellationToken token = default)
    {
        throw new NotImplementedException();
        /*
        var data = new Dictionary<string, string>()
        {
            // TODO convert
            // {"members_admin_ids",adminsMembers},
            // {"members_member_ids",roomName },
            // {"members_readonly_ids",preset.ToAliasOrDefault() }
        };
        return await this.chatworkClient.QueryAsync<RoomMember>(EndPoints.RoomMember(roomId), HttpMethod.Get, data);
        */
    }
}
