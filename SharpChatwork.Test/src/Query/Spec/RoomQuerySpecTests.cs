using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query.Spec;

public class RoomQuerySpecTests
{
    [Test]
    public async Task EndPoints_Rooms_should_be_lowercase_per_spec()
    {
        await Assert.That(EndPoints.Rooms.ToString())
            .IsEqualTo("https://api.chatwork.com/v2/rooms");
    }

    [Test]
    public async Task CreateAsync_should_use_POST_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomIdResult);
        var query = new RoomQuery(stub);

        await query.CreateAsync("new room", new long[] { 1 });

        await stub.Received(1).QueryAsync(
            EndPoints.Rooms,
            HttpMethod.Post,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task CreateAsync_should_send_required_name_and_admin_ids_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomIdResult);
        var query = new RoomQuery(stub);

        await query.CreateAsync(
            "new room",
            new long[] { 1, 2 },
            description: "desc",
            normalMemberIds: new long[] { 3 },
            readonlyMemberIds: new long[] { 4 },
            iconPreset: RoomIconPreset.Project);

        await StubChatworkClient.AssertFormDataAsync(stub,
            ("name", "new room"),
            ("members_admin_ids", "1,2"),
            ("description", "desc"),
            ("members_member_ids", "3"),
            ("members_readonly_ids", "4"),
            ("icon_preset", "project"));
    }

    [Test]
    public async Task UpdateAsync_should_use_PUT_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomIdResult);
        var query = new RoomQuery(stub);

        await query.UpdateAsync(123, "new room", "desc", RoomIconPreset.Project);

        await stub.Received(1).QueryAsync(
            EndPoints.RoomOf(123),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task UpdateAsync_should_send_distinct_description_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomIdResult);
        var query = new RoomQuery(stub);

        await query.UpdateAsync(123, "new room", "a separate description", RoomIconPreset.Project);

        await StubChatworkClient.AssertFormDataAsync(stub,
            ("name", "new room"),
            ("description", "a separate description"),
            ("icon_preset", "project"));
    }

    [Test]
    public async Task DeleteAsync_should_DELETE_room_of_id_per_spec()
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
    public async Task LeaveAsync_should_DELETE_room_of_id_per_spec()
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
}
