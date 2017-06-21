using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using RestService.Models;
using RestService.BLL;

namespace RestService
{
    public class OrderData
    {
        //Проверка наличия QR кода
        public static bool SqlCheckQR(string qr)
        {
            bool check = false;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.CheckQR", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@qr", SqlDbType.VarChar).Value = qr;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return check = true;
                    }
                }
            }
            catch
            {
                 return check;
            }
            return check;
        }

        //Добавление QRкода в БД
        public static void SqlSaveQR(string qr)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.InsertQR", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@qr", SqlDbType.VarChar).Value = qr;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SqlSaveQR", "qr=" + qr, "Внутренняя ошибка сервиса: " + e.Message, 1);
            }
        }

        //Запись заказа в БД
        public static void SqlInsertOrders(int restaurantID, string phoneNumber, string user_key, Order order, string phoneCode = "7")
        {
            string orders = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.InsertOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    cmd.Parameters.Add("@user_key", SqlDbType.VarChar).Value = user_key;
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@phoneCode", SqlDbType.VarChar).Value = phoneCode;
                    cmd.Parameters.Add("@orderNumber", SqlDbType.VarChar).Value = Helper.getNewGUID();
                    cmd.Parameters.Add("@orderModule", SqlDbType.VarChar).Value = order.OrderNumber;
                    cmd.Parameters.Add("@orderTotal", SqlDbType.Decimal).Value = order.OrderPayment.OrderTotal;
                    cmd.Parameters.Add("@discountSum", SqlDbType.Decimal).Value = order.OrderPayment.DiscountSum;
                    cmd.Parameters.Add("@orderSum", SqlDbType.Decimal).Value = order.OrderPayment.OrderSum;
                    cmd.Parameters.Add("@orderBank", SqlDbType.BigInt).Value = order.OrderPayment.OrderBank;
                    cmd.Parameters.Add("@orderStatus", SqlDbType.BigInt).Value = order.OrderStatus.StatusID;
                    cmd.Parameters.Add("@waiterID", SqlDbType.Int).Value = order.Waiter.ID;
                    cmd.Parameters.Add("@waiterName", SqlDbType.VarChar).Value = order.Waiter.Name;
                    cmd.Parameters.Add("@tableID", SqlDbType.VarChar).Value = order.TableID;
                    cmd.Parameters.Add("@veep_procent", SqlDbType.Decimal).Value = order.MainDiscountProc;
                    cmd.Parameters.Add("@veep_sum", SqlDbType.Decimal).Value = order.MainDiscountSum;

                    //конвертить list в xml
                    XMLGenerator<Order> orderXML = new XMLGenerator<Order>(order);
                    orders = orderXML.GetStringXML();
                    cmd.Parameters.Add("@orders", SqlDbType.VarChar).Value = orders;

                    //запись позиций заказа в Html
                    string items = GetOrderHtml(order);
                    cmd.Parameters.Add("@items", SqlDbType.VarChar).Value = items;

                    con.Open();
                    cmd.ExecuteNonQuery();


                    Helper.saveToLog(0, user_key, "SqlInsertOrder", "restaurantID=" + restaurantID.ToString() + ", phoneNumber=" + phoneNumber + ", order: " + orders, "Заказ записан в БД", 0);
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, user_key, "SqlInsertOrder", "restaurantID=" + restaurantID.ToString() + ", phoneNumber=" + phoneNumber + ", order: " + orders, "Внутренняя ошибка сервиса: " + e.Message, 1);
            }

        }

        //Поиск заказа в БД по order_module
        public static string SqlFindOrders(string phoneNumber, string user_key, string order_module, int restaurantID)
        {
            string ret = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.FindOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.VarChar).Value = restaurantID;
                    cmd.Parameters.Add("@orderModule", SqlDbType.VarChar).Value = order_module;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return ret = (string)reader["order_number"];
                    }

                    Helper.saveToLog(0, user_key, "SqlFindOrders", "phoneNumber=" + phoneNumber + ", order_module: " + order_module, "Поиск заказа в БД перед оплатой в банке", 0);
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, user_key, "SqlFindOrders", "phoneNumber=" + phoneNumber + ", order_module: " + order_module, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
            return null;
        }

        //Замена номера заказа на новый для повторного платежа
        public static int SqlChangeNumberOrder(string order_number, string new_number)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.ChangeNumberOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@order_number", SqlDbType.VarChar).Value = order_number;
                    cmd.Parameters.Add("@new_number", SqlDbType.VarChar).Value = new_number;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    result = 1;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SqlChangeNumberOrder", "order_number=" + order_number, "Внутренняя ошибка сервиса: " + e.Message, 1);
            }
            return result;
        }


        //Поиск заказа в БД по order_number
        public static Order SqlFindOrder(string order_number)
        {
            Order order = new Order();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.FindOrderToClose", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@order_number", SqlDbType.VarChar).Value = order_number;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        order.UserKey = (string)reader["user_key"];
                        order.PhoneCode = (string)reader["phoneCode"];
                        order.PhoneNumber = (string)reader["phoneNumber"];
                        order.RestaurantID = (int)reader["restaurantID"];
                        order.OrderNumber = (string)reader["order_module"];
                        order.TableID = (string)reader["tableID"];
                        order.ItemsHtml = (reader["order_items"]!=DBNull.Value)?(string)reader["order_items"]:"";
                        OrderPayment op = new OrderPayment();
                        op.OrderSum = (decimal)reader["order_rest_sum"];
                        op.OrderBank = (long)reader["order_bank_sum"];
                        order.OrderPayment = op;
                        order.TippingProcent = (reader["tipping_procent"]!=DBNull.Value)?(decimal)reader["tipping_procent"]:0;
                        order.TippingSum = (reader["tipping_sum"] != DBNull.Value) ? (decimal)reader["tipping_sum"] : 0;
                        order.MainDiscountProc = (reader["veep_procent"] != DBNull.Value) ? (decimal)reader["veep_procent"] : 0;
                        order.MainDiscountSum = (reader["veep_sum"] != DBNull.Value) ? (decimal)reader["veep_sum"] : 0;
                        Waiter waiter = new Waiter();
                        waiter.ID = (int)reader["waiterID"];
                        waiter.Name = (string)reader["waiterName"];
                        order.Waiter = waiter;
                        if (order != null)
                        {
                            return order;
                        }
                    }

                    Helper.saveToLog(0, "", "SqlFindOrder", "order_number: " + order_number, "Поиск заказа в БД по order_number", 0);
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SqlFindOrder", "order_number: " + order_number, "Поиск заказа в БД по order_number. Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
            return null;
        }

        //Запись платежа в БД
        public static string SqlInsertPayment(string user_key, int restaurantID, string phoneNumber, string orderModule, string orderNumber, long paymentBank, DateTime paymentDate, string orderId, string formUrl, int errorCode, string errorMessage, string clientId, string bindingId, decimal tippingProcent, string phoneCode = "7")
        {
            string result = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.InsertPayment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user_key", SqlDbType.VarChar).Value = user_key;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@phoneCode", SqlDbType.VarChar).Value = phoneCode;
                    cmd.Parameters.Add("@orderNumber", SqlDbType.VarChar).Value = orderNumber;
                    cmd.Parameters.Add("@orderModule", SqlDbType.VarChar).Value = orderModule;
                    cmd.Parameters.Add("@paymentBank", SqlDbType.BigInt).Value = paymentBank;
                    cmd.Parameters.Add("@paymentDate", SqlDbType.DateTime).Value = paymentDate;
                    cmd.Parameters.Add("@orderId", SqlDbType.VarChar).Value = orderId;
                    cmd.Parameters.Add("@formUrl", SqlDbType.VarChar).Value = formUrl;
                    cmd.Parameters.Add("@errorCode", SqlDbType.Int).Value = errorCode;
                    cmd.Parameters.Add("@errorMessage", SqlDbType.VarChar).Value = errorMessage;
                    cmd.Parameters.Add("@clientId", SqlDbType.VarChar).Value = clientId;
                    cmd.Parameters.Add("@bindingId", SqlDbType.VarChar).Value = bindingId;
                    cmd.Parameters.Add("@tippingProcent", SqlDbType.Decimal).Value = tippingProcent;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    result = "Успешно";
                    Helper.saveToLog(0, user_key, "SqlInsertPayment", "phoneNumber=" + phoneNumber + ", orderNumber=" + orderNumber + ", orderModule=" + orderModule + ", paymentBank=" + paymentBank.ToString() + ", paymentDate=" + paymentDate.ToString() + ", orderId=" + orderId + ", formUrl=" + formUrl + ", errorCode=" + errorCode.ToString() + ", errorMessage=" + errorMessage + ", clientId=" + clientId + ", bindingId=" + bindingId + ", tippingProcent = " + tippingProcent.ToString(), "Платеж записан в БД", 0);
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, user_key, "SqlInsertPayment", "phoneNumber=" + phoneNumber + ", orderNumber=" + orderNumber + ", orderModule=" + orderModule + ", paymentBank=" + paymentBank.ToString() + ", paymentDate=" + paymentDate.ToString() + ", orderId=" + orderId + ", formUrl=" + formUrl + ", errorCode=" + errorCode.ToString() + ", errorMessage=" + errorMessage, "Внутренняя ошибка сервиса: " + e.Message, 1);
            }

            return result;
        }

        //Запись чаевых в БД
        public static void SqlInsertTipping(int restaurantID, string orderNumber, decimal tippingProcent, string phoneNumber, string phoneCode = "7")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.InsertTipping", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    cmd.Parameters.Add("@orderNumber", SqlDbType.VarChar).Value = orderNumber;
                    cmd.Parameters.Add("@tippingProcent", SqlDbType.Decimal).Value = tippingProcent;
                    con.Open();
                    cmd.ExecuteNonQuery();

                    Helper.saveToLog(0, "", "SqlInsertTipping", "restaurantID=" + restaurantID.ToString() + ", phoneNumber=" + phoneNumber + ", phoneCode=" + phoneCode + ", orderNumber=" + orderNumber + ", tippingProcent=" + tippingProcent.ToString(), "Чаевые записаны в БД", 0);
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SqlInsertTipping", "restaurantID=" + restaurantID.ToString() + ", phoneNumber=" + phoneNumber + ", phoneCode=" + phoneCode + ", orderNumber=" + orderNumber + ", tippingProcent=" + tippingProcent.ToString(), "Внутренняя ошибка сервиса: " + e.Message, 1);
            }
        }

        //Поиск платежа в БД
        public static string SqlFindPayment(string phone_number, string order_system)
        {
            string ret = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.FindPayment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = phone_number;
                    cmd.Parameters.Add("@order_system", SqlDbType.VarChar).Value = order_system;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = (string)reader["orderId"];
                    }

                    Helper.saveToLog(0, "", "SqlFindPayment", "phone_number=" + phone_number + ", order_system: " + order_system, "Поиск заказа в БД перед оплатой в банке", 0);
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SqlFindPayment", "phone_number=" + phone_number + ", order_system: " + order_system, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
            return ret;
        }

        //Запись данных запроса статуса заказа в БД (табл. Payment)
        public static string SqlInsertStatus(string orderId, int orderStatus, int errorCode, string errorMessage, string orderNumber, string pan, string expiration, string cardholderName, Int64 amount, string ip, string clientId, string bindingId)
        {
            string result = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.InsertStatus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@order_number", SqlDbType.VarChar).Value = orderNumber;
                    cmd.Parameters.Add("@orderId", SqlDbType.VarChar).Value = orderId;
                    cmd.Parameters.Add("@orderStatus", SqlDbType.Int).Value = orderStatus;
                    cmd.Parameters.Add("@errorCode", SqlDbType.Int).Value = errorCode;
                    cmd.Parameters.Add("@errorMessage", SqlDbType.VarChar).Value = errorMessage;
                    cmd.Parameters.Add("@pan", SqlDbType.VarChar).Value = pan;
                    cmd.Parameters.Add("@expiration", SqlDbType.VarChar).Value = expiration;
                    cmd.Parameters.Add("@cardholderName", SqlDbType.VarChar).Value = cardholderName;
                    cmd.Parameters.Add("@amount", SqlDbType.BigInt).Value = amount;
                    cmd.Parameters.Add("@ip", SqlDbType.VarChar).Value = ip;
                    cmd.Parameters.Add("@reg_date", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@clientId", SqlDbType.VarChar).Value = clientId;
                    cmd.Parameters.Add("@bindingId", SqlDbType.VarChar).Value = bindingId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    result = "1";
                }
                Helper.saveToLog(0, "", "SqlInsertStatus", "orderNumber=" + orderNumber + ", orderId=" + orderId + ". Запись статуса заказа: orderStatus=" + orderStatus.ToString() + ", errorCode=" + errorCode.ToString() + ", errorMessage=" + errorMessage + ", pan=" + pan + ", expiration=" + expiration + ", cardholderName=" + cardholderName + ", amount=" + amount.ToString() + ", ip=" + ip + ", reg_date=" + DateTime.Now.ToString() + ", clientId=" + clientId + ", bindingId=" + bindingId, "", 0);
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SqlInsertStatus", "orderNumber=" + orderNumber + ", orderId=" + orderId + ". Запись статуса заказа: orderStatus=" + orderStatus.ToString() + ", errorCode=" + errorCode.ToString() + ", errorMessage=" + errorMessage + ", pan=" + pan + ", expiration=" + expiration + ", cardholderName=" + cardholderName + ", amount=" + amount.ToString() + ", ip=" + ip + ", reg_date=" + DateTime.Now.ToString() + ", clientId=" + clientId + ", bindingId=" + bindingId, "Внутренняя ошибка сервиса: " + e.Message, 1);
            }

            return result;
        }

        //Запись связки в БД (табл. Bindings)
        public static string SqlInsertBinding(string clientId, string bindingId, string pan, string expiration, int active)
        {
            string result = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.InsertBinding", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@clientId", SqlDbType.VarChar).Value = clientId;
                    cmd.Parameters.Add("@bindingId", SqlDbType.VarChar).Value = bindingId;  
                    cmd.Parameters.Add("@pan", SqlDbType.VarChar).Value = pan;
                    cmd.Parameters.Add("@expiration", SqlDbType.VarChar).Value = expiration;   
                    cmd.Parameters.Add("@active", SqlDbType.Int).Value = active;          
                    con.Open();
                    cmd.ExecuteNonQuery();
                    result = "Успешно";
                    Helper.saveToLog(0, "", "SqlInsertBinding", "clientId=" + clientId + ", bindingId=" + bindingId + ", pan=" + pan + ", expiration=" + expiration + ", active=" + active.ToString(), "Вставка связки в БД: " + result, 0);
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SqlInsertBinding", "clientId=" + clientId + ", bindingId=" + bindingId + ", pan=" + pan + ", expiration=" + expiration + ", active=" + active.ToString(), "Внутренняя ошибка сервиса: " + e.Message, 1);
            }
            return result;
        }

        //Получение списка связок
        public static List<Binding> SqlFindBindings(string clientId)
        {
            List<Binding> list = new List<Binding>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.FindBindings", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@clientId", SqlDbType.VarChar).Value = clientId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Models.Binding binding = new Models.Binding();
                        binding.clientId = clientId;
                        binding.bindingId = (reader["bindingId"]!=DBNull.Value)?(string)reader["bindingId"]:"";
                        binding.cardName = (reader["cardName"] != DBNull.Value) ? (string)reader["cardName"] : "Название карты";
                        binding.maskedPan = (reader["maskedPan"]!= DBNull.Value) ? (string)reader["maskedPan"]:"";
                        binding.expiryDate = (reader["expireDate"]!= DBNull.Value) ? (string)reader["expireDate"]:"";
                        binding.active = (reader["active"]!= DBNull.Value) ?(int)reader["active"]:1;
                        if (binding != null)
                        {
                            list.Add(binding);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SqlFindBindings", "clientId=" + clientId, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }

        //Удаление привязки карты
        public static string SqlUnbindCard(string bindingId)
        {
            string result = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.UnbindCard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@bindingId", SqlDbType.VarChar).Value = bindingId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    result = "Привязка удалена из БД успешно";
                    Helper.saveToLog(0, "", "SqlUnbindCard", "bindingId=" + bindingId, "Удаление связки в БД: " + result, 0);
                }
            }
            catch (Exception e)
            {
                result = "Ошибка при удалении привязки из БД";
                Helper.saveToLog(0, "", "SqlUnbindCard", "bindingId=" + bindingId, "Удаление связки в БД: " + result, 1);
            }
            return result;
        }

        //Изменение названия карты
        public static int SqlChangeCardName(int clientId, string bindingId, string cardName)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.ChangeCardName", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@bindingId", SqlDbType.VarChar).Value = bindingId;
                    cmd.Parameters.Add("@cardName", SqlDbType.VarChar).Value = cardName;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    result = 1;
                    Helper.saveToLog(clientId, "", "SqlChangeCardName", "clientId=" + clientId.ToString() + ", bindingId=" + bindingId + ", cardName=" + cardName, "Изменение названия банковсеой карты", 0);
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(clientId, "", "SqlChangeCardName", "clientId=" + clientId.ToString() + ", bindingId=" + bindingId + ", cardName=" + cardName, "Ошибка при изменении названия карты", 1);
            }
            return result;
        }

        //Оценка качества обслуживания в ресторане
        public static int SaveRating(int restaurantID, string orderNumber, int rating, string user_key)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.SaveRating", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    cmd.Parameters.Add("@orderNumber", SqlDbType.VarChar).Value = orderNumber;
                    cmd.Parameters.Add("@rating", SqlDbType.Int).Value = rating;
                    cmd.Parameters.Add("@user_key", SqlDbType.VarChar).Value = user_key;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Helper.saveToLog(0, "", "SaveRating", "restaurantID=" + restaurantID + ", orderNumber=" + orderNumber + ", rating=" + rating, "Рейтинг ресторана записан в БД", 0);
                    return 1;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SaveRating", "restaurantID=" + restaurantID + ", orderNumber=" + orderNumber + ", rating=" + rating, "Внутренняя ошибка сервиса: " + e.Message, 1);

                return 0;
            }
        }

        //Запись статуса заказа в БД
        public static string UpdateOrderStatus(string orderNumber, int orderStatus)
        {
            string ret = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.UpdateOrderStatus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@order_number", SqlDbType.VarChar).Value = orderNumber;
                    cmd.Parameters.Add("@order_status", SqlDbType.Int).Value = orderStatus;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Helper.saveToLog(0, "", "SqlUpdateStatus", "orderNumber=" + orderNumber + ", orderStatus=" + orderStatus, "Статус заказа обновлен в БД", 0);
                    return "Успешно";
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SqlUpdateStatus", "orderNumber=" + orderNumber + ", orderStatus=" + orderStatus, "Внутренняя ошибка сервиса: " + e.Message, 1);
            }
            return ret;
        }

        //Заказ в Html
        private static string GetOrderHtml(Order order)
        {
            StringBuilder sb = new StringBuilder("");
            if (order != null && order.OrderItems != null)
            {
                sb.Append("<table cellpadding=\"3\" cellspacing=\"0\" border=\"1\">");
                sb.Append("<tr><td>№</td><td>Наименование блюда</td><td>Цена</td><td>Количество</td><td>Сумма</td></tr>");
                int i = 1;
                foreach (var item in order.OrderItems)
                {
                    if (item.Name != "Online оплата")
                    {
                        sb.Append("<tr><td align=\"center\" valign=\"top\">" + i.ToString() + "</td><td align=\"left\" valign=\"top\">" + item.Name + "</td><td align=\"center\" valign=\"top\">" + item.Price.ToString("0.00") + "</td><td align=\"center\" valign=\"top\">" + item.Qty.ToString("0.00") + "</td><td align=\"center\" valign=\"top\">" + (item.Qty*item.Price).ToString("0.00") + "</td></tr>");
                        i++;
                    }
                }
                sb.Append("<tr><td align=\"left\" valign=\"top\" colspan=\"4\">Итого:</td><td align=\"center\" valign=\"top\">" + order.OrderPayment.OrderTotal.ToString("0.00") + "</td></tr>");
                sb.Append("</table>");
            }

            return sb.ToString();
        }

        //Данные о заказе для сообщения клиенту по email
        public static MessageData SqlMessageData(string orderId)
        {
            MessageData data = new MessageData();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetMessageData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@orderId", SqlDbType.VarChar).Value = orderId;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        data.orderID = orderId;
                        data.restaurantID = (reader["restaurantID"]!=DBNull.Value)?reader["restaurantID"].ToString():"";
                        if (reader["restaurant_name"] != DBNull.Value)
                        {
                            data.restaurantName = (!String.IsNullOrWhiteSpace((string)reader["restaurant_name"])) ? (string)reader["restaurant_name"] : "";
                        }
                        if (reader["pan"] != DBNull.Value)
                        {
                            data.pan = (!String.IsNullOrWhiteSpace((string)reader["pan"])) ? (string)reader["pan"] : "";
                        }
                        if (data.pan != "" && data.pan.Length >= 4)
                        {
                            int from = data.pan.Length - 4;
                            data.pan = data.pan.Substring(from, 4); //последние 4 символа
                        }
                        if (reader["order_items"] != DBNull.Value)
                        {
                            data.orderItems = (!String.IsNullOrWhiteSpace((string)reader["order_items"])) ? (string)reader["order_items"] : "";
                        }
                        if (reader["order_sum_bank"] != DBNull.Value)
                        {
                            data.orderBank = (reader["order_sum_bank"] != null) ? Convert.ToDecimal(((long)reader["order_sum_bank"]) / 100) : 0;
                        }
                        if (reader["order_rest_sum"] != DBNull.Value)
                        {
                            data.orderSum = (reader["order_rest_sum"] != null) ? (decimal)reader["order_rest_sum"] : 0;
                        }
                        if (reader["order_total"] != DBNull.Value)
                        {
                            data.orderTotal = (reader["order_total"] != null) ? (decimal)reader["order_total"] : 0;
                        }
                        if (reader["discount_sum"] != DBNull.Value)
                        {
                            data.discountSum = (reader["discount_sum"] != null) ? (decimal)reader["discount_sum"] : 0;
                        }
                       // if (data.orderSum != 0)
                       // {
                       //     data.tipping = data.orderBank - data.orderSum;
                       //     data.tippingProcent = Math.Round(data.tipping * 100 / data.orderSum).ToString();
                       // }
                        if (reader["payment_date"] != DBNull.Value)
                        {
                            data.orderDate = (reader["payment_date"] != null) ? ((DateTime)reader["payment_date"]).ToShortDateString() : "";
                            data.orderTime = (reader["payment_date"] != null) ? ((DateTime)reader["payment_date"]).ToShortTimeString() : "";
                        }
                        if (reader["clientId"] != DBNull.Value)
                        {
                            data.clientID = (!String.IsNullOrWhiteSpace((string)reader["clientId"])) ? Convert.ToInt32(reader["clientId"]) : 0;
                        }
                        if (reader["email"] != DBNull.Value)
                        {
                            data.clientEmail = (!String.IsNullOrWhiteSpace((string)reader["email"])) ? (string)reader["email"] : "";
                        }
                        if (data != null)
                        {
                            return data;
                        }
                    }
                    Helper.saveToLog(0, "", "SqlMessageData", "orderId: " + orderId, "Поиск данных заказа в БД для сообщение по email. Успешно.", 0);
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SqlMessageData", "orderId: " + orderId, "Поиск данных заказа в БД для сообщение по email. Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
            return null;
        }

        //Проверка возможности онлайн оплаты в ресторане
        public static bool SqlCheckRestaurantPayment(int RestaurantID)
        {
            int check = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.CheckRestPayment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = RestaurantID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["is_pay"] != DBNull.Value)
                        {
                            check = (reader["is_pay"] != DBNull.Value) ? Convert.ToInt32(reader["is_pay"]) : 0;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SqlCheckRestaurantPayment", "RestaurantID: " + RestaurantID, "Проверка возможности онлайн оплаты в ресторане. Внутренняя ошибка сервиса: " + e.Message, 1);
                return false;
            }

            if (check == 1)
            {
                return true;
            }

            return false;
        }

        //Список заказов (админка)
        public static List<Order> GetOrderList(DateTime dfrom, DateTime dto)
        {
            List<Order> list = new List<Order>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetOrderList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dfrom", SqlDbType.DateTime).Value = dfrom;
                    cmd.Parameters.Add("@dto", SqlDbType.DateTime).Value = dto;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.ID = (reader["id"] != DBNull.Value) ? Convert.ToInt32(reader["id"]) : 0;
                        order.RestaurantID = (reader["restaurantID"] != DBNull.Value) ? Convert.ToInt32(reader["restaurantID"]) : 0;
                        order.RestaurantName = (reader["name"] != DBNull.Value) ? Convert.ToString(reader["name"]) : "";
                        order.TableID = (reader["tableID"] != DBNull.Value) ? Convert.ToString(reader["tableID"]) : "";
                        order.PhoneNumber = (reader["phoneNumber"] != DBNull.Value) ? Convert.ToString(reader["phoneNumber"]) : "";
                        string first_name = (reader["first_name"] != DBNull.Value) ? Convert.ToString(reader["first_name"]) : "";
                        string last_name = (reader["last_name"] != DBNull.Value) ? Convert.ToString(reader["last_name"]) : "";
                        order.FIO = first_name + " " + last_name;
                        order.OrderNumber = (reader["order_module"] != DBNull.Value) ? Convert.ToString(reader["order_module"]) : "";
                        order.OrderNumberService = (reader["order_number"] != DBNull.Value) ? Convert.ToString(reader["order_number"]) : "";
                        order.OrderNumberBank = (reader["order_system"] != DBNull.Value) ? Convert.ToString(reader["order_system"]) : "";
                        order.OrderPayment = new OrderPayment { OrderTotal = (reader["order_total"] != DBNull.Value) ? Convert.ToDecimal(reader["order_total"]) : 0, DiscountSum = (reader["discount_sum"] != DBNull.Value) ? Convert.ToDecimal(reader["discount_sum"]) : 0, OrderSum = (reader["order_rest_sum"] != DBNull.Value) ? Convert.ToDecimal(reader["order_rest_sum"]) : 0 };
                        order.MainDiscountProc = (reader["veep_procent"] != DBNull.Value) ? Convert.ToDecimal(reader["veep_procent"]) : 0;
                        order.MainDiscountSum = (reader["veep_sum"] != DBNull.Value) ? Convert.ToDecimal(reader["veep_sum"]) : 0;
                        //order.OrderPayment.OrderSum = order.OrderPayment.OrderSum - order.MainDiscountSum;
                        if (reader["cdate"] != DBNull.Value)
                        {
                            order.OrderDate = (DateTime)reader["cdate"];
                        }
                        if (reader["create_date"] != DBNull.Value)
                        {
                            order.CreateDate = (DateTime)reader["create_date"];
                        }
                        order.OrderStatus = new OrderStatus { StatusID = (reader["order_status"] != DBNull.Value) ? Convert.ToInt32(reader["order_status"]) : 0, StatusName = (reader["status_name"] != DBNull.Value) ? Convert.ToString(reader["status_name"]) : "" };
                        order.Waiter = new Waiter { ID = (reader["waiterID"] != DBNull.Value) ? Convert.ToInt32(reader["waiterID"]) : 0, Name = (reader["waiterName"] != DBNull.Value) ? Convert.ToString(reader["waiterName"]) : "" };
                        order.ItemsHtml = (reader["order_items"] != DBNull.Value) ? Convert.ToString(reader["order_items"]) : "";
                        if (order != null)
                        {
                            list.Add(order);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string except = e.Message;
            }

            return list;
        }

        //Детали заказа по id заказа (админка)
        public static string GetOrderDetails(int? id)
        {
            string details = "";
            if (id != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("Rest.dbo.GetOrderDetails", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            if (reader["order_items"] != DBNull.Value)
                            {
                                details = (!String.IsNullOrWhiteSpace((string)reader["order_items"])) ? (string)reader["order_items"] : "";
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    return "";
                }
            }
            return details;
        }

        //Проверка статуса заказа
        public static int GetStatus(string order_module)
        {
            int ret = 0;
            if (order_module != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("Rest.dbo.CheckOrderStatus", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@order_module", SqlDbType.NVarChar).Value = order_module;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            if (reader["order_status"] != DBNull.Value)
                            {
                                ret = (int)reader["order_status"];
                            }
                            return ret;
                        }
                    }
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
            return 0;
        }

        //Запись чаевых в базу
        public static void SqlSaveTipping(string phoneCode, string phoneNumber, int restaurantID, string tableID, string orderNumber, string orderSum,  decimal tippingProcent, decimal tippingSum, int waiterID, string waiterName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.SaveTipping", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phoneCode", SqlDbType.VarChar).Value = phoneCode;
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    cmd.Parameters.Add("@tableID", SqlDbType.VarChar).Value = tableID;
                    cmd.Parameters.Add("@orderNumber", SqlDbType.VarChar).Value = orderNumber;
                    cmd.Parameters.Add("@orderSum", SqlDbType.VarChar).Value = orderSum;
                    cmd.Parameters.Add("@tippingProcent", SqlDbType.Decimal).Value = tippingProcent;
                    cmd.Parameters.Add("@tippingSum", SqlDbType.Decimal).Value = tippingSum;
                    cmd.Parameters.Add("@waiterID", SqlDbType.Int).Value = waiterID;
                    cmd.Parameters.Add("@waiterName", SqlDbType.VarChar).Value = waiterName;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Helper.saveToLog(0, "", "SqlSaveTipping", "phoneNumber=" + phoneNumber + ", restaurantID=" + restaurantID.ToString() + ", tableID=" + tableID + ", orderNumber=" + orderNumber + ", orderSum=" + orderSum + ", tippingProcent=" + tippingProcent.ToString() + ", tippingSum=" + tippingSum.ToString()/* + ", waiterName=" + waiterName*/, "Чаевые записаны в БД", 0);
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, "", "SqlSaveTipping", "phoneNumber=" + phoneNumber + ", restaurantID=" + restaurantID.ToString() + ", tableID=" + tableID + ", orderNumber=" + orderNumber + ", orderSum=" + orderSum + ", tippingProcent=" + tippingProcent.ToString() + ", tippingSum=" + tippingSum.ToString()/* + ", waiterName=" + waiterName*/, "Внутренняя ошибка сервиса: " + e.Message, 1);
            }

        }

        //Список чаевых (админка)
        public static List<Tip> GetTipsList(DateTime dfrom, DateTime dto)
        {
            List<Tip> list = new List<Tip>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetTipsList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dfrom", SqlDbType.DateTime).Value = dfrom;
                    cmd.Parameters.Add("@dto", SqlDbType.DateTime).Value = dto;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tip tip = new Tip();
                        tip.PhoneCode = (reader["phone_code"] != DBNull.Value) ? "+" + Convert.ToString(reader["phone_code"]) + " " : "";
                        tip.PhoneNumber = (reader["phone_number"] != DBNull.Value) ? tip.PhoneCode + Convert.ToString(reader["phone_number"]) : "";
                        tip.RestaurantID = (reader["restaurantID"] != DBNull.Value) ? Convert.ToInt32(reader["restaurantID"]) : 0;
                        tip.RestaurantName = (reader["name"] != DBNull.Value) ? Convert.ToString(reader["name"]) : "";
                        tip.TableID = (reader["tableID"] != DBNull.Value) ? Convert.ToString(reader["tableID"]) : "";
                        tip.OrderNumber = (reader["order_number"] != DBNull.Value) ? Convert.ToString(reader["order_number"]) : "";
                        tip.OrderSum = (reader["order_sum"] != DBNull.Value) ? Convert.ToString(reader["order_sum"]) : "";
                        tip.TippingProcent = (reader["tipping_procent"] != DBNull.Value) ? Convert.ToDecimal(reader["tipping_procent"]) : 0;
                        tip.TippingSum = (reader["tipping_sum"] != DBNull.Value) ? Convert.ToDecimal(reader["tipping_sum"]) : 0;
                        if (reader["payment_date"] != DBNull.Value)
                        {
                            tip.PaymentDate = (DateTime)reader["payment_date"];
                        }
                        tip.WaiterID = (reader["waiter_id"] != DBNull.Value) ? Convert.ToInt32(reader["waiter_id"]) : 0;
                        tip.WaiterName = (reader["waiter_name"] != DBNull.Value) ? Convert.ToString(reader["waiter_name"]) : "";
                        if (tip != null)
                        {
                            list.Add(tip);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string except = e.Message;
            }

            return list;
        }
    }
}