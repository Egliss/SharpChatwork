using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Client.Query
{
	public interface IContactQuery : IClientQuery
	{
		ValueTask<List<Contact>> GetAsync();
	}
}
