using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query.Spec;

public class RoomMemberQuerySpecTests
{
    [Test]
public async Task UpdateAsync_should_PUT_members_with_form_data_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse("""{"admin":[1],"member":[2],"readonly":[3]}""");
        var query = new RoomMemberQuery(stub);

        await query.UpdateAsync(42, [1L], [2L], [3L]);

        await stub.Received(1).QueryAsync(
            EndPoints.RoomMember(42),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub,
            ("members_admin_ids", "1"),
            ("members_member_ids", "2"),
            ("members_readonly_ids", "3"));
    }
}
