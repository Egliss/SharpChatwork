using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Query;

public interface IRoomQuery
{

    public IRoomMessageQuery message { get; }
    public IRoomMemberQuery member { get; }
    public IRoomInviteQuery invite { get; }
    public IRoomFileQuery file { get; }
    public IRoomTaskQuery task { get; }
    public ValueTask<IEnumerable<Room>> GetAllAsync(CancellationToken cancellation = default);
    public ValueTask<RoomId> CreateAsync(
        string name,
        IEnumerable<long> adminMemberIds,
        string description = null,
        IEnumerable<long> normalMemberIds = null,
        IEnumerable<long> readonlyMemberIds = null,
        RoomIconPreset? iconPreset = null,
        CancellationToken cancellation = default);
    public ValueTask<Room> GetAsync(long roomId, CancellationToken cancellation = default);
    public ValueTask<RoomId> UpdateAsync(long roomId, string roomName, string description, RoomIconPreset preset, CancellationToken cancellation = default);
    public ValueTask LeaveAsync(long roomId, CancellationToken cancellation = default);
    public ValueTask DeleteAsync(long roomId, CancellationToken cancellation = default);
}
