namespace SharpChatwork.Query.Types
{
    public class MessageId : ElementId
    {
        public long message_id
        {
            get => this.id;
            set => this.id = value;
        }
    }
}
