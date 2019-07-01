using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

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


        public Task<Status> GetMyStatusAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetMeAsync()
        {
            throw new NotImplementedException();
        }

        Task<List<UserTask>> IChatworkClient.GetMyTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Contact>> GetContactsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<IncomingRequest>> GetIncomingRequestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IncomingRequest> AcceptIncomingRequest(long requestId)
        {
            throw new NotImplementedException();
        }

        public Task CancelIncomingRequestAsync(long requestId)
        {
            throw new NotImplementedException();
        }
    }
}
