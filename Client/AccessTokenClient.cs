using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Client.Query.Rooms;
using SharpChatwork.Query;

namespace SharpChatwork
{
    public class AccessTokenClient : IChatworkClient
    {
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        protected AccessTokenClient(SerializationInfo info, StreamingContext context)
        {
        }


        Task<List<Room>> IChatworkClient.GetRooms()
        {
            throw new NotImplementedException();
        }
    }
}
