using Skrin.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Skrin.Models.Iss.Content;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Skrin.BLL.Iss
{
    public class FedresursRepository
    {
        private static string ClearHtml(string html)
        {
            var matches = Regex.Matches(html, "<pre(.*?)pre>");
            foreach (Match m in matches)
            {
                XElement elemPre = XElement.Parse(m.Value);
                var reader = elemPre.CreateReader();
                reader.MoveToContent();
                html = html.Replace(m.Value, reader.ReadInnerXml());
            }

            XElement elem = XElement.Parse(html);

            var elem3 = elem.XPathSelectElements("//a[@id=\"ctl00_MainContent_ucMessageView_lnkDocument\"]");
            foreach (var e in elem3)
                e.Remove();

            elem3 = elem.XPathSelectElements("//div[@id=\"ctl00_MainContent_ucMessageView_ucDocumentsPopup\"]");
            foreach (var e in elem3)
                e.Remove();

            foreach (XElement elem2 in elem.XPathSelectElements("//b"))
                elem2.Name = "span";
            foreach (XElement elem2 in elem.XPathSelectElements("//a"))
                elem2.Name = "span";

            string[] s = { 
                "style=\"border-bottom: solid 1px Blue; height: auto;\"",
                "style=\"float: left; height: auto;\"",
                "xmlns:ms=\"urn:schemas-microsoft-com:xslt\"",
                "xmlns:fn=\"http://www.w3.org/2005/02/xpath-functions\"",
                "xmlns:dt=\"http://www.interfax.ru/significantevents\"",
                "class=\"bluetr\"",
                "id=\"ctl00_MainContent_ucMessageView_ltrAnnulment\"",
                "class=\"marginLeft10\"",
                "id=\"ctl00_MainContent_ucMessageView_msgAdditionalInfo\"",
                "id=\"ctl00_MainContent_ucMessageView_ucDocumentsPopup\"",
                "id=\"ctl00_MainContent_ucMessageView_divNotary\" style=\"margin-top: -10px;\""};
            StringBuilder sb = new StringBuilder(elem.ToString());
            foreach (string ss in s)
                sb.Replace(ss, "");
            sb.Replace("class=\"mesview\"", "style=\"width: 100%;\"");

            return sb.ToString();
        }

        public static async Task<FedresursMessageItem> GetFedresursMessage(string id, CompanyData company)
        {
            FedresursMessageItem message = new FedresursMessageItem();
            int hv = 0;

                try
                {
                    using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                    {
                        string sql = "Select cast(FORMAT(n_mes, '00000000') as varchar(20)) as n_mes,company,adress_egr,inn,ogrn,convert(varchar(10),pub_date,104) as dt,text_mes,xml_text,d.name as tname, case when exists(Select 1 from naufor..Fedresurs_messages_values2 v where v.n_mes=a.n_mes) then 1 else 0 end as hv from naufor..Fedresurs_messages2 a inner join naufor..Fedresurs_messages_types2 d on d.type=a.type where n_mes=@id";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.Add("@id", SqlDbType.VarChar, 32).Value = id;
                            con.Open();
                            SqlDataReader rd = await cmd.ExecuteReaderAsync();
                            if (await rd.ReadAsync())
                            {
                                message.MessNum = rd.ReadEmptyIfDbNull("n_mes");
                                message.CompanyName = (rd["company"] != DBNull.Value) ? rd.ReadEmptyIfDbNull("company") : company.SearchedName;
                                if (string.IsNullOrEmpty(message.CompanyName))
                                    message.CompanyName = "";
                                message.INN = (rd["inn"] != DBNull.Value) ? rd.ReadEmptyIfDbNull("inn") : company.INN;
                                message.OGRN = (rd["ogrn"] != DBNull.Value) ? rd.ReadEmptyIfDbNull("ogrn") : company.OGRN;
                                message.Address = rd.ReadEmptyIfDbNull("adress_egr");
                                message.ShowPubDate = rd.ReadEmptyIfDbNull("dt");
                                message.TypeName = rd.ReadEmptyIfDbNull("tname");
                                message.Contents = rd.ReadEmptyIfDbNull("text_mes");
                                message.HtmlTable = ClearHtml(rd.ReadEmptyIfDbNull("xml_text"));
                                //hv = (rd["hv"] != DBNull.Value) ? (int)rd["hv"] : 0;
                            }
                            rd.Close();
//                            if (hv == 1)
//                            {
//                                //sql = "Select b.name,a.value from naufor..Fedresurs_messages_values2 a inner join naufor..Fedresurs_messages_value_type2 b on a.type_id=b.type_id  where n_mes=@id";
//                                sql = @"Select a.name, replace(ValueNames, char(10), '<br/>') as value
//from naufor..Fedresurs_messages_value_type2 a
//   CROSS APPLY ( SELECT b.value + char(10)
//                 FROM naufor..Fedresurs_messages_values2 b
//                 WHERE a.type_id=b.type_id and b.n_mes=@id
//                 ORDER BY b.id
//                 FOR XML PATH('') )  D ( ValueNames )
//where ValueNames is not null";
//                                SqlCommand cmd2 = new SqlCommand(sql, con);
//                                cmd2.Parameters.Add("@id", SqlDbType.VarChar, 32).Value = id;
//                                SqlDataReader rd2 = await cmd2.ExecuteReaderAsync();
//                                message.MessValues = new List<MessageValue>();
//                                while (await rd2.ReadAsync())
//                                {
//                                    message.MessValues.Add(new MessageValue()
//                                    {
//                                        name = rd2.ReadEmptyIfDbNull("name"),
//                                        value = rd2.ReadEmptyIfDbNull("value")
//                                    });
//                                }
//                            }
                     }
                }
                catch(Exception e)
                {
                    return null;
                }
                return message;
        }
    }
}