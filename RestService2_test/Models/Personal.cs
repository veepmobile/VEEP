using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace RestService.Models
{

    public class Personal
    {
        [DataMember]
        public int ID { get; set; }                         // ID пользователя
        [DataMember]
        public int RestaurantID { get; set; }               // ID ресторана
        [DataMember]
        public int RolesID { get; set; }                    // ID роли: 1 - Администратор, 2 - Официант
        [DataMember]
        public string PhoneNumber { get; set; }             // Номер телефона для роли Администратор
        [DataMember]
        public string Login { get; set; }             // Логин Администратора
        [DataMember]
        public string Psw { get; set; }             // Пароль Администратора
    }

    public class Profile
    {
        [DataMember]
        public int RestaurantID { get; set; }               // ID ресторана
        [DataMember]
        public int AdminID { get; set; }                    // ID администратора
        [DataMember]
        public string AdminLogin { get; set; }             // Логин администратора
        [DataMember]
        public string AdminPsw { get; set; }             // Пароль администратора
        [DataMember]
        public string PhoneNumber { get; set; }             // Номер телефона администратора
        [DataMember]
        public int OfficiantID { get; set; }                    // ID официанта
        [DataMember]
        public string OfficiantLogin { get; set; }             // Логин официанта
        [DataMember]
        public string OfficiantPsw { get; set; }             // Пароль официанта
    }

    public class Messages
    {
        [DataMember]
        public int ID { get; set; }               // ID сообщения   
        [DataMember]
        public int RestaurantID { get; set; }               // ID ресторана        
        public string RestaurantName { get; set; }          // Наименование ресторана
        [DataMember]
        public string TableID { get; set; }               // ID стола
        [DataMember]
        public int MessageType { get; set; }               // Тип сообщения   
        public int MessageGroup { get; set; }                 // Группа сообщений (1 - основные, 2 - информационные) 
        [DataMember]
        public string MessageText { get; set; }               // Текст сообщения
        [DataMember]
        public int ErrorCode { get; set; }               // Код ошибки    
        [DataMember]
        public string ErrorText { get; set; }               // Текст ошибки
        [DataMember]
        public DateTime MessageDate { get; set; }               // Дата/время сообщения
        [DataMember]
        public int IsRead { get; set; }               // 0 - сообщение не прочитано, 1 - сообщение прочитано
        [DataMember]
        public int MessageReader { get; set; }               // PersonalID прочитавшего сообщение
        public string ReaderLogin { get; set; }                 // Логин прочитавшего сообщение
    }

    public class HistoryPayments
    {
        [DataMember]
        public int ID { get; set; }               // ID платежа   
        [DataMember]
        public int RestaurantID { get; set; }               // ID ресторана        
        [DataMember]
        public string TableID { get; set; }               // ID стола
        [DataMember]
        public string OrderNumber { get; set; }               // Номер заказа в кассовом ПО - order_module
        [DataMember]
        public decimal OrderTotal { get; set; }	        // Суммарная стоимость заказа без учета чаевых
        [DataMember]
        public decimal DiscountSum { get; set; }	    // Скидка
        [DataMember]
        public decimal OrderSum { get; set; }	        // Стоимость заказа к оплате (OrderTotal минус DiscountSum)
        [DataMember]
        public decimal TippingSum { get; set; }	        //Сумма чаевых
        [DataMember]
        public int WaiterID { get; set; }	            //ID официанта
        [DataMember]
        public string WaiterName { get; set; }	        //ФИО официанта
        [DataMember]
        public DateTime PaymentDate { get; set; }	    //Дата платежа

    }
}