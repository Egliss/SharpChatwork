using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Client.Query
{
	public interface IIncomingRequestQuery
	{
		ValueTask<List<IncomingRequest>> GetAsync();
		ValueTask<IncomingRequest> AcceptAsync(long requestId);
		ValueTask CancelAsync(long requestId);
	}
}
