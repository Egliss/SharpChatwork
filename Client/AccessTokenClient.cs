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

        public Task<UserTask> CreateRoomInviteAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<long> CreateRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<long>> CreateRoomTaskAsync(long roomId)
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

        public Task<List<UserFile>> GetRoomFilesAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<UserTask> GetRoomInviteAsync(long roomId)
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

        public Task<List<UserMessage>> GetRoomMessagesAsync(long roomId, bool isForceMode)
        {
            throw new NotImplementedException();
        }

        public Task<UserTask> GetRoomTaskAsync(long roomId, long taskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetRoomTasksAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task LeaveRoomAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<MessageReadUnread> ReadRoomMessagesAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<long> RemoveRoomMessageAsync(long roomId, long messageId)
        {
            throw new NotImplementedException();
        }

        public Task<long> SendRoomMessagesAsync(long roomId, string message, bool isSelfUnread)
        {
            throw new NotImplementedException();
        }

        public Task<MessageReadUnread> UnReadRoomMessagesAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateRoomAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<UserTask> UpdateRoomInviteAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<RoomMember> UpdateRoomMembersAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateRoomMessageAsync(long roomId, long messageId)
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateRoomTaskAsync(long roomId, long taskId, TaskStateType state)
        {
            throw new NotImplementedException();
        }

        public Task<long> UploadRoomFileAsync(long roomId)
        {
            throw new NotImplementedException();
        }

        Task<ElementId> IChatworkClient.SendRoomMessagesAsync(long roomId, string message, bool isSelfUnread)
        {
            throw new NotImplementedException();
        }
    }
}
