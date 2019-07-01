using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query
{
	internal class ContactQuery : ClientQuery, IContactQuery
	{
		public ContactQuery(IChatworkClient client) : base(client)
		{
		}

		public async ValueTask<List<Contact>> GetAsync()
		{
            return await this.chatworkClient.QueryAsync<List<Contact>>(EndPoints.Contacts, HttpMethod.Get, new Dictionary<string, string>());
        }
    }
}
