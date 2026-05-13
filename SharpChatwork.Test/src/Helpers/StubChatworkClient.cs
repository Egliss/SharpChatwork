namespace SharpChatwork.Test.Helpers;

internal static class StubChatworkClient
{
    public static ChatworkClient WithJsonResponse(string json, int statusCode = 200)
    {
        var stub = Substitute.For<ChatworkClient>();
        stub.QueryAsync(
                Arg.Any<Uri>(),
                Arg.Any<HttpMethod>(),
                Arg.Any<HttpContent>(),
                Arg.Any<CancellationToken>())
            .Returns(_ => new ValueTask<ResponseWrapper>(new ResponseWrapper
            {
                statusCode = statusCode,
                content = json,
                headers = new Dictionary<string, IEnumerable<string>>(),
            }));
        return stub;
    }

    public static async Task<Dictionary<string, string>> ReadFormValuesAsync(HttpContent? content)
    {
        if(content is null)
        {
            return new Dictionary<string, string>();
        }
        var raw = await content.ReadAsStringAsync();
        var dict = new Dictionary<string, string>();
        foreach(var pair in raw.Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            var eq = pair.IndexOf('=');
            if(eq < 0)
            {
                dict[Uri.UnescapeDataString(pair)] = string.Empty;
                continue;
            }
            var key = Uri.UnescapeDataString(pair[..eq].Replace('+', ' '));
            var value = Uri.UnescapeDataString(pair[(eq + 1)..].Replace('+', ' '));
            dict[key] = value;
        }
        return dict;
    }

    public static HttpContent? LastQueryAsyncContent(ChatworkClient stub)
    {
        var call = stub.ReceivedCalls()
            .Last(c => string.Equals(c.GetMethodInfo().Name, nameof(ChatworkClient.QueryAsync), StringComparison.Ordinal)
                       && c.GetArguments().Length == 4
                       && c.GetArguments()[2] is null or HttpContent);
        return (HttpContent?)call.GetArguments()[2];
    }

    public static async Task AssertFormDataAsync(ChatworkClient stub, params (string Key, string Value)[] expected)
    {
        var content = LastQueryAsyncContent(stub);
        var form = await ReadFormValuesAsync(content);
        foreach(var (key, value) in expected)
        {
            var actual = form.TryGetValue(key, out var found) ? found : null;
            await Assert.That(actual).IsEqualTo(value).Because($"form key '{key}' should equal '{value}'");
        }
    }
}
