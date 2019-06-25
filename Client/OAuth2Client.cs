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
using SharpChatwork.Query.Types.Ids;

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

        private async Task<string> QueryRawTextAsync(Uri uri, HttpMethod method)
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
                return Regex.Unescape(reader.ReadToEnd().Replace("\"", "\\\""));
            }
        }
        private async Task<string> QueryRawTextAsync(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);
            request.Content = new FormUrlEncodedContent(data);
            HttpClient client = new HttpClient();
            var result = await client.SendAsync(request);
            using (StreamReader reader = new StreamReader(await result.Content.ReadAsStreamAsync(), Encoding.UTF8))
            {
                return Regex.Unescape(reader.ReadToEnd().Replace("\"", "\\\""));
            }
        }
        private async Task<ResultT> QueryAsync<ResultT>(Uri uri, HttpMethod method)
        {
            var text = await QueryRawTextAsync(uri,method);
            return JsonConvert.DeserializeObject<ResultT>(text);
        }
        private async Task<ResultT> QueryAsync<ResultT>(Uri uri, HttpMethod method, Dictionary<string,string> data)
        {
            var text = await QueryRawTextAsync(uri, method, data);
            return JsonConvert.DeserializeObject<ResultT>(text);
        }

        private async Task QueryNoReturnAsync(Uri uri, HttpMethod method)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri,
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);
            HttpClient client = new HttpClient();
            var result = await client.SendAsync(request);
        }

        private static int ToIntBool(bool value)
        {
            if (value)
                return 1;
            return 0;
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

        public async Task<List<Contact>> GetContactsAsync()
        {
            return await QueryAsync<List<Contact>>(EndPoints.Contacts, HttpMethod.Get);
        }

        public async Task<List<IncomingRequest>> GetIncomingRequestsAsync()
        {
            return await QueryAsync<List<IncomingRequest>>(EndPoints.IncomingRequests, HttpMethod.Get);
        }
        public async Task<IncomingRequest> AcceptIncomingRequest(long requestId)
        {
            return await QueryAsync<IncomingRequest>(EndPoints.IncomingRequestsOf(requestId), HttpMethod.Post);
        }
        public async Task CancelIncomingRequestAsync(long requestId)
        {
            await QueryNoReturnAsync(EndPoints.IncomingRequestsOf(requestId), HttpMethod.Delete);
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            return await QueryAsync<List<Room>>(EndPoints.Rooms, HttpMethod.Get);
        }
        public async Task<ElementId> CreateRoomsAsync()
        {
            return await QueryAsync<RoomId>(EndPoints.Rooms, HttpMethod.Get);
        }
        public async Task<Room> GetRoomAsync(long roomId)
        {
            return await QueryAsync<Room>(EndPoints.RoomOf(roomId), HttpMethod.Get);
        }
        public async Task<ElementId> UpdateRoomAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task LeaveRoomAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<User>> GetRoomMembersAsync(long roomId)
        {
            return await this.QueryAsync<List<User>>(EndPoints.Rooms, HttpMethod.Get);
        }
        public async Task<RoomMember> UpdateRoomMembersAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<UserMessage>> GetRoomMessagesAsync(long roomId,bool isForceMode = false)
        {
            // TODO QueryAsync + data is error
            // single arg is invalid ?
            var uri = $"{EndPoints.RoomMessages(roomId)}?force={ToIntBool(isForceMode).ToString()}";
            return await this.QueryAsync<List<UserMessage>>(new Uri(uri), HttpMethod.Get);
        }
        public async Task<ElementId> SendRoomMessagesAsync(long roomId, string message, bool isSelfUnread)
        {
            var data = new Dictionary<string, string>()
            {
                { "body" , message },
                { "self_unread" , ToIntBool(isSelfUnread).ToString() }
            };
            return await this.QueryAsync<MessageId>(EndPoints.RoomMessages(roomId), HttpMethod.Post, data);
        }
        public async Task<MessageReadUnread> ReadRoomMessagesAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task<MessageReadUnread> UnReadRoomMessagesAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task<UserMessage> GetRoomMessageAsync(long roomId, long messageId)
        {
            throw new NotImplementedException();
        }
        public async Task<ElementId> UpdateRoomMessageAsync(long roomId, long messageId)
        {
            throw new NotImplementedException();
        }
        public async Task<ElementId> RemoveRoomMessageAsync(long roomId, long messageId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Room>> GetRoomTasksAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<ElementId>> CreateRoomTaskAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task<UserTask> GetRoomTaskAsync(long roomId, long taskId)
        {
            throw new NotImplementedException();
        }
        public async Task<ElementId> UpdateRoomTaskAsync(long roomId, long taskId, TaskStateType state)
        {
            throw new NotImplementedException();
        }
        public async Task<List<UserFile>> GetRoomFilesAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task<ElementId> UploadRoomFileAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task<UserFile> GetRoomFileAsync(long roomId, long fileId, bool createDownloadLink)
        {
            throw new NotImplementedException();
        }
        public async Task<UserTask> GetRoomInviteAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task<UserTask> CreateRoomInviteAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task<UserTask> UpdateRoomInviteAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task DestroyRoomInviteAsync(long roomId)
        {
            throw new NotImplementedException();
        }
    }
}
