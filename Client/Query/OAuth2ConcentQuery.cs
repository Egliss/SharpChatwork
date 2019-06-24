using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Client.Query
{
    public class OAuth2ConcentQuery
    {
        public string response_type => "code";
        public string client_id { get; set; } = string.Empty;
        public string redirect_uri { get; set; } = string.Empty;
        public string scope { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string code_challenge { get; set; } = string.Empty;
        public string code_challenge_method { get; set; } = string.Empty;

        public ScopeType scopeType { set => scope = value.ToURLArg(); }
    }
}
