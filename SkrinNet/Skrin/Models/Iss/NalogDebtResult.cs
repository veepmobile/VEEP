using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkrinService.Domain.AddressSearch;
using System.Text;

namespace Skrin.Models.Iss
{
    public class NalogDebtResult
    {
        public string NalogDebt { get; set; }
        public Result SearchResult { get; set; }
        public QueueStatus QStatus { get; set; }
    }
}