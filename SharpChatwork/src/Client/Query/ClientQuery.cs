namespace SharpChatwork.Query;

internal class ClientQuery(IChatworkClient client)
{
    internal ChatworkClient chatworkClient { get; private set; } = client as ChatworkClient;
}
