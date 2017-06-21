using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace RestService.Models
{
    public class getBindingsResponse
    {
        public int errorCode { get; set; }              //Код завершения
        public string errorMessage { get; set; }        //Сообщение об ошибке
        public List<Binding> bindings { get; set; }      //Связки
    }

    public class messageResponse
    {
        public int errorCode { get; set; }              //Код завершения
        public string errorMessage { get; set; }        //Сообщение об ошибке
    }

    public class Binding
    {
        public string clientId { get; set; }            // AccountID
        public string bindingId { get; set; }           // ID связки
        public string cardName { get; set; }           // Название карты, данное пользователем
        public string maskedPan { get; set; }           // Маскированный номер карты
        public string expiryDate { get; set; }          // Срок действия карты
        public int active { get; set; }                 // карта активна/неактивна
    }

    public class registerResponse
    {
        public int errorCode { get; set; }              //Код завершения
        public string errorMessage { get; set; }        //Сообщение об ошибке
        public string orderId { get; set; }             //Номер заказа в платежной системе
        public string formUrl { get; set; }             //URL платежной формы, на который надо перенаправить броузер клиента.
    }

    public class orderStatusResponse
    {
        public int OrderStatus { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string OrderNumber { get; set; }
        public string Pan { get; set; }
        public string expiration { get; set; }
        public string cardholderName { get; set; }
        public long Amount { get; set; }
        public string Ip { get; set; }
        public string clientId { get; set; }
        public string bindingId { get; set; }
    }

    public class paymentOrderBindingResponse
    {
        public int errorCode { get; set; }              //Код завершения
        public string errorMessage { get; set; }        //Сообщение об ошибке
        public string info { get; set; }             //Информация о результате платежа
        public string redirect { get; set; }             //URL платежной формы, на который переадресуется клиент.
        public string acsUrl { get; set; }             //URL переходы для платежа по ASC
        public string pareq { get; set; }             //Параметры платежа
        public string termUrl { get; set; }             //URL переходы для завершения платежа по ASC
    }
}