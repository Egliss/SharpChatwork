using System.Text.Json.Serialization;

namespace SharpChatwork
{
    internal sealed class ChatworkQueryError
    {
        public int statusCode { get; set; }
        public string jsonBody { get; set; }

        /// <summary>
        /// 一定期間におけるAPIの最大利用回数
        /// </summary>
        [JsonPropertyName("x-ratelimit-limit")]
        public int rateLimitLimit { get; set; }
        /// <summary>
        /// 一定期間におけるAPIの残り利用回数
        /// </summary>
        [JsonPropertyName("x-ratelimit-remaining")]
        public int rateLimitRemaining { get; set; }
        /// <summary>
        /// API利用回数制限が次にリセットされる時間。
        /// Unix時間（秒）で示されます。
        /// </summary>
        [JsonPropertyName("x-ratelimit-reset")]
        public int rateLimitReset { get; set; }
    }
}
