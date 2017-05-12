using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Skrin.BLL.Iss
{
    public class DisFindRepository
    {
        public static async Task<DisfindSearchResult> GetResultAsync(string ogrn, string inn)
        {
            string dis_res = null;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("fsns2.dbo.get_disfind", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ogrn", SqlDbType.VarChar).Value = ogrn;
                cmd.Parameters.Add("@inn", SqlDbType.VarChar).Value = inn;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                {
                    if (await reader.ReadAsync())
                    {
                        dis_res = (String)reader.ReadNullIfDbNull(0);
                    }
                }
            }
            if (dis_res == null)
                return new DisfindSearchResult { IsFinded = -3 };

            return DeSerializeJson(dis_res);
        }

        private static DisfindSearchResult DeSerializeJson(string sr)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            DisfindSearchResult retval=ser.Deserialize<DisfindSearchResult>(sr);
            return retval;
        }
    }
}