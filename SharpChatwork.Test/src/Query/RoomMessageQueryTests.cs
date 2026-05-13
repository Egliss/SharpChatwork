using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query;

public class RoomMessageQueryTests
{
    [Test]
    public async Task GetAllAsync_calls_messages_endpoint_with_force_query_param()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomMessagesGet);
        var query = new RoomMessageQuery(stub);

        var messages = (await query.GetAllAsync(42, isForceMode: true)).ToList();

        await Assert.That(messages.Count).IsEqualTo(1);
        await Assert.That(messages[0].body).IsEqualTo("Hello Chatwork!");
        await stub.Received(1).QueryAsync(
            Arg.Is<Uri>(u => u.OriginalString.EndsWith("/rooms/42/messages?force=1", StringComparison.Ordinal)),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task GetAsync_calls_messages_of_id_with_GET()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomMessageGet);
        var query = new RoomMessageQuery(stub);

        var message = await query.GetAsync(42, 5);

        await Assert.That(message.message_id).IsEqualTo("5");
        await Assert.That(message.body).IsEqualTo("Hello Chatwork!");
        await Assert.That(message.send_time).IsEqualTo(1384242850);
        await Assert.That(message.account.account_id).IsEqualTo(123);
        await Assert.That(message.account.name).IsEqualTo("Bob");
        await stub.Received(1).QueryAsync(
            EndPoints.RoomMessagesOf(42, 5),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task SendAsync_posts_body_and_self_unread_form_data()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.MessageIdResult);
        var query = new RoomMessageQuery(stub);

        var result = await query.SendAsync(42, "hello", isSelfUnread: true);

        await Assert.That(result.message_id).IsEqualTo("9876");
        await stub.Received(1).QueryAsync(
            EndPoints.RoomMessages(42),
            HttpMethod.Post,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub, ("body", "hello"), ("self_unread", "1"));
    }

    [Test]
    public async Task UpdateAsync_puts_to_messages_of_id_with_body_form()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.MessageIdResult);
        var query = new RoomMessageQuery(stub);

        var result = await query.UpdateAsync(42, 5, "updated");

        await Assert.That(result.message_id).IsEqualTo("9876");
        await stub.Received(1).QueryAsync(
            EndPoints.RoomMessagesOf(42, 5),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub, ("body", "updated"));
    }

    [Test]
    public async Task RemoveAsync_calls_messages_of_id_with_DELETE()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.MessageIdResult);
        var query = new RoomMessageQuery(stub);

        await query.RemoveAsync(42, 5);

        await stub.Received(1).QueryAsync(
            EndPoints.RoomMessagesOf(42, 5),
            HttpMethod.Delete,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task ReadAsync_puts_to_messages_read_with_message_id_form()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.MessageReadUnreadResult);
        var query = new RoomMessageQuery(stub);

        var result = await query.ReadAsync(42, 5);

        await Assert.That(result.unread_num).IsEqualTo(3);
        await stub.Received(1).QueryAsync(
            EndPoints.RoomMessagesRead(42),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub, ("message_id", "5"));
    }

    [Test]
    public async Task UnReadAsync_puts_to_messages_unread_with_message_id_form()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.MessageReadUnreadResult);
        var query = new RoomMessageQuery(stub);

        var result = await query.UnReadAsync(42, 5);

        await Assert.That(result.mention_num).IsEqualTo(1);
        await stub.Received(1).QueryAsync(
            EndPoints.RoomMessagesUnread(42),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub, ("message_id", "5"));
    }
}
