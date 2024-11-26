#pragma warning disable CA1707 // Underscore

namespace SharpChatwork.Query.Types;

public class FileId : ElementId
{
    public string file_id
    {
        get => this.id;
        set => this.id = value;
    }
}
