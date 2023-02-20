#pragma warning disable CA1707 // Underscore

namespace SharpChatwork.Query.Types
{
    public class TaskId : ElementId
    {
        public long task_id
        {
            get => this.id;
            set => this.id = value;
        }
    }
}
