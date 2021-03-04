using System;
using System.Collections.Generic;
using System.Text;

namespace SharpChatwork.Query.Types
{
    public class FileId : ElementId
    {
        public long File_id
        {
            get => this.id;
            set => this.id = value;
        }
    }
}
