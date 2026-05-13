using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query.Spec;

public class RoomInviteQuerySpecTests
{
    [Test]
public async Task GetAsync_should_use_GET_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.InviteLinkGet);
        var query = new RoomInviteQuery(stub);

        await query.GetAsync(42);

        await stub.Received(1).QueryAsync(
            EndPoints.RoomLink(42),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
public async Task CreateAsync_should_target_link_endpoint_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.InviteLinkGet);
        var query = new RoomInviteQuery(stub);

        await query.CreateAsync(42, "code123", "Welcome", requireAcceptance: true);

        await stub.Received(1).QueryAsync(
            EndPoints.RoomLink(42),
            HttpMethod.Post,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
public async Task UpdateAsync_should_target_link_endpoint_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.InviteLinkGet);
        var query = new RoomInviteQuery(stub);

        await query.UpdateAsync(42, "code456", "Hi", requireAcceptance: false);

        await stub.Received(1).QueryAsync(
            EndPoints.RoomLink(42),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }
}
