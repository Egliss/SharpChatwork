using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query;

public interface IRoomFileQuery
{
    public ValueTask<IEnumerable<UserFile>> GetAllAsync(long roomId, long accountId, CancellationToken cancellation = default);
    public ValueTask<FileId> UploadAsync(long roomId, string filePath, string message, CancellationToken cancellation = default);
    public ValueTask<FileId> UploadAsync(long roomId, Stream stream, string filePath, string message, CancellationToken cancellation = default);
    public ValueTask<UserFile> GetAsync(long roomId, long fileId, bool createDownloadLink, CancellationToken cancellation = default);
}
