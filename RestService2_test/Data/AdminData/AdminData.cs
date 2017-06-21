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
    public class AdminData
    {

        public static Users UserLogin(string name, string psw)
        {
            Users user = new Users();

            try
            {
                using (SqlConnection con = new SqlConnection(BLL.Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("Rest.dbo.Login", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@psw", SqlDbType.VarChar).Value = psw;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user.UserID = (reader["id"] != DBNull.Value) ? Convert.ToInt32(reader["id"]) : 0;
                        //user.Roles = (reader["roles_id"] != DBNull.Value) ? Convert.ToInt32(reader["roles_id"]) : 0;
                        user.RestaurantID = (reader["restaurant_id"] != DBNull.Value) ? Convert.ToInt32(reader["restaurant_id"]) : 0;
                        return user;
                    }
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

    }
}