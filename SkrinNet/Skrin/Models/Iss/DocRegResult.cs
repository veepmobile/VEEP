using SkrinService.Domain.AddressSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss
{
    public class DocRegResult
    {
        public string Result { get; set; }
        public QueueStatus QStatus { get; set; }
        public string Ogrn { get; set; }
    }
}