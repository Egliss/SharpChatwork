using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query.Spec;

public class RoomTaskQuerySpecTests
{
    [Test]
    public async Task GetAllAsync_should_send_filters_as_query_string_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomTasksGet);
        var query = new RoomTaskQuery(stub);

        await query.GetAllAsync(42, accountId: 1, autherId: 2, isDone: false);

        await stub.Received(1).QueryAsync(
            Arg.Is<Uri>(u =>
                u.OriginalString.Contains("account_id=1", StringComparison.Ordinal)
                && u.OriginalString.Contains("assigned_by_account_id=2", StringComparison.Ordinal)
                && u.OriginalString.Contains("status=open", StringComparison.Ordinal)),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task UpdateAsync_should_PUT_tasks_of_id_status_with_body_in_form_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.TaskIdResult);
        var query = new RoomTaskQuery(stub);

        await query.UpdateAsync(42, 3, TaskStateType.Done);

        await stub.Received(1).QueryAsync(
            Arg.Is<Uri>(u => u.OriginalString == "https://api.chatwork.com/v2/rooms/42/tasks/3/status"),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub, ("body", "done"));
    }

    [Test]
    [Skip("interface-gap: IRoomTaskQuery.CreateAsync(roomId, taskText, limit) is missing required to_ids/limit_type params per spec docs/apis/rooms/tasks/post.md. Signature extension needed before this can be implemented.")]
    public async Task CreateAsync_should_POST_tasks_with_required_body_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse("""{"task_ids":[123,124]}""");
        var query = new RoomTaskQuery(stub);

        await query.CreateAsync(42, "buy milk", 1384354799);

        await stub.Received(1).QueryAsync(
            EndPoints.RoomTasks(42),
            HttpMethod.Post,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }
}
