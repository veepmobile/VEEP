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
    }
}