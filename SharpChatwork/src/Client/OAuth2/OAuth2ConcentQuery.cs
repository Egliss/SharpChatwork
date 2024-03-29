#pragma warning disable CA1707 // Underscore

namespace SharpChatwork.OAuth2
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

        /// <summary>
        /// auto set to <see cref="scope"/> from input bit field flag
        /// </summary>
        public ScopeType scopeType { set => this.scope = value.ToUrlArg(); }
    }
}
