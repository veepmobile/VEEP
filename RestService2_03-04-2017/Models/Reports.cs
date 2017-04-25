using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace RestService.Models
{
    public class ReportAccountNew
    {
        public Account AccountReport { get; set; }
        public List<Order> AccountOrders { get; set; }
    }


}