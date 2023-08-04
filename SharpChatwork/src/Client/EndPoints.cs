using System;

namespace SharpChatwork
{
    public class EndPoints
    {
        public static readonly Uri Token = new Uri($@"https://oauth.chatwork.com/token");
        public static readonly Uri Oauth2 = new Uri($@"https://www.chatwork.com/packages/oauth2/login.php");
        public static readonly Uri Me = new Uri($@"https://api.chatwork.com/v2/me");
        public static readonly Uri MyStatus = new Uri($@"https://api.chatwork.com/v2/my/status");
        public static readonly Uri MyTasks = new Uri($@"https://api.chatwork.com/v2/my/tasks");
        public static readonly Uri Rooms = new Uri($@"https://api.chatwork.com/v2/Rooms");
        public static readonly Uri Contacts = new Uri($@"https://api.chatwork.com/v2/contacts");
        public static readonly Uri IncomingRequests = new Uri($@"https://api.chatwork.com/v2/incoming_requests");

        public static Uri RoomOf(long roomId)
        {
            return new Uri($@"https://api.chatwork.com/v2/rooms/{roomId}");
        }

        public static Uri RoomMember(long roomId)
        {
            return new Uri($@"https://api.chatwork.com/v2/rooms/{roomId}/members");
        }

        public static Uri RoomMessages(long roomId)
        {
            return new Uri($@"https://api.chatwork.com/v2/rooms/{roomId}/messages");
        }

        public static Uri RoomMessagesOf(long roomId, long messageId)
        {
            return new Uri($@"https://api.chatwork.com/v2/rooms/{roomId}/messages/{messageId}");
        }

        public static Uri RoomMessagesRead(long roomId)
        {
            return new Uri($@"https://api.chatwork.com/v2/rooms/{roomId}/messages/read");
        }

        public static Uri RoomMessagesUnread(long roomId)
        {
            return new Uri($@"https://api.chatwork.com/v2/rooms/{roomId}/messages/unread");
        }

        public static Uri RoomTasks(long roomId)
        {
            return new Uri($@"https://api.chatwork.com/v2/rooms/{roomId}/tasks");
        }

        public static Uri RoomTasksOf(long roomId, long taskId)
        {
            return new Uri($@"https://api.chatwork.com/v2/rooms/{roomId}/tasks/{taskId}");
        }

        public static Uri RoomFiles(long roomId)
        {
            return new Uri($@"https://api.chatwork.com/v2/rooms/{roomId}/files");
        }

        public static Uri RoomFilesOf(long roomId, long fileId)
        {
            return new Uri($@"https://api.chatwork.com/v    2/rooms/{roomId}/files/{fileId}");
        }

        public static Uri RoomLink(long roomId)
        {
            return new Uri($@"https://api.chatwork.com/v2/rooms/{roomId}/link");
        }

        public static Uri IncomingRequestsOf(long requestId)
        {
            return new Uri($@"https://api.chatwork.com/v2/incoming_requests/{requestId}");
        }
    }
}
