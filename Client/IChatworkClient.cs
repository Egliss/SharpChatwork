using SharpChatwork;
using SharpChatwork.Query;
using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SharpChatwork
{
    public interface IChatworkClient : ISerializable
	{
        IMeQuery me { get; }
		IRoomQuery room { get; }
		IContactQuery contact { get; }
		IIncomingRequestQuery incomingRequest { get; }
	}

	public abstract class ChatworkClient : IChatworkClient
	{
		public ChatworkClient()
		{
			this.me = new MeQuery(this);
			this.contact= new ContactQuery(this);
			this.room = new RoomQuery(this);
			this.incomingRequest = new IncomingRequestQuery(this);
		}

		public IMeQuery me { get; private set; }
		public IRoomQuery room { get; private set; }
		public IContactQuery contact { get; private set; }
		public IIncomingRequestQuery incomingRequest { get; private set; }

        internal abstract string clientName { get; }
		internal abstract ValueTask<string> QueryTextAsync(Uri uri, HttpMethod method, Dictionary<string, string> data);
		internal abstract ValueTask<ReturnT> QueryAsync<ReturnT>(Uri uri, HttpMethod method, Dictionary<string, string> data);
		internal abstract ValueTask QueryAsync(Uri uri, HttpMethod method, Dictionary<string, string> data);

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
    }
}
