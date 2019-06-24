using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SharpChatwork
{
    public interface IChatworkClient : ISerializable
    {
        Task<User> GetMeAsync();
        Task<Status> GetMyStatusAsync();
        Task<List<UserTask>> GetMyTasksAsync();

        Task<List<Contact>> GetContactsAsync();

        Task<List<Room>> GetRoomsAsync();

        Task<List<IncomingRequest>> GetIncomingRequestsAsync();
        Task<IncomingRequest> AcceptIncomingRequest(long requestId);
        Task CancelIncomingRequestAsync(long requestId);
    }
}
