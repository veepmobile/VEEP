using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace RestService.Models
{
    public class Order
    {
        public int ID { get; set; }            // ID заказа
        [DataMember]
        public string OrderNumber { get; set; }            // Номер заказа в ИМ

        public string OrderNumberService { get; set; }            // Номер заказа в сервисе
        [DataMember]
        public int RestaurantID { get; set; }          // ID ресторана
        [DataMember]
        public string TableID { get; set; }          // ID стола
        [DataMember]
        public Waiter Waiter { get; set; }                 // Официант
        [DataMember]
        public List<OrderItem> OrderItems { get; set; }         // Позиции заказа
        [DataMember]
        public DiscountCard DiscountCard { get; set; }      // Дисконтная карта
        [DataMember]
        public OrderStatus OrderStatus { get; set; }       // Статус заказа
        [DataMember]
        public OrderPayment OrderPayment { get; set; }          // Стоимость заказа к оплате с учетом скидок и чаевых
        [DataMember]
        public string Message { get; set; }                 // Сообщение
        [DataMember]
        public int ErrorCode { get; set; }                 // Код ошибки
        [DataMember]
        public string Error { get; set; }                 // Текст ошибки
        [DataMember]
        public string FormUrl { get; set; }               // Url на который переводится приложение при оплате

        public string RestaurantName { get; set; }          // Наименование ресторана
        public string UserKey { get; set; }
        public string PhoneCode { get; set; }         //Код страны
        public string PhoneNumber { get; set; }         //Номер телефона
        public string FIO { get; set; }                 // ФИО клиента
        public DateTime CreateDate { get; set; }                 // Дата создания аккаунта
        public string OrderNumberBank { get; set; }                 // Номер заказа в сбербанке
        public DateTime OrderDate { get; set; }                 // Дата заказа
        public string ItemsHtml { get; set; }               // Состав заказа в Html
        public decimal TippingProcent { get; set; }         //Чаевые в процентах
        public decimal TippingSum { get; set; }             //Чаевые в рублях
    }

    public class OrderItem
    {
        [DataMember]
        public string Name { get; set; }                 // Наименование
        [DataMember]
        public decimal Price { get; set; }              //Прайсовая цена
        [DataMember]
        public decimal Qty { get; set; }                        // Количество
    }


    public class OrderStatus
    { 
        [DataMember]
        public int StatusID  { get; set; }		        // ID статуса: 0 - Открыт, 1 – Пречек, 2 - Оплачен, стол не закрыт, 3 - Оплачен, стол закрыт
        [DataMember]
        public DateTime StatusDate { get; set; }	    // Дата/время назначения статуса
        public string StatusName { get; set; }                 // Наименование статуса
    }

    public class OrderPayment
    {
        [DataMember]
        public decimal OrderTotal { get; set; }	        // Суммарная стоимость заказа без учета чаевых
        [DataMember]
        public decimal OrderSum { get; set; }	        // Стоимость заказа к оплате (OrderTotal минус DiscountSum)
        [DataMember]
        public decimal DiscountSum { get; set; }	    // Скидка
        [DataMember]
        public Int64 OrderBank { get; set; }	        // Стоимость заказа к оплате в банк (OrderSum плюс чаевые), в копейках
        [DataMember]
        public decimal TippingProcent { get; set; }     //Чаевые в процентах
        [DataMember]
        public decimal TippingSum { get; set; }     //Чаевые в рублях
    }

    public class RegistrOrder
    {
        public string orderId { get; set; }	            //Номер заказа в платежной системе
        public string formUrl { get; set; }	            //URL платежной формы
        public int errorCode { get; set; }	            //Код ошибки
        public string errorMessage { get; set; }	    //Описание ошибки
    }

    public class MessageData
    {
        public string orderID { get; set; }	            //Номер заказа в платежной системе
        public string restaurantID { get; set; }	    //ID ресторана
        public string restaurantName { get; set; }	    //Наименование ресторана
        public string pan { get; set; }	                //PAN банковской карты
        public string orderItems { get; set; }	        //Состав заказа в Html
        public string tippingProcent { get; set; }	    //Чаевые в процентах
        public decimal orderTotal { get; set; }	        //Сумма заказа без учета скидки
        public decimal discountSum { get; set; }	        //Скидка
        public decimal orderSum { get; set; }	        //Сумма заказа к оплате с учетом скидки (для ресторана)
        public decimal orderBank { get; set; }          //Стоимость заказа к оплате в банк в руб.
        public decimal tipping { get; set; }            //Сумма чаевых
        public string orderDate { get; set; }         //Дата заказа
        public string orderTime { get; set; }         //Время заказа
        public int clientID { get; set; }            //ID клиента
        public string clientEmail { get; set; }         //Email клиента
    }   
}

