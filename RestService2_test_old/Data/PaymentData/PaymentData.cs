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
    public class PaymentData
    {
        //Список платежей (админка)
        public static List<Payments> GetPaymentsList(DateTime dfrom, DateTime dto)
        {
            List<Payments> list = new List<Payments>();
            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.GetPaymentList", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@dfrom", SqlDbType.DateTime).Value = dfrom;
                    cmd.Parameters.Add("@dto", SqlDbType.DateTime).Value = dto;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Payments payment = new Payments();
                        payment.ID = (reader["id"] != DBNull.Value) ? Convert.ToInt32(reader["id"]) : 0;
                        payment.ClientID = (reader["clientId"] != DBNull.Value) ? Convert.ToInt32(reader["clientId"]) : 0;
                        payment.RestaurantID = (reader["restaurantID"] != DBNull.Value) ? Convert.ToInt32(reader["restaurantID"]) : 0;
                        payment.RestaurantName = (reader["name"] != DBNull.Value) ? Convert.ToString(reader["name"]) : "";
                        payment.PhoneNumber = (reader["phone_number"] != DBNull.Value) ? Convert.ToString(reader["phone_number"]) : "";
                        string first_name = (reader["first_name"] != DBNull.Value) ? Convert.ToString(reader["first_name"]) : "";
                        string last_name = (reader["last_name"] != DBNull.Value) ? Convert.ToString(reader["last_name"]) : "";
                        payment.FIO = first_name + " " + last_name;
                        payment.OrderNumberModule = (reader["order_module"] != DBNull.Value) ? Convert.ToString(reader["order_module"]) : "";
                        payment.OrderNumber = (reader["order_number"] != DBNull.Value) ? Convert.ToString(reader["order_number"]) : "";
                        payment.OrderNumberBank = (reader["orderId"] != DBNull.Value) ? Convert.ToString(reader["orderId"]) : "";
                        payment.OrderSumBank = (reader["order_sum_bank"] != DBNull.Value) ? Convert.ToDecimal(reader["order_sum_bank"]) : 0;
                        if (reader["payment_date"] != DBNull.Value)
                        {
                            payment.PaymentDate = (DateTime)reader["payment_date"];
                        }
                        if (reader["reg_date"] != DBNull.Value)
                        {
                            payment.RegDate = (DateTime)reader["reg_date"];
                        }
                        payment.ErrorCode = (reader["errorCode"] != DBNull.Value) ? Convert.ToInt32(reader["errorCode"]) : 0;
                        payment.ErrorMessage = (reader["errorMessage"] != DBNull.Value) ? Convert.ToString(reader["errorMessage"]) : "";
                        payment.OrderStatus = (reader["orderStatus"] != DBNull.Value) ? Convert.ToInt32(reader["orderStatus"]) : 0;
                        payment.Pan = (reader["pan"] != DBNull.Value) ? Convert.ToString(reader["pan"]) : "";
                        payment.Expiration = (reader["expiration"] != DBNull.Value) ? Convert.ToString(reader["expiration"]) : "";
                        payment.CardHolderName = (reader["cardholderName"] != DBNull.Value) ? Convert.ToString(reader["cardholderName"]) : "";
                        payment.BindingID = (reader["bindingID"] != DBNull.Value) ? Convert.ToString(reader["bindingID"]) : "";
                        if (payment != null)
                        {
                            list.Add(payment);
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