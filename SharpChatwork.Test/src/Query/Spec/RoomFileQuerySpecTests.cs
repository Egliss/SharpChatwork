using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query.Spec;

public class RoomFileQuerySpecTests
{
    [Test]
    public async Task EndPoints_RoomFilesOf_should_not_contain_extra_whitespace_per_spec()
    {
        var uri = EndPoints.RoomFilesOf(42, 7).ToString();

        await Assert.That(uri).IsEqualTo("https://api.chatwork.com/v2/rooms/42/files/7");
    }

    [Test]
    public async Task GetAsync_should_target_file_of_id_path_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomFileGet);
        var query = new RoomFileQuery(stub);

        await query.GetAsync(42, 7, createDownloadLink: true);

        await stub.Received(1).QueryAsync(
            Arg.Is<Uri>(u => u.OriginalString.StartsWith(
                "https://api.chatwork.com/v2/rooms/42/files/7", StringComparison.Ordinal)),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }
}
