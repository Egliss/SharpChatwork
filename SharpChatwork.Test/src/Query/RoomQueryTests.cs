using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query;

public class RoomQueryTests
{
    [Test]
    public async Task GetAllAsync_calls_rooms_endpoint_with_GET()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomsGet);
        var query = new RoomQuery(stub);

        var rooms = (await query.GetAllAsync()).ToList();

        await Assert.That(rooms.Count).IsEqualTo(1);
        await Assert.That(rooms[0].room_id).IsEqualTo(123);
        await stub.Received(1).QueryAsync(
            EndPoints.Rooms,
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task GetAsync_calls_room_of_id_with_GET()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomGet);
        var query = new RoomQuery(stub);

        var room = await query.GetAsync(123);

        await Assert.That(room.room_id).IsEqualTo(123);
        await Assert.That(room.name).IsEqualTo("Group Chat Name");
        await Assert.That(room.type).IsEqualTo("group");
        await Assert.That(room.role).IsEqualTo("admin");
        await Assert.That(room.sticky).IsFalse();
        await Assert.That(room.message_num).IsEqualTo(122);
        await Assert.That(room.last_update_time).IsEqualTo(1298905200);
        await stub.Received(1).QueryAsync(
            EndPoints.RoomOf(123),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task CreateAsync_posts_to_rooms_endpoint()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomIdResult);
        var query = new RoomQuery(stub);

        var result = await query.CreateAsync("new room", new long[] { 1 });

        await Assert.That(result.room_id).IsEqualTo("1234");
        await stub.Received(1).QueryAsync(
            EndPoints.Rooms,
            HttpMethod.Post,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task CreateAsync_omits_optional_form_fields_when_not_supplied()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomIdResult);
        var query = new RoomQuery(stub);

        await query.CreateAsync("new room", new long[] { 1 });

        var content = StubChatworkClient.LastQueryAsyncContent(stub);
        var form = await StubChatworkClient.ReadFormValuesAsync(content);
        await Assert.That(form.ContainsKey("name")).IsTrue();
        await Assert.That(form.ContainsKey("members_admin_ids")).IsTrue();
        await Assert.That(form.ContainsKey("description")).IsFalse();
        await Assert.That(form.ContainsKey("icon_preset")).IsFalse();
    }

    [Test]
    public async Task UpdateAsync_puts_form_data_to_room_of_id()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomIdResult);
        var query = new RoomQuery(stub);

        var result = await query.UpdateAsync(123, "new room", "desc", RoomIconPreset.Project);

        await Assert.That(result.room_id).IsEqualTo("1234");
        await stub.Received(1).QueryAsync(
            EndPoints.RoomOf(123),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub,
            ("name", "new room"),
            ("description", "desc"),
            ("icon_preset", "project"));
    }

    [Test]
    public async Task LeaveAsync_deletes_room_of_id_with_action_type_leave()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomIdResult);
        var query = new RoomQuery(stub);

        await query.LeaveAsync(123);

        await stub.Received(1).QueryAsync(
            EndPoints.RoomOf(123),
            HttpMethod.Delete,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub, ("action_type", "leave"));
    }

    [Test]
    public async Task DeleteAsync_deletes_room_of_id_with_action_type_delete()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomIdResult);
        var query = new RoomQuery(stub);

        await query.DeleteAsync(123);

        await stub.Received(1).QueryAsync(
            EndPoints.RoomOf(123),
            HttpMethod.Delete,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub, ("action_type", "delete"));
    }

    [Test]
    public async Task SubQueries_are_instantiated_with_correct_types()
    {
        var stub = StubChatworkClient.WithJsonResponse("{}");
        var query = new RoomQuery(stub);

        await Assert.That(query.message).IsTypeOf<RoomMessageQuery>();
        await Assert.That(query.member).IsTypeOf<RoomMemberQuery>();
        await Assert.That(query.invite).IsTypeOf<RoomInviteQuery>();
        await Assert.That(query.file).IsTypeOf<RoomFileQuery>();
        await Assert.That(query.task).IsTypeOf<RoomTaskQuery>();
    }
}
