namespace SharpChatwork.Query;

public enum TaskLimitType
{
    [EnumAlias("none")]
    None,
    [EnumAlias("date")]
    Date,
    [EnumAlias("time")]
    Time,
}
