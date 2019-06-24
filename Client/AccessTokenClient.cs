using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Rooms;
using SharpChatwork.Query;
using SharpChatwork.Query.Me;
using SharpChatwork.Query.My;

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

        public Task<Status> GetMyStatusAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetMeAsync()
        {
            throw new NotImplementedException();
        }

        Task<List<Query.My.Task>> IChatworkClient.GetMyTasksAsync()
        {
            throw new NotImplementedException();
        }
    }
}
