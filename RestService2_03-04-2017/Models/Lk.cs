using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace RestService.Models
{
    public class Prize
    {
        public string PhoneNumber { get; set; }      //Номер телефона
        public DateTime PaymentDate { get; set; }       // Дата платежа
        public int RestaurantID { get; set; }           // ID ресторана
        public string RestaurantName { get; set; }      //Наименование ресторана
        public string TableID { get; set; }             // ID стола
        public int WaiterID { get; set; }               // ID официанта
        public string WaiterName { get; set; }          // ФИО официанта
    }
}