using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Skrin.Models.Iss.Content;

namespace Skrin.BLL.Iss
{
    public class VestnikRepository
    {
        public static async Task<VestnikMessage> GetVestnikMessage(string id, CompanyData company)
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
                        message.name = company.SearchedName;
                        message.ticker = company.Ticker;
                        message.id = id;
                        message.inn = rd.ReadEmptyIfDbNull("inn");
                        message.ogrn = rd.ReadEmptyIfDbNull("ogrn");
                        message.contents = rd.ReadEmptyIfDbNull("contents");
                        message.dt = rd.ReadEmptyIfDbNull("dt");
                        message.nomera = rd.ReadEmptyIfDbNull("nomera");
                        message.region = rd.ReadEmptyIfDbNull("region");
                        message.type_id = (rd["tid"] != DBNull.Value)?(int)rd["tid"]:0;
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

    }
}