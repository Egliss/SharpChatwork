using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharpChatwork.Query.Types;

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

        internal override async ValueTask<ReturnT> QueryAsync<ReturnT>(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            var text = await this.QueryTextAsync(uri, method, data);
            return JsonConvert.DeserializeObject<ReturnT>(text);
        }

        internal override async ValueTask QueryAsync(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("X-ChatWorkToken", this.accessToken);
            if (data.Count != 0)
                request.Content = new FormUrlEncodedContent(data);
            HttpClient client = new HttpClient();
            await client.SendAsync(request);
            return;
        }

        internal override async ValueTask<string> QueryTextAsync(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri
            };
            request.Headers.Add("X-ChatWorkToken", this.accessToken);
            if (data.Count != 0)
                request.Content = new FormUrlEncodedContent(data);
            HttpClient client = new HttpClient();
            var result = await client.SendAsync(request);
            using (StreamReader reader = new StreamReader(await result.Content.ReadAsStreamAsync(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
