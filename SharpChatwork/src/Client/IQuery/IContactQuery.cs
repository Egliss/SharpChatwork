using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    public interface IContactQuery
    {
        ValueTask<IEnumerable<Contact>> GetAllAsync();
    }
}
