using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using RestService.Models;
using RestService.BLL;
using RestService.CommService;

namespace RestService
{
    public class PersonalData
    {
        // Проверка логина/пароля
        public static Personal SqlLogin(string login, string psw)
        {
            Personal personal = new Personal();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.PersonalLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
                    cmd.Parameters.Add("@psw", SqlDbType.VarChar).Value = psw;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        personal.ID = (reader["id"] != DBNull.Value) ? Convert.ToInt32(reader["id"]) : 0;
                        personal.RestaurantID = (reader["restaurantID"] != DBNull.Value) ? Convert.ToInt32(reader["restaurantID"]) : 0;
                        personal.RolesID = (reader["rolesID"] != DBNull.Value) ? Convert.ToInt32(reader["rolesID"]) : 0;
                        personal.PhoneNumber = (reader["phone"] != DBNull.Value) ? Convert.ToString(reader["phone"]) : "";
                        personal.Psw = (reader["psw"] != DBNull.Value) ? Convert.ToString(reader["psw"]) : "";
                        if (personal.ID != 0)
                        {
                            Helper.saveToMessagesLog(personal.ID, "PersonalLogin", "login: " + login + ", pswd= " + psw, "Авторизован.", 0);
                            return personal;
                        }
                        else
                        {
                            Helper.saveToMessagesLog(null, "PersonalLogin", "login= " + login + ", pswd= " + psw, "Неправильный логин/пароль.", 1);
                            return null;
                        }
                    }
                    else
                    {
                        Helper.saveToMessagesLog(null, "PersonalLogin", "login= " + login + ", pswd= " + psw, "Неудачная авторизация.", 1);
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.saveToMessagesLog(null, "PersonalLogin", "login= " + login + ", pswd= " + psw, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }

        // Получение профиля
        public static Profile SqlProfile(int restaurantID)
        {
            Profile profile = new Profile();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetProfile", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        profile.RestaurantID = restaurantID;
                        while (reader.Read())
                        {
                            int roles = (reader["rolesID"] != DBNull.Value) ? Convert.ToInt32(reader["rolesID"]) : 0;
                            switch (roles)
                            {
                                case 1:
                                    profile.AdminID = (reader["id"] != DBNull.Value) ? Convert.ToInt32(reader["id"]) : 0;
                                    profile.AdminLogin = (reader["login"] != DBNull.Value) ? Convert.ToString(reader["login"]) : "";
                                    profile.AdminPsw = (reader["psw"] != DBNull.Value) ? Convert.ToString(reader["psw"]) : "";
                                    profile.PhoneNumber = (reader["phone"] != DBNull.Value) ? Convert.ToString(reader["phone"]) : "";
                                    break;
                                case 2:
                                    profile.OfficiantID = (reader["id"] != DBNull.Value) ? Convert.ToInt32(reader["id"]) : 0;
                                    profile.OfficiantLogin = (reader["login"] != DBNull.Value) ? Convert.ToString(reader["login"]) : "";
                                    profile.OfficiantPsw = (reader["psw"] != DBNull.Value) ? Convert.ToString(reader["psw"]) : "";
                                    break;
                            }
                        }
                        Helper.saveToMessagesLog(null, "GetProfile", "restaurantID: " + restaurantID, "Профиль найден.", 0);
                        return profile;
                    }
                    else
                    {
                        Helper.saveToMessagesLog(null, "GetProfile", "restaurantID= " + restaurantID, "Профиль не найден.", 1);
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.saveToMessagesLog(null, "GetProfile", "restaurantID= " + restaurantID, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }

        }

        //Смена пароля администратора/официанта
        public static int SqlChangePersonalPsw(int personalID, string newPsw)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.ChangePersonalPsw", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@personalID", SqlDbType.Int).Value = personalID;
                    cmd.Parameters.Add("@newPsw", SqlDbType.VarChar).Value = newPsw;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = (reader["id"] != DBNull.Value) ? 1 : 0;
                        Helper.saveToMessagesLog(null, "SqlChangePersonalPsw", "personalID=" + personalID + ", newPsw=" + newPsw, "Смена пароля прошла успешно", 0);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                Helper.saveToMessagesLog(null, "SqlChangePersonalPsw", "personalID=" + personalID + ", newPsw=" + newPsw, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return result;
            }
        }

