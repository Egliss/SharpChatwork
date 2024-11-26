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

    private readonly HttpMessageInvoker _messageInvoker = invoker ?? new HttpClient();
    private string clientKey { get; } = clientKey;
    private string secretKey { get; } = secretKey;
    private string oauth2Code { get; set; } = string.Empty;
    private string accessToken { get; set; } = string.Empty;
    private string refleshToken { get; set; } = string.Empty;
    private long tokenExpired { get; set; } = 0;

    private string scope { get; set; } = string.Empty;
    private string redirectUri { get; set; } = string.Empty;
    private DateTime tokenQueryTime { get; set; } = DateTime.Now;

    private HttpRequestMessage GenerateRequestMessage(Uri uri, HttpMethod method)
    {
        var request = new HttpRequestMessage
        {
            Method = method,
            RequestUri = uri,
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);
        return request;
    }

    public override async ValueTask<ResponseWrapper> QueryAsync(Uri uri, HttpMethod method, HttpContent content, CancellationToken cancellation = default)
    {
        var requestMessage = this.GenerateRequestMessage(uri, method);
        requestMessage.Content = content;
        var client = this._messageInvoker;
        var result = await client.SendAsync(requestMessage, cancellation);
        var code = (int)result.StatusCode;

        return new ResponseWrapper
        {
            content = await result.Content.ReadAsStringAsync(),
            headers = result.Headers.ToDictionary(m => m.Key, m => m.Value),
            statusCode = code,
        };
    }

    public OAuth2ConcentQueryResult Authorization(OAuth2ConcentQuery query, string codeVerifer = "")
    {
        query.client_id = this.clientKey;
        this.scope = query.scope;
        this.redirectUri = query.redirect_uri;
        // TODO Only windows
        var concentUrlArg = EndPoints.Oauth2.OriginalString + $"{UrlArgEncoder.ToURLArg(query)}";
        Console.WriteLine("Please input code of redirect url code=");
        Process.Start(
            new ProcessStartInfo("cmd", $"/c start {concentUrlArg}")
            {
                CreateNoWindow = true,
            }
        );
        this.oauth2Code = Console.ReadLine();

        if(string.IsNullOrEmpty(this.oauth2Code))
        {
            return new OAuth2ConcentQueryResult
            {
                error = "oauth_code_error",
                //error_description = "inputed oauth_code is null or empty",
            };
        }
        query.client_id = this.clientKey;
        return new OAuth2ConcentQueryResult
        {
            code = this.oauth2Code,
        };
    }

    public async Task<OAuth2TokenQueryResult> UpdateToken(OAuth2TokenQuery.GrantType grantType = OAuth2TokenQuery.GrantType.RefreshToken, string codeVerifer = "", CancellationToken cancellation = default)
    {
        var tokenQuery = new OAuth2TokenQuery(grantType)
        {
            scope = this.scope,
            redirect_uri = this.redirectUri,
        };
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = EndPoints.Token,
        };

        if(grantType == OAuth2TokenQuery.GrantType.AuthroizationCode)
            tokenQuery.code = this.oauth2Code;
        else if(grantType == OAuth2TokenQuery.GrantType.RefreshToken)
            tokenQuery.refresh_token = this.refleshToken;

        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Basic",
            Convert.ToBase64String(Encoding.ASCII.GetBytes($"{this.clientKey}:{this.secretKey}"))
        );
        request.Content = new FormUrlEncodedContent(UrlArgEncoder.ToDictionary(tokenQuery));

        var client = this._messageInvoker;
        var response = await client.SendAsync(request, cancellation);
        var stream = await response.Content.ReadAsStreamAsync();

        using var reader = new StreamReader(stream);
        var result = JsonSerializer.Deserialize<OAuth2TokenQueryResult>(reader.ReadToEnd());
        this.tokenExpired = result.expires_in;
        this.tokenQueryTime = DateTime.Now;
        this.refleshToken = result.refresh_token;
        this.accessToken = result.access_token;
        return result;
    }
}
