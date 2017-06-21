using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace RestService.Models
{
    public class Users
    {
        public int UserID { get; set; }
        public int Roles { get; set; }
        public int RestaurantID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}