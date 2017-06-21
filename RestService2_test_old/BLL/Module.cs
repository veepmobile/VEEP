using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Text.RegularExpressions;
using RestService.BLL;
using RestService.Models;

namespace RestService.BLL
{
    //Класс связи с Интеграционным модулем
    public class Module
    {
        //Получение информации о заказах стола, привязанных к блюду
        public List<Order> FindOrders(int restaurantID, int techItem)
        {
            List<Order> list = new List<Order>();
            //ModuleClient client = new ModuleClient();
            //List<Order> list = client.GetOrdersList(restaurantID, techItem);

            //Тестовые данные:
            Order order = new Order
            {
                OrderNumber = "10",
                RestaurantID = 45,
                TableID = "A10",
                Waiter = new Waiter { ID = 6, Name = "Иванов Иван" },
                OrderItems = new List<OrderItem> { new OrderItem { Name = "Пиво", Price = 200, Qty = 4 }, new OrderItem { Name = "Водка", Price = 200, Qty = 4 } },
                OrderStatus = new OrderStatus { StatusID = 0, StatusDate = new DateTime(2015, 8, 25, 21, 35, 0) },
                OrderPayment = new OrderPayment { OrderTotal = 1600, DiscountSum = 0, OrderSum = 1600 },
                Message = "Welcome",
                Error = ""
            };
            if (order != null)
            {
                list.Add(order);
            }
            return list;
        }

        //Получение информации о заказе по его номеру
        public Order GetOrder(int restaurantID, string orderNumber)
        {
            //ModuleClient client = new ModuleClient();
            //Order order = client.GetOrder(restaurantID, orderNumber);

            //Тестовые данные:
            Order order = new Order
            {
                OrderNumber = "10",
                RestaurantID = 45,
                TableID = "A15",
                Waiter = new Waiter { ID = 6, Name = "Иванов Иван" },
                OrderItems = new List<OrderItem> { new OrderItem { Name = "Пиво", Price = 200, Qty = 4 }, new OrderItem { Name = "Водка", Price = 200, Qty = 4 } },
                OrderStatus = new OrderStatus { StatusID = 0, StatusDate = new DateTime(2015, 8, 25, 21, 35, 0) },
                OrderPayment = new OrderPayment { OrderTotal = 1600, DiscountSum = 0, OrderSum = 1600 },
                Message = "",
                Error = ""
            };
            if (order != null)
            {
                return order;
            }
            return null;
        }

        //Вызов официанта
        public int CallWaiter(int restaurantID, string tableID, string orderNumber, int code)
        {
            int result = 0;
            //ModuleClient client = new ModuleClient();
            //result = client.CallWaiter(restaurantID,tableID,orderNumber,code);
            result = 1;

            return result;
        }

        //Начало оплаты заказа - назначение статуса заказа precheck
        public List<Order> OrderPrecheck(int restaurantID, string orderNumber, Int64 discountCardNumber)
        {
            List<Order> list = new List<Order>();
            //ModuleClient client = new ModuleClient();
            //List<Order> list = client.OrderPrecheck(restaurantID, orderNumber, discountCardNumber);

            //Тестовые данные:
            Order order = new Order
            {
                OrderNumber = "10",
                RestaurantID = 45,
                TableID = "A10",
                Waiter = new Waiter { ID = 6, Name = "Иванов Иван" },
                OrderItems = new List<OrderItem> { new OrderItem { Name = "Пиво", Price = 200, Qty = 4 }, new OrderItem { Name = "Водка", Price = 200, Qty = 4 } },
                OrderStatus = new OrderStatus { StatusID = 1, StatusDate = new DateTime(2015, 8, 25, 21, 35, 0) },
                DiscountCard = new DiscountCard { CardNumber = 12345678, LastDate = new DateTime(2015, 8, 25, 21, 35, 0), CardStatus = 0 },
                OrderPayment = new OrderPayment { OrderTotal = 1600, DiscountSum = 0, OrderSum = 1600 },
                Message = "",
                Error = ""
            };
            if (order != null)
            {
                list.Add(order);
            }
            return list;
        }


        //Отмена начала оплаты заказа - назначение статуса заказа Открыт
      /*  public List<Order> OrderCancelPrecheck(int restaurantID, string orderNumber)
        {
            List<Order> list = new List<Order>();
            //ModuleClient client = new ModuleClient();
            //List<Order> list = client.OrderPrecheck(restaurantID, orderNumber, discountCardNumber);

            //Тестовые данные:
            Order order = new Order
            {
                OrderNumber = "10",
                RestaurantID = 45,
                TableID = "A10",
                Waiter = new Waiter { ID = 6, Name = "Иванов Иван" },
                OrderItems = new List<OrderItem> { new OrderItem { Name = "Пиво", Price = 200, Qty = 4 }, new OrderItem { Name = "Водка", Price = 200, Qty = 4 } },
                OrderStatus = new OrderStatus { StatusID = 0, StatusDate = new DateTime(2015, 8, 25, 21, 35, 0) },
                DiscountCard = new DiscountCard { CardNumber = 12345678, LastDate = new DateTime(2015, 8, 25, 21, 35, 0), CardStatus = 0 },
                OrderPayment = new OrderPayment { OrderTotal = 1600, DiscountSum = 0, OrderSum = 1600 },
                Message = "",
                Error = ""
            };
            if (order != null)
            {
                list.Add(order);
            }
            return list;
        }*/

        //Оплата и закрытие заказа
        public List<Order> GetPayment(int restaurantID, string orderNumber, decimal paymentSum, DateTime paymentDate)
        {
            List<Order> list = new List<Order>();
            //ModuleClient client = new ModuleClient();
            //List<Order> list = client.GetPayment(restaurantID, orderNumber, paymentSum, paymentDate);

            //Тестовые данные:
            Order order = new Order
            {
                OrderNumber = "10",
                RestaurantID = 45,
                TableID = "A10",
                Waiter = new Waiter { ID = 6, Name = "Иванов Иван" },
                OrderItems = new List<OrderItem> { new OrderItem { Name = "Пиво", Price = 200, Qty = 4 }, new OrderItem { Name = "Водка", Price = 200, Qty = 4 } },
                OrderStatus = new OrderStatus { StatusID = 2, StatusDate = new DateTime(2015, 8, 25, 21, 35, 0) },
                DiscountCard = new DiscountCard { CardNumber = 12345678, LastDate = new DateTime(2015, 8, 25, 21, 35, 0), CardStatus = 1 },
                OrderPayment = new OrderPayment { OrderTotal = 1600, DiscountSum = 100, OrderSum = 1500 },
                Message = "Заказ оплачен",
                Error = ""
            };
            if (order != null)
            {
                list.Add(order);
            }
            return list;
        }





    }





}