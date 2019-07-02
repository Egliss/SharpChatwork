using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
	public interface IContactQuery
	{
		ValueTask<IEnumerable<Contact>> GetAllAsync();
	}
}
