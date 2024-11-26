using System;
using System.Collections.Generic;
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

    public async ValueTask<RoomId> CreateAsync(CancellationToken token = default)
    {
        return await this.chatworkClient.QueryAsync<RoomId>(EndPoints.Rooms, HttpMethod.Get, new Dictionary<string, string>(), token);
    }

    public async ValueTask DeleteAsync(long roomId, CancellationToken token = default)
    {
        var uri = $"{EndPoints.RoomMessages(roomId)}?action_type=delete";
        await this.chatworkClient.QueryAsync<RoomId>(new Uri(uri), HttpMethod.Post, new Dictionary<string, string>(), token);
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
        var uri = $"{EndPoints.RoomMessages(roomId)}?action_type=leave";
        await this.chatworkClient.QueryAsync<RoomId>(new Uri(uri), HttpMethod.Post, new Dictionary<string, string>(), token);
    }

    public async ValueTask<RoomId> UpdateAsync(
        long roomId, string roomName, string description, RoomIconPreset preset,
        CancellationToken token = default
    )
    {
        var data = new Dictionary<string, string>
        {
            {
                "name", roomName
            },
            {
                "description", roomName
            },
            {
                "icon_preset", preset.ToAliasOrDefault()
            },
        };
        return await this.chatworkClient.QueryAsync<RoomId>(EndPoints.RoomOf(roomId), HttpMethod.Post, data, token);
    }
}