        //Запись сообщения в базу
        public static int SqlInsertMessage(Messages msg)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.InsertMessage", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = msg.RestaurantID;
                    cmd.Parameters.Add("@tableID", SqlDbType.VarChar).Value = msg.TableID;
                    cmd.Parameters.Add("@messageType", SqlDbType.Int).Value = msg.MessageType;
                    cmd.Parameters.Add("@messageText", SqlDbType.VarChar).Value = msg.MessageText;
                    cmd.Parameters.Add("@errorCode", SqlDbType.Int).Value = msg.ErrorCode;
                    cmd.Parameters.Add("@errorText", SqlDbType.VarChar).Value = msg.ErrorText;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Helper.saveToMessagesLog(0, "SqlInsertMessage", "restaurantID=" + msg.RestaurantID.ToString() + ", tableID=" + msg.TableID + ", messageType=" + msg.MessageType.ToString() + ", messageText=" + msg.MessageText + ", errorCode=" + msg.ErrorCode.ToString() + ", errorText=" + msg.ErrorText, "Сообщение записано в БД", 0);
                    result = 1;
                }
            }
            catch (Exception e)
            {
                Helper.saveToMessagesLog(0, "SqlInsertMessage", "restaurantID=" + msg.RestaurantID.ToString() + ", tableID=" + msg.TableID + ", messageType=" + msg.MessageType.ToString() + ", messageText=" + msg.MessageText + ", errorCode=" + msg.ErrorCode.ToString() + ", errorText=" + msg.ErrorText, "Ошибка при записи сообщение БД: " + e.Message, 1);
            }
            return result;
        }

        //Получение списка сообщений
        public static List<Messages> SqlGetMessages(int restaurantID, int mode)
        {
            List<Messages> list = new List<Messages>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    string proc = "Rest.dbo.GetMessages";
                    SqlCommand cmd = new SqlCommand(proc, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    cmd.Parameters.Add("@mode", SqlDbType.Int).Value = mode;
                    //cmd.Parameters.Add("@personalID", SqlDbType.Int).Value = personalID;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Messages msg = new Messages();
                        msg.ID = (int)reader["id"];
                        msg.RestaurantID = (reader["restaurantID"]!=DBNull.Value)?(int)reader["restaurantID"]:0;
                        msg.TableID = (reader["tableID"]!=DBNull.Value)?(string)reader["tableID"]:"";
                        msg.MessageType = (reader["messageType"]!=DBNull.Value)?(int)reader["messageType"]:0;
                        msg.MessageText = (reader["messageText"]!=DBNull.Value)?(string)reader["messageText"]:"";
                        msg.ErrorCode = (reader["errorCode"]!=DBNull.Value)?(int)reader["errorCode"]:0;
                        msg.ErrorText = (reader["errorText"]!=DBNull.Value)?(string)reader["errorText"]:"";
                        msg.MessageDate = (reader["cdate"]!=DBNull.Value)?(DateTime)reader["cdate"]:DateTime.Now;
                        msg.IsRead = (reader["isRead"]!=DBNull.Value)?(int)reader["isRead"]:0;
                        msg.MessageReader = (reader["messageReader"]!=DBNull.Value)?(int)reader["messageReader"]:0;

                        if (msg != null)
                        {
                            list.Add(msg);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                Helper.saveToMessagesLog(0, "SqlGetMessages", "mode=" + mode.ToString(), "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }

        //Получение списка сообщений
        public static List<Messages> SqlGetMessages(int restaurantID, int mode, int personalID)
        {
            List<Messages> list = new List<Messages>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    string proc = "Rest.dbo.GetMessagesPersonal";
                    SqlCommand cmd = new SqlCommand(proc, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    cmd.Parameters.Add("@mode", SqlDbType.Int).Value = mode;
                    cmd.Parameters.Add("@personalID", SqlDbType.Int).Value = personalID;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Messages msg = new Messages();
                        msg.ID = (int)reader["id"];
                        msg.RestaurantID = (reader["restaurantID"] != DBNull.Value) ? (int)reader["restaurantID"] : 0;
                        msg.TableID = (reader["tableID"] != DBNull.Value) ? (string)reader["tableID"] : "";
                        msg.MessageType = (reader["messageType"] != DBNull.Value) ? (int)reader["messageType"] : 0;
                        msg.MessageText = (reader["messageText"] != DBNull.Value) ? (string)reader["messageText"] : "";
                        msg.ErrorCode = (reader["errorCode"] != DBNull.Value) ? (int)reader["errorCode"] : 0;
                        msg.ErrorText = (reader["errorText"] != DBNull.Value) ? (string)reader["errorText"] : "";
                        msg.MessageDate = (reader["cdate"] != DBNull.Value) ? (DateTime)reader["cdate"] : DateTime.Now;
                        msg.IsRead = (reader["isRead"] != DBNull.Value) ? (int)reader["isRead"] : 0;
                        msg.MessageReader = (reader["messageReader"] != DBNull.Value) ? (int)reader["messageReader"] : 0;

                        if (msg != null)
                        {
                            list.Add(msg);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                Helper.saveToMessagesLog(0, "SqlGetMessages", "mode=" + mode.ToString(), "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }

        //Закрытие сообщения - прооставление статуса прочитано
        public static int SqlIsRead(int messageID, int personalID, int isRead)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.MessageIsRead", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@messageID", SqlDbType.Int).Value = messageID;
                    cmd.Parameters.Add("@personalID", SqlDbType.Int).Value = personalID;
                    cmd.Parameters.Add("@isRead", SqlDbType.Int).Value = isRead;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Helper.saveToMessagesLog(0, "MessageIsRead", "messageID=" + messageID + ", personalID=" + personalID.ToString() + ", isRead=" + isRead.ToString(), "Сообщение прочитано", 0);
                    result = 1;
                }
            }
            catch (Exception e)
            {
                Helper.saveToMessagesLog(0, "MessageIsRead", "personalID=" + personalID.ToString() + ", isRead=" + isRead.ToString(), "Ошибка при записи статуса сообщения IsRead: " + e.Message, 1);
            }
            return result;
        }

        //Получение истории платежей
        public static List<HistoryPayments> SqlGetHistoryPayments(int restaurantID)
        {
            List<HistoryPayments> list = new List<HistoryPayments>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetHistoryPayments", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        HistoryPayments payment = new HistoryPayments();
                        payment.ID = (reader["id"]!=DBNull.Value)?(int)reader["id"]:0;
                        payment.RestaurantID = (reader["restaurantID"]!=DBNull.Value)?(int)reader["restaurantID"]:0;
                        payment.TableID = (reader["tableID"]!=DBNull.Value)?(string)reader["tableID"]:"";
                        payment.OrderNumber = (reader["order_module"]!=DBNull.Value)?(string)reader["order_module"]:"";
                        payment.OrderTotal = (reader["order_total"]!=DBNull.Value)?(decimal)reader["order_total"]:0;
                        payment.DiscountSum = (reader["discount_sum"]!=DBNull.Value)?(decimal)reader["discount_sum"]:0;
                        payment.OrderSum = (reader["order_rest_sum"]!=DBNull.Value)?(decimal)reader["order_rest_sum"]:0;
                        payment.TippingSum = (reader["tipping_sum"]!=DBNull.Value)?(decimal)reader["tipping_sum"]:0;
                        payment.WaiterID = (reader["waiterID"]!=DBNull.Value)?(int)reader["waiterID"]:0;
                        payment.WaiterName = (reader["waiterName"]!=DBNull.Value)?(string)reader["waiterName"]:"";
                        payment.PaymentDate = (reader["reg_date"]!=DBNull.Value)?(DateTime)reader["reg_date"]:DateTime.Now;

                        if (payment != null)
                        {
                            list.Add(payment);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                Helper.saveToMessagesLog(0, "SqlGetHistoryPayments", "restaurantID=" + restaurantID.ToString(), "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }

        //Получение истории платежей
        public static List<HistoryPayments> SqlGetHistoryPayments(int restaurantID, int personalID)
        {
            List<HistoryPayments> list = new List<HistoryPayments>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetHistoryPayments", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        HistoryPayments payment = new HistoryPayments();
                        payment.ID = (reader["id"] != DBNull.Value) ? (int)reader["id"] : 0;
                        payment.RestaurantID = (reader["restaurantID"] != DBNull.Value) ? (int)reader["restaurantID"] : 0;
                        payment.TableID = (reader["tableID"] != DBNull.Value) ? (string)reader["tableID"] : "";
                        payment.OrderNumber = (reader["order_module"] != DBNull.Value) ? (string)reader["order_module"] : "";
                        payment.OrderTotal = (reader["order_total"] != DBNull.Value) ? (decimal)reader["order_total"] : 0;
                        payment.DiscountSum = (reader["discount_sum"] != DBNull.Value) ? (decimal)reader["discount_sum"] : 0;
                        payment.OrderSum = (reader["order_rest_sum"] != DBNull.Value) ? (decimal)reader["order_rest_sum"] : 0;
                        payment.TippingSum = (reader["tipping_sum"] != DBNull.Value) ? (decimal)reader["tipping_sum"] : 0;
                        payment.WaiterID = (reader["waiterID"] != DBNull.Value) ? (int)reader["waiterID"] : 0;
                        payment.WaiterName = (reader["waiterName"] != DBNull.Value) ? (string)reader["waiterName"] : "";
                        payment.PaymentDate = (reader["reg_date"] != DBNull.Value) ? (DateTime)reader["reg_date"] : DateTime.Now;

                        if (payment != null)
                        {
                            list.Add(payment);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                Helper.saveToMessagesLog(0, "SqlGetHistoryPayments", "restaurantID=" + restaurantID.ToString(), "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }

        //Получение списка сообщений (для админки)
        public static List<Messages> SqlGetMessagesAdm(DateTime dfrom, DateTime dto)
        {
            List<Messages> list = new List<Messages>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetMessagesAdm", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@restaurantID", SqlDbType.Int).Value = restaurantID;
                    cmd.Parameters.Add("@dfrom", SqlDbType.DateTime).Value = dfrom;
                    cmd.Parameters.Add("@dto", SqlDbType.DateTime).Value = dto;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Messages msg = new Messages();
                        msg.ID = (int)reader["id"];
                        msg.RestaurantID = (reader["restaurantID"] != DBNull.Value) ? (int)reader["restaurantID"] : 0;
                        msg.RestaurantName = (reader["name"] != DBNull.Value) ? (string)reader["name"] : "";
                        msg.TableID = (reader["tableID"] != DBNull.Value) ? (string)reader["tableID"] : "";
                        msg.MessageType = (reader["messageType"] != DBNull.Value) ? (int)reader["messageType"] : 0;
                        msg.MessageGroup = (reader["groups"] != DBNull.Value) ? (int)reader["groups"] : 1;
                        msg.MessageText = (reader["messageText"] != DBNull.Value) ? (string)reader["messageText"] : "";
                        msg.ErrorCode = (reader["errorCode"] != DBNull.Value) ? (int)reader["errorCode"] : 0;
                        msg.ErrorText = (reader["errorText"] != DBNull.Value) ? (string)reader["errorText"] : "";
                        msg.MessageDate = (reader["cdate"] != DBNull.Value) ? (DateTime)reader["cdate"] : DateTime.Now;
                        msg.IsRead = (reader["isRead"] != DBNull.Value) ? (int)reader["isRead"] : 0;
                        msg.MessageReader = (reader["messageReader"] != DBNull.Value) ? (int)reader["messageReader"] : 0;
                        msg.ReaderLogin = (reader["login"] != DBNull.Value) ? (string)reader["login"] : "";

                        if (msg != null)
                        {
                            list.Add(msg);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                Helper.saveToMessagesLog(0, "SqlGetMessagesAdm", "dfrom=" + dfrom.ToString() + "dto=" + dto.ToString(), "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }
    }
}