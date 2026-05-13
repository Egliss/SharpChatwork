using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query;

public class MeQueryTests
{
    [Test]
    public async Task GetUserAsync_calls_me_endpoint_with_GET()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.MeGet);
        var query = new MeQuery(stub);

        var user = await query.GetUserAsync();

        await Assert.That(user.account_id).IsEqualTo(123);
        await Assert.That(user.chatwork_id).IsEqualTo("tarochatworkid");
        await Assert.That(user.organization_name).IsEqualTo("Hello Company");
        await Assert.That(user.login_mail).IsEqualTo("account@example.com");
        await stub.Received(1).QueryAsync(
            EndPoints.Me,
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task GetMyStatusAsync_calls_my_status_endpoint_with_GET()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.MyStatusGet);
        var query = new MeQuery(stub);

        var status = await query.GetMyStatusAsync();

        await Assert.That(status.unread_room_num).IsEqualTo(2);
        await Assert.That(status.mytask_num).IsEqualTo(8);
        await stub.Received(1).QueryAsync(
            EndPoints.MyStatus,
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task GetMyTasksAsync_calls_my_tasks_endpoint_with_GET()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.MyTasksGet);
        var query = new MeQuery(stub);

        var tasks = (await query.GetMyTasksAsync()).ToList();

        await Assert.That(tasks.Count).IsEqualTo(1);
        await Assert.That(tasks[0].task_id).IsEqualTo(3);
        await stub.Received(1).QueryAsync(
            EndPoints.MyTasks,
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }
}
