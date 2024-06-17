#pragma warning disable CA1707 // Underscore

namespace SharpChatwork.Query.Types
{
    public class UserId : ElementId
    {
        public long user_id
        {
            get => this.id;
            set => this.id = value;
        }
    }
}
