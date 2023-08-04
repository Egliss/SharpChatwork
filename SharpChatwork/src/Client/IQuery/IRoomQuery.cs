using SharpChatwork.Query.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
    public interface IRoomQuery
    {
        public ValueTask<IEnumerable<Room>> GetAllAsync(CancellationToken cancellation = default);
        public ValueTask<ElementId> CreateAsync(CancellationToken cancellation = default);
        public ValueTask<Room> GetAsync(long roomId, CancellationToken cancellation = default);
        public ValueTask<ElementId> UpdateAsync(long roomId, string roomName, string description, RoomIconPreset preset, CancellationToken cancellation = default);
        public ValueTask LeaveAsync(long roomId, CancellationToken cancellation = default);
        public ValueTask DeleteAsync(long roomId, CancellationToken cancellation = default);

        public IRoomMessageQuery message { get; }
        public IRoomMemberQuery member { get; }
        public IRoomInviteQuery invite { get; }
        public IRoomFileQuery file { get; }
        public IRoomTaskQuery task { get; }
    }
}
