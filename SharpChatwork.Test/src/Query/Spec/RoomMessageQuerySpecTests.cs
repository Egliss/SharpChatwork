using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query.Spec;

public class RoomMessageQuerySpecTests
{
    [Test]
    public async Task ReadAsync_should_PUT_messages_read_with_message_id_body_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.MessageReadUnreadResult);
        var query = new RoomMessageQuery(stub);

        await query.ReadAsync(42, 5);

        await stub.Received(1).QueryAsync(
            Arg.Is<Uri>(u => u.OriginalString == "https://api.chatwork.com/v2/rooms/42/messages/read"),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub, ("message_id", "5"));
    }

    [Test]
    public async Task UnReadAsync_should_PUT_messages_unread_with_message_id_body_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.MessageReadUnreadResult);
        var query = new RoomMessageQuery(stub);

        await query.UnReadAsync(42, 5);

        await stub.Received(1).QueryAsync(
            Arg.Is<Uri>(u => u.OriginalString == "https://api.chatwork.com/v2/rooms/42/messages/unread"),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub, ("message_id", "5"));
    }

    [Test]
    public async Task UpdateAsync_should_PUT_messages_of_id_with_body_in_form_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.MessageIdResult);
        var query = new RoomMessageQuery(stub);

        await query.UpdateAsync(42, 5, "updated");

        await stub.Received(1).QueryAsync(
            EndPoints.RoomMessagesOf(42, 5),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub, ("body", "updated"));
    }
}
