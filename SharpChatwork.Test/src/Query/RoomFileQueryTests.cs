using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query;

public class RoomFileQueryTests
{
    [Test]
    public async Task GetAllAsync_calls_files_endpoint_with_account_id_query_param()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomFilesGet);
        var query = new RoomFileQuery(stub);

        var files = (await query.GetAllAsync(42, accountId: 9)).ToList();

        await Assert.That(files.Count).IsEqualTo(1);
        await Assert.That(files[0].file_id).IsEqualTo(3);
        await Assert.That(files[0].filename).IsEqualTo("README.md");
        await Assert.That(files[0].filesize).IsEqualTo(21);
        await Assert.That(files[0].message_id).IsEqualTo("22");
        await stub.Received(1).QueryAsync(
            Arg.Is<Uri>(u => u.OriginalString.EndsWith("/rooms/42/files?account_id=9", StringComparison.Ordinal)),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task GetAsync_calls_files_of_id_endpoint_with_create_download_url_query()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomFileGet);
        var query = new RoomFileQuery(stub);

        var file = await query.GetAsync(42, 3, createDownloadLink: true);

        await Assert.That(file.file_id).IsEqualTo(3);
        await stub.Received(1).QueryAsync(
            Arg.Is<Uri>(u => u.OriginalString.EndsWith("/rooms/42/files/3?create_download_url=1", StringComparison.Ordinal)),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task UploadAsync_with_stream_posts_multipart_to_files_endpoint()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.FileIdResult);
        var query = new RoomFileQuery(stub);
        using var stream = new System.IO.MemoryStream(new byte[] { 1, 2, 3 });

        var result = await query.UploadAsync(42, stream, "test.txt", "hello");

        await Assert.That(result.id).IsEqualTo("1234");
        await stub.Received(1).QueryAsync(
            EndPoints.RoomFiles(42),
            HttpMethod.Post,
            Arg.Is<HttpContent>(c => c is System.Net.Http.MultipartFormDataContent),
            Arg.Any<CancellationToken>());
    }
}
