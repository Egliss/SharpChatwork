using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Query.Types
{
    public class IncomingRequest
    {
        public int request_id { get; set; }
        public int account_id { get; set; }
        public string message { get; set; }
        public string name { get; set; }
        public string chatwork_id { get; set; }
        public int organization_id { get; set; }
        public string organization_name { get; set; }
        public string department { get; set; }
        public string avatar_image_url { get; set; }
    }
}
