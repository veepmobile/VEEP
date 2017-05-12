using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;
using System.Data;
using Skrin.Models.News;
using System.Threading.Tasks;

namespace Skrin.BLL.Root
{
    public class NewsRepository
    {
        public static async Task<Statistic> GetStatisticAsync()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "skrin_net..get_index_page_news";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@news_type", SqlDbType.Int).Value = (int)NewsType.Statistc;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                Statistic stat = null;
                if (await reader.ReadAsync())
                {
                    stat = new Statistic
                    {
                        ul_count = (int)reader["ul_count"],
                        fl_count = (int)reader["ip_count"],
                        pravo_count = (int)reader["pravo_count"],
                        zak_count = (int)reader["zak_count"]
                    };
                }
                return stat;
            }
        }

        public static async Task<MessageInfo> GetSiteUpdatesAsync()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "skrin_net..get_index_page_news";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@news_type", SqlDbType.Int).Value = (int)NewsType.SiteUpdates;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                MessageInfo mi = new MessageInfo();
                while (await reader.ReadAsync())
                {
                    mi.items.Add(new MessageInfoItem
                    {
                        message_link = ((int)reader["id"]).ToString(),
                        message_title = (string)reader.ReadNullIfDbNull("header"),
                        datetime = (string)reader["dt"]
                    });
                }
                return mi;
            }
        }

        public static async Task<MessageInfo> GetLastReportsAsync()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "skrin_net..get_index_page_news";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@news_type", SqlDbType.Int).Value = (int)NewsType.LastReports;
                con.Open();
                SqlDataReader reader =await cmd.ExecuteReaderAsync();
                MessageInfo mi = new MessageInfo();
                while (await reader.ReadAsync())
                {
                    if (string.IsNullOrEmpty(mi.info_date))
                    {
                        mi.info_date = ((DateTime)reader["insert_date"]).ToShortDateString();
                    }
                    mi.items.Add(new MessageInfoItem
                    {
                        message_link = (string)reader["id"],
                        message_title = (string)reader.ReadNullIfDbNull("title"),
                        datetime = ((DateTime)reader["insert_date"]).ToString("HH:mm:ss"),
                        company_title = (string)reader.ReadNullIfDbNull("FName"),
                        company_link = (string)reader.ReadNullIfDbNull("href")
                    });
                }
                return mi;
            }
        }

        public static async Task<MessageInfo> GetLastEventsAsync()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "skrin_net..get_index_page_news";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@news_type", SqlDbType.Int).Value = (int)NewsType.LastEvents;
                con.Open();
                SqlDataReader reader =await cmd.ExecuteReaderAsync();
                MessageInfo mi = new MessageInfo();
                while (await reader.ReadAsync())
                {
                    mi.items.Add(new MessageInfoItem
                    {
                        message_link = (string)reader["id"],
                        message_title = (string)reader.ReadNullIfDbNull("name"),
                        datetime = ((DateTime)reader["event_date"]).ToShortDateString(),
                        company_title = (string)reader.ReadNullIfDbNull("short_name"),
                        company_link = (string)reader.ReadNullIfDbNull("rts_code")
                    });
                }
                return mi;
            }
        }

        public static async Task<MessageInfo> GetAnaliticsAsync()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "skrin_net..get_index_page_news";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@news_type", SqlDbType.Int).Value = (int)NewsType.Analitics;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                MessageInfo mi = new MessageInfo();
                while (await reader.ReadAsync())
                {
                    mi.items.Add(new MessageInfoItem
                    {
                        message_link = (string)reader.ReadNullIfDbNull("link"),
                        message_title = (string)reader.ReadNullIfDbNull("name"),
                        datetime = ((DateTime)reader["date"]).ToString("dd.MM HH:mm"),
                        company_title = (string)reader.ReadNullIfDbNull("author_name"),
                        company_link = (string)reader.ReadNullIfDbNull("author_id")
                    });
                }
                return mi;
            }
        }

        public static async Task<MessageInfo> GetUCAsync()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "skrin_net..get_index_page_news";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@news_type", SqlDbType.Int).Value = (int)NewsType.UC;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                MessageInfo mi = new MessageInfo();
                while (await reader.ReadAsync())
                {
                    mi.items.Add(new MessageInfoItem
                    {
                        message_title = (string)reader.ReadNullIfDbNull("SemFullName"),
                        datetime = (string)reader.ReadNullIfDbNull("ProgrammName"),
                    });
                }
                return mi;
            }
        }

        public static async Task<MessageInfo> GetULUpdatesAsync()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "skrin_net..get_index_page_news";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@news_type", SqlDbType.Int).Value = (int)NewsType.ULUpdates;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                MessageInfo mi = new MessageInfo();
                while (await reader.ReadAsync())
                {
                    if (string.IsNullOrEmpty(mi.info_date))
                    {
                        mi.info_date = ((DateTime)reader["dt"]).ToShortDateString();
                    }
                    mi.items.Add(new MessageInfoItem
                    {
                        datetime = ((DateTime)reader["dt"]).ToString("HH:mm:ss"),
                        company_title = (string)reader.ReadNullIfDbNull("short_name"),
                        company_link = (string)reader.ReadNullIfDbNull("rts_code")
                    });
                }
                return mi;
            }
        }

        public static async Task<List<NewsHeader>> GetNewsHeadersAsync()
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "Select  id,convert(varchar(10),data,104) dt,header from security..aboutNews where is_active=1  order by data desc ";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                var ret = new List<NewsHeader>();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ret.Add(new NewsHeader
                        {
                            id = (int)reader["id"],
                            dt = (string)reader["dt"],
                            header = (string)reader["header"]
                        });
                    }
                }
                return ret;
            }
        }

        public static async Task<string> GetNewsAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select txt from security..aboutNews where id=@id",con);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                con.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if(await reader.ReadAsync())
                    {
                        return (string)reader[0];
                    }
                    return "";
                }
            }
        }

        public static string GetBankrotDate(int source)
        {
            string sql = "";
            switch (source)
            {
                case -1:
                    /*sql = "select convert(varchar(10),max(rd),104) from (Select DATEADD(day,-1,max(PublishDate)) as rd from naufor..bankruptcyEFRSB ) t"; */
                    sql = "select convert(varchar(10),max(rd),104) from (Select DATEADD(day,0,max(PublishDate)) as rd from naufor..bankruptcyEFRSB ) t";
                    break;
                case 34:
                    sql = "select convert(varchar(10),max(rd),104) from (Select DATEADD(day,0,max(reg_date)) as rd from naufor..bankruptcy2 where skrin_source_id=34) t";
                    break;
                case 38:
                    sql = "select convert(varchar(10),max(rd),104) from (Select DATEADD(day,0,max(reg_date)) as rd from naufor..bankruptcy2 where skrin_source_id=38) t";
                    break;
                default:
                    sql = "select convert(varchar(10),max(rd),104) from (Select DATEADD(day,0,max(reg_date)) as  rd from naufor..bankruptcy2 union Select DATEADD(day,0,max(PublishDate)) as rd from naufor..bankruptcyEFRSB ) t";
                    /*sql = "select convert(varchar(10),max(rd),104) from (Select DATEADD(day,-1,max(reg_date)) as  rd from naufor..bankruptcy2 union Select DATEADD(day,-1,max(PublishDate)) as rd from naufor..bankruptcyEFRSB ) t";*/
                    break;
            }
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string result = "";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = reader.ReadNullIfDbNull(0).ToString();
                }
                return result;
            }

        }

        public static EventInfo GetEventInfo(int id, int agency_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @"Select a.header, a.Event_Date, a.Event_Text, 
                                a.insert_date, c.Name event_group_name, d.Name event_type_name, f.FULL_NAME_RUS firm_name 
                                from disclosure..Events a 
                                join disclosure..Event_Types b on a.Event_Type_ID=b.id 
                                join disclosure..Event_Type_Groups c on b.Event_Type_Group_ID=c.Id
                                join disclosure..Event_Types d on a.Event_Type_ID=d.ID
                                join disclosure..Firm f on a.Firm_ID=f.FIRM_Id
                                where a.ID=@id and agency_id=@agency_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@agency_id", SqlDbType.Int).Value = agency_id;
                con.Open();
                EventInfo ei = null;
                using(SqlDataReader reader=cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                    {
                        ei = new EventInfo
                        {
                            Id = id,
                            AgencyId = agency_id,
                            Header = (string)reader.ReadNullIfDbNull("header"),
                            EventDate = (DateTime?)reader.ReadNullIfDbNull("Event_Date"),
                            EventText = (string)reader.ReadNullIfDbNull("Event_Text"),
                            InsertDate = (DateTime)reader["insert_date"],
                            EventGroupName = (string)reader.ReadNullIfDbNull("event_group_name"),
                            EventTypeName = (string)reader.ReadNullIfDbNull("event_type_name"),
                            FirmName = (string)reader.ReadNullIfDbNull("firm_name")
                        };
                    }
                }
                return ei;
            }
        }
    }
}