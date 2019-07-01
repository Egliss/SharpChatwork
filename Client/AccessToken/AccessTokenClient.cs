using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork
{
	internal class AccessTokenClient : ChatworkClient
	{
		internal override string clientName => nameof(AccessTokenClient);

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        internal override ValueTask<ReturnT> QueryAsync<ReturnT>(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }

        internal override ValueTask QueryAsync(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }

        internal override ValueTask<string> QueryTextAsync(Uri uri, HttpMethod method, Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }
    }
}
