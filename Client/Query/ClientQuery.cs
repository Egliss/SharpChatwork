using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Client.Query
{
	internal class ClientQuery
	{
		public ClientQuery(IChatworkClient  client)
		{
			this.chatworkClient = client as ChatworkClient;
		}

		internal ChatworkClient chatworkClient { get; private set; }
	}
}
