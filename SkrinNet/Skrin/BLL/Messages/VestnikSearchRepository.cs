using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Skrin.Models.Iss.Content;
using Skrin.Models.Vestnik;

namespace Skrin.BLL.Messages
{
    public class VestnikSearchRepository
    {
        public static async Task<VestnikMessage> GetVestnikMessage(string id)
        {
            VestnikMessage message = new VestnikMessage();
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..getVestnikEvent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 32).Value = id;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        message.name = rd.ReadEmptyIfDbNull("name");
                        message.id = id;
                        message.inn = rd.ReadEmptyIfDbNull("inn");
                        message.ogrn = rd.ReadEmptyIfDbNull("ogrn");
                        message.contents = rd.ReadEmptyIfDbNull("contents");
                        message.dt = rd.ReadEmptyIfDbNull("dt");
                        message.nomera = rd.ReadEmptyIfDbNull("nomera");
                        message.region = rd.ReadEmptyIfDbNull("region");
                        message.type_id = (rd["tid"] != DBNull.Value) ? (int)rd["tid"] : 0;
                        message.type_name = rd.ReadEmptyIfDbNull("tname");
                        message.orig_id = rd.ReadEmptyIfDbNull("oid");
                        message.orig_content = rd.ReadEmptyIfDbNull("origin_cont");
                        message.orig_dt = rd.ReadEmptyIfDbNull("odt");
                        message.orig_nomera = rd.ReadEmptyIfDbNull("onomera");
                        message.orig_region = rd.ReadEmptyIfDbNull("oregion");
                        message.orig_type_name = rd.ReadEmptyIfDbNull("otname");
                        message.corr_id = rd.ReadEmptyIfDbNull("cid");
                        message.corr_content = rd.ReadEmptyIfDbNull("corr_cont");
                        message.corr_dt = rd.ReadEmptyIfDbNull("cdt");
                        message.corr_nomera = rd.ReadEmptyIfDbNull("cnomera");
                        message.corr_region = rd.ReadEmptyIfDbNull("cregion");
                        message.corr_type_name = rd.ReadEmptyIfDbNull("ctname");
                    }
                }
            }
            return message;
        }

        public static string GetVestnikTypes(string ids)
        {
            if (ids == null || ids == "" || ids == "undefined")
            {
                return null;
            }

            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "SELECT STUFF((SELECT ','+cast(id as varchar(50)) FROM  (Select a.id from naufor..Vestnik_Types a inner join (Select *  from searchdb2.dbo.kodesplitter(0,'" + ids + "')) b on (a.parent_id=b.kod and exists(select 1 from naufor..Vestnik_Types c where c.id=b.kod and c.parent_id=0)) or (b.kod=a.id and parent_id!=0)) o ORDER BY o.id FOR XML PATH('')),1,1,'')";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        string result = (rd.ReadEmptyIfDbNull(0) != "") ? rd.ReadEmptyIfDbNull(0) + "," + ids: ids;
                        return result;
                    }
                    return null;
                }
            }

        }
    }
}