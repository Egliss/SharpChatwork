using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Client.Query
{
	internal class ContactQuery : ClientQuery, IContactQuery
	{
		public ContactQuery(IChatworkClient client) : base(client)
		{
		}

		public ValueTask<List<Contact>> GetAsync()
		{
			throw new NotImplementedException();
		}
	}
}
