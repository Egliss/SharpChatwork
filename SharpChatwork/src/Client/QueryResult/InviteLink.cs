#pragma warning disable CA1707 // Underscore

namespace SharpChatwork.Query.Types
{
    public class InviteLink
    {
        public bool @public { get; set; }
        public string url { get; set; }
        public bool need_acceptance { get; set; }
        public string description { get; set; }
    }
}
