using System.Text;
using SharpChatwork.OAuth2;
using SharpChatwork.Test.Helpers;

namespace SharpChatwork.Test.Client;

public class OAuth2ClientTests
{
    [Test]
    public async Task UpdateTokenAsync_uses_basic_auth_with_client_and_secret()
    {
        var (client, handler) = MockedOAuth2Client.Create("ck", "sk");
        var expected = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes("ck:sk"));
        handler.Expect(HttpMethod.Post, EndPoints.Token.ToString())
            .WithHeaders("Authorization", expected)
            .Respond("application/json", ApiFixtures.OAuth2TokenResult);

        var result = await client.UpdateTokenAsync(OAuth2TokenQuery.GrantType.RefreshToken);

        await Assert.That(result.access_token).IsEqualTo("abc-access");
        await Assert.That(result.refresh_token).IsEqualTo("abc-refresh");
        await Assert.That(result.expires_in).IsEqualTo(3600L);
        await Assert.That(result.isError).IsFalse();
        handler.VerifyNoOutstandingExpectation();
    }

    [Test]
    public async Task QueryAsync_sends_Bearer_authorization_with_access_token()
    {
        var (client, handler) = MockedOAuth2Client.Create();
        handler.When(HttpMethod.Post, EndPoints.Token.ToString())
            .Respond("application/json", ApiFixtures.OAuth2TokenResult);
        await client.UpdateTokenAsync(OAuth2TokenQuery.GrantType.RefreshToken);

        handler.Expect(HttpMethod.Get, EndPoints.Me.ToString())
            .WithHeaders("Authorization", "Bearer abc-access")
            .Respond("application/json", ApiFixtures.MeGet);

        var user = await client.me.GetUserAsync();

        await Assert.That(user.account_id).IsEqualTo(123);
        handler.VerifyNoOutstandingExpectation();
    }

    [Test]
    public async Task ClientName_returns_class_name()
    {
        var (client, _) = MockedOAuth2Client.Create();
        await Assert.That(client.clientName).IsEqualTo(nameof(OAuth2Client));
    }

    [Test]
    public async Task Authorization_invokes_openBrowser_with_oauth_url_and_uses_readCode_result()
    {
        var (client, _) = MockedOAuth2Client.Create();
        string capturedUrl = null;
        var query = new OAuth2ConcentQuery
        {
            redirect_uri = "https://example.com/cb",
            scope = "rooms.all:read",
        };

        var result = client.Authorization(
            query,
            openBrowser: url => capturedUrl = url,
            readCode: () => "fake-code-123");

        await Assert.That(result.code).IsEqualTo("fake-code-123");
        await Assert.That(result.error ?? string.Empty).IsEqualTo(string.Empty);
        await Assert.That(capturedUrl).IsNotNull();
        await Assert.That(capturedUrl.StartsWith(EndPoints.Oauth2.OriginalString, StringComparison.Ordinal)).IsTrue();
    }

    [Test]
    public async Task Authorization_returns_oauth_code_error_when_readCode_returns_empty()
    {
        var (client, _) = MockedOAuth2Client.Create();
        var query = new OAuth2ConcentQuery { scope = "rooms.all:read" };

        var result = client.Authorization(
            query,
            openBrowser: _ => { },
            readCode: () => string.Empty);

        await Assert.That(result.error).IsEqualTo("oauth_code_error");
    }
}
