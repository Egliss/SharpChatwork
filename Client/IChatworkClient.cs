using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SharpChatwork
{
    public interface IChatworkClient : ISerializable
    {
        Task<Status> GetMyStatusAsync();
        Task<List<UserTask>> GetMyTasksAsync();
        Task<User> GetMeAsync();

        Task<List<Contact>> GetContactsAsync();

        Task<List<IncomingRequest>> GetIncomingRequestsAsync();
        Task<IncomingRequest> AcceptIncomingRequest(long requestId);
        Task CancelIncomingRequestAsync(long requestId);

        Task<List<Room>> GetRoomsAsync();
        Task<ElementId> CreateRoomsAsync();
        Task<Room> GetRoomAsync(long roomId);
        Task<ElementId> UpdateRoomAsync(long roomId, string roomName, string description, RoomIconPreset preset);
        Task LeaveRoomAsync(long roomId);
        Task DeleteRoomAsync(long roomId);
        Task<List<User>> GetRoomMembersAsync(long roomId);
        Task<RoomMember> UpdateRoomMembersAsync(long roomId, IEnumerable<long> adminsMembers, IEnumerable<long> normalMembers, IEnumerable<long> readonlyMembers);
        Task<List<UserMessage>> GetRoomMessagesAsync(long roomId, bool isForceMode = false);
        Task<ElementId> SendRoomMessagesAsync(long roomId, string message, bool isSelfUnread);
        Task<MessageReadUnread> ReadRoomMessagesAsync(long roomId, long messageId);
        Task<MessageReadUnread> UnReadRoomMessagesAsync(long roomId, long messageId);
        Task<UserMessage> GetRoomMessageAsync(long roomId, long messageId);
        Task<ElementId> UpdateRoomMessageAsync(long roomId, long messageId, string message);
        Task<ElementId> RemoveRoomMessageAsync(long roomId, long messageId);
        Task<List<UserTask>> GetRoomTasksAsync(long roomId, long accountId, long autherId, bool isDone = false);
        Task<List<ElementId>> CreateRoomTaskAsync(long roomId, string taskText, long limit);
        Task<UserTask> GetRoomTaskAsync(long roomId, long taskId);
        Task<ElementId> UpdateRoomTaskAsync(long roomId, long taskId, TaskStateType state);
        Task<List<UserFile>> GetRoomFilesAsync(long roomId, long accountId);
        Task<ElementId> UploadRoomFileAsync(long roomId);
        Task<UserFile> GetRoomFileAsync(long roomId, long fileId, bool createDownloadLink);
        Task<InviteLink> GetRoomInviteAsync(long roomId);
        Task<InviteLink> CreateRoomInviteAsync(long roomId, string uniqueName, string description, bool requireAcceptance);
        Task<InviteLink> UpdateRoomInviteAsync(long roomId, string uniqueName, string description, bool requireAcceptance);
        Task DestroyRoomInviteAsync(long roomId);
    }
}
