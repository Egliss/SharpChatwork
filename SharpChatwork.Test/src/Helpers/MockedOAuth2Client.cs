using SharpChatwork.OAuth2;

namespace SharpChatwork.Test.Helpers;

internal static class MockedOAuth2Client
{
    public const string DefaultClientKey = "client-key";
    public const string DefaultSecretKey = "secret-key";

    public static (OAuth2Client Client, MockHttpMessageHandler Handler) Create(
        string clientKey = DefaultClientKey,
        string secretKey = DefaultSecretKey)
    {
        var handler = new MockHttpMessageHandler();
        var http = new HttpClient(handler);
        return (new OAuth2Client(clientKey, secretKey, http), handler);
    }
}
