using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query;

public class RoomTaskQueryTests
{
    [Test]
    public async Task GetAllAsync_sends_account_id_and_status_as_query_string()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomTasksGet);
        var query = new RoomTaskQuery(stub);

        var tasks = (await query.GetAllAsync(42, accountId: 1, autherId: 2, isDone: false)).ToList();

        await Assert.That(tasks.Count).IsEqualTo(1);
        await stub.Received(1).QueryAsync(
            Arg.Is<Uri>(u =>
                u.OriginalString.Contains("/rooms/42/tasks?", StringComparison.Ordinal)
                && u.OriginalString.Contains("account_id=1", StringComparison.Ordinal)
                && u.OriginalString.Contains("assigned_by_account_id=2", StringComparison.Ordinal)
                && u.OriginalString.Contains("status=open", StringComparison.Ordinal)),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task GetAllAsync_uses_status_done_when_isDone_true()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomTasksGet);
        var query = new RoomTaskQuery(stub);

        await query.GetAllAsync(42, 1, 2, isDone: true);

        await stub.Received(1).QueryAsync(
            Arg.Is<Uri>(u => u.OriginalString.Contains("status=done", StringComparison.Ordinal)),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task GetAsync_calls_room_task_of_id_with_GET()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.RoomTaskGet);
        var query = new RoomTaskQuery(stub);

        var task = await query.GetAsync(42, 3);

        await Assert.That(task.task_id).IsEqualTo(3);
        await stub.Received(1).QueryAsync(
            EndPoints.RoomTasksOf(42, 3),
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task UpdateAsync_puts_to_task_status_with_body_form()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.TaskIdResult);
        var query = new RoomTaskQuery(stub);

        var result = await query.UpdateAsync(42, 3, TaskStateType.Done);

        await Assert.That(result.task_id).IsEqualTo("1234");
        await stub.Received(1).QueryAsync(
            EndPoints.RoomTasksOfStatus(42, 3),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
        await StubChatworkClient.AssertFormDataAsync(stub, ("body", "done"));
    }

    [Test]
    public async Task CreateAsync_throws_NotImplementedException()
    {
        var stub = StubChatworkClient.WithJsonResponse("{}");
        var query = new RoomTaskQuery(stub);

        await Assert.That(async () => await query.CreateAsync(1, "body", 0))
            .Throws<NotImplementedException>();
    }
}
