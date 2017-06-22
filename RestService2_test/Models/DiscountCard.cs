using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace RestService.Models
{
    public class DiscountProgram
    {
        [DataMember]
        public int ID { get; set; }                         // ID программы в системе
        [DataMember]
        public string Name { get; set; }                    // Наименование программы лояльности
        [DataMember]
        public string Description { get; set; }             // Описание программы лояльности
        [DataMember]
        public string Logo { get; set; }                    // Логотип программы лояльности
        [DataMember]
        public decimal DisacountValue { get; set; }        // Размер скидки по программе
        [DataMember]
        public int RestNetworkID { get; set; }              // ID сети ресторанов
        [DataMember]
        public int RestaurantID { get; set; }              // ID ресторана
        [DataMember]
        public int ProgramStatus { get; set; }              // Статус программы (активна/неактивна)
    }

    public class DiscountCard
    {
        [DataMember]
        public int ID { get; set; }                         // ID карты в системе
        [DataMember]
        public int ProgramID { get; set; }                 // ID программы
        [DataMember]
        public int RestaurantID { get; set; }                 // ID ресторана
        [DataMember]
        public Int64? CardNumber { get; set; }                // Номер карты
        [DataMember]
        public string CardName { get; set; }                // Наименование карты
        [DataMember]
        public Account Account { get; set; }                // Аккаунт пользователя
        [DataMember]
        public DateTime InsertDate { get; set; }            // Дата привязки карты
        [DataMember]
        public DateTime LastDate { get; set; }            // Дата последней проверки карты
        [DataMember]
        public int CardStatus { get; set; }              // Статус карты (активна/заблокирована/удалена)
    }


}