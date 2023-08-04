using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    internal sealed class RoomInviteQuery : ClientQuery, IRoomInviteQuery
    {
        public RoomInviteQuery(IChatworkClient client) : base(client)
        {
        }

        public async ValueTask<InviteLink> CreateAsync(long roomId, string uniqueName, string description, bool requireAcceptance, CancellationToken token = default)
        {
            var data = new Dictionary<string, string>()
            {
                { "code" , uniqueName },
                { "description" , description},
                { "need_acceptance" , UrlArgEncoder.BoolToInt(requireAcceptance).ToString()},
            };
            return await this.chatworkClient.QueryAsync<InviteLink>(EndPoints.RoomTasks(roomId), HttpMethod.Post, data, token);
        }

        public async ValueTask DestroyAsync(long roomId, CancellationToken token = default)
        {
            await this.chatworkClient.QueryAsync(EndPoints.RoomLink(roomId), HttpMethod.Delete, new Dictionary<string, string>(), token);
        }

        public async ValueTask<InviteLink> GetAsync(long roomId, CancellationToken token = default)
        {
            return await this.chatworkClient.QueryAsync<InviteLink>(EndPoints.RoomLink(roomId), HttpMethod.Post, new Dictionary<string, string>(), token);
        }

        public async ValueTask<InviteLink> UpdateAsync(long roomId, string uniqueName, string description, bool requireAcceptance, CancellationToken token = default)
        {
            var data = new Dictionary<string, string>()
            {
                { "code" , uniqueName },
                { "description" , description},
                { "need_acceptance" , UrlArgEncoder.BoolToInt(requireAcceptance).ToString()},
            };
            return await this.chatworkClient.QueryAsync<InviteLink>(EndPoints.RoomTasks(roomId), HttpMethod.Put, data, token);
        }
    }
}
