using Newtonsoft.Json;
using SharpChatwork.Query;
using SharpChatwork.Query.Types;
using SharpChatwork.Query.Types.Ids;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
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
                return reader.ReadToEnd();
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
                return reader.ReadToEnd();
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
        public async Task<ElementId> UpdateRoomAsync(long roomId, string roomName, string description, RoomIconPreset preset)
        {
            var data = new Dictionary<string, string>()
            {
                {"name",roomName },
                {"description",roomName },
                {"icon_preset",preset.ToAliasOrDefault() }
            };
            return await QueryAsync<RoomId>(EndPoints.RoomOf(roomId), HttpMethod.Post, data);
        }
        public async Task LeaveRoomAsync(long roomId)
        {
            var uri = $"{EndPoints.RoomMessages(roomId)}?action_type=leave";
            await QueryAsync<RoomId>(new Uri(uri), HttpMethod.Post);
        }
        public async Task DeleteRoomAsync(long roomId)
        {
            var uri = $"{EndPoints.RoomMessages(roomId)}?action_type=delete";
            await QueryAsync<RoomId>(new Uri(uri), HttpMethod.Post);
        }
        public async Task<List<User>> GetRoomMembersAsync(long roomId)
        {
            return await this.QueryAsync<List<User>>(EndPoints.Rooms, HttpMethod.Get);
        }
        public async Task<RoomMember> UpdateRoomMembersAsync(long roomId, IEnumerable<long> adminsMembers, IEnumerable<long> normalMembers, IEnumerable<long> readonlyMembers)
        {
            var data = new Dictionary<string, string>()
            {
                // TODO convert
                //{"members_admin_ids",adminsMembers },
                //{"members_member_ids",roomName },
                //{"members_readonly_ids",preset.ToAliasOrDefault() }
            };
            return await this.QueryAsync<RoomMember>(EndPoints.Rooms, HttpMethod.Get, data);
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
        public async Task<MessageReadUnread> ReadRoomMessagesAsync(long roomId,long messageId)
        {
            var uri = $"{EndPoints.RoomMessages(roomId)}?message_id={messageId.ToString()}";
            return await this.QueryAsync<MessageReadUnread>(new Uri(uri), HttpMethod.Post);
        }
        public async Task<MessageReadUnread> UnReadRoomMessagesAsync(long roomId, long messageId)
        {
            var uri = $"{EndPoints.RoomMessages(roomId)}?message_id={messageId.ToString()}";
            return await this.QueryAsync<MessageReadUnread>(new Uri(uri), HttpMethod.Post);
        }
        public async Task<UserMessage> GetRoomMessageAsync(long roomId, long messageId)
        {
            return await this.QueryAsync<UserMessage>(EndPoints.RoomMessagesOf(roomId, messageId), HttpMethod.Get);
        }
        public async Task<ElementId> UpdateRoomMessageAsync(long roomId, long messageId, string message)
        {
            var uri = $"{EndPoints.RoomMessagesOf(roomId, messageId)}?body={message}";
            return await this.QueryAsync<ElementId>(new Uri(uri), HttpMethod.Post);
        }
        public async Task<ElementId> RemoveRoomMessageAsync(long roomId, long messageId)
        {
            return await this.QueryAsync<ElementId>(EndPoints.RoomMessagesOf(roomId,messageId), HttpMethod.Delete);
        }
        public async Task<List<UserTask>> GetRoomTasksAsync(long roomId, long accountId, long autherId, bool isDone = false)
        {
            var doneString = "done";
            if (!isDone)
                doneString = "open";
            var data = new Dictionary<string, string>()
            {
                { "account_id" , accountId.ToString() },
                { "assigned_by_account_id" , autherId.ToString()},
                { "status" , doneString},
            };
            return await this.QueryAsync<List<UserTask>>(EndPoints.RoomTasks(roomId), HttpMethod.Get, data);
        }
        public async Task<List<ElementId>> CreateRoomTaskAsync(long roomId,string taskText,long limit)
        {
            //var data = new Dictionary<string, string>()
            //{
            //    { "body" , taskText },
            //    { "limit" , limit},
            //    { "limit_type" , doneString},
            //    { "to_ids" , doneString},
            //};
            //return await this.QueryAsync<List<UserTask>>(EndPoints.RoomTasks(roomId), HttpMethod.Get, data);
            throw new NotImplementedException();
        }
        public async Task<UserTask> GetRoomTaskAsync(long roomId, long taskId)
        {
            return await this.QueryAsync<UserTask>(EndPoints.RoomTasksOf(roomId,taskId), HttpMethod.Get);
        }
        public async Task<ElementId> UpdateRoomTaskAsync(long roomId, long taskId, TaskStateType state)
        {
            var uri = $"{EndPoints.RoomTasksOf(roomId, taskId)}?body={state.ToAliasOrDefault()}";
            return await this.QueryAsync<ElementId>(new Uri(uri), HttpMethod.Post);
        }
        public async Task<List<UserFile>> GetRoomFilesAsync(long roomId,long accountId)
        {
            var uri = $"{EndPoints.RoomFiles(roomId)}?account_id={accountId.ToString()}";
            return await this.QueryAsync<List<UserFile>>(new Uri(uri), HttpMethod.Get);
        }
        public async Task<ElementId> UploadRoomFileAsync(long roomId)
        {
            throw new NotImplementedException();
        }
        public async Task<UserFile> GetRoomFileAsync(long roomId, long fileId, bool createDownloadLink)
        {
            var uri = $"{EndPoints.RoomFiles(roomId)}?create_download_url={ToIntBool(createDownloadLink)}";
            return await this.QueryAsync<UserFile>(new Uri(uri), HttpMethod.Get);
        }
        public async Task<InviteLink> GetRoomInviteAsync(long roomId)
        {
            return await this.QueryAsync<InviteLink>(EndPoints.RoomLink(roomId), HttpMethod.Post);
        }
        public async Task<InviteLink> CreateRoomInviteAsync(long roomId, string uniqueName, string description, bool requireAcceptance)
        {
            var data = new Dictionary<string, string>()
            {
                { "code" , uniqueName },
                { "description" , description},
                { "need_acceptance" , ToIntBool(requireAcceptance).ToString()},
            };
            return await this.QueryAsync<InviteLink>(EndPoints.RoomTasks(roomId), HttpMethod.Post, data);
        }
        public async Task<InviteLink> UpdateRoomInviteAsync(long roomId,string uniqueName,string description,bool requireAcceptance)
        {
            var data = new Dictionary<string, string>()
            {
                { "code" , uniqueName },
                { "description" , description},
                { "need_acceptance" , ToIntBool(requireAcceptance).ToString()},
            };
            return await this.QueryAsync<InviteLink>(EndPoints.RoomTasks(roomId), HttpMethod.Put, data);
        }
        public async Task DestroyRoomInviteAsync(long roomId)
        {
            await this.QueryNoReturnAsync(EndPoints.RoomLink(roomId), HttpMethod.Delete);
        }
    }
}
