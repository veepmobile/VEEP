using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Monitoring;
using System.Text;

namespace Skrin.BLL.Root
{
    public static class MonitorRepository
    {
        static readonly string _constring = Configs.ConnectionString;


        public static async Task<int> AddToEgrulMonitorAsync(string issuer_id, int user_id)
        {
             using (SqlConnection con = new SqlConnection(_constring))
             {
                 SqlCommand cmd = new SqlCommand("skrin_net..monitoring_add_egrul_company", con);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.Add("@ids", SqlDbType.VarChar).Value = issuer_id;
                 cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                 con.Open();
                 return (int) await cmd.ExecuteScalarAsync();
             }
        }

        public static async Task  RemoveFromEgrulMonitorAsync(string issuer_id, int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("delete from web_shop..users_egrul_ogrn  where src=1 and user_id=@user_id and issuer_id=@id",con);
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = issuer_id;
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task RemoveFromMessMonitorAsync(string issuer_id, int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("delete from web_shop..users_Mess  where src=1 and user_id=@user_id and issuer_id=@id", con);
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = issuer_id;
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task<List<SubscriptionInfo>> GetUpdateMonitorInfoAsync(int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("select group_id, email from skrin_subscription..user_group_updates where user_id=@user_id", con);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                var info = new List<SubscriptionInfo>();
                using(SqlDataReader reader=await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        info.Add(new SubscriptionInfo
                        {
                            id = (int)reader["group_id"],
                            email = (string)reader["email"]
                        });
                    }
                }
                return info;
            }
        }

        public static async  Task AddGroupForUpdateAsync(int user_id, List<SubscriptionInfo> si)
        {
            StringBuilder sb=new StringBuilder("<root>");
            foreach (var item in si)
	        {
		        sb.Append(string.Format("<dta email=\"{0}\" group_id=\"{1}\" />",item.email,item.id));
	        }
            sb.Append("</root>");
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..monitoring_add_groups_for_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@data",SqlDbType.VarChar).Value=sb.ToString();
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }


