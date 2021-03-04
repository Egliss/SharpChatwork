namespace SharpChatwork.OAuth2
{
    public class OAuth2ConcentQueryResult
    {
        public string code { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string error { get; set; } = string.Empty;

        public bool isError => string.IsNullOrEmpty(this.error);
    }
}
