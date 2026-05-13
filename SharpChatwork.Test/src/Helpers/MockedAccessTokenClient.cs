using SharpChatwork.AccessToken;

namespace SharpChatwork.Test.Helpers;

internal static class MockedAccessTokenClient
{
    public const string DefaultToken = "test-token";

    public static (AccessTokenClient Client, MockHttpMessageHandler Handler) Create(string token = DefaultToken)
    {
        var handler = new MockHttpMessageHandler();
        var http = new HttpClient(handler);
        return (new AccessTokenClient(token, http), handler);
    }
}
