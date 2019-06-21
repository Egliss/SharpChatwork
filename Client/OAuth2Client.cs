using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Client
{
    public class OAuth2Client// : IChatworkClient
    {
        public IChatworkQueryResult Query(IChatworkQuery query)
        {
            throw new NotImplementedException();
        }
        /*
        public async Task<IChatworkQueryResult> QueryAsync(IChatworkQuery query)
        {
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(query.endPoint);
        }

        public void Initialize()
        {
            AccessTokenQuery query = AccessTokenQuery.CreateFirst(authCode);
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = EndPoints.Token,
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ChatworkApplication.Client}:{ChatworkApplication.Secret}")));
            request.Content = new FormUrlEncodedContent(URLArgEncoder.FromData(query));
            HttpClient client = new HttpClient();

            try
            {
                var result = await client.SendAsync(request);
                using (StreamReader reader = new StreamReader(await result.Content.ReadAsStreamAsync()))
                {
                    return JsonConvert.DeserializeObject<AccessTokenQueryResult>(reader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        */
    }
}
