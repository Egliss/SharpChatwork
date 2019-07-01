namespace SharpChatwork.Query.Types
{
    public enum TaskStateType
    {
        [EnumAlias("open")]
        Open,
        [EnumAlias("done")]
        Done
    }

    public class UserTask
    {
        public int task_id { get; set; }
        public Room room { get; set; }
        public User assigned_by_account { get; set; }
        public string message_id { get; set; }
        public string body { get; set; }
        public int limit_time { get; set; }
        public string status { get; set; }
        public string limit_type { get; set; }
    }
}
