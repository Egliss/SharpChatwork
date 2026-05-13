using System.Text.Json;
using SharpChatwork.Client.Exceptions;
using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Client;

public class AccessTokenClientTests
{
    [Test]
    public async Task QueryAsync_sends_x_chatworktoken_header_and_deserializes_response()
    {
        var (client, handler) = MockedAccessTokenClient.Create("my-token");
        handler.Expect(HttpMethod.Get, EndPoints.Me.ToString())
            .WithHeaders("X-ChatWorkToken", "my-token")
            .Respond("application/json", ApiFixtures.MeGet);

        var user = await client.me.GetUserAsync();

        await Assert.That(user.account_id).IsEqualTo(123);
        await Assert.That(user.name).IsEqualTo("John Smith");
        handler.VerifyNoOutstandingExpectation();
    }

    [Test]
    public async Task QueryAsync_throws_ChatworkClientException_on_non_success_status()
    {
        var (client, handler) = MockedAccessTokenClient.Create();
        handler.When(EndPoints.Me.ToString())
            .Respond(HttpStatusCode.InternalServerError, "application/json", ApiFixtures.ErrorResponse);

        await Assert.That(async () => await client.me.GetUserAsync())
            .ThrowsExactly<ChatworkClientException>();
    }

    [Test]
    public async Task QueryAsync_throws_JsonException_on_malformed_response()
    {
        var (client, handler) = MockedAccessTokenClient.Create();
        handler.When(EndPoints.Me.ToString())
            .Respond("application/json", "{not-json");

        await Assert.That(async () => await client.me.GetUserAsync())
            .Throws<JsonException>();
    }

    [Test]
    public async Task QueryAsync_uses_specified_http_method_for_post_request()
    {
        var (client, handler) = MockedAccessTokenClient.Create();
        handler.Expect(HttpMethod.Post, EndPoints.RoomMessages(42).ToString())
            .Respond("application/json", ApiFixtures.MessageIdResult);

        var result = await client.room.message.SendAsync(42, "hi", isSelfUnread: false);

        await Assert.That(result.message_id).IsEqualTo("9876");
        handler.VerifyNoOutstandingExpectation();
    }

    [Test]
    public async Task ClientName_returns_class_name()
    {
        var (client, _) = MockedAccessTokenClient.Create();
        await Assert.That(client.clientName).IsEqualTo(nameof(AccessToken.AccessTokenClient));
    }
}
