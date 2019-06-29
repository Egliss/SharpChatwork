using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork
{
	internal class AccessTokenClient : ChatworkClient
	{
		protected internal override string clientName => nameof(AccessTokenClient);
		protected internal override AuthenticationHeaderValue authenticationHeader => throw new NotImplementedException();

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}
	}
}
