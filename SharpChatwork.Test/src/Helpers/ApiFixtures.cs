namespace SharpChatwork.Test.Helpers;

internal static class ApiFixtures
{
    public const string MeGet = """
    {
      "account_id": 123,
      "room_id": 322,
      "name": "John Smith",
      "chatwork_id": "tarochatworkid",
      "organization_id": 101,
      "organization_name": "Hello Company",
      "department": "Marketing",
      "title": "CMO",
      "url": "http://mycompany.example.com",
      "introduction": "Self Introduction",
      "mail": "taro@example.com",
      "tel_organization": "XXX-XXXX-XXXX",
      "tel_extension": "YYY-YYYY-YYYY",
      "tel_mobile": "ZZZ-ZZZZ-ZZZZ",
      "skype": "myskype_id",
      "facebook": "myfacebook_id",
      "twitter": "mytwitter_id",
      "avatar_image_url": "https://example.com/abc.png",
      "login_mail": "account@example.com"
    }
    """;

    public const string MyStatusGet = """
    {
      "unread_room_num": 2,
      "mention_room_num": 1,
      "mytask_room_num": 3,
      "unread_num": 12,
      "mention_num": 1,
      "mytask_num": 8
    }
    """;

    public const string MyTasksGet = """
    [
      {
        "task_id": 3,
        "room": { "room_id": 5, "name": "Group Chat Name", "icon_path": "https://example.com/ico.png" },
        "assigned_by_account": { "account_id": 78, "name": "Anna", "avatar_image_url": "https://example.com/anna.png" },
        "message_id": "13",
        "body": "buy milk",
        "limit_time": 1384354799,
        "status": "open",
        "limit_type": "date"
      }
    ]
    """;

    public const string ContactsGet = """
    [
      {
        "account_id": 123,
        "room_id": 322,
        "name": "John Smith",
        "chatwork_id": "tarochatworkid",
        "organization_id": 101,
        "organization_name": "Hello Company",
        "department": "Marketing",
        "avatar_image_url": "https://example.com/abc.png"
      }
    ]
    """;

    public const string RoomsGet = """
    [
      {
        "room_id": 123,
        "name": "Group Chat Name",
        "type": "group",
        "role": "admin",
        "sticky": false,
        "unread_num": 10,
        "mention_num": 1,
        "mytask_num": 0,
        "message_num": 122,
        "file_num": 10,
        "task_num": 17,
        "icon_path": "https://example.com/ico.png",
        "last_update_time": 1298905200
      }
    ]
    """;

    public const string RoomGet = """
    {
      "room_id": 123,
      "name": "Group Chat Name",
      "type": "group",
      "role": "admin",
      "sticky": false,
      "unread_num": 10,
      "mention_num": 1,
      "mytask_num": 0,
      "message_num": 122,
      "file_num": 10,
      "task_num": 17,
      "icon_path": "https://example.com/ico.png",
      "last_update_time": 1298905200
    }
    """;

    public const string RoomIdResult = """
    {
      "room_id": "1234"
    }
    """;

    public const string RoomMembersGet = """
    [
      {
        "account_id": 123,
        "role": "admin",
        "name": "John Smith",
        "chatwork_id": "tarochatworkid",
        "organization_id": 101,
        "organization_name": "Hello Company",
        "department": "Marketing",
        "avatar_image_url": "https://example.com/abc.png"
      }
    ]
    """;

    public const string RoomMessagesGet = """
    [
      {
        "message_id": "5",
        "account": {
          "account_id": 123,
          "name": "Bob",
          "avatar_image_url": "https://example.com/bob.png"
        },
        "body": "Hello Chatwork!",
        "send_time": 1384242850,
        "update_time": 0
      }
    ]
    """;

    public const string RoomMessageGet = """
    {
      "message_id": "5",
      "account": {
        "account_id": 123,
        "name": "Bob",
        "avatar_image_url": "https://example.com/bob.png"
      },
      "body": "Hello Chatwork!",
      "send_time": 1384242850,
      "update_time": 0
    }
    """;

    public const string MessageIdResult = """
    {
      "message_id": "9876"
    }
    """;

    public const string MessageReadUnreadResult = """
    {
      "unread_num": 3,
      "mention_num": 1
    }
    """;

    public const string RoomTasksGet = """
    [
      {
        "task_id": 3,
        "room": { "room_id": 5, "name": "Group Chat", "icon_path": "https://example.com/ico.png" },
        "assigned_by_account": { "account_id": 78, "name": "Anna", "avatar_image_url": "https://example.com/anna.png" },
        "message_id": "13",
        "body": "buy milk",
        "limit_time": 1384354799,
        "status": "open",
        "limit_type": "date"
      }
    ]
    """;

    public const string RoomTaskGet = """
    {
      "task_id": 3,
      "room": { "room_id": 5, "name": "Group Chat", "icon_path": "https://example.com/ico.png" },
      "assigned_by_account": { "account_id": 78, "name": "Anna", "avatar_image_url": "https://example.com/anna.png" },
      "message_id": "13",
      "body": "buy milk",
      "limit_time": 1384354799,
      "status": "open",
      "limit_type": "date"
    }
    """;

    public const string TaskIdResult = """
    {
      "task_id": "1234"
    }
    """;

    public const string RoomFilesGet = """
    [
      {
        "file_id": 3,
        "account": {
          "account_id": 123,
          "name": "Bob",
          "avatar_image_url": "https://example.com/bob.png"
        },
        "message_id": "22",
        "filename": "README.md",
        "filesize": 21,
        "upload_time": 1384164237
      }
    ]
    """;

    public const string RoomFileGet = """
    {
      "file_id": 3,
      "account": {
        "account_id": 123,
        "name": "Bob",
        "avatar_image_url": "https://example.com/bob.png"
      },
      "message_id": "22",
      "filename": "README.md",
      "filesize": 21,
      "upload_time": 1384164237
    }
    """;

    public const string FileIdResult = """
    {
      "file_id": "1234"
    }
    """;

    public const string InviteLinkGet = """
    {
      "public": true,
      "url": "https://example.com/abc123",
      "need_acceptance": true,
      "description": "Welcome"
    }
    """;

    public const string IncomingRequestsGet = """
    [
      {
        "request_id": 1,
        "account_id": 2,
        "message": "Please add me!",
        "name": "Mike",
        "chatwork_id": "mike-id",
        "organization_id": 101,
        "organization_name": "Hello Company",
        "department": "Engineering",
        "avatar_image_url": "https://example.com/mike.png"
      }
    ]
    """;

    public const string IncomingRequestPut = """
    {
      "request_id": 1,
      "account_id": 2,
      "message": "Please add me!",
      "name": "Mike",
      "chatwork_id": "mike-id",
      "organization_id": 101,
      "organization_name": "Hello Company",
      "department": "Engineering",
      "avatar_image_url": "https://example.com/mike.png"
    }
    """;

    public const string OAuth2TokenResult = """
    {
      "access_token": "abc-access",
      "refresh_token": "abc-refresh",
      "token_type": "bearer",
      "expires_in": 3600,
      "scope": "rooms.all:read_write"
    }
    """;

    public const string ErrorResponse = """
    { "errors": ["Invalid API token"] }
    """;
}
