using SkrinService.Domain.AddressSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss
{
    public class BadAddressResult
    {
        public string BadAddress { get; set; }
        public Result SearchResult { get; set; }
        public QueueStatus QStatus { get; set; }
    }
}