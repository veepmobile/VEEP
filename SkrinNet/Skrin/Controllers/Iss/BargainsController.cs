using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.Models;
using Skrin.BLL;
using Skrin.BLL.Root;
using System.Data.SqlClient;
using System.Data;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Content;
using Skrin.BLL.Iss;

namespace Skrin.Controllers.Iss
{
    public class BargainsController : BaseController
    {
        public async Task<ActionResult> GetMessageDatesAsync(string issuer_id)
        {
            string dStart = "";
            string dEnd = "";
            string sql = "select convert(varchar(10),min(mind),104) as minrd, convert(varchar(10),max(maxd),104) as maxrd  from ( " +
                "(select min(reg_date) as mind, max(reg_date) as maxd  " +
                "from (	naufor..Issuer_Docs c   " +
                "	inner join naufor..Issuer_Doc_Types2 f on f.doc_type_id=c.doc_type_id and f.id=c.doc_type2_id   " +
                "	inner join naufor..Docs_New d on c.doc_id=d.id  " +
                "	inner join naufor..Issuer_Doc_types IDT on IDT.group_id=1 and f.doc_type_id=IDT.id)   " +
                "	left join naufor..Doc_Pages_New dp on d.id=dp.doc_id AND (dp.no=0 or dp.no is null or dp.no=1)  " +
                "	where issuer_id='" + issuer_id + "')  " +
                "	union all  " +
                "select min(reg_date) as mind, max(reg_date) as maxd  from news a inner join Issuer_Join_News b " +
                "on a.id=b.news_id where join_id='" + issuer_id + "') d";

            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {        
                SqlCommand cmd = new SqlCommand(sql, con);             
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    dStart = (string)reader["minrd"];
                    dEnd = (string)reader["maxrd"];
                    return Json(new {dStart=dStart, dEnd=dEnd });               
                }
                return null;
            }
        }

        public async Task<ActionResult> GetMessageTypesAsync(string issuer_id)
        {
            List<Bargain_types> types = new List<Bargain_types>();
            string sql = "select distinct f.id,f.name from naufor..Issuer_Docs c inner join skrin_content_output..bargains_types f on f.id=c.doc_type_id*1000+c.doc_type2_id where issuer_id= '"+issuer_id+"'";           
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    types.Add(new Bargain_types() { id = (Int32)reader["id"], name = (string)reader["name"] });             
                }
                return Json(types);
            }
        }

        public async Task<ActionResult> BargainsSearchAsync(BargainsSearch BS)
        {
            BargainSearchResult res = new BargainSearchResult();
            res.Items = new List<BargainSearchItem>();
            string sql = "skrin_content_output..bargains4 '{0}','{1}','{2}','{3}',{4},20,0";
            sql = string.Format(sql,BS.iss,BS.types,BS.dfrom,BS.dto,BS.page);
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    res.Items.Add(new BargainSearchItem() {
                        id=(string)reader["id"],
                        file_name = reader["file_name"] != DBNull.Value ?  (string)reader["file_name"] : "",
                        name = (string)reader["name"],                      
                        reg_date = (string)reader["rd"],
                        total = (string)reader["total"]
                    });
                }              
            }
            return Json(res);
        }

        public async Task<ActionResult> GetBarg(string ticker,string issuer_id, string id,string fn)
        {
            return Content(await GetBargMsg(ticker, issuer_id, id, fn));
        }

        public async Task<string> GetBargMsg(string ticker,string issuer_id, string id,string fn)
        {          
            var path = Configs.DocPath + "\\" + issuer_id + '\\' + id + '\\' + fn;
            string header = "";
            string FileText = "";
            string name = SqlUtiltes.GetFullName(ticker);
            string bargTypeName = await GetBargTypeNameAsync(id);
            if (System.IO.File.Exists(path))
            {
                FileText = System.IO.File.ReadAllText(path, System.Text.Encoding.Default);
                header = "<div class=\"minicaption\">СООБЩЕНИЯ, РАСКРЫТИЕ КОТОРЫХ ПРЕДУСМОТРЕНО ЗАКОНОДАТЕЛЬСТВОМ О ЦЕННЫХ   БУМАГАХ</div><br/><div class=\"bluecaption\">" + name + "</div><br/><hr/>" +
                   "<span class=\"bluecaption\">" + bargTypeName + "</span></br>";
                var bg = await GetBargGroupNameAsync(id);
                header += "<br/><table width=\"100%\"><tr><td style=\"width:100px;font-weight:bold;\">Группа событий</td><td>" + bg.Item2 + "</td></tr><tr><td style=\"width:100px;font-weight:bold;\">Тип события</td><td>" + bg.Item1 + "</td></tr></table>";
                FileText = header + FileText;
                FileText += "<hr/><div>" + SqlUtiltes.GetShortName(ticker) + "(<a onclick=\"openIssuer('" + ticker + "');\" href=\"#\">" + ticker + "</a>)</div>";
                FileText += "<span class=\"author\">СКРИН," + await GetBargDateAsync(id) + "</span><hr/>";
            }
            return FileText;
        }

        public static async Task<string> GetBargTypeNameAsync(string doc_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "select NAME from skrin_content_output..bargains_types where id=(Select doc_type_id*1000+doc_type2_id from naufor..Issuer_Docs where doc_id='" + doc_id + "')";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return (string)rd[0];
                    }
                    return null;
                }
            }
        }

        public static async Task<Tuple<string,string>> GetBargGroupNameAsync(string doc_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "select a.name as btype,b.name as bgroup from skrin_content_output..bargains_types a inner join skrin_content_output..bargains_types b on a.parent_id=b.id where a.id=(Select doc_type_id*1000+doc_type2_id from naufor..Issuer_Docs where doc_id='" + doc_id + "')";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return (new Tuple<string,string>((string)rd[0],(string)rd[1]));
                    }
                    return null;
                }
            }
        }

        public static async Task<string> GetBargDateAsync(string doc_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                string sql = "Select convert(varchar(10),reg_date,104) as rd from naufor..issuer_docs where doc_id='" + doc_id + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await rd.ReadAsync())
                    {
                        return (string)rd[0];
                    }
                    return null;
                }
            }
        }

        public async Task<ActionResult> GetMessage(string issuser_id,string ss, string events_id)
        {
            string newsText = await AdditionalRepository.GetNewsMessage(issuser_id, ss, events_id);
            return Content(newsText);
        }

        public async Task<ActionResult> GetSelectedMessages(string ids, string IsBargs,string iss_code,string ticker, string fns)
        {
            var idsLst = ids.Split(',');
            var bargsLst = IsBargs.Split(',');
            var fnames = fns.Split(',');
            var res = "";

            for (int i = 0; i < idsLst.Length; i++)
            {
                switch (bargsLst[i])
                {
                    case "0":
                        res += await AdditionalRepository.GetNewsMessage(ticker, "", idsLst[i]);
                        break;
                    case "1":
                        res += await GetBargMsg(ticker, iss_code, idsLst[i], fnames[i]);
                        break;
                }
            }
           
            return Content(res);
        }

    }
}