using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace RestService.Models
{
    public class Filters
    {
        public DateTime FilterBeginDate { get; set; }
        public DateTime FilterEndDate { get; set; }
        public int FilterRestNetworkID { get; set; }
        public int FilterRestaurantID { get; set; }
    }

    public class LogFilters
    {
        public DateTime FilterBeginDate { get; set; }
        public DateTime FilterEndDate { get; set; }
        public int FilterRestaurantID { get; set; }
        public int FilterUserID { get; set; }
        public int FilterPhoneNumber { get; set; }
        public int FilterMethod { get; set; }
    }

}