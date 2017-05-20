using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Skrin.Models.Iss.Content;
using Skrin.Models.Disclosure;

namespace Skrin.BLL.Messages
{
    public class DisclosureRepository
    {

        public static DisclosureItem GetDisclosureMsg(string id, string agency_id, string ticker, string kw)
        {
            if (id == null || id =="")
            {
                return null;
            }
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "Select a.id, a.Event_Type_id, a.header, a.Event_Date, a.Event_Text" +
                        ", b.Event_Type_Group_ID, b.name as Event_Type_name, g.name as Event_Type_Group_name" +
                        ", a.Firm_Id, Firm.SHORT_NAME_RUS, Firm.FULL_NAME_RUS" +
                        ", a.insert_date, convert(varchar(10), us.update_date, 104) as update_date" +
                   " from disclosure..Events a" +
                            " inner join disclosure..Event_Types b on a.Event_Type_ID=b.id" +
                            " inner join disclosure..Event_Type_Groups g on b.Event_Type_Group_ID = g.id" +
                            " inner join disclosure..Firm with(nolock) on a.Firm_id=Firm.Firm_id" +
                            " left join searchdb2..union_search us on us.ticker = case '" + ticker + "' when '' then disclosure.dbo.fn_getSkrinTicker(isNull(Firm.inn, ''), isNull(Firm.OKPO, '')) else '" + ticker + "' end " +
                            " where a.ID=" + id + " and agency_id=" + agency_id;
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        DisclosureItem item = new DisclosureItem();
                        item.FULL_NAME_RUS = rd.ReadEmptyIfDbNull("FULL_NAME_RUS").ToUpper();
                        item.Event_Type_name = rd.ReadEmptyIfDbNull("Event_Type_name");
                        item.Event_Type_Group_name = rd.ReadEmptyIfDbNull("Event_Type_Group_name");
                        item.Event_Text = rd.ReadEmptyIfDbNull("Event_Text");
                        item.SHORT_NAME_RUS = rd.ReadEmptyIfDbNull("SHORT_NAME_RUS");
                        item.ticker = ticker;
                        item.update_date = rd.ReadEmptyIfDbNull("update_date");

                        return item;
                    }
                    return null;
                }
            }
        }

        public static string GetDisclosureTypes(string ids)
        {
            if (ids == null || ids == "" || ids == "undefined")
            {
                return null;
            }

            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = ";WITH C ([ID], parent_id) AS  (SELECT B.ID, B.parent_id FROM skrin_content_output..disclosure_mt AS B WHERE B.[ID] in (" + ids + ")" +  
                        "UNION ALL  SELECT D.[ID], D.parent_id FROM skrin_content_output..disclosure_mt AS D  INNER JOIN C ON c.ID = d.parent_ID) " +
                        "Select STUFF((Select ',' + cast(c.id as varchar(10)) from C where parent_id>0 FOR XML PATH('')),1,1,'')";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        string result = rd.ReadEmptyIfDbNull(0);
                        return result;
                    }
                    return null;
                }
            }

        }
    }
}