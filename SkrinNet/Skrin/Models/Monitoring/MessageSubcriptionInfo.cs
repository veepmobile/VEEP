using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Monitoring
{
    public class MessageSubcriptionInfo
    {
        public int i { get; set; }
        public int group_id { get; set; }
        public string email { get; set; }
        public List<int> mt { get; set; }
    }
}