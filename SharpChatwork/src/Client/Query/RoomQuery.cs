using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query;

internal sealed class RoomQuery(IChatworkClient client) : ClientQuery(client), IRoomQuery
{
    public IRoomMessageQuery message { get; } = new RoomMessageQuery(client);
    public IRoomMemberQuery member { get; } = new RoomMemberQuery(client);
    public IRoomInviteQuery invite { get; } = new RoomInviteQuery(client);
    public IRoomFileQuery file { get; } = new RoomFileQuery(client);
    public IRoomTaskQuery task { get; } = new RoomTaskQuery(client);

    public async ValueTask<RoomId> CreateAsync(
        string name,
        IEnumerable<long> adminMemberIds,
        string description = null,
        IEnumerable<long> normalMemberIds = null,
        IEnumerable<long> readonlyMemberIds = null,
        RoomIconPreset? iconPreset = null,
        CancellationToken token = default)
    {
        var data = new Dictionary<string, string>
        {
            {"name", name},
            {"members_admin_ids", JoinIds(adminMemberIds)},
        };
        if(description is not null)
            data["description"] = description;
        if(normalMemberIds is not null)
            data["members_member_ids"] = JoinIds(normalMemberIds);
        if(readonlyMemberIds is not null)
            data["members_readonly_ids"] = JoinIds(readonlyMemberIds);
        if(iconPreset.HasValue)
            data["icon_preset"] = iconPreset.Value.ToAliasOrDefault();
        return await this.chatworkClient.QueryAsync<RoomId>(EndPoints.Rooms, HttpMethod.Post, data, token);
    }

    private static string JoinIds(IEnumerable<long> ids)
    {
        return string.Join(",", ids.Select(id => id.ToString(CultureInfo.InvariantCulture)));
    }

    public async ValueTask DeleteAsync(long roomId, CancellationToken token = default)
    {
        var data = new Dictionary<string, string>
        {
            {"action_type", "delete"},
        };
        await this.chatworkClient.QueryAsync(EndPoints.RoomOf(roomId), HttpMethod.Delete, data, token);
    }

    public async ValueTask<IEnumerable<Room>> GetAllAsync(CancellationToken token = default)
    {
        return await this.chatworkClient.QueryAsync<List<Room>>(EndPoints.Rooms, HttpMethod.Get, new Dictionary<string, string>(), token);
    }

    public async ValueTask<Room> GetAsync(long roomId, CancellationToken token = default)
    {
        return await this.chatworkClient.QueryAsync<Room>(EndPoints.RoomOf(roomId), HttpMethod.Get, new Dictionary<string, string>(), token);
    }

    public async ValueTask LeaveAsync(long roomId, CancellationToken token = default)
    {
        var data = new Dictionary<string, string>
        {
            {"action_type", "leave"},
        };
        await this.chatworkClient.QueryAsync(EndPoints.RoomOf(roomId), HttpMethod.Delete, data, token);
    }

    public async ValueTask<RoomId> UpdateAsync(
        long roomId, string roomName, string description, RoomIconPreset preset,
        CancellationToken token = default
    )
    {
        var data = new Dictionary<string, string>
        {
            {"name", roomName},
            {"description", description},
            {"icon_preset", preset.ToAliasOrDefault()},
        };
        return await this.chatworkClient.QueryAsync<RoomId>(EndPoints.RoomOf(roomId), HttpMethod.Put, data, token);
    }
}
