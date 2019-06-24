using SharpChatwork.Client.Query.Rooms;
using SharpChatwork.Query;
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
    }
}
