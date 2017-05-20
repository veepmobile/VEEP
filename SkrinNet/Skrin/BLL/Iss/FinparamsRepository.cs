using Skrin.BLL.Infrastructure;
using Skrin.BLL.Report;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Skrin.BLL.Iss
{
    public class FinparamsRepository
    {
        private static async Task<int> getIssClass(string ticker)
        {
            int iss_class = 0;
            string sql = "";
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                sql = "select top 1 1 ex from naufor..v_CbrAnnualUnits_f1_f2 cbr " +
                "where regnum in (select top 1 reg_no from naufor..issuers where rts_code='" + ticker + "' and class_id=1)";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (await rd.ReadAsync())
                    {
                        iss_class = 1;
                    }
                }
            }

            if (iss_class == 0)
            {
                sql = "select top 1 1 " +
                "from naufor..fksm_insurer_accs a " +
                "inner join searchdb2..union_search i with(nolock) on a.inn=i.inn and a.ogrn=i.ogrn and uniq=1 " +
                "where ticker = '" + ticker + "'";
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            iss_class = 2;
                        }
                    }
                }
            }

            return iss_class;
        }

        public static async Task<string> getRsbuXml(string ticker, string per, string fo_no, string curr, string fn,string isXLS)
        {
            int iss_class = await getIssClass(ticker);            
            string xslQuery = "";
            Dictionary<string, object> prms = new Dictionary<string, object>(){ {"@iss",ticker},{"@periods",per},{"@form_no",fo_no},{"@curr",curr},{"@ext_data",fn}};
            Dictionary<string, object> outparams = new Dictionary<string, object>() { { "iss", ticker }, { "PDF", -1 }, { "id", 49 } };
            switch (iss_class)
            {
                case 1:
                    xslQuery = "skrin_content_output..finparams_rsbu2_bank2 ";              
                    break;
                case 2:
                    xslQuery = "skrin_content_output..finparams_rsbu2_insurer ";                    
                    break;
                default:
                    xslQuery = "skrin_content_output..finparams_rsbu3 ";
                    prms = new Dictionary<string, object>() { { "@iss", ticker }, { "@periods", per }, { "@form_no", "1,2,3,5" }, { "@curr", curr }, { "@ext_data", fn } };
                    break;
            }

            XSLGenerator g = new XSLGenerator(xslQuery, prms, "tab_content/finparams/rsbu/" + (isXLS == "1" ? "xlsjson" : "templ"), outparams);            
            return await g.GetResultAsync();
        }
    }
}