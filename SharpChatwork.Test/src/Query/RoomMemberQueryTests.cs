using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query;

public class RoomMemberQueryTests
{
    [Test]
    public async Task GetAllAsync_calls_room_member_endpoint_with_GET()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomMembersGet);
        var query = new RoomMemberQuery(stub);

        var members = (await query.GetAllAsync(123)).ToList();

        await Assert.That(members.Count).IsEqualTo(1);
        await Assert.That(members[0].account_id).IsEqualTo(123);
        await stub.Received(1).QueryAsync(
            EndPoints.RoomMember(123),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task UpdateAsync_puts_form_data_to_room_member_endpoint()
    {
        var stub = StubChatworkClient.WithJsonResponse("""{"admin":[10],"member":[20,21],"readonly":[30]}""");
        var query = new RoomMemberQuery(stub);

        var result = await query.UpdateAsync(42, [10L], [20L, 21L], [30L]);

        await Assert.That(result.admin).Contains(10L);
        await Assert.That(result.member).Contains(20L);
        await Assert.That(result.member).Contains(21L);
        await Assert.That(result.@readonly).Contains(30L);
        await stub.Received(1).QueryAsync(
            EndPoints.RoomMember(42),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub,
            ("members_admin_ids", "10"),
            ("members_member_ids", "20,21"),
            ("members_readonly_ids", "30"));
    }
}
