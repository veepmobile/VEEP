using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Monitoring
{
    public class EgrulMonitorGroup
    {
        public int id { get; set; }
        public int group_id { get; set; }
        public string email { get; set; }
        public string egrul_types { get; set; }
        public string egrip_types { get; set; }
    }
}