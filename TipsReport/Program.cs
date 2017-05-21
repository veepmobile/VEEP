using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipsReport
{
    class Program
    {
        public class Reports
        {
            public int RestaurantID { get; set; }
            public string RestaurantEmail { get; set; }
            public List<Tip> Tips {get; set;}
        }

        public class Tip
        {
            public int ID { get; set; }                     // ID чаевых
            public string PhoneCode { get; set; }         //Код телефона
            public string PhoneNumber { get; set; }         //Номер телефона
            public int RestaurantID { get; set; }          // ID ресторана
            public string RestaurantName { get; set; }          //Наименование ресторана
            public string TableID { get; set; }          // ID стола
            public string OrderNumber { get; set; }                 // Номер заказа в ИМ
            public string OrderSum { get; set; }                 // Сумма заказа
            public decimal TippingProcent { get; set; }                 // Чаевые в процентах
            public decimal TippingSum { get; set; }                 // Чаевые в рублях
            public DateTime PaymentDate { get; set; }                 // Дата платежа
            public int WaiterID { get; set; }          // ID официанта
            public string WaiterName { get; set; }          // ФИО официанта


        }
        

        static void Main(string[] args)
        {
        } 
    }
}
