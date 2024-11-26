using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query;

internal sealed class ContactQuery(IChatworkClient client) : ClientQuery(client), IContactQuery
{
    public async ValueTask<IEnumerable<Contact>> GetAllAsync(CancellationToken token = default)
    {
        return await this.chatworkClient.QueryAsync<List<Contact>>(EndPoints.Contacts, HttpMethod.Get, new Dictionary<string, string>(), token);
    }
}
