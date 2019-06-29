using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Client.Query
{
	public interface IClientQuery
	{
		IChatworkClient chatworkClient { get; }
	}
}
