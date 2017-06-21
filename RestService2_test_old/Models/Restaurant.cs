using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace RestService.Models
{
    public class RestNetwork
    {
        [DataMember]
        public int ID { get; set; }                                 // ID сети ресторанов

        [DataMember]
        public string Name { get; set; }                            // Название сети ресторанов

        [DataMember]
        public string Logo { get; set; }                            // Логотип

        [DataMember]
        public string WWW { get; set; }                             // Cайт

        [DataMember]
        public string Notes { get; set; }                     // Описание

        [DataMember]
        public string Image { get; set; }                             // Рисунок в описании

        //[DataMember]
        //public RestNetworkStatus RestNetworkStatus { get; set; }   // Статус сети ресторанов

        [DataMember]
        public List<Restaurant> Restaurants { get; set; }          // Список ресторанов в сети

    }

    public class RestNetworkStatus
    {
        [DataMember]
        public int ID { get; set; }         // ID

        [DataMember]
        public string Name { get; set; }    // Наименование
    }

    public class Restaurant
    {
        [DataMember]
        public int ID { get; set; }                             // ID ресторана

        [DataMember]
        public string Name { get; set; }                        // Название ресторана

        [DataMember]
        public string Logo { get; set; }                        // Логотип

        [DataMember]
        public string WorkTime { get; set; }                    // Время работы

        [DataMember]
        public string Address { get; set; }                     // Адрес

        [DataMember]
        public string Phone { get; set; }                       // Телефон

        [DataMember]
        public string WWW { get; set; }                         // Cайт

        [DataMember]
        public string Geocode { get; set; }                    // Координаты (E134.854,S25.828) для геолокации

        [DataMember]
        public string Notes { get; set; }                 // Описание ресторана

        [DataMember]
        public string Image { get; set; }                         // Рисунок в описании ресторана

        //[DataMember]
        //public RestaurantStatus RestaurantStatus { get; set; }  // Статус ресторана

        //[DataMember]
        //public int Priority { get; set; }                       // Приоритет ресторана над сетью

        [DataMember]
        public bool Call { get; set; }                      // Возможность вызова официанта: true – да, false - нет

        [DataMember]
        public int Tipping { get; set; }                  // Возможность учета чаевых: 0 - нет, 1 - да

        [DataMember]
        public decimal TippingMin { get; set; }                  // Минимальный размер чаевых

        [DataMember]
        public decimal TippingMax { get; set; }                  // Максимальный размер чаевых

        [DataMember]
        public int Rating { get; set; }                  // Рейтинг ресторана

        [DataMember]
        public int IsPay { get; set; }                  // Возможность закрытия стола online: 0 - нет, 1 - да

        //[DataMember]
        //public string TipDescription { get; set; }              // Сообщение о чаевых

        //[DataMember]
        //public List<Waiter> Waiters { get; set; }               // Список официантов  

       // [DataMember]
        //public List<Table> Tables { get; set; }                 // Список столов в ресторане  

        [DataMember]
        public int IsActive { get; set; }                     //светофор
    }

    public class RestaurantStatus
    {
        [DataMember]
        public int ID { get; set; }         // ID

        [DataMember]
        public string Name { get; set; }    // Наименование статуса ресторана
    }

    public class TipRules
    {
        [DataMember]
        public int ID { get; set; }         // ID

        [DataMember]
        public string Name { get; set; }    // Наименование правила учета чаевых

        [DataMember]
        public decimal TipValue { get; set; }    // Процент или сумма (?) чаевых
    }

    public class Waiter
    {
        [DataMember]
        public int ID { get; set; }         // ID

        [DataMember]
        public string Name { get; set; }     // ФИО официанта
    }
    /*
    public class Table
    {
        [DataMember]
        public int ID { get; set; }             // ID

        [DataMember]
        public int TableNumber { get; set; }    // Номер стола в ресторане
    }

    */


}