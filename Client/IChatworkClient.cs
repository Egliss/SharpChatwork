using SharpChatwork.Client;
using SharpChatwork.Client.Query;
using SharpChatwork.Query.Types;
using System.Collections.Generic;
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
		IIncomingRequestQuery IncomingRequest { get; }
	}

	public abstract class ChatworkClient : IChatworkClient
	{
		public ChatworkClient()
		{
			this.me = new MeQuery(this);
			this.contact= new ContactQuery(this);
			this.room = new RoomQuery(this);
			this.IncomingRequest = new IncomingRequestQuery(this);
		}

		internal protected abstract string clientName { get; }
		internal protected abstract AuthenticationHeaderValue authenticationHeader { get; }

		public IMeQuery me { get; private set; }
		public IRoomQuery room { get; private set; }
		public IContactQuery contact { get; private set; }
		public IIncomingRequestQuery IncomingRequest { get; private set; }

		public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
	}
}
