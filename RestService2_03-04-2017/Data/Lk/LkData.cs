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
    public class LkData
    {

        public static List<Tip> GetTipsList(DateTime dfrom, DateTime dto, int restaurantID)
        {
            List<Tip> list = new List<Tip>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetLkTips", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dfrom", SqlDbType.DateTime).Value = dfrom;
                    cmd.Parameters.Add("@dto", SqlDbType.DateTime).Value = dto;
                    cmd.Parameters.Add("@restaurant_id", SqlDbType.Int).Value = restaurantID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Tip tip = new Tip();
                        if (reader["payment_date"] != DBNull.Value)
                        {
                            tip.PaymentDate = (DateTime)reader["payment_date"];
                        }
                        tip.RestaurantID = (reader["restaurantID"] != DBNull.Value) ? Convert.ToInt32(reader["restaurantID"]) : 0;
                        tip.RestaurantName = (reader["name"] != DBNull.Value) ? Convert.ToString(reader["name"]) : "";
                        tip.TableID = (reader["tableID"] != DBNull.Value) ? Convert.ToString(reader["tableID"]) : "";
                        tip.OrderNumber = (reader["order_number"] != DBNull.Value) ? Convert.ToString(reader["order_number"]) : "";
                        tip.OrderSum = (reader["order_sum"] != DBNull.Value) ? Convert.ToString(reader["order_sum"]) : "";
                        tip.TippingProcent = (reader["tipping_procent"] != DBNull.Value) ? Convert.ToDecimal(reader["tipping_procent"]) : 0;
                        tip.TippingSum = (reader["tipping_sum"] != DBNull.Value) ? Convert.ToDecimal(reader["tipping_sum"]) : 0;
                        decimal tips = (tip.TippingSum * 90) / 100;
                        tip.TippingResult = Decimal.Round(tips);
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


        public static List<Prize> GetPrizeList(DateTime dfrom, DateTime dto, int restaurantID)
        {
            List<Prize> list = new List<Prize>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetLkAllPrize", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dfrom", SqlDbType.DateTime).Value = dfrom;
                    cmd.Parameters.Add("@dto", SqlDbType.DateTime).Value = dto;
                    cmd.Parameters.Add("@restaurant_id", SqlDbType.Int).Value = restaurantID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Prize prize = new Prize();
                        prize.PhoneNumber = (string)reader["phone_number"];
                        if (reader["payment_date"] != DBNull.Value)
                        {
                            prize.PaymentDate = (DateTime)reader["payment_date"];
                        }
                        prize.PaymentID = (reader["id"] != DBNull.Value) ? Convert.ToInt32(reader["id"]) : 0;
                        prize.RestaurantID = (reader["restaurantID"] != DBNull.Value) ? Convert.ToInt32(reader["restaurantID"]) : 0;
                        prize.RestaurantName = (reader["name"] != DBNull.Value) ? Convert.ToString(reader["name"]) : "";
                        prize.TableID = (reader["tableID"] != DBNull.Value) ? Convert.ToString(reader["tableID"]) : "";
                        prize.WaiterID = (reader["waiterId"] != DBNull.Value) ? Convert.ToInt32(reader["waiterId"]) : 0;
                        prize.WaiterName = (reader["waiterName"] != DBNull.Value) ? Convert.ToString(reader["waiterName"]) : "";
                        if (prize != null && prize.PaymentID != 0 && CheckFirst(prize.PhoneNumber, prize.PaymentID))
                        {
                            list.Add(prize);
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

        public static List<Prize> GetPrizeOtherList(DateTime dfrom, DateTime dto, int restaurantID)
        {
            List<Prize> list = new List<Prize>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetLkAllPrize", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dfrom", SqlDbType.DateTime).Value = dfrom;
                    cmd.Parameters.Add("@dto", SqlDbType.DateTime).Value = dto;
                    cmd.Parameters.Add("@restaurant_id", SqlDbType.Int).Value = restaurantID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Prize prize = new Prize();
                        prize.PhoneNumber = (string)reader["phone_number"];
                        if (reader["payment_date"] != DBNull.Value)
                        {
                            prize.PaymentDate = (DateTime)reader["payment_date"];
                        }
                        prize.PaymentID = (reader["id"] != DBNull.Value) ? Convert.ToInt32(reader["id"]) : 0;
                        prize.RestaurantID = (reader["restaurantID"] != DBNull.Value) ? Convert.ToInt32(reader["restaurantID"]) : 0;
                        prize.RestaurantName = (reader["name"] != DBNull.Value) ? Convert.ToString(reader["name"]) : "";
                        prize.TableID = (reader["tableID"] != DBNull.Value) ? Convert.ToString(reader["tableID"]) : "";
                        prize.WaiterID = (reader["waiterId"] != DBNull.Value) ? Convert.ToInt32(reader["waiterId"]) : 0;
                        prize.WaiterName = (reader["waiterName"] != DBNull.Value) ? Convert.ToString(reader["waiterName"]) : "";
                        if (prize != null && prize.PaymentID != 0 && !CheckFirst(prize.PhoneNumber, prize.PaymentID))
                        {
                            list.Add(prize);
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


        private static bool CheckFirst(string phone_number, int payment_id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetLkFirstPrize", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = phone_number;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (reader["id"] != DBNull.Value) ? (int)reader["id"] : 0;
                        if (id == payment_id) { return true; }
                    }
                }
            }
            catch (Exception e)
            {
                string except = e.Message;
            }
            return false;
        }

        /*
        public static List<Prize> GetPrizeList(DateTime dfrom, DateTime dto, int restaurantID)
        {
            List<Prize> list = new List<Prize>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetLkPrize", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dfrom", SqlDbType.DateTime).Value = dfrom;
                    cmd.Parameters.Add("@dto", SqlDbType.DateTime).Value = dto;
                    cmd.Parameters.Add("@restaurant_id", SqlDbType.Int).Value = restaurantID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Prize prize = new Prize();
                        prize.PhoneNumber = (string)reader["phone_number"];
                        if (reader["payment_date"] != DBNull.Value)
                        {
                            prize.PaymentDate = (DateTime)reader["payment_date"];
                        }
                        prize.RestaurantID = (reader["restaurantID"] != DBNull.Value) ? Convert.ToInt32(reader["restaurantID"]) : 0;
                        prize.RestaurantName = (reader["name"] != DBNull.Value) ? Convert.ToString(reader["name"]) : "";
                        prize.TableID = (reader["tableID"] != DBNull.Value) ? Convert.ToString(reader["tableID"]) : "";
                        prize.WaiterID = (reader["waiterId"] != DBNull.Value) ? Convert.ToInt32(reader["waiterId"]) : 0;
                        prize.WaiterName = (reader["waiterName"] != DBNull.Value) ? Convert.ToString(reader["waiterName"]) : "";
                        if (prize != null)
                        {
                            list.Add(prize);
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


        public static List<Prize> GetPrizeOtherList(DateTime dfrom, DateTime dto, int restaurantID)
        {
            List<Prize> list = new List<Prize>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetLkOtherPrize", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dfrom", SqlDbType.DateTime).Value = dfrom;
                    cmd.Parameters.Add("@dto", SqlDbType.DateTime).Value = dto;
                    cmd.Parameters.Add("@restaurant_id", SqlDbType.Int).Value = restaurantID;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Prize prize = new Prize();
                        prize.PhoneNumber = (string)reader["phone_number"];
                        if (reader["payment_date"] != DBNull.Value)
                        {
                            prize.PaymentDate = (DateTime)reader["payment_date"];
                        }
                        prize.RestaurantID = (reader["restaurantID"] != DBNull.Value) ? Convert.ToInt32(reader["restaurantID"]) : 0;
                        prize.RestaurantName = (reader["name"] != DBNull.Value) ? Convert.ToString(reader["name"]) : "";
                        prize.TableID = (reader["tableID"] != DBNull.Value) ? Convert.ToString(reader["tableID"]) : "";
                        prize.WaiterID = (reader["waiterId"] != DBNull.Value) ? Convert.ToInt32(reader["waiterId"]) : 0;
                        prize.WaiterName = (reader["waiterName"] != DBNull.Value) ? Convert.ToString(reader["waiterName"]) : "";
                        if (prize != null)
                        {
                            list.Add(prize);
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
        */

    }
}