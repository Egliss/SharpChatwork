using SharpChatwork.Query.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharpChatwork.Query
{
	public interface IRoomInviteQuery
	{
		ValueTask<InviteLink> GetAsync(long roomId);
		ValueTask<InviteLink> CreateAsync(long roomId, string uniqueName, string description, bool requireAcceptance);
		ValueTask<InviteLink> UpdateAsync(long roomId, string uniqueName, string description, bool requireAcceptance);
		ValueTask DestroyAsync(long roomId);
	}
}
