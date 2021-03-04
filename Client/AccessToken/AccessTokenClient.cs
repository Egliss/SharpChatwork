using Newtonsoft.Json;
using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.AccessToken
{
    [Serializable]
    public class AccessTokenClient : ChatworkClient
    {
        internal override string clientName => nameof(AccessTokenClient);

        private string accessToken { get; set; } = string.Empty;

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.accessToken), this.accessToken);
        }
        public AccessTokenClient(string accessToken)
        {
            this.accessToken = accessToken;
        }
        protected AccessTokenClient(SerializationInfo info, StreamingContext context)
        {
            this.accessToken = info.GetString(nameof(this.accessToken));
        }

        private HttpRequestMessage GenerateRequestMessage(Uri uri, HttpMethod method)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri
            };
            request.Headers.Add("X-ChatWorkToken", this.accessToken);
            return request;
        }

        internal override async ValueTask<ReturnT> QueryAsync<ReturnT>(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            var text = await this.QueryTextAsync(uri, method, data);
            return JsonConvert.DeserializeObject<ReturnT>(text);
        }

        internal override async ValueTask QueryAsync(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            HttpContent content = null;
            if(data.Count != 0)
                content = new FormUrlEncodedContent(data);
            await QueryContentTextAsync(uri, method, content);
        }

        internal override async ValueTask<string> QueryTextAsync(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            HttpContent content = null;
            if(data.Count != 0)
                content = new FormUrlEncodedContent(data);
            return await QueryContentTextAsync(uri, method, content);
        }

        internal override async ValueTask<string> QueryContentTextAsync(Uri uri, HttpMethod method, HttpContent content)
        {
            var requestMessage = this.GenerateRequestMessage(uri, method);
            requestMessage.Content = content;
            HttpClient client = new HttpClient();
            var result = await client.SendAsync(requestMessage);
            var code = (int)result.StatusCode;
            var textContent = await result.Content.ReadAsStringAsync();
            if(code >= 300)
            {
                throw new Exception($"[{code}] {textContent}");
            }

            return textContent;
        }

        internal override async ValueTask<ReturnT> QueryContentAsync<ReturnT>(Uri uri, HttpMethod method, HttpContent content)
        {
            var text = await this.QueryContentTextAsync(uri, method, content);
            return JsonConvert.DeserializeObject<ReturnT>(text);
        }
    }
}
