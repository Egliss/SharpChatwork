﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SharpChatwork.Query.Types;

namespace SharpChatwork.Client.Query
{
	internal class RoomMemberQuery : ClientQuery, IRoomMemberQuery
	{
		public RoomMemberQuery(IChatworkClient client) : base(client)
		{
		}

		public async ValueTask<List<User>> GetAllAsync(long roomId)
		{
            return await this.chatworkClient.QueryAsync<List<User>>(EndPoints.RoomMember(roomId), HttpMethod.Get, new Dictionary<string, string>());
        }

        public async ValueTask<RoomMember> UpdateAsync(long roomId, IEnumerable<long> adminsMembers, IEnumerable<long> normalMembers, IEnumerable<long> readonlyMembers)
		{
            throw new NotImplementedException();
            var data = new Dictionary<string, string>()
            {
                // TODO convert
                // {"members_admin_ids",adminsMembers},
                // {"members_member_ids",roomName },
                // {"members_readonly_ids",preset.ToAliasOrDefault() }
            };
            return await this.chatworkClient.QueryAsync<RoomMember>(EndPoints.RoomMember(roomId), HttpMethod.Get, data);
        }
    }
}
