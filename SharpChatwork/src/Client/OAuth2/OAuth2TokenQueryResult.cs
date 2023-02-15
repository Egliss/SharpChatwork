namespace SharpChatwork.OAuth2
{
    public class OAuth2TokenQueryResult
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
        public string scope { get; set; }
        // TODO ScopeType FromString
        // public ScopeType scope_type => 

        public string error { get; set; }
        public string error_description { get; set; }
        public string error_uri { get; set; }

        public bool isError => !string.IsNullOrEmpty(this.error);
    }
}
