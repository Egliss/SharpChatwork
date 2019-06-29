using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Client.OAuth2
{
	internal class OAuth2Client2 : ChatworkClient
	{
		protected override string clientName => nameof(OAuth2Client2);

		private string clientKey { get; set; } = string.Empty;
		private string secretKey { get; set; } = string.Empty;
		private string oauth2Code { get; set; } = string.Empty;
		private string accessToken { get; set; } = string.Empty;
		private string refleshToken { get; set; } = string.Empty;
		private long tokenExpired { get; set; } = 0;
		private string scope { get; set; } = string.Empty;
		private string redirectUri { get; set; } = string.Empty;
		private DateTime tokenQueryTime { get; set; } = DateTime.Now;

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}
		public OAuth2Client2() { }
		protected OAuth2Client2(SerializationInfo info, StreamingContext context)
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

		public override async ValueTask<ReturnT> QueryAsync<ReturnT>(Uri uri, HttpMethod method, Dictionary<string, string> data)
		{
			var text = await this.QueryTextAsync(uri, method, data);
			return JsonConvert.DeserializeObject<ReturnT>(text);
		}
		public override async ValueTask QueryAsync(Uri uri, HttpMethod method, Dictionary<string, string> data)
		{
			HttpRequestMessage request = new HttpRequestMessage
			{
				Method = method,
				RequestUri = uri
			};
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.accessToken);
			request.Content = new FormUrlEncodedContent(data);
			HttpClient client = new HttpClient();
			await client.SendAsync(request);
			return;
			//using (StreamReader reader = new StreamReader(await result.Content.ReadAsStreamAsync(), Encoding.UTF8))
			//{
			//	return reader.ReadToEnd();
			//}
		}
		public override async ValueTask<string> QueryTextAsync(Uri uri, HttpMethod method, Dictionary<string, string> data)
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
	}
}
