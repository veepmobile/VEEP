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
    public class ReportData
    {
        public static List<ReportMain> GetMainReport(DateTime dfrom, DateTime dto)
        {
            List<ReportMain> list = new List<ReportMain>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetMainReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dfrom", SqlDbType.DateTime).Value = dfrom;
                    cmd.Parameters.Add("@dto", SqlDbType.DateTime).Value = dto;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ReportMain item = new ReportMain();
                        int id = (reader["id"] != DBNull.Value) ? Convert.ToInt32(reader["id"]) : 0;
                        item.PhoneCode = !String.IsNullOrEmpty((string)reader["phone_code"]) ? reader["phone_code"].ToString() : "+7";
                        item.PhoneNumber = !String.IsNullOrEmpty((string)reader["phone_number"]) ? reader["phone_number"].ToString() : "";
                        item.IsValid = (reader["isValid"] != DBNull.Value) ? Convert.ToInt32(reader["isValid"]) : 0;
                        item.OS = (reader["phone_os"] != DBNull.Value) ? Convert.ToInt32(reader["phone_os"]) : 0;
                        item.PhoneModel = !String.IsNullOrEmpty((string)reader["phone_model"]) ? reader["phone_model"].ToString() : "";
                        //item.BankCards = !String.IsNullOrEmpty((string)reader["bank_card"]) ? reader["bank_card"].ToString() : "";
                        List<Binding> bindings_list = OrderData.SqlFindBindings(id.ToString());
                        List<string> bindings = new List<string>();
                        foreach (var b in bindings_list)
                        {
                            bindings.Add(b.maskedPan);                        
                        }
                        item.BankCards = String.Join(", ", bindings);
                        //item.DiscountCards = !String.IsNullOrEmpty((string)reader["discount_card"]) ? reader["discount_card"].ToString() : "";
                        List<string> discount = DiscountCardData.SqlDiscountCardList(id);
                        item.DiscountCards = String.Join(", ", discount);
                        if (reader["cdate"] != DBNull.Value)
                        {
                            item.OrderDate = (DateTime)reader["cdate"];
                        }
                        item.RestaurantID = (reader["restaurant_id"] != DBNull.Value) ? Convert.ToInt32(reader["restaurant_id"]) : 0;
                        item.RestaurantName = !String.IsNullOrEmpty((string)reader["restaurant_name"]) ? reader["restaurant_name"].ToString() : "";
                        item.TableID = !String.IsNullOrEmpty((string)reader["table_id"]) ? reader["table_id"].ToString() : "";
                        item.OrderTotal = (reader["order_total"] != DBNull.Value) ? Convert.ToDecimal(reader["order_total"]) : 0;
                        item.DiscountSum = (reader["discount_sum"] != DBNull.Value) ? Convert.ToDecimal(reader["discount_sum"]) : 0;
                        item.OrderSum = (reader["order_rest_sum"] != DBNull.Value) ? Convert.ToDecimal(reader["order_rest_sum"]) : 0;
                        item.TippingProcent = (reader["tipping_proc"] != DBNull.Value) ? Convert.ToDecimal(reader["tipping_proc"]) : 0;
                        item.TippingSum = (reader["tipping_sum"] != DBNull.Value) ? Convert.ToDecimal(reader["tipping_sum"]) : 0;
                        item.Waiter = (reader["waiterName"] != DBNull.Value) ? Convert.ToString(reader["waiterName"]) : "";
                        item.PaymentResult = (reader["payment_result"] != DBNull.Value) ? Convert.ToString(reader["payment_result"]) : "";
                        item.CardMaskPan = (reader["pan"] != DBNull.Value) ? Convert.ToString(reader["pan"]) : "";
                        item.CardExpiration = (reader["expiration"] != DBNull.Value) ? Convert.ToString(reader["expiration"]) : "";
                        item.CardHolderName = (reader["cardholderName"] != DBNull.Value) ? Convert.ToString(reader["cardholderName"]) : "";
                        if (reader["create_date"] != DBNull.Value)
                        {
                            item.AccountCreateDate = (DateTime)reader["create_date"];
                        }
                        if (reader["update_date"] != DBNull.Value)
                        {
                            item.AccountUpdateDate = (DateTime)reader["update_date"];
                        }
                        if (reader["last_date"] != DBNull.Value)
                        {
                            item.AccountLastDate = (DateTime)reader["last_date"];
                        }
                        if (item != null)
                        {
                            list.Add(item);
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