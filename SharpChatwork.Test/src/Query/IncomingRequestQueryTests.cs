using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query;

public class IncomingRequestQueryTests
{
    [Test]
    public async Task GetAllAsync_calls_incoming_requests_endpoint_with_GET()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.IncomingRequestsGet);
        var query = new IncomingRequestQuery(stub);

        var requests = (await query.GetAllAsync()).ToList();

        await Assert.That(requests.Count).IsEqualTo(1);
        await Assert.That(requests[0].request_id).IsEqualTo(1);
        await Assert.That(requests[0].account_id).IsEqualTo(2);
        await Assert.That(requests[0].message).IsEqualTo("Please add me!");
        await Assert.That(requests[0].name).IsEqualTo("Mike");
        await Assert.That(requests[0].chatwork_id).IsEqualTo("mike-id");
        await stub.Received(1).QueryAsync(
            EndPoints.IncomingRequests,
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task AcceptAsync_puts_to_incoming_requests_of_id()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.IncomingRequestPut);
        var query = new IncomingRequestQuery(stub);

        var result = await query.AcceptAsync(7);

        await Assert.That(result.request_id).IsEqualTo(1);
        await stub.Received(1).QueryAsync(
            EndPoints.IncomingRequestsOf(7),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task CancelAsync_deletes_incoming_requests_of_id()
    {
        var stub = StubChatworkClient.WithJsonResponse("{}");
        var query = new IncomingRequestQuery(stub);

        await query.CancelAsync(9);

        await stub.Received(1).QueryAsync(
            EndPoints.IncomingRequestsOf(9),
            HttpMethod.Delete,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }
}
