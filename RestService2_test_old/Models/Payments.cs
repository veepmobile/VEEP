using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace RestService.Models
{
    public class Payments
    {
        public int ID { get; set; }                     // ID платежа
        public int ClientID { get; set; }                     // ID клиента
        public DateTime PaymentDate { get; set; }                 // Дата заказа
        public int RestaurantID { get; set; }          // ID ресторана
        public string RestaurantName { get; set; }          // Наименование ресторана
        public string PhoneNumber { get; set; }         //Номер телефона
        public string FIO { get; set; }                 // ФИО клиента
        public string OrderNumberModule { get; set; }                 // Номер заказа в ИМ
        public string OrderNumber { get; set; }                 // Номер заказа в данном сервисе
        public string OrderNumberBank { get; set; }                 // Номер заказа в сбербанке
        public decimal OrderSumBank { get; set; }	        // Стоимость заказа к оплате в банке
        public int ErrorCode { get; set; }          // Код ошибки при платеже
        public string ErrorMessage { get; set; }         //Текст ошибки при платеже
        public int? OrderStatus { get; set; }         //Статус заказа (2 - заказ оплачен, null - платеж не прошел)
        public DateTime RegDate { get; set; }                 // Дата регистрации платежа в платежном шлюзе
        public string Pan { get; set; }         //Маскированный номер карты
        public string Expiration { get; set; }         //Срок действия карты
        public string CardHolderName { get; set; }         //Держатель карты
        public string BindingID { get; set; }         //ID связки
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
}