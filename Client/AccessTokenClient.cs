using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork
{
    public class AccessTokenClient : IChatworkClient
    {
        public Task<IncomingRequest> AcceptIncomingRequest(long requestId)
        {
            throw new NotImplementedException();
        }

        public Task CancelIncomingRequestAsync(long requestId)
        {
            throw new NotImplementedException();
        }

        public Task<InviteLink> CreateRoomInviteAsync(long roomId, string uniqueName, string description, bool requireAcceptance)
        {
            throw new NotImplementedException();
        }

        public Task<ElementId> CreateRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ElementId>> CreateRoomTaskAsync(long roomId, string taskText, long limit)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoomAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task DestroyRoomInviteAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Contact>> GetContactsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<IncomingRequest>> GetIncomingRequestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetMeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status> GetMyStatusAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserTask>> GetMyTasksAsync()
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public Task<Room> GetRoomAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<UserFile> GetRoomFileAsync(long roomId, long fileId, bool createDownloadLink)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserFile>> GetRoomFilesAsync(long roomId, long accountId)
        {
            throw new NotImplementedException();
        }

        public Task<InviteLink> GetRoomInviteAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetRoomMembersAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<UserMessage> GetRoomMessageAsync(long roomId, long messageId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserMessage>> GetRoomMessagesAsync(long roomId, bool isForceMode = false)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserTask> GetRoomTaskAsync(long roomId, long taskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserTask>> GetRoomTasksAsync(long roomId, long accountId, long autherId, bool isDone = false)
        {
            throw new NotImplementedException();
        }

        public Task LeaveRoomAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<MessageReadUnread> ReadRoomMessagesAsync(long roomId, long messageId)
        {
            throw new NotImplementedException();
        }

        public Task<ElementId> RemoveRoomMessageAsync(long roomId, long messageId)
        {
            throw new NotImplementedException();
        }

        public Task<ElementId> SendRoomMessagesAsync(long roomId, string message, bool isSelfUnread)
        {
            throw new NotImplementedException();
        }

        public Task<MessageReadUnread> UnReadRoomMessagesAsync(long roomId, long messageId)
        {
            throw new NotImplementedException();
        }

        public Task<ElementId> UpdateRoomAsync(long roomId, string roomName, string description, RoomIconPreset preset)
        {
            throw new NotImplementedException();
        }

        public Task<InviteLink> UpdateRoomInviteAsync(long roomId, string uniqueName, string description, bool requireAcceptance)
        {
            throw new NotImplementedException();
        }

        public Task<RoomMember> UpdateRoomMembersAsync(long roomId, IEnumerable<long> adminsMembers, IEnumerable<long> normalMembers, IEnumerable<long> readonlyMembers)
        {
            throw new NotImplementedException();
        }

        public Task<ElementId> UpdateRoomMessageAsync(long roomId, long messageId, string message)
        {
            throw new NotImplementedException();
        }

        public Task<ElementId> UpdateRoomTaskAsync(long roomId, long taskId, TaskStateType state)
        {
            throw new NotImplementedException();
        }

        public Task<ElementId> UploadRoomFileAsync(long roomId)
        {
            throw new NotImplementedException();
        }
    }
}
