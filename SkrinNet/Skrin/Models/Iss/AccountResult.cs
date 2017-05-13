using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Skrin.Models.Iss
{
    public class AccountResult
    {
        public int IsFinded { get; set; }
        public QueueStatus QStatus { get; set; }
        public string SearchResult { get; set; }
    }
}
