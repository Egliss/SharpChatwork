using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Query;

public class ContactQueryTests
{
    [Test]
    public async Task GetAllAsync_calls_contacts_endpoint_with_GET()
    {
        var stub = StubChatworkClient.WithJsonResponse(ApiFixtures.ContactsGet);
        var query = new ContactQuery(stub);

        var contacts = (await query.GetAllAsync()).ToList();

        await Assert.That(contacts.Count).IsEqualTo(1);
        await Assert.That(contacts[0].account_id).IsEqualTo(123);
        await Assert.That(contacts[0].chatwork_id).IsEqualTo("tarochatworkid");
        await Assert.That(contacts[0].organization_name).IsEqualTo("Hello Company");
        await stub.Received(1).QueryAsync(
            EndPoints.Contacts,
            HttpMethod.Get,
            Arg.Any<HttpContent>(),
            Arg.Any<CancellationToken>());
    }
}