        public static async Task AddGroupForMessTypeAsync(int user_id,List<MessageSubcriptionInfo> si)
        {
            StringBuilder sb = new StringBuilder("<root>");
            foreach (var item in si)
            {
                sb.Append(string.Format("<dta i=\"{2}\" email=\"{0}\" group_id=\"{1}\">", item.email, item.group_id,item.i));
                foreach (var mt in item.mt)
                {
                    sb.Append(string.Format("<mt id=\"{0}\"/>", mt));
                }
                sb.Append("</dta>");
            }
            sb.Append("</root>");
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..monitoring_add_groups_for_messtypes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@data", SqlDbType.VarChar).Value = sb.ToString();
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task<List<EgrulMonitorULDetails>> GetCompaniesInEgrulListAsync(int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = @"select a.issuer_id,ticker,isnull(us.ogrn,'-') as ogrn,egrul_ogrn_status_id as egrul_ogrn_status,
                            isnull(us.short_name,us.name)name,us.inn,us.okpo,us.ruler,us.legal_address,isnull(convert (varchar(10),us.gks_delete_date,104),'0') del,CAST(us.type_id as VARCHAR(2)) type_id,
                            (select top 1 name  from searchdb2..okveds where kod=us.okved) okved
                            from web_shop..users_egrul_ogrn a inner join searchdb2..union_search us on a.issuer_id=us.issuer_id 
                            where a.src=1 and user_id=@user_id order by so,type_id, bones";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                var ret = new List<EgrulMonitorULDetails>();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ret.Add(new EgrulMonitorULDetails
                        {
                            issuer_id = (string)reader["issuer_id"],
                            ticker = (string)reader["ticker"],
                            ogrn = (string)reader["ogrn"],
                            egrul_ogrn_status = (int)reader["egrul_ogrn_status"],
                            name = reader.ReadEmptyIfDbNull("name"),
                            inn = reader.ReadEmptyIfDbNull("inn"),
                            okpo = reader.ReadEmptyIfDbNull("okpo"),
                            ruler = reader.ReadEmptyIfDbNull("ruler"),
                            legal_address = reader.ReadEmptyIfDbNull("legal_address"),
                            del = reader.ReadEmptyIfDbNull("del"),
                            type_id = reader.ReadEmptyIfDbNull("type_id"),
                            okved=reader.ReadEmptyIfDbNull("okved")
                        });
                    }
                }
                return ret;
            }
        }

        public static async Task EgrulUpdateEmailAsyc(string email,int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..monitoring_egrul_update_email", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.VarChar,250).Value = email;
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task<string> GetReportEmailAsysnc(SubcriptionType s_type,int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("select isnull(report_email,'') report_email  from skrin_subscription.dbo.users_subscription where subscription_type=@s_type and user_id=@user_id", con);
                cmd.Parameters.Add("@s_type", SqlDbType.VarChar, 250).Value = (int)s_type;
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                if (await reader.ReadAsync())
                {
                    return (string)reader[0];
                }
                return "";
            }
        }

        public static async Task DeleteCompanyFromEgrulListAsync(int user_id, string issuer_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("delete from web_shop..users_egrul_ogrn  where src=1 and user_id=@user_id and issuer_id=@issuer_id", con);
                cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 250).Value = issuer_id;
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task<List<MessageMonitorGroup>> GetMessageMonitorGroupAsync(int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = @"select id,isnull(ListID,0) as group_id,isnull(email,'') as email,  
                    (SELECT STUFF((SELECT ','+cast(mess_type_id as varchar(50)) FROM  skrin_subscription..user_group_mess_types where a.id=user_group_mess_id FOR XML PATH('')),1,1,'')) as mt
                    from skrin_subscription..user_group_mess a inner join security..secUserLists b on b.ListID=a.group_id and a.user_id=b.userid where user_id=@user_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                var ret = new List<MessageMonitorGroup>();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    ret.Add(new MessageMonitorGroup
                    {
                        id = (int)reader["id"],
                        group_id = (int)reader["group_id"],
                        email = (string)reader["email"],
                        message_types = (string)reader["mt"]
                    });
                }
                return ret;
            }
        }

        public static async Task<Dictionary<int,string>>GetMessageTypesAsync()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("SELECT id,name from skrin_subscription..mess_types", con);
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                var ret = new Dictionary<int, string>();
                while (await reader.ReadAsync())
                {
                    ret.Add((int)reader[0],(string)reader[1]);
                }
                return ret;
            }
        }


        public static async Task<List<EgrulMonitorGroup>> GetEgrulMonitorGroupAsync(int user_id)
        {
            using (SqlConnection con = new SqlConnection(_constring))
            {
                string sql = @"select id,isnull(ListID,0) as group_id,isnull(email,'') as email, egrul_types + case when isnull(egrip_types,'')='' then '' else ',' + egrip_types end as egrul_types
                    from skrin_subscription..user_group_egrul a inner join security..secUserLists b on b.ListID=a.group_id and a.user_id=b.userid where user_id=@user_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                var ret = new List<EgrulMonitorGroup>();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    ret.Add(new EgrulMonitorGroup
                    {
                        id = (int)reader["id"],
                        group_id = (int)reader["group_id"],
                        email = (string)reader["email"],
                        egrul_types = (string)reader["egrul_types"]
                    });
                }
                return ret;
            }
        }


        public static async Task AddGroupForEgrulTypeAsync(int user_id, List<EgrulMonitorGroup> si)
        {
            StringBuilder sb = new StringBuilder("<root>");
            foreach (var item in si)
            {
                sb.Append(string.Format("<dta i=\"{2}\" email=\"{0}\" group_id=\"{1}\" et=\"{3}\" eit=\"{4}\"/>", item.email, item.group_id, item.id, item.egrul_types, item.egrip_types));
            }
            sb.Append("</root>");
            string _temp = sb.ToString();
            using (SqlConnection con = new SqlConnection(_constring))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..monitoring_add_groups_for_egrultypes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@data", SqlDbType.VarChar).Value = sb.ToString();
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

    }
}