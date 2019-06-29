using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Query
{
    public class OAuth2TokenQuery
    {
        public enum GrantType
        {
            [EnumAlias("authorization_code")]
            AuthroizationCode,
            [EnumAlias("refresh_token")]
            RefreshToken,
        }

        public string grant_type { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
        public string redirect_uri { get; set; } = string.Empty;
        public string code_verifier { get; set; } = string.Empty;
        public string refresh_token { get; set; } = string.Empty;
        public string scope { get; set; } = string.Empty;

        private ScopeType _scopeType = 0L;
        public ScopeType scopeType
        {
            get => _scopeType;
            set { _scopeType = value; scope = _scopeType.ToURLArg(); }
        }

        public OAuth2TokenQuery(GrantType type)
        {
            this.grant_type = type.ToAliasOrDefault();
        }
    }
}
