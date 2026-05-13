using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using MimeMapping;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query;

internal sealed class RoomFileQuery(IChatworkClient client) : ClientQuery(client), IRoomFileQuery
{
    public async ValueTask<IEnumerable<UserFile>> GetAllAsync(long roomId, long accountId, CancellationToken token = default)
    {
        var uri = $"{EndPoints.RoomFiles(roomId)}?account_id={accountId}";
        return await this.chatworkClient.QueryAsync<List<UserFile>>(new Uri(uri), HttpMethod.Get, new Dictionary<string, string>(), token);
    }

    public async ValueTask<UserFile> GetAsync(long roomId, long fileId, bool createDownloadLink, CancellationToken token = default)
    {
        var uri = $"{EndPoints.RoomFilesOf(roomId, fileId)}?create_download_url={UrlArgEncoder.BoolToInt(createDownloadLink)}";
        return await this.chatworkClient.QueryAsync<UserFile>(new Uri(uri), HttpMethod.Get, new Dictionary<string, string>(), token);
    }
    public async ValueTask<FileId> UploadAsync(long roomId, Stream stream, string filePath, string message, CancellationToken token = default)
    {
        var uri = $"{EndPoints.RoomFiles(roomId)}";
        var multipart = new MultipartFormDataContent();
        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = "\"file\"",
            FileName = $"\"{Path.GetFileName(filePath)}\"",
        };
        var mimeName = MimeUtility.GetMimeMapping(filePath);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(mimeName);
        var messageContent = new StringContent(message);
        messageContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = "\"message\"",
        };
        messageContent.Headers.ContentType = null;
        multipart.Add(fileContent);
        multipart.Add(messageContent);
        return await this.chatworkClient.QueryAsync<FileId>(new Uri(uri), HttpMethod.Post, multipart, token);
    }
    public async ValueTask<FileId> UploadAsync(long roomId, string filePath, string message, CancellationToken token = default)
    {
        using var stream = new FileStream(filePath, FileMode.Open);
        return await this.UploadAsync(roomId, stream, filePath, message, token);
    }
}
