using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace RestService.Models
{

    public class Account
    {
        [DataMember]
        public int ID { get; set; }                         // ID пользователя
        [DataMember]
        public string FirstName { get; set; }               // Имя
        [DataMember]
        public string LastName { get; set; }                // Фамилия
        [DataMember]
        public string PhoneModel { get; set; }              // Модель телефона
        [DataMember]
        public string PhoneCode { get; set; }             // Код страны
        [DataMember]
        public string PhoneNumber { get; set; }             // Номер телефона
        [DataMember]
        public string PhoneUID { get; set; }                // Уникальный идентификатор телефона
        [DataMember]
        public PhoneOS PhoneOS { get; set; }                // Платформа клиентского Приложения
        [DataMember]
        public int IsValid { get; set; }                   // Номер телефона валиден
        [DataMember]
        public string SMScode { get; set; }                    // Код отправляемый по SMS
        [DataMember]
        public string Pswd { get; set; }                    // хэш пароля
        [DataMember]
        public string Email { get; set; }                   // Email
        [DataMember]
        public AccountStatus AccountStatus { get; set; }    // Статус пользователя
        [DataMember]
        public DateTime CreateDate { get; set; }            // Дата создания аккаунта

        public DateTime UpdateDate { get; set; }            // Дата последнего изменения аккаунта

        public DateTime LastDate { get; set; }              // Дата последнего использования
        [DataMember]
        public AccountBlock AccountBlock { get; set; }      // Причина блокировки аккаунта
        [DataMember]
        public string Message { get; set; }                 // Сообщения / ошибки сервиса
        [DataMember]
        public string Cards { get; set; }                 // Список привязанных карт
        [DataMember]
        public string Comment { get; set; }                 // Комментарий
    }

    public class PhoneOS
    {
        [DataMember]
        public int ID { get; set; }         // ID
        [DataMember]
        public string Name { get; set; }    // Наименование
    }

    public class AccountStatus
    {
        [DataMember]
        public int ID { get; set; }         // ID
        [DataMember]
        public string Name { get; set; }    // Наименование
    }

    public class AccountBlock
    {
        [DataMember]
        public int ID { get; set; }         // ID
        [DataMember]
        public string Name { get; set; }    // Наименование
    }

}