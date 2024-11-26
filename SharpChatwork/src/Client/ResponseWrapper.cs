using System.Collections.Generic;

namespace SharpChatwork;

public class ResponseWrapper
{
    public int statusCode { get; set; }
    public string content { get; set; }
    public Dictionary<string, IEnumerable<string>> headers { get; set; }
}
