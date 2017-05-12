using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Monitoring
{
    public class SubscriptionInfo
    {
        public int id { get; set; }
        public string email { get; set; }
    }

    public enum SubcriptionType
    {
        Egrul=1,
        Messages=3
    }
}