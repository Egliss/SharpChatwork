using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    internal class RoomInviteQuery : ClientQuery, IRoomInviteQuery
    {
        public RoomInviteQuery(IChatworkClient client) : base(client)
        {
        }

        public async ValueTask<InviteLink> CreateAsync(long roomId, string uniqueName, string description, bool requireAcceptance)
        {
            var data = new Dictionary<string, string>()
            {
                { "code" , uniqueName },
                { "description" , description},
                { "need_acceptance" , URLArgEncoder.BoolToInt(requireAcceptance).ToString()},
            };
            return await this.chatworkClient.QueryAsync<InviteLink>(EndPoints.RoomTasks(roomId), HttpMethod.Post, data);
        }

        public async ValueTask DestroyAsync(long roomId)
        {
            await this.chatworkClient.QueryAsync(EndPoints.RoomLink(roomId), HttpMethod.Delete, new Dictionary<string, string>());
        }

        public async ValueTask<InviteLink> GetAsync(long roomId)
        {
            return await this.chatworkClient.QueryAsync<InviteLink>(EndPoints.RoomLink(roomId), HttpMethod.Post, new Dictionary<string, string>());
        }

        public async ValueTask<InviteLink> UpdateAsync(long roomId, string uniqueName, string description, bool requireAcceptance)
        {
            var data = new Dictionary<string, string>()
            {
                { "code" , uniqueName },
                { "description" , description},
                { "need_acceptance" , URLArgEncoder.BoolToInt(requireAcceptance).ToString()},
            };
            return await this.chatworkClient.QueryAsync<InviteLink>(EndPoints.RoomTasks(roomId), HttpMethod.Put, data);
        }
    }
}
