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
    public class EventsRepository
    {
        public static async Task<Tuple<string, string>> GetCorpCalAsync(string issuer_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "select convert(varchar(10),min(reg_date),104) as minrd, convert(varchar(10),max(reg_date),104) as maxrd from events a inner join naufor..Issuer_Join_Events b on b.event_id=a.id where join_id=@issuer_id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@issuer_id", SqlDbType.VarChar, 32).Value = issuer_id;
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return new Tuple<string, string>((string)rd[0], (string)rd[1]);
                    }
                    return null;
                }
            }
        }

        public static async Task<List<EventTypes>> GetEventTypesAsync()
        {
            List<EventTypes> rows = new List<EventTypes>();
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "Select id,name from naufor..event_types event_types where parent_id=0 ";
                SqlCommand cmd = new SqlCommand(sql, con);      
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    rows.Add(new EventTypes() { id = (Int32)reader[0], name = (string)reader[1] });
                }  
            }
            return rows;
        }

        public static async Task<List<EventSearch>> EventSearchAsync(string issuer_id, string type_id, string dfrom, string dto, int page)
        {
            List<EventSearch> rows = new List<EventSearch>();
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = @" ;WITH Q_EVENTS AS ( select ticker,short_name,a.id, convert(varchar(10),a.reg_date,104) as rd, a.reg_date, et.name,  reestr_event_id,
                   ROW_NUMBER() OVER (order by a.reg_date desc,short_name asc)as RowNumber        
	                   from naufor..Events a left join naufor..Event_Type_Rel ee on ee.event_id = a.id
                       inner join naufor..Event_types et on et.id=ee.event_type_id
                       inner join naufor..issuer_join_events ije on a.id=ije.event_id
                       inner join searchdb2..union_search us on us.issuer_id=join_id and us.type_id=ije.type_id WHERE join_id = '" + issuer_id + "'";
                string sWhere = "";          
                if (!String.IsNullOrEmpty(dfrom) && !String.IsNullOrEmpty(dto)) sWhere += " AND  a.reg_date BETWEEN convert(varchar(10),convert(datetime,'" + dfrom + "',104),112) and convert(varchar(10),convert(datetime,'" + dto + "',104),112) "; 
		        if (!String.IsNullOrEmpty(type_id) && (type_id != "0")) sWhere += @" AND (event_type_id in ("+type_id+") or  event_type_id in  (select id from naufor..event_types where parent_id in ("+type_id+")))";             
                if (sWhere.Length > 0)
                {
                    sql +=  sWhere ;
                }
//                sql += @") SELECT * FROM Q_EVENTS   QE left join naufor..events Event_Close on Event_Close.id=QE.reestr_event_id
//                left join naufor..events Close_Event on Close_Event.reestr_event_id=QE.id
//		        order by  qe.reg_date desc,short_name asc";
                //WHERE RowNumber between " + ((page-1)*20) + " AND " + (page*20)
                sql += @") SELECT ticker,short_name,QE.id,rd,QE.reg_date,name,QE.reestr_event_id,
                   Event_Close.headline ec_headline, convert(varchar(10),Event_Close.reg_date,104) ec_date, Event_Close.id as ECID,
                   Close_Event.headline ce_headline,Close_Event.id as CEID,   convert(varchar(10),Close_Event.reg_date,104) ce_date,
                   (SELECT a.id+'','' FROM  naufor..news a inner join naufor..Event_News_Rel b on a.id=b.news_id where event_id=QE.id ORDER BY a.reg_date FOR XML PATH('')) as enr_news,
                      (SELECT a.id+'','' FROM  naufor..news a inner join naufor..Event_News_Rel b on a.id=b.news_id where event_id=Event_Close.id ORDER BY a.reg_date FOR XML PATH('')) as ec_news,
	                     (SELECT a.id+'','' FROM  naufor..news a inner join naufor..Event_News_Rel b on a.id=b.news_id where event_id=Close_Event.id ORDER BY a.reg_date FOR XML PATH('')) as ce_news
                      FROM Q_EVENTS  QE   
		                  left join naufor..events Event_Close on Event_Close.id=QE.reestr_event_id
                           left join naufor..events Close_Event on Close_Event.reestr_event_id=QE.id
		                    order by  qe.reg_date desc,short_name asc ";
            
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader rd = await cmd.ExecuteReaderAsync();
                while (await rd.ReadAsync())
                {
                    rows.Add(new EventSearch()
                    {
                        ticker = (string)rd[0],
                        short_name = (string)rd[1],
                        id = (string)rd[2],
                        rd = (string)rd[3],
                        reg_date = rd[4] != DBNull.Value ? ((DateTime)rd[4]).ToString("dd.MM.yyyy") : "",
                        name = rd[5] != DBNull.Value ? (string)rd[5] : "",
                        reestr_event_id = rd[6] != DBNull.Value ? (string)rd[6] : "",
                        ec_headline = rd[7] != DBNull.Value ? (string)rd[7] : "",
                        ec_date = rd[8] != DBNull.Value ? (string)rd[8] : "",
                        ECID = rd[9] != DBNull.Value ? (string)rd[9] : "",
                        ce_headline = rd[10] != DBNull.Value ? (string)rd[10] : "",
                        CEID = rd[11] != DBNull.Value ? (string)rd[11] : "",
                        ce_date = rd[12] != DBNull.Value ? (string)rd[12] : "",
                        enr_news = rd[13] != DBNull.Value ? (string)rd[13] : "",
                        ec_news = rd[14] != DBNull.Value ? (string)rd[14] : "",
                        ce_news = rd[15] != DBNull.Value ? (string)rd[15] : ""
                    });
                } 
            }
            return rows;
        }


        public static async Task<List<EventSearch>> EventsListSearchAsync(string search_name, string type_id, int type_excl, string dfrom, string dto, int page, int page_length, int? grp)
        {
            List<EventSearch> rows = new List<EventSearch>();
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..EventsSearch1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBeg", SqlDbType.VarChar, 22).Value = dfrom;
                cmd.Parameters.Add("@DEnd", SqlDbType.VarChar, 22).Value = dto;
                cmd.Parameters.Add("@Event_Types", SqlDbType.VarChar, 512).Value = type_id;
                cmd.Parameters.Add("@Event_Types_excl", SqlDbType.Int).Value = type_excl;
                cmd.Parameters.Add("@search_name", SqlDbType.VarChar, 512).Value = search_name;
                cmd.Parameters.Add("@PG", SqlDbType.Int).Value = page;
                cmd.Parameters.Add("@page_length", SqlDbType.Int).Value = page_length;
                cmd.Parameters.Add("@grp", SqlDbType.Int).Value = (grp.HasValue ? grp.Value : 0);

                con.Open();
                SqlDataReader rd = await cmd.ExecuteReaderAsync();

                while (await rd.ReadAsync())
                {
                    rows.Add(new EventSearch()
                    {
                        ticker = (string)rd["ticker"],
                        short_name = (string)rd["short_name"],
                        id = (string)rd["event_id"],
                        rd = (string)rd["rd"],
                        reg_date = rd["reg_date"] != DBNull.Value ? ((DateTime)rd["reg_date"]).ToString("dd.MM.yyyy") : "",
                        name = rd["event_type_name"] != DBNull.Value ? (string)rd["event_type_name"] : "",
                        reestr_event_id = rd["reestr_event_id"] != DBNull.Value ? (string)rd["reestr_event_id"] : "",
                        ec_headline = rd["ec_headline"] != DBNull.Value ? (string)rd["ec_headline"] : "",
                        ec_date = rd["ec_date"] != DBNull.Value ? (string)rd["ec_date"] : "",
                        ECID = rd["ECID"] != DBNull.Value ? (string)rd["ECID"] : "",
                        ce_headline = rd["ce_headline"] != DBNull.Value ? (string)rd["ce_headline"] : "",
                        CEID = rd["CEID"] != DBNull.Value ? (string)rd["CEID"] : "",
                        ce_date = rd["ce_date"] != DBNull.Value ? (string)rd["ce_date"] : "",
                        enr_news = rd["enr_news"] != DBNull.Value ? (string)rd["enr_news"] : "",
                        ec_news = rd["ec_news"] != DBNull.Value ? (string)rd["ec_news"] : "",
                        ce_news = rd["ce_news"] != DBNull.Value ? (string)rd["ce_news"] : "",
                        event_count = (rd["cnt"] != DBNull.Value) ? Convert.ToInt32(rd["cnt"]) : 0
                    });
                }
            }
            return rows;
        }

        public static string EventStartDate()
        {
            string start_date = "";
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("skrin_net..EventsStartDate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        start_date = rd["start_date"] != DBNull.Value ? (string)rd["start_date"] : "";
                    }
                }
                catch
                {

                }
            }
            return start_date;
        }

    }
}