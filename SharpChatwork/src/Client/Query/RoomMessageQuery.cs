using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace SharpChatwork.Query
{
    internal sealed class RoomMessageQuery : ClientQuery, IRoomMessageQuery
    {
        public RoomMessageQuery(IChatworkClient client) : base(client)
        {
        }

        public async ValueTask<IEnumerable<UserMessage>> GetAllAsync(long roomId, bool isForceMode = false, CancellationToken token = default)
        {
            // TODO QueryAsync + data is error
            // single arg is invalid ?
            var uri = $"{EndPoints.RoomMessages(roomId)}?force={UrlArgEncoder.BoolToInt(isForceMode)}";
            return await this.chatworkClient.QueryAsync<List<UserMessage>>(new Uri(uri), HttpMethod.Get, new Dictionary<string, string>());
        }

        public async ValueTask<UserMessage> GetAsync(long roomId, long messageId, CancellationToken token = default)
        {
            return await this.chatworkClient.QueryAsync<UserMessage>(EndPoints.RoomMessagesOf(roomId, messageId), HttpMethod.Get, new Dictionary<string, string>());
        }

        public async ValueTask<MessageReadUnread> ReadAsync(long roomId, long messageId, CancellationToken token = default)
        {
            var uri = $"{EndPoints.RoomMessages(roomId)}?message_id={messageId}";
            return await this.chatworkClient.QueryAsync<MessageReadUnread>(new Uri(uri), HttpMethod.Post, new Dictionary<string, string>());
        }

        public async ValueTask<ElementId> RemoveAsync(long roomId, long messageId, CancellationToken token = default)
        {
            return await this.chatworkClient.QueryAsync<MessageId>(EndPoints.RoomMessagesOf(roomId, messageId), HttpMethod.Delete, new Dictionary<string, string>());
        }

        public async ValueTask<ElementId> SendAsync(long roomId, string message, bool isSelfUnread, CancellationToken token = default)
        {
            var data = new Dictionary<string, string>()
            {
                { "body" , message },
                { "self_unread" , UrlArgEncoder.BoolToInt(isSelfUnread).ToString() }
            };
            return await this.chatworkClient.QueryAsync<MessageId>(EndPoints.RoomMessages(roomId), HttpMethod.Post, data);
        }

        public async ValueTask<MessageReadUnread> UnReadAsync(long roomId, long messageId, CancellationToken token = default)
        {
            var uri = $"{EndPoints.RoomMessages(roomId)}?message_id={messageId}";
            return await this.chatworkClient.QueryAsync<MessageReadUnread>(new Uri(uri), HttpMethod.Post, new Dictionary<string, string>());
        }

        public async ValueTask<ElementId> UpdateAsync(long roomId, long messageId, string message, CancellationToken token = default)
        {
            var uri = $"{EndPoints.RoomMessagesOf(roomId, messageId)}?body={message}";
            return await this.chatworkClient.QueryAsync<MessageId>(new Uri(uri), HttpMethod.Post, new Dictionary<string, string>());
        }
    }
}
