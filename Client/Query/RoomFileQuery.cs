using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Client.Query
{
	internal class RoomFileQuery : ClientQuery, IRoomFileQuery
	{
		public RoomFileQuery(IChatworkClient client) : base(client)
		{
		}

		public ValueTask<List<UserFile>> GetAllAsync(long roomId, long accountId)
		{
			throw new NotImplementedException();
		}

		public ValueTask<UserFile> GetAsync(long roomId, long fileId, bool createDownloadLink)
		{
			throw new NotImplementedException();
		}

		public ValueTask<ElementId> UploadAsync(long roomId)
		{
			throw new NotImplementedException();
		}
	}
}
