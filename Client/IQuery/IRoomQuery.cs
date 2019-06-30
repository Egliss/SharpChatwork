﻿using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Client.Query
{
	public interface IRoomQuery
	{
		ValueTask<List<Room>> GetAllAsync();
		ValueTask<ElementId> CreateAsync();
		ValueTask<Room> GetAsync(long roomId);
		ValueTask<ElementId> UpdateAsync(long roomId, string roomName, string description, RoomIconPreset preset);
		ValueTask LeaveAsync(long roomId);
		ValueTask DeleteAsync(long roomId);

		IRoomMessageQuery message { get; }
		IRoomMemberQuery member { get; }
		IRoomInviteQuery invite { get; }
		IRoomFileQuery file { get; }
		IRoomTaskQuery task { get; }
	}
}