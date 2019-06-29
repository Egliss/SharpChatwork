using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Client.Query
{
	internal class IncomingRequestQuery : ClientQuery, IIncomingRequestQuery
	{
		public IncomingRequestQuery(IChatworkClient client) : base(client)
		{
		}

		public ValueTask<IncomingRequestQuery> AcceptAsync(long requestId)
		{
			throw new NotImplementedException();
		}

		public ValueTask CancelAsync(long requestId)
		{
			throw new NotImplementedException();
		}

		public ValueTask<List<IncomingRequestQuery>> GetAsync()
		{
			throw new NotImplementedException();
		}
	}
}
