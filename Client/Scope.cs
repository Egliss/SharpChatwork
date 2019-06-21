using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SharpChatwork
{
    /// <summary>
    /// APIアクセスのスコープです<para/>
    /// http://developer.chatwork.com/ja/oauth.html#secAppendix を基に実装されています
    /// </summary>
    public enum ScopeType : long
    {
        [Description("永続的なAPIアクセスの許可")]
        [EnumAlias("offline_access")]
        OfflineAccess = 0x100000000,

        [Description("自分のアカウントに紐づく情報の取得")]
        [EnumAlias("users.all:read")]
        UserAllR = 0x00000FFF,

        [Description("自分のプロフィール情報の取得")]
        [EnumAlias("users.profile.me:read")]
        UserProfileMeR = 0x0000000F,

        [Description("自分の未既読数の取得")]
        [EnumAlias("users.status.me:read")]
        UserStatusMeR = 0x000000F0,

        [Description("自分のタスク一覧の取得")]
        [EnumAlias("users.tasks.me:read")]
        UsersTasksMeR = 0x00000F00,

        [Description("チャットルームに紐づくメッセージ・タスク・ファイル・概要・メンバー情報の操作/取得")]
        [EnumAlias("rooms.all:read_write")]
        RoomsAllRW = 0x00000000,

        [Description("チャットルームに紐づくメッセージ・タスク・ファイル・概要・メンバー情報の取得")]
        [EnumAlias("rooms.all:read")]
        RoomsAllR,

        [Description("チャットルームに紐づくメッセージ・タスク・ファイル・概要・メンバー情報の操作")]
        [EnumAlias("rooms.all:write")]
        RoomsAllW,

        [Description("チャットルームの作成と参加しているチャットルームの削除")]
        [EnumAlias("rooms:write")]
        RoomsW,

        [Description("自分が参加しているチャットルーム一覧の取得")]
        [EnumAlias("rooms.info:read")]
        RoomsInfoR,

        [Description("自分が参加しているチャットルーム一覧の更新")]
        [EnumAlias("rooms:info:write")]
        RoomsInfoW,

        [Description("自分が参加しているチャットルームのメンバーの取得")]
        [EnumAlias("rooms.members:read")]
        RoomsMembersR,

        [Description("自分が参加しているチャットルームのメンバーの追加/削除/権限変更")]
        [EnumAlias("rooms.members:write")]
        RoomsMembersW,

        [Description("自分か参加しているチャットルームのメッセージ取得")]
        [EnumAlias("rooms.messages:read")]
        RoomsMessagesR,

        [Description("自分が参加しているチャットルームへのメッセージ投稿")]
        [EnumAlias("rooms.messages:write")]
        RoomsMessagesW,

        [Description("自分が参加しているチャットルームのタスク取得")]
        [EnumAlias("rooms.tasks:read")]
        RoomsTasksR,

        [Description("自分が参加しているチャットルームでタスクを作成")]
        [EnumAlias("rooms.tasks:write")]
        RoomsTasksW,

        [Description("自分が参加しているチャットルームにアップロードされているファイル情報を取得")]
        [EnumAlias("rooms.files:read")]
        RoomsFilesR,

        [Description("自分が参加しているチャットルームへのファイルのアップロード")]
        [EnumAlias("rooms.files:write")]
        RoomsFilesW,

        [Description("自分のコンタクト、及びコンタクト承認依頼情報の取得/操作")]
        [EnumAlias("contacts.all:read_write")]
        RoomsFilesRW,

        [Description("自分のコンタクト、及びコンタクト承認依頼情報の取得")]
        [EnumAlias("contacts.all:read")]
        ContactsAllR,

        [Description("自分あてのコンタクト承認依頼情報を操作")]
        [EnumAlias("contacts.all:write")]
        ContactsAllW,
    }

    public static class ScopeTypeExtension
    {
        public static string ToURLArg(this ScopeType type)
        {
            // TODO implement
            return string.Empty;
        }
    }
}
