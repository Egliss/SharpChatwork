using Newtonsoft.Json;
using SharpChatwork.Query.Types;
using SharpChatwork.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpChatwork
{
    [Serializable]
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

        public OAuth2Client() { }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
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
        protected OAuth2Client(SerializationInfo info,StreamingContext context)
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

        public async Task<List<Room>> GetRooms()
        {
            return await QueryAsync<List<Room>>(EndPoints.Rooms, HttpMethod.Get);
        }

        private async Task<ResultT> QueryAsync<ResultT>(Uri uri, HttpMethod method)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri,
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);
            HttpClient client = new HttpClient();
            var result = await client.SendAsync(request);
            using (StreamReader reader = new StreamReader(await result.Content.ReadAsStreamAsync(), Encoding.UTF8))
            {
                var text = Regex.Unescape(reader.ReadToEnd().Replace("\"", "\\\""));
                var ret = JsonConvert.DeserializeObject<ResultT>(text);
                return ret;
            }
        }

        public async Task<Status> GetMyStatusAsync()
        {
            return await QueryAsync<Status>(EndPoints.MyStatus, HttpMethod.Get);
        }
        public async Task<List<UserTask>> GetMyTasksAsync()
        {
            return await QueryAsync<List<UserTask>>(EndPoints.MyTasks, HttpMethod.Get);

        }
        public async Task<User> GetMeAsync()
        {
            return await QueryAsync<User>(EndPoints.Me, HttpMethod.Get);
        }
    }
}
