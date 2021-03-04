using System;
using System.Collections.Generic;
using System.Text;

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

        public static Uri RoomOf(long roomId) => new Uri($@"https://api.chatwork.com/v2/rooms/{roomId.ToString()}");
        public static Uri RoomMember(long roomId) => new Uri($@"https://api.chatwork.com/v2/rooms/{roomId.ToString()}/members");
        public static Uri RoomMessages(long roomId) => new Uri($@"https://api.chatwork.com/v2/rooms/{roomId.ToString()}/messages");
        public static Uri RoomMessagesOf(long roomId, long messageId) => new Uri($@"https://api.chatwork.com/v2/rooms/{roomId.ToString()}/messages/{messageId.ToString()}");
        public static Uri RoomMessagesRead(long roomId) => new Uri($@"https://api.chatwork.com/v2/rooms/{roomId.ToString()}/messages/read");
        public static Uri RoomMessagesUnread(long roomId) => new Uri($@"https://api.chatwork.com/v2/rooms/{roomId.ToString()}/messages/unread");
        public static Uri RoomTasks(long roomId) => new Uri($@"https://api.chatwork.com/v2/rooms/{roomId.ToString()}/tasks");
        public static Uri RoomTasksOf(long roomId, long taskId) => new Uri($@"https://api.chatwork.com/v2/rooms/{roomId.ToString()}/tasks/{taskId.ToString()}");
        public static Uri RoomFiles(long roomId) => new Uri($@"https://api.chatwork.com/v2/rooms/{roomId.ToString()}/files");
        public static Uri RoomFilesOf(long roomId, long fileId) => new Uri($@"https://api.chatwork.com/v2/rooms/{roomId.ToString()}/files/{fileId.ToString()}");
        public static Uri RoomLink(long roomId) => new Uri($@"https://api.chatwork.com/v2/rooms/{roomId.ToString()}/link");
        public static Uri IncomingRequestsOf(long requestId) => new Uri($@"https://api.chatwork.com/v2/incoming_requests/{requestId.ToString()}");
    }
}
