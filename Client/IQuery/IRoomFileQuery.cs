using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Client.Query
{
	public interface IRoomFileQuery
	{
		ValueTask<List<UserFile>> GetAllAsync(long roomId, long accountId);
		ValueTask<ElementId> UploadAsync(long roomId);
		ValueTask<UserFile> GetAsync(long roomId, long fileId, bool createDownloadLink);
	}
}
