using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Client.Query
{
	internal class ClientQuery : IClientQuery
	{
		public ClientQuery(IChatworkClient  client)
		{
			this.chatworkClient = client;
		}

		public IChatworkClient chatworkClient { get; private set; }
	}
}
