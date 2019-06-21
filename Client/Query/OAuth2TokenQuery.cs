using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Client.Query
{
    public class OAuth2TokenQuery : IQueryScope
    {
        public enum GrantType
        {
            [EnumAlias("authorization_code")]
            AuthroizationCode,
            [EnumAlias("reflesh_token")]
            RefleshToken,
        }

        public string grant_type { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
        public string redirect_uri { get; set; } = string.Empty;
        public string code_verifier { get; set; } = string.Empty;
        public string refresh_token { get; set; } = string.Empty;
        public string scope { get; set; } = string.Empty;
        // TODO implement
        public ScopeType scopeType { set => this.scope = string.Empty; }

        public OAuth2TokenQuery(GrantType type)
        {
            this.grant_type = type.ToAliasOrDefault();

        }
    }
}
