using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Monitoring
{
    public class MessageMonitorGroup
    {
        public int id { get; set; }
        public int group_id { get; set; }
        public string email { get; set; }
        public string message_types { get; set; }
    }
}