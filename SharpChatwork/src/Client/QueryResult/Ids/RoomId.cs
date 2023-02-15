namespace SharpChatwork.Query.Types
{
    public class RoomId : ElementId
    {
        public long room_id
        {
            get => this.id;
            set => this.id = value;
        }
    }
}
