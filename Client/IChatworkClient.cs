using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SharpChatwork
{
    public interface IChatworkClient : ISerializable
    {
        Task<List<Room>> GetRooms();
        Task<User> GetMeAsync();
        Task<List<UserTask>> GetMyTasksAsync();
        Task<Status> GetMyStatusAsync();
    }
}
