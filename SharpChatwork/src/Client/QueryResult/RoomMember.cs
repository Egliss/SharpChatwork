#pragma warning disable CA1707 // Underscore

using System.Collections.Generic;

namespace SharpChatwork.Query.Types;

public class RoomMember
{
    public List<long> admin { get; set; } = [];
    public List<long> member { get; set; } = [];
    public List<long> @readonly { get; set; } = [];
}
