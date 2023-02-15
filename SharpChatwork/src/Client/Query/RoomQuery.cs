using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace SharpChatwork.Query
{
    internal class RoomQuery : ClientQuery, IRoomQuery
    {
        public RoomQuery(IChatworkClient client) : base(client)
        {
            this.message = new RoomMessageQuery(client);
            this.member = new RoomMemberQuery(client);
            this.invite = new RoomInviteQuery(client);
            this.file = new RoomFileQuery(client);
            this.task = new RoomTaskQuery(client);
        }

        public IRoomMessageQuery message { get; private set; }
        public IRoomMemberQuery member { get; private set; }
        public IRoomInviteQuery invite { get; private set; }
        public IRoomFileQuery file { get; private set; }
        public IRoomTaskQuery task { get; private set; }

        public async ValueTask<ElementId> CreateAsync()
        {
            return await this.chatworkClient.QueryAsync<RoomId>(EndPoints.Rooms, HttpMethod.Get, new Dictionary<string, string>());
        }

        public async ValueTask DeleteAsync(long roomId)
        {
            var uri = $"{EndPoints.RoomMessages(roomId)}?action_type=delete";
            await this.chatworkClient.QueryAsync<RoomId>(new Uri(uri), HttpMethod.Post, new Dictionary<string, string>());
        }

        public async ValueTask<IEnumerable<Room>> GetAllAsync()
        {
            return await this.chatworkClient.QueryAsync<List<Room>>(EndPoints.Rooms, HttpMethod.Get, new Dictionary<string, string>());
        }

        public async ValueTask<Room> GetAsync(long roomId)
        {
            return await this.chatworkClient.QueryAsync<Room>(EndPoints.RoomOf(roomId), HttpMethod.Get, new Dictionary<string, string>());
        }

        public async ValueTask LeaveAsync(long roomId)
        {
            var uri = $"{EndPoints.RoomMessages(roomId)}?action_type=leave";
            await this.chatworkClient.QueryAsync<RoomId>(new Uri(uri), HttpMethod.Post, new Dictionary<string, string>());
        }

        public async ValueTask<ElementId> UpdateAsync(long roomId, string roomName, string description, RoomIconPreset preset)
        {
            var data = new Dictionary<string, string>()
            {
                {"name",roomName },
                {"description",roomName },
                {"icon_preset",preset.ToAliasOrDefault() }
            };
            return await this.chatworkClient.QueryAsync<RoomId>(EndPoints.RoomOf(roomId), HttpMethod.Post, data);
        }
    }
}
