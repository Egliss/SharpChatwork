using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.Query;

public interface IContactQuery
{
    public ValueTask<IEnumerable<Contact>> GetAllAsync(CancellationToken token = default);
}
