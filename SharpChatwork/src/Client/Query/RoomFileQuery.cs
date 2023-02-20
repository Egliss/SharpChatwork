using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    internal sealed class RoomFileQuery : ClientQuery, IRoomFileQuery
    {
        public RoomFileQuery(IChatworkClient client) : base(client)
        {
        }

        public async ValueTask<IEnumerable<UserFile>> GetAllAsync(long roomId, long accountId)
        {
            var uri = $"{EndPoints.RoomFiles(roomId)}?account_id={accountId}";
            return await this.chatworkClient.QueryAsync<List<UserFile>>(new Uri(uri), HttpMethod.Get, new Dictionary<string, string>());
        }

        public async ValueTask<UserFile> GetAsync(long roomId, long fileId, bool createDownloadLink)
        {
            var uri = $"{EndPoints.RoomFiles(roomId)}?create_download_url={UrlArgEncoder.BoolToInt(createDownloadLink)}";
            return await this.chatworkClient.QueryAsync<UserFile>(new Uri(uri), HttpMethod.Get, new Dictionary<string, string>());
        }
        public async ValueTask<ElementId> UploadAsync(long roomId, Stream stream, string filePath, string message)
        {
            var uri = $"{EndPoints.RoomFiles(roomId)}";

            MultipartFormDataContent multipart = new MultipartFormDataContent();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"file\"",
                FileName = $"\"{Path.GetFileName(filePath)}\"",
            };
            var mimeName = MimeMapping.MimeUtility.GetMimeMapping(filePath);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(mimeName);
            var messageContent = new StringContent(message);
            messageContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            messageContent.Headers.ContentDisposition.Name = "\"message\"";
            messageContent.Headers.ContentType = null;
            multipart.Add(fileContent);
            multipart.Add(messageContent);

            return await this.chatworkClient.QueryContentAsync<ElementId>(new Uri(uri), HttpMethod.Post, multipart);
        }
        public async ValueTask<ElementId> UploadAsync(long roomId, string filePath, string message)
        {
            using(FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                return await this.UploadAsync(roomId, stream, filePath, message);
            }
        }
    }
}
