using SharpChatwork.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharpChatwork.OAuth2
{
    [Serializable]
    public class OAuth2Client : ChatworkClient
    {
        internal override string clientName => nameof(OAuth2Client);
        private string clientKey { get; set; } = string.Empty;
        private string secretKey { get; set; } = string.Empty;
        private string oauth2Code { get; set; } = string.Empty;
        private string accessToken { get; set; } = string.Empty;
        private string refleshToken { get; set; } = string.Empty;
        private long tokenExpired { get; set; } = 0;

        private string scope { get; set; } = string.Empty;
        private string redirectUri { get; set; } = string.Empty;
        private DateTime tokenQueryTime { get; set; } = DateTime.Now;

        public OAuth2Client() { }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.clientKey), this.clientKey);
            info.AddValue(nameof(this.secretKey), this.secretKey);
            info.AddValue(nameof(this.oauth2Code), this.oauth2Code);
            info.AddValue(nameof(this.accessToken), this.accessToken);
            info.AddValue(nameof(this.refleshToken), this.refleshToken);
            info.AddValue(nameof(this.tokenExpired), this.tokenExpired);
            info.AddValue(nameof(this.tokenQueryTime), this.tokenQueryTime);
            info.AddValue(nameof(this.redirectUri), this.redirectUri);
            info.AddValue(nameof(this.scope), this.scope);
        }
        protected OAuth2Client(SerializationInfo info, StreamingContext context)
        {
            this.clientKey = info.GetString(nameof(this.clientKey));
            this.secretKey = info.GetString(nameof(this.secretKey));
            this.oauth2Code = info.GetString(nameof(this.oauth2Code));
            this.accessToken = info.GetString(nameof(this.accessToken));
            this.refleshToken = info.GetString(nameof(this.refleshToken));
            this.tokenExpired = info.GetInt64(nameof(this.tokenExpired));
            this.tokenQueryTime = info.GetDateTime(nameof(this.tokenQueryTime));
            this.redirectUri = info.GetString(nameof(this.redirectUri));
            this.scope = info.GetString(nameof(this.scope));
        }
        private HttpRequestMessage GenerateRequestMessage(Uri uri, HttpMethod method)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);
            return request;
        }
        internal override async ValueTask<ReturnT> QueryAsync<ReturnT>(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            var text = await this.QueryTextAsync(uri, method, data);
            return JsonSerializer.Deserialize<ReturnT>(text);
        }
        internal override async ValueTask QueryAsync(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            HttpContent content = null;
            if(data.Count != 0)
                content = new FormUrlEncodedContent(data);
            await this.QueryContentTextAsync(uri, method, content);
            return;
        }
        internal override async ValueTask<string> QueryTextAsync(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            HttpContent content = null;
            if(data.Count != 0)
                content = new FormUrlEncodedContent(data);
            return await this.QueryContentTextAsync(uri, method, content);
        }

        internal override async ValueTask<string> QueryContentTextAsync(Uri uri, HttpMethod method, HttpContent content)
        {
            HttpRequestMessage request = this.GenerateRequestMessage(uri, method);
            request.Content = content;
            HttpClient client = new HttpClient();
            var result = await client.SendAsync(request);
            var code = (int)result.StatusCode;
            var textContent = await result.Content.ReadAsStringAsync();
            if(code >= 300)
            {
                throw new ChatworkRequestException(code, textContent);
            }

            return textContent;
        }

        internal override async ValueTask<ReturnT> QueryContentAsync<ReturnT>(Uri uri, HttpMethod method, HttpContent content)
        {
            var text = await this.QueryContentTextAsync(uri, method, content);
            return JsonSerializer.Deserialize<ReturnT>(text);
        }

        public OAuth2ConcentQueryResult Authorization(OAuth2ConcentQuery query, string codeVerifer = "")
        {
            query.client_id = this.clientKey;
            this.scope = query.scope;
            this.redirectUri = query.redirect_uri;

            // TODO Only windows
            var concentUrlArg = EndPoints.Oauth2.OriginalString + $"{UrlArgEncoder.ToURLArg(query)}";
            Console.WriteLine("Please input code of redirect url code=");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {concentUrlArg}") { CreateNoWindow = true });
            this.oauth2Code = Console.ReadLine();

            if(string.IsNullOrEmpty(this.oauth2Code))
            {
                return new OAuth2ConcentQueryResult()
                {
                    error = "oauth_code_error"
                    //error_description = "inputed oauth_code is null or empty",
                };
            }
            query.client_id = this.clientKey;
            return new OAuth2ConcentQueryResult()
            {
                code = this.oauth2Code,
            };
        }
        public async Task<OAuth2TokenQueryResult> UpdateToken(OAuth2TokenQuery.GrantType grantType = OAuth2TokenQuery.GrantType.RefreshToken, string codeVerifer = "")
        {
            OAuth2TokenQuery tokenQuery = new OAuth2TokenQuery(grantType)
            {
                scope = this.scope,
                redirect_uri = this.redirectUri,
            };
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = EndPoints.Token,
            };

            if(grantType == OAuth2TokenQuery.GrantType.AuthroizationCode)
                tokenQuery.code = this.oauth2Code;
            else if(grantType == OAuth2TokenQuery.GrantType.RefreshToken)
                tokenQuery.refresh_token = this.refleshToken;

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{this.clientKey}:{this.secretKey}")));
            request.Content = new FormUrlEncodedContent(UrlArgEncoder.ToDictionary(tokenQuery));

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(request);
            var stream = await response.Content.ReadAsStreamAsync();

            using(StreamReader reader = new StreamReader(stream))
            {
                var result = JsonSerializer.Deserialize<OAuth2TokenQueryResult>(reader.ReadToEnd());
                this.tokenExpired = result.expires_in;
                this.tokenQueryTime = DateTime.Now;
                this.refleshToken = result.refresh_token;
                this.accessToken = result.access_token;
                return result;
            }
        }
    }
}
