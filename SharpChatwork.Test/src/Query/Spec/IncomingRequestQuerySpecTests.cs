using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query.Spec;

public class IncomingRequestQuerySpecTests
{
    [Test]
    public async Task AcceptAsync_should_use_PUT_per_spec()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.IncomingRequestPut);
        var query = new IncomingRequestQuery(stub);

        await query.AcceptAsync(7);

        await stub.Received(1).QueryAsync(
            EndPoints.IncomingRequestsOf(7),
            HttpMethod.Put,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }
}
