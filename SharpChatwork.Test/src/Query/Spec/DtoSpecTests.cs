namespace SharpChatwork.Test.Query.Spec;

public class DtoSpecTests
{
    [Test]
    public async Task Room_should_expose_description_per_spec()
    {
        var prop = typeof(Room).GetProperty("description");
        await Assert.That(prop).IsNotNull();
    }

    [Test]
    public async Task UserTask_should_expose_account_per_spec()
    {
        var prop = typeof(UserTask).GetProperty("account");
        await Assert.That(prop).IsNotNull();
    }

    [Test]
    public async Task UserFile_should_expose_download_url_per_spec()
    {
        var prop = typeof(UserFile).GetProperty("download_url");
        await Assert.That(prop).IsNotNull();
    }
}
