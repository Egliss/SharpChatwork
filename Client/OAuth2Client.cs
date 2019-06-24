using Newtonsoft.Json;
using SharpChatwork.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork
{
    public class OAuth2Client : IChatworkClient
    {
        public string clientKey { get; set; } = string.Empty;
        public string secretKey { get; set; } = string.Empty;
        private string oauth2Code { get; set; } = string.Empty;
        private string accessToken { get; set; } = string.Empty;
        private string refleshToken { get; set; } = string.Empty;
        private long tokenExpired { get; set; } = 0;
        private string scope { get; set; } = string.Empty;
        private string redirectUri { get; set; } = string.Empty;

        private DateTime tokenQueryTime { get; set; } = DateTime.Now;

        public OAuth2ConcentQueryResult Authorization(OAuth2ConcentQuery query,string codeVerifer = "")
        {
            query.client_id = this.clientKey;
            this.scope = query.scope;
            this.redirectUri = query.redirect_uri;

            var concentUrlArg = EndPoints.Oauth2.OriginalString + $"{URLArgEncoder.ToURLArg(query)}";
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

            if (grantType == OAuth2TokenQuery.GrantType.AuthroizationCode)
                tokenQuery.code = this.oauth2Code;
            else if (grantType == OAuth2TokenQuery.GrantType.RefreshToken)
                tokenQuery.refresh_token = this.refleshToken;

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{this.clientKey}:{this.secretKey}")));
            request.Content = new FormUrlEncodedContent(URLArgEncoder.ToDictionary(tokenQuery));

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(request);
            var stream = await response.Content.ReadAsStreamAsync();

            using (StreamReader reader = new StreamReader(stream))
            {
                var result = JsonConvert.DeserializeObject<OAuth2TokenQueryResult>(reader.ReadToEnd());
                this.tokenExpired = result.expires_in;
                this.tokenQueryTime = DateTime.Now;
                this.refleshToken = result.refresh_token;
                this.accessToken = result.access_token;
                return result;
            }
        }

        public IChatworkQueryResult Query(IChatworkQuery query)
        {
            throw new NotImplementedException();
        }
        public async Task<IChatworkQueryResult> QueryAsync(IChatworkQuery query)
        {
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(query.endPoint);
            var webResponse = await webRequest.GetResponseAsync();
            using (StreamReader reader = new StreamReader(await webRequest.GetRequestStreamAsync()))
            {
                //return JsonConvert.DeserializeObject<OAuth2TokenQueryResult>(reader.ReadToEnd());
            }
            return null;
        }
    }
}
