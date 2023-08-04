using SharpChatwork.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading;
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

        internal override async ValueTask<ReturnT> QueryAsync<ReturnT>(Uri uri, HttpMethod method, Dictionary<string, string> data, CancellationToken cancellation = default)
        {
            var text = await this.QueryTextAsync(uri, method, data, cancellation);
            return JsonSerializer.Deserialize<ReturnT>(text);
        }

        internal override async ValueTask QueryAsync(Uri uri, HttpMethod method, Dictionary<string, string> data, CancellationToken cancellation = default)
        {
            HttpContent content = null;
            if(data.Count != 0)
                content = new FormUrlEncodedContent(data);
            await this.QueryContentTextAsync(uri, method, content, cancellation);
        }

        internal override async ValueTask<string> QueryTextAsync(Uri uri, HttpMethod method, Dictionary<string, string> data, CancellationToken cancellation = default)
        {
            HttpContent content = null;
            if(data.Count != 0)
                content = new FormUrlEncodedContent(data);
            return await this.QueryContentTextAsync(uri, method, content, cancellation);
        }

        internal override async ValueTask<string> QueryContentTextAsync(Uri uri, HttpMethod method, HttpContent content, CancellationToken cancellation = default)
        {
            var requestMessage = this.GenerateRequestMessage(uri, method);
            requestMessage.Content = content;
            HttpClient client = new HttpClient();
            var result = await client.SendAsync(requestMessage, cancellation);
            var code = (int)result.StatusCode;
            var textContent = await result.Content.ReadAsStringAsync();
            if(code >= 300)
            {
                throw new ChatworkRequestException(code, textContent);
            }

            return textContent;
        }

        internal override async ValueTask<ReturnT> QueryContentAsync<ReturnT>(Uri uri, HttpMethod method, HttpContent content, CancellationToken cancellation = default)
        {
            var text = await this.QueryContentTextAsync(uri, method, content, cancellation);
            return JsonSerializer.Deserialize<ReturnT>(text);
        }
    }
}
