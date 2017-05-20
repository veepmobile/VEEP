using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Skrin.Models.Iss.Content;
using Skrin.Models.Bankrot;

namespace Skrin.BLL.Messages
{
    public class BankrotRepository
    {
        public static BankrotItem GetBankruptcytMsg(string id)
        {
            if (id == null || id == "")
            {
                return null;
            }
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "select contents, convert(varchar(10), a.reg_date, 104) as reg_date,b.name as source,inn,ogrn from naufor..Bankruptcy2 a inner join naufor..Skrin_Sources b on skrin_source_id=b.id left join naufor..Bankruptcy2_join c on c.Bankruptcy_id=a.id where a.id ='" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (rd.Read())
                    {
                        BankrotItem item = new BankrotItem();
                        item.id = id;
                        item.reg_date = rd.ReadEmptyIfDbNull("reg_date");
                        item.contents = rd.ReadEmptyIfDbNull("contents");
                        item.source = rd.ReadEmptyIfDbNull("source");
                        item.inn = rd.ReadEmptyIfDbNull("inn");
                        item.ogrn = rd.ReadEmptyIfDbNull("ogrn");

                        return item;
                    }
                    return null;
                }
            }
        }

    }
}