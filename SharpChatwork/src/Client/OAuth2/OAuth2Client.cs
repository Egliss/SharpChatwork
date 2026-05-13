#pragma warning disable CA1805 // Default value initialize

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.OAuth2;

public class OAuth2Client(string clientKey, string secretKey, HttpMessageInvoker invoker = null) : ChatworkClient
{
    public override string clientName => nameof(OAuth2Client);

    private readonly HttpMessageInvoker MessageInvoker = invoker ?? new HttpClient();
    private string _clientKey { get; } = clientKey;
    private string _secretKey { get; } = secretKey;
    private string _oauth2Code { get; set; } = string.Empty;
    private string _accessToken { get; set; } = string.Empty;
    private string _refreshToken { get; set; } = string.Empty;
    private long _tokenExpired { get; set; } = 0;

    private string _scope { get; set; } = string.Empty;
    private string _redirectUri { get; set; } = string.Empty;
    private DateTime _tokenQueryTime { get; set; } = DateTime.Now;

    private HttpRequestMessage GenerateRequestMessage(Uri uri, HttpMethod method)
    {
        var request = new HttpRequestMessage
        {
            Method = method,
            RequestUri = uri,
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this._accessToken);
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

    public OAuth2ConcentQueryResult Authorization(
        OAuth2ConcentQuery query,
        string codeVerifer = "",
        Action<string> openBrowser = null,
        Func<string> readCode = null)
    {
        query.client_id = this._clientKey;
        this._scope = query.scope;
        this._redirectUri = query.redirect_uri;
        var contentUrlArg = EndPoints.Oauth2.OriginalString + $"{UrlArgEncoder.ToURLArg(query)}";
        (openBrowser ?? DefaultOpenBrowser)(contentUrlArg);
        this._oauth2Code = (readCode ?? DefaultReadCode)();

        if(string.IsNullOrEmpty(this._oauth2Code))
        {
            return new OAuth2ConcentQueryResult
            {
                error = "oauth_code_error",
                //error_description = "inputed oauth_code is null or empty",
            };
        }
        query.client_id = this._clientKey;
        return new OAuth2ConcentQueryResult
        {
            code = this._oauth2Code,
        };
    }

    // TODO Only windows
    private static void DefaultOpenBrowser(string url)
    {
        Console.WriteLine("Please input code of redirect url code=");
        Process.Start(
            new ProcessStartInfo("cmd", $"/c start {url}")
            {
                CreateNoWindow = true,
            }
        );
    }

    private static string DefaultReadCode()
    {
        return Console.ReadLine();
    }

    public async Task<OAuth2TokenQueryResult> UpdateTokenAsync(OAuth2TokenQuery.GrantType grantType = OAuth2TokenQuery.GrantType.RefreshToken, string codeVerifer = "", CancellationToken cancellation = default)
    {
        var tokenQuery = new OAuth2TokenQuery(grantType)
        {
            scope = this._scope,
            redirect_uri = this._redirectUri,
        };
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = EndPoints.Token,
        };

        if(grantType == OAuth2TokenQuery.GrantType.AuthroizationCode)
            tokenQuery.code = this._oauth2Code;
        else if(grantType == OAuth2TokenQuery.GrantType.RefreshToken)
            tokenQuery.refresh_token = this._refreshToken;

        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Basic",
            Convert.ToBase64String(Encoding.ASCII.GetBytes($"{this._clientKey}:{this._secretKey}"))
        );
        request.Content = new FormUrlEncodedContent(UrlArgEncoder.ToDictionary(tokenQuery));

        var client = this.MessageInvoker;
        var response = await client.SendAsync(request, cancellation);
        var stream = await response.Content.ReadAsStreamAsync();

        using var reader = new StreamReader(stream);
        var result = JsonSerializer.Deserialize<OAuth2TokenQueryResult>(await reader.ReadToEndAsync());
        this._tokenExpired = result.expires_in;
        this._tokenQueryTime = DateTime.Now;
        this._refreshToken = result.refresh_token;
        this._accessToken = result.access_token;
        return result;
    }
}
