using SharpChatwork.Query;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork
{
    public interface IChatworkClient : ISerializable
    {
        public IMeQuery me { get; }
        public IRoomQuery room { get; }
        public IContactQuery contact { get; }
        public IIncomingRequestQuery incomingRequest { get; }
    }

    public abstract class ChatworkClient : IChatworkClient
    {
        public ChatworkClient()
        {
            this.me = new MeQuery(this);
            this.contact = new ContactQuery(this);
            this.room = new RoomQuery(this);
            this.incomingRequest = new IncomingRequestQuery(this);
        }

        [JsonIgnore]
        public IMeQuery me { get; private set; }
        [JsonIgnore]
        public IRoomQuery room { get; private set; }
        [JsonIgnore]
        public IContactQuery contact { get; private set; }
        [JsonIgnore]
        public IIncomingRequestQuery incomingRequest { get; private set; }

        [JsonIgnore]
        internal abstract string clientName { get; }
        internal abstract ValueTask<string> QueryTextAsync(Uri uri, HttpMethod method, Dictionary<string, string> data, CancellationToken cancellation = default);
        internal abstract ValueTask<ReturnT> QueryAsync<ReturnT>(Uri uri, HttpMethod method, Dictionary<string, string> data, CancellationToken cancellation = default);
        internal abstract ValueTask QueryAsync(Uri uri, HttpMethod method, Dictionary<string, string> data, CancellationToken cancellation = default);
        internal abstract ValueTask<string> QueryContentTextAsync(Uri uri, HttpMethod method, HttpContent content, CancellationToken cancellation = default);
        internal abstract ValueTask<ReturnT> QueryContentAsync<ReturnT>(Uri uri, HttpMethod method, HttpContent content, CancellationToken cancellation = default);

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
    }
}
