using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query;

public class RoomInviteQueryTests
{
    [Test]
    public async Task GetAsync_gets_room_link()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.InviteLinkGet);
        var query = new RoomInviteQuery(stub);

        var link = await query.GetAsync(42);

        await Assert.That(link.url).IsEqualTo("https://example.com/abc123");
        await stub.Received(1).QueryAsync(
            EndPoints.RoomLink(42),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task CreateAsync_posts_form_data_to_room_link_endpoint()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.InviteLinkGet);
        var query = new RoomInviteQuery(stub);

        var link = await query.CreateAsync(42, "code123", "Welcome", requireAcceptance: true);

        await Assert.That(link.need_acceptance).IsTrue();
        await stub.Received(1).QueryAsync(
            EndPoints.RoomLink(42),
            HttpMethod.Post,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub,
            ("code", "code123"),
            ("description", "Welcome"),
            ("need_acceptance", "1"));
    }

    [Test]
    public async Task UpdateAsync_puts_form_data_to_room_link_endpoint()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.InviteLinkGet);
        var query = new RoomInviteQuery(stub);

        await query.UpdateAsync(42, "code456", "Hi", requireAcceptance: false);

        await stub.Received(1).QueryAsync(
            EndPoints.RoomLink(42),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub,
            ("code", "code456"),
            ("description", "Hi"),
            ("need_acceptance", "0"));
    }

    [Test]
    public async Task DestroyAsync_calls_room_link_with_DELETE()
    {
        var stub = StubChatworkClient.WithJsonResponse("{}");
        var query = new RoomInviteQuery(stub);

        await query.DestroyAsync(42);

        await stub.Received(1).QueryAsync(
            EndPoints.RoomLink(42),
            HttpMethod.Delete,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }
}
