using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.AccessToken;

public class AccessTokenClient(string accessToken, HttpMessageInvoker messageInvoker = null) : ChatworkClient
{
    private readonly HttpMessageInvoker MessageInvoker = messageInvoker ?? new HttpClient();
    public override string clientName => nameof(AccessTokenClient);

    private string _accessToken { get; } = accessToken;

    private HttpRequestMessage GenerateRequestMessage(Uri uri, HttpMethod method)
    {
        var request = new HttpRequestMessage
        {
            Method = method,
            RequestUri = uri,
        };
        request.Headers.Add("X-ChatWorkToken", this._accessToken);
        return request;
    }

    public override async ValueTask<ResponseWrapper> QueryAsync(Uri uri, HttpMethod method, HttpContent content, CancellationToken cancellation = default)
    {
        var requestMessage = this.GenerateRequestMessage(uri, method);
        requestMessage.Content = content;
        var client = this.MessageInvoker;
        var result = await client.SendAsync(requestMessage, cancellation);
        var code = (int)result.StatusCode;
        return new ResponseWrapper
        {
            content = await result.Content.ReadAsStringAsync(),
            headers = result.Headers.ToDictionary(m => m.Key, m => m.Value),
            statusCode = code,
        };
    }
}
