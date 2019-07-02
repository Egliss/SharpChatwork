using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query
{
	internal class RoomFileQuery : ClientQuery, IRoomFileQuery
	{
		public RoomFileQuery(IChatworkClient client) : base(client)
		{
		}

		public async ValueTask<IEnumerable<UserFile>> GetAllAsync(long roomId, long accountId)
		{
            var uri = $"{EndPoints.RoomFiles(roomId)}?account_id={accountId.ToString()}";
            return await this.chatworkClient.QueryAsync<List<UserFile>>(new Uri(uri), HttpMethod.Get, new Dictionary<string, string>());
        }

		public async ValueTask<UserFile> GetAsync(long roomId, long fileId, bool createDownloadLink)
		{
            var uri = $"{EndPoints.RoomFiles(roomId)}?create_download_url={URLArgEncoder.BoolToInt(createDownloadLink)}";
            return await this.chatworkClient.QueryAsync<UserFile>(new Uri(uri), HttpMethod.Get,new Dictionary<string, string>());
        }

		public async ValueTask<ElementId> UploadAsync(long roomId)
		{
            // TODO implement
			throw new NotImplementedException();
		}
	}
}
