using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Client.Query
{
    public class OAuth2ConcentQueryResult
    {
        public string code { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string error { get; set; } = string.Empty;

        public bool isError => string.IsNullOrEmpty(error);
    }
}
