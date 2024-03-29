using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    internal sealed class ContactQuery : ClientQuery, IContactQuery
    {
        public ContactQuery(IChatworkClient client) : base(client)
        {
        }

        public async ValueTask<IEnumerable<Contact>> GetAllAsync(CancellationToken token = default)
        {
            return await this.chatworkClient.QueryAsync<List<Contact>>(EndPoints.Contacts, HttpMethod.Get, new Dictionary<string, string>(), token);
        }
    }
}
