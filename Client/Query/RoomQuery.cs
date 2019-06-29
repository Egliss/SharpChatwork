using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Client.Query
{
	internal class RoomQuery : ClientQuery, IRoomQuery
	{
		public RoomQuery(IChatworkClient client) : base(client)
		{
			this.message = new RoomMessageQuery(client);
			this.member= new RoomMemberQuery(client);
			this.invite = new RoomInviteQuery(client);
			this.file = new RoomFileQuery(client);
			this.task = new RoomTaskQuery(client);
		}

		public IRoomMessageQuery message { get; private set; }
		public IRoomMemberQuery member { get; private set; }
		public IRoomInviteQuery invite { get; private set; }
		public IRoomFileQuery file { get; private set; }
		public IRoomTaskQuery task { get; private set; }

		public ValueTask<ElementId> CreateAsync()
		{
			throw new NotImplementedException();
		}

		public ValueTask DeleteAsync(long roomId)
		{
			throw new NotImplementedException();
		}

		public ValueTask<List<Room>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public ValueTask<Room> GetAsync(long roomId)
		{
			throw new NotImplementedException();
		}

		public ValueTask LeaveAsync(long roomId)
		{
			throw new NotImplementedException();
		}

		public ValueTask<ElementId> UpdateAsync(long roomId, string roomName, string description, RoomIconPreset preset)
		{
			throw new NotImplementedException();
		}
	}
}
