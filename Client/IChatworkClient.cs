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

        // Task<List<Room>> GetRoomsAsync();
        // Task<long> CreateRoomsAsync();
        // Task<Room> GetRoomAsync(long roomId);
        // Task<long> UpdateRoomAsync(long roomId);
        // Task LeaveRoom(long roomId);
        // Task<List<Room>> GetRoomMembersAsync(long roomId);
        // Task<RoomMember> UpdateRoomMembersAsync(long roomId);
        // Task<List<UserMessage>> GetRoomMessages(long roomId);
        // Task<long> AddRoomMessages(long roomId);
        // Task<MessageReadUnread> ReadRoomMessages(long roomId);
        // Task<MessageReadUnread> UnReadRoomMessages(long roomId);
        // Task<UserMessage> GetRoomMessage(long roomId,long messageId);
        // Task<long> UpdateRoomMessage(long roomId, long messageId);
        // Task<long> RemoveRoomMessage(long roomId, long messageId);
        // Task<List<Room>> GetRoomTasks(long roomId);
        // Task<List<long>> CreateRoomTask(long roomId);
        // Task<UserTask> GetRoomTask(long roomId,long taskId);
        // Task<long> UpdateRoomTask(long roomId, long taskId, TaskStateType state);
        // Task<List<UserFile>> GetRoomFiles(long roomId);
        // Task<long> UploadRoomFile(long roomId);
        // Task<UserFile> GetRoomFile(long roomId,long fileId,bool createDownloadLink);
        // Task<UserTask> GetRoomInvite(long roomId);
        // Task<UserTask> CreateRoomInvite(long roomId);
        // Task<UserTask> UpdateRoomInvite(long roomId);
        // Task DestroyRoomInvite(long roomId);

        Task<List<IncomingRequest>> GetIncomingRequestsAsync();
        Task<IncomingRequest> AcceptIncomingRequest(long requestId);
        Task CancelIncomingRequestAsync(long requestId);
    }
}
