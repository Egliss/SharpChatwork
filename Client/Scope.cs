﻿using System;
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
        UsersAllR = UsersProfileMeR | UsersTasksMeR | UsersStatusMeR,

        [Description("自分のプロフィール情報の取得")]
        [EnumAlias("users.profile.me:read")]
        UsersProfileMeR = 0x0400_0000,

        [Description("自分の未既読数の取得")]
        [EnumAlias("users.status.me:read")]
        UsersStatusMeR = 0x0200_0000,

        [Description("自分のタスク一覧の取得")]
        [EnumAlias("users.tasks.me:read")]
        UsersTasksMeR = 0x0100_0000,

        [Description("チャットルームに紐づくメッセージ・タスク・ファイル・概要・メンバー情報の操作/取得")]
        [EnumAlias("rooms.all:read_write")]
        RoomsAllRW =  RoomsAllR | RoomsAllW,

        [Description("チャットルームに紐づくメッセージ・タスク・ファイル・概要・メンバー情報の取得")]
        [EnumAlias("rooms.all:read")]
        RoomsAllR = 0x00F0_0000,

        [Description("チャットルームに紐づくメッセージ・タスク・ファイル・概要・メンバー情報の操作")]
        [EnumAlias("rooms.all:write")]
        RoomsAllW = 0x0000_00F0,

        [Description("チャットルームの作成と参加しているチャットルームの削除")]
        [EnumAlias("rooms:write")]
        RoomsW = 0x0000_0080,

        [Description("自分が参加しているチャットルーム一覧の取得")]
        [EnumAlias("rooms.info:read")]
        RoomsInfoR = 0x0040_0000,

        [Description("自分が参加しているチャットルーム一覧の更新")]
        [EnumAlias("rooms:info:write")]
        RoomsInfoW = 0x0000_0040,

        [Description("自分が参加しているチャットルームのメンバーの取得")]
        [EnumAlias("rooms.members:read")]
        RoomsMembersR = 0x0020_0000,

        [Description("自分が参加しているチャットルームのメンバーの追加/削除/権限変更")]
        [EnumAlias("rooms.members:write")]
        RoomsMembersW = 0x0000_0020,

        [Description("自分か参加しているチャットルームのメッセージ取得")]
        [EnumAlias("rooms.messages:read")]
        RoomsMessagesR = 0x0010_0000,

        [Description("自分が参加しているチャットルームへのメッセージ投稿")]
        [EnumAlias("rooms.messages:write")]
        RoomsMessagesW = 0x0000_0010,

        [Description("自分が参加しているチャットルームのタスク取得")]
        [EnumAlias("rooms.tasks:read")]
        RoomsTasksR = 0x0004_0000,

        [Description("自分が参加しているチャットルームでタスクを作成")]
        [EnumAlias("rooms.tasks:write")]
        RoomsTasksW = 0x0000_0004,

        [Description("自分が参加しているチャットルームにアップロードされているファイル情報を取得")]
        [EnumAlias("rooms.files:read")]
        RoomsFilesR = 0x0002_0000,

        [Description("自分が参加しているチャットルームへのファイルのアップロード")]
        [EnumAlias("rooms.files:write")]
        RoomsFilesW = 0x0000_0002,

        [Description("自分のコンタクト、及びコンタクト承認依頼情報の取得/操作")]
        [EnumAlias("contacts.all:read_write")]
        RoomsFilesRW = ContactsAllR | ContactsAllW,

        [Description("自分のコンタクト、及びコンタクト承認依頼情報の取得")]
        [EnumAlias("contacts.all:read")]
        ContactsAllR = 0x0001_0000,

        [Description("自分あてのコンタクト承認依頼情報を操作")]
        [EnumAlias("contacts.all:write")]
        ContactsAllW = 0x0000_0001,
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