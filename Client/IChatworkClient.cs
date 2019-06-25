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

        Task<ElementId> CreateRoomsAsync();
        Task<Room> GetRoomAsync(long roomId);
        Task<ElementId> UpdateRoomAsync(long roomId);
        Task LeaveRoomAsync(long roomId);
        Task<List<User>> GetRoomMembersAsync(long roomId);
        Task<RoomMember> UpdateRoomMembersAsync(long roomId);
        Task<List<UserMessage>> GetRoomMessagesAsync(long roomId, bool isForceMode);
        Task<ElementId> SendRoomMessagesAsync(long roomId, string message, bool isSelfUnread);
        Task<MessageReadUnread> ReadRoomMessagesAsync(long roomId);
        Task<MessageReadUnread> UnReadRoomMessagesAsync(long roomId);
        Task<UserMessage> GetRoomMessageAsync(long roomId, long messageId);
        Task<ElementId> UpdateRoomMessageAsync(long roomId, long messageId);
        Task<ElementId> RemoveRoomMessageAsync(long roomId, long messageId);
        Task<List<Room>> GetRoomTasksAsync(long roomId);
        Task<List<ElementId>> CreateRoomTaskAsync(long roomId);
        Task<UserTask> GetRoomTaskAsync(long roomId, long taskId);
        Task<ElementId> UpdateRoomTaskAsync(long roomId, long taskId, TaskStateType state);
        Task<List<UserFile>> GetRoomFilesAsync(long roomId);
        Task<ElementId> UploadRoomFileAsync(long roomId);
        Task<UserFile> GetRoomFileAsync(long roomId, long fileId, bool createDownloadLink);
        Task<UserTask> GetRoomInviteAsync(long roomId);
        Task<UserTask> CreateRoomInviteAsync(long roomId);
        Task<UserTask> UpdateRoomInviteAsync(long roomId);
        Task DestroyRoomInviteAsync(long roomId);

        Task<List<IncomingRequest>> GetIncomingRequestsAsync();
        Task<IncomingRequest> AcceptIncomingRequest(long requestId);
        Task CancelIncomingRequestAsync(long requestId);
    }
}
