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

    public class ReportMain
    {
        public string PhoneCode { get; set; }
        public string PhoneNumber { get; set; }
        public int IsValid { get; set; }
        public int OS { get; set; }
        public string PhoneModel { get; set; }
        public string BankCards { get; set; }
        public string DiscountCards { get; set; }
        public DateTime OrderDate { get; set; }
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }
        public string TableID { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal DiscountSum { get; set; }
        public decimal OrderSum { get; set; }
        public decimal TippingProcent { get; set; }
        public decimal TippingSum { get; set; }
        public string Waiter { get; set; }
        public string PaymentResult { get; set; }
        public string CardMaskPan { get; set; }
        public string CardExpiration { get; set; }
        public string CardHolderName { get; set; }
        public DateTime AccountCreateDate { get; set; }
        public DateTime AccountUpdateDate { get; set; }
        public DateTime AccountLastDate { get; set; }
    }

    public class ReportLog
    {


    }
}