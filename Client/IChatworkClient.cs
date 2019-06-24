using SharpChatwork.Query.My;
using SharpChatwork.Query.Rooms;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork
{
    public interface IChatworkClient : ISerializable
    {
        Task<List<Room>> GetRooms();
        Task<Query.Me.User> GetMeAsync();
        Task<List<Query.My.Task>> GetMyTasksAsync();
        Task<Status> GetMyStatusAsync();
    }
}
