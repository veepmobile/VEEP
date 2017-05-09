using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using RestService.Models;
using RestService.BLL;

namespace RestService
{
    public class DiscountCardData
    {
        //Загрузка привязанных карт в Приложение
        public static List<Models.DiscountCard> SqlFindDiscountCard(int accountID, string user_key)
        {
            List<Models.DiscountCard> list = new List<Models.DiscountCard>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.FindDiscountCard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@account_id", SqlDbType.Int).Value = accountID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Models.DiscountCard card = new Models.DiscountCard();
                        card.ID = (int)reader["id"];
                        card.RestaurantID = (int)reader["restaurant_id"];
                        card.CardNumber = (Int64)reader["card_number"];
                        //card.CardName = (String)reader["card_name"];
                        Models.Account account = new Models.Account();
                        account.ID = (int)reader["account_id"];
                        card.Account = account;
                        card.InsertDate = (DateTime)reader["insert_date"];
                        if (reader["last_date"] != DBNull.Value)
                        {
                            card.LastDate = (DateTime)reader["last_date"];
                        }
                        card.CardStatus = (int)reader["status"];
                        if (card != null)
                        {
                            list.Add(card);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                // Helper.saveToLog(0, user_key, "SqlFindDiscountCard", "phoneNumber=" + phoneNumber, "Внутренняя ошибка сервиса: " + e.Message, 1);Helper.saveToLog(0, user_key, "SqlFindDiscountCard", "accountID=" + accountID, "Внутренняя ошибка сервиса: " + e.Message, 1);
                Helper.saveToLog(0, user_key, "SqlFindDiscountCard", "accountID=" + accountID, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }


        //Привязка дисконтной карты в Системе
        public static int SqlInsertDiscountCard(string phoneNumber, int restaurantID, Int64 cardNumber, int cardStatus, string user_key)
        {
            string param = "";
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    Account account = AccountData.SqlFindAccount(phoneNumber);
                    SqlCommand cmd = new SqlCommand("Rest.dbo.InsertDiscountCard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@restaurant_id", SqlDbType.Int).Value = restaurantID;
                    cmd.Parameters.Add("@card_number", SqlDbType.BigInt).Value = cardNumber;
                    cmd.Parameters.Add("@account_id", SqlDbType.Int).Value = account.ID;
                    cmd.Parameters.Add("@status", SqlDbType.Int).Value = cardStatus;
                    con.Open();
                    object oIdent = DBNull.Value;
                    oIdent = cmd.ExecuteScalar();
                    int pid = (oIdent != DBNull.Value) ? Convert.ToInt32(oIdent) : -1;
                    param = "@restaurant_id=" + restaurantID + ", @card_number=" + cardNumber + ", @account_id=" + account.ID + ", @status=" + cardStatus;
                    Helper.saveToLog(account.ID, user_key, "SqlInsertDiscountCard", "param: " + param, "Дисконтная карта привязана: card.ID = " + pid.ToString(), 0);

                    return pid;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, user_key, "SqlInsertDiscountCard", "phoneNumber: " + phoneNumber, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return 0;
            }
        }


        //Изменение статуса / удаление дисконтной карты в Системе
        public static Int64 SqlUpdateDiscountCard(string phoneNumber, string user_key, Int64 cardNumber, int cardStatus, string phoneCode = "7")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.UpdateDiscountCard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@card_number", SqlDbType.BigInt).Value = cardNumber;
                    cmd.Parameters.Add("@status", SqlDbType.Int).Value = cardStatus;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Helper.saveToLog(0, user_key, "SqlUpdateDiscountCard", "phoneNumber: " + phoneNumber + ", cardNumber: " + cardNumber + ", cardStatus: " + cardStatus, "", 0);
                    return cardNumber;
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, user_key, "SqlUpdateDiscountCard", "phoneNumber: " + phoneNumber + ", cardNumber: " + cardNumber, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return 0;
            }
        }

        //Получение номер дисконтной карты, привязанной к ресторану
        public static Int64? SqlGetDiscountCard(string phoneNumber, string user_key, int restaurant_id, string phoneCode = "7")
        {
            Int64? discountCardNumber = null;
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetDiscountCard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@phone_code", SqlDbType.VarChar).Value = phoneCode;
                    cmd.Parameters.Add("@restaurant_id", SqlDbType.Int).Value = restaurant_id;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        discountCardNumber = (reader["card_number"] != DBNull.Value) ? (Int64?)reader["card_number"] : null;
                    }
                    Helper.saveToLog(0, user_key, "SqlGetDiscountCard", "phoneNumber: " + phoneNumber + ", restaurant_id: " + restaurant_id, "card_number = " + discountCardNumber.ToString(), 0);
                    if (discountCardNumber == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return discountCardNumber;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.saveToLog(0, user_key, "SqlUpdateDiscountCard", "phoneNumber: " + phoneNumber + ", cardNumber: " + discountCardNumber, "Внутренняя ошибка сервиса: " + e.Message, 1);
                return null;
            }
        }

        //Список номеров дисконтных карт, привязанных к клиенту
        public static List<string> SqlDiscountCardList(int accountID)
        {
            List<string> list = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.FindDiscountCard", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@account_id", SqlDbType.Int).Value = accountID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string num = reader["card_number"].ToString();
                        if (!String.IsNullOrEmpty(num))
                        {
                            list.Add(num);
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                 return null;
            }
        }

    }
}