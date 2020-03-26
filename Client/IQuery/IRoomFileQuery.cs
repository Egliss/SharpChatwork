using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    public interface IRoomFileQuery
    {
        ValueTask<IEnumerable<UserFile>> GetAllAsync(long roomId, long accountId);
        ValueTask<ElementId> UploadAsync(long roomId, string filePath, string contentType, string message);
        ValueTask<UserFile> GetAsync(long roomId, long fileId, bool createDownloadLink);
    }
}
