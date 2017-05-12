using Skrin.BLL.Infrastructure;
using Skrin.Models.Iss.Content;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Skrin.BLL.Root;
using System.Text.RegularExpressions;
using Skrin.BLL.Report;


namespace Skrin.BLL.Iss
{
    public class AdditionalRepository
    {
        public static string GetReviewsTable(string XmlTable, string ticker)
        {
            XmlTable = "<root>" + XmlTable + "</root>";
            XDocument doc = XDocument.Parse(XmlTable);
            foreach (XElement el in doc.Root.Elements().Where(i => i.Name == "issReview").ToList())
            {
                var filename = string.Format(@"{0}{1}\{2}\{3}", Configs.DocPath, el.Attribute("author_id").Value, el.Attribute("doc_id").Value, el.Attribute("file_name").Value);
                string size = Utilites.GetFileSize(filename).Replace("&nbsp;", ""); ;
                el.Add(new XAttribute("size", size));
            }
            XmlTable = doc.ToString();
            return XmlTable.Replace("<root>", "").Replace("</root>", "");
        }

        private static string FileSize2Str(double fsize)
        {
            var retval = "-";
            if (fsize / 1000000000 > 1)
            {
                retval = Convert.ToString(fsize / 1024 / 1024 / 1024).Substring(0, Convert.ToString(fsize / 1024 / 1024 / 1024).IndexOf(",") + 3) + " ГБ";
            }
            else if (fsize / 1024 / 1024 > 1)
            {
                retval = Convert.ToString(fsize / 1024 / 1024).Substring(0, Convert.ToString(fsize / 1024 / 1024).IndexOf(",") + 3) + " МБ";
            }
            else if (fsize / 1024 > 1)
            {
                retval = Convert.ToString(fsize / 1024).Substring(0, Convert.ToString(fsize / 1024).IndexOf(",") + 3) + " КБ";
            }
            else
            {
                retval = Convert.ToString(fsize) + "";
            }
            return retval;
        }

        private static string _GetExtraAttrs(Dictionary<string, object> extra_params)
        {
            if (extra_params == null || extra_params.Count == 0)
            {
                return "";
            }

            List<string> ret = new List<string>();

            foreach (var par in extra_params)
            {
                ret.Add(string.Format("{0}='{1}'", par.Key, par.Value));
            }

            return string.Join(" ", ret);
        }

        public static async Task<Tuple<string, string>> GetBargainsDatesAsync(string issuer_id)
        {
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                var sql = "select convert(varchar(10),min(mind),104) as minrd, convert(varchar(10),max(maxd),104) as maxrd  from ( " +
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

        //eventMessages
        #region EventMessages
        public static async Task<string> GetEventMessage(string iss_code, string ss, string events_id)
        {
            var txt = "<div class=minicaption>КОРПОРАТИВНЫЕ СОБЫТИЯ</div><br/>";
            var events = events_id.Split(',');
            foreach (var Event in events)
            {
                if (!String.IsNullOrEmpty(Event))
                {
                    var EventsIds = await GetEventsIds(Event);
                    foreach (var item in EventsIds)
                    {
                        txt += await GetEventNewsText(item.Key, item.Value, iss_code, ss);
                    }
                }
            }
            txt += "</div>";
            txt = txt.Replace("/\"/g", "&quot;").Replace("/“/g", "&laquo;").Replace("/”/g", "&raquo;");
            return txt;
        }

        public static async Task<string> GetEventNewsText(string id, string name, string iss_code, string ss)
        {
            string retStr = "";
            string sSql = "";
            string nm = "";

            if (iss_code != "")
            {
                nm = SqlUtiltes.GetFullName(iss_code);
                if (nm.Length < 3)
                {
                    sSql = "select name + ' - '+fio as fullName from GKS_IP.dbo.GKS_IPS where id= '" + iss_code + "'";
                    nm = await GetEventName(sSql, iss_code);
                }

            }
            else
            {
                sSql = "select name " +
                    "from naufor..Issuer_Join_Events ije " +
                    "inner join searchdb2..union_search us on us.issuer_id=ije.join_id and us.type_id=ije.type_id " +
                    "where ije.event_id = '" + id + "'";
                nm = await GetEventName(sSql, id);
            }

            retStr += "<span class=bluecaption>" + nm + "</span><br/><hr/>";
            retStr += "<span class=bluecaption>" + name + "</span><br/><br/>";
            retStr += await GetEventContent(id, ss);

            return retStr;
        }

        public static async Task<string> GetEventContent(string id, string ss)
        {
            string retStr = "";
            string sSql = "select convert(varchar(10),reg_date,104) as rd,Replace(Replace(Replace(Replace(headline,char(10),''),char(13),'<br/>'),'\"','&quot;'),char(9),' ') as headline, " +
                         "Replace(Replace(Replace(Replace(cast(contents as varchar(max)),char(10),''),char(13),'<br/>'),'\"','&quot;'),char(9),' ') as contents,news_id,ns.name as source  from " +
                         "naufor..event_news_rel a inner join naufor..news b on b.id=a.news_id  " +
                         "left join naufor..News_Sources ns on ns.id=source_id where event_id='" + id + "' order by reg_date desc";
            var reg_expr = new Regex("([^ ]*" + ss + "[^ ,.!?():-=]*)", RegexOptions.IgnoreCase);

            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sSql, con);
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    retStr += "<span class=caption>" + reader["headline"].ToString() + "</span><br/>";
                    string newsText = reader["contents"].ToString().Replace("/\n/g", "<br/>");
                    if (!String.IsNullOrEmpty(ss))
                    {
                        newsText = reg_expr.Replace(newsText, "<span class=\'search_text\'>$1</span>");
                    }
                    retStr += newsText + "<hr/>" + await GetEventIssuers(reader["news_id"].ToString());// add showIssuer
                    retStr += "<span class=author>СКРИН, " + reader["rd"].ToString() + "</span><br><br>";
                }
            }
            return retStr;
        }

        public static async Task<string> GetEventIssuers(string id)
        {
            string retStr = "";
            string sql = "select ticker,Replace(short_name,'\"','&quot;') as short_name, 1 as perm   from searchdb2..union_search a inner join issuer_join_news b on " +
                "b.join_id=a.issuer_id left join searchdb2..Group_Emitent ge on ge.issuer_id=b.join_id where news_id='" + id + "'";

            try
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        retStr += "<div>" + (string)reader["short_name"] + " (<a href='#' onclick='../issuers/" + reader["ticker"] + " taget='_blank'; >" + (string)reader["ticker"] + "</a>)</div>";
                    }
                    /*
                    sql = "select id, FIO from GKS_IP.dbo.GKS_IPS join naufor.dbo.IP_Join_News on id=ip_id where news_id='" + id + "'";
                    con.Close();
                    cmd = new SqlCommand(sql, con);
                    con.Open();
                    reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        retStr += "<tr><td valign=top width=16>" + "<img src=/images/icon_information.gif width=16 height=16 border=0>" +
                         "</td><td> (<a href='#' onclick=\"openPeople('" + (string)reader["id"] + "');\">" + reader["FIO"] + "</a>)<br></td></tr>";
                    }
                     */
                }
            }
            catch (Exception ee)
            {

                throw;
            }

            return retStr;
        }

        public static async Task<string> GetEventName(string sql, string id)
        {
            string result = "";
            Dictionary<string, string> EventsIds = new Dictionary<string, string>();
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    return (string)reader[0];
                }
            }
            return result;
        }

        public static async Task<Dictionary<string, string>> GetEventsIds(string events_id)
        {
            string sql = "skrin_content_output..getEventsIDS";
            Dictionary<string, string> EventsIds = new Dictionary<string, string>();
            try
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IDs", SqlDbType.VarChar).Value = (string)events_id;
                    con.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        EventsIds.Add((string)reader["id"], (string)reader["name"]);
                    }
                }
            }
            catch (Exception ee)
            {
                EventsIds.Add("", ee.Message);
            }
            return EventsIds;
        }

        #endregion
        //---- eventMessages

        //---- newsMessages
        #region newsmessages

        public static async Task<List<string>> GetNewsIds(string events_id)
        {
            string sql = "skrin_content_output..getNewsIDS";
            List<string> EventsIds = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IDs", SqlDbType.VarChar).Value = (string)events_id;
                    con.Open();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        EventsIds.Add((string)reader["id"]);
                    }
                }
            }
            catch (Exception ee)
            {
                EventsIds.Add(ee.Message);
            }
            return EventsIds;
        }

        public static async Task<string> GetNewsMessage(string iss_code, string ss, string events_id)
        {
            var txt = "<div class=minicaption>Новости</div><br/>";
            var events = events_id.Split(',');
            foreach (var Event in events)
            {
                if (!String.IsNullOrEmpty(Event))
                {
                    var EventsIds = await GetNewsIds(Event);
                    foreach (var item in EventsIds)
                    {
                        txt += await GetNewsText(item, "", iss_code, ss);
                    }
                }
            }
            // txt += "</div>";
            txt = txt.Replace("/\"/g", "&quot;").Replace("/“/g", "&laquo;").Replace("/”/g", "&raquo;");
            return txt;
        }

        public static async Task<string> GetNewsText(string id, string name, string iss_code, string ss)
        {
            string retStr = "";
            string sSql = "";
            string nm = "";

            if (iss_code != "")
            {
                nm = SqlUtiltes.GetFullName(iss_code);
                nm = String.IsNullOrEmpty(nm) ? "" : nm;
                if (nm.Length < 3)
                {
                    sSql = "select name + ' - '+fio as fullName from GKS_IP.dbo.GKS_IPS where id= '" + iss_code + "'";
                    nm = await GetEventName(sSql, iss_code);
                }

            }
            else
            {
                sSql = "select e.name from naufor..Issuer_Join_News b with(nolock) inner join SearchDB2..union_search e with(nolock) on b.join_id=e.issuer_id and b.type_id=e.type_id" +
                        " where b.news_id='" + id + "'";
                nm = await GetEventName(sSql, id);
            }

            sSql = @" SELECT STUFF((SELECT '/'+name FROM  naufor..news_type_rel a inner join naufor..news_types  b on 
                       b.id=a.news_type_id where news_id='A187A04E22966EDD43256D5500500EA3' ORDER BY name FOR XML PATH('')),1,1,'')";
            name = await GetEventName(sSql, id);

            retStr += "<span class=bluecaption>" + nm + "</span><br/><br/>";
            retStr += "<span class=bluecaption>" + name + "</span><br/><br/>";
            retStr += await GetNewsContent(id, ss);

            return retStr;
        }

        public static async Task<string> GetNewsContent(string id, string ss)
        {
            string retStr = "";
            string sSql = "select case when exists(select 1 from naufor..News_Type_Rel with(nolock) " +
                   " where news_id = isNull(a.id, '') and news_type_id in (select id from naufor..news_types with(nolock) where lower([name]) like '%банкрот%')) " +
                  "  then '' else Replace(Replace(Replace(Replace(a.headline,char(10),''),char(13),'<br/>'),'\"','&quot;'),char(9),' ') end as headline, " +
                   "  Replace(Replace(Replace(Replace(cast(isnull(c.text,a.contents) as varchar(max)),char(10),''),char(13),'<br/>'),'\"','&quot;'),char(9),' ') " +
                  "    as contents, Replace(b.name,'\"','&quot;') as name, convert(varchar(10), a.reg_date, 104)+' '+convert(varchar(5), a.reg_date, 114), " +
                   "   case when not c.ID is null then 1 else 0 end as isHTML from naufor..News a left join naufor..News_Sources b on a.source_id=b.id  " +
                   "   left join dkk..FD726 c on 'DKK'+Right('0000000000'+ cast(c.ID as varchar(10)),10) = a.id where a.id='" + id + "'";
            var reg_expr = new Regex("([^ ]*" + ss + "[^ ,.!?():-=]*)", RegexOptions.IgnoreCase);

            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sSql, con);
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    retStr += "<span class=\"caption\">" + reader["headline"].ToString() + "</span><br/>";
                    string newsText = reader["contents"].ToString().Replace("/\n/g", "<br/>");
                    if (!String.IsNullOrEmpty(ss))
                    {
                        newsText = reg_expr.Replace(newsText, "<span class=\"search_text\">$1</span>");
                    }
                    retStr += newsText + "<hr/>" + await GetEventIssuers(id);// add showIssuer
                    retStr += "<span class=\"author\">СКРИН, " + reader[3].ToString() + "</span><br><br>";
                }
            }
            return retStr;
        }

        #endregion


        public static async Task<string> GetAuditorsClients(string aud_id)
        {
            string sHtml = "<table width=\"100%;\" style=\"border-collapse: collapse;\">";
            var clrs = new string[] { "#FFFFFF", "#F0F0F0" };
            int i = 0;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                var sql = "select ticker, short_name from searchdb2..union_search a inner join " +
                " naufor..Issuer_Auditors b on a.issuer_id=b.issuer_id " +
                " inner join (Select issuer_id,max(year) as y from naufor..issuer_auditors where auditor_id='" + aud_id + "' group by issuer_id) c " +
                " on c.issuer_id=b.issuer_id and b.year=c.y " +
                " where b.auditor_id='" + aud_id + "' order by so,bones";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (await rd.ReadAsync())
                    {
                        i++;
                        string background = i % 2 == 0 ? clrs[0] : clrs[1];
                        sHtml += "<tr style=\"background:" + background + "\"><td style=\"border:1px solid gray;padding:3px;\" align=\"right\">" + i + "</td><td style=\"border:1px solid gray;padding:3px;\"><a href=\"#\" onclick=\"openIssuer('" + (string)rd["ticker"] + "')\">" + (string)rd["short_name"] + "</a></td></tr>";
                    }
                }
                sHtml += "</table>";
            }
            return sHtml;
        }

        public static async Task<string> GetRegisterMessage(string reg_id, int reg_type)
        {
            string sHtml = "<table width=\"100%;\" style=\"border-collapse: collapse;\">";
            var clrs = new string[] { "#FFFFFF", "#F0F0F0" };
            int i = 0;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                var sql = "";
                switch (reg_type)
                {
                    case 0:
                        sql = "select ISNULL(ticker,'')ticker, ISNULL(short_name,'')short_name from searchdb2..union_search a inner join naufor..Issuer_Reg_Holders b on a.issuer_id=b.issuer_id where b.reg_holder_id='" + reg_id + "' order by so,bones";
                        break;
                    case 1:
                        sql = "select ISNULL(ticker,'')ticker, ISNULL(short_name,'')short_name  from dkk_new..fd708 a inner join naufor..issuer_joins b on cast(a.issuerID as varchar(8))=b.id  " +
                         "inner join searchdb2..union_search c on c.issuer_id=isnull(b.parent_id,b.id) " +
                         "where regID=" + reg_id + " order by so,bones";
                        break;
                }

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    while (await rd.ReadAsync())
                    {
                        i++;
                        string background = i % 2 == 0 ? clrs[0] : clrs[1];
                        sHtml += "<tr style=\"background:" + background + "\"><td style=\"border:1px solid gray;padding:3px;\" align=\"right\">" + i + "</td><td style=\"border:1px solid gray;padding:3px;\"><a href=\"#\" onclick=\"openIssuer('" + (string)rd["ticker"] + "')\">" + (string)rd["short_name"] + "</a></td></tr>";
                    }
                }
                sHtml += "</table>";
            }
            return sHtml;
        }

        private static async Task<string> ActionXmls(string id, string ticker, int type_id, string xslpath)
        {
            var short_name = SqlUtiltes.GetShortName(ticker);
            string sqlName = "";
            string contentName = "";
            switch (type_id)
            {
                case 1:
                    sqlName = "[actions]";
                    contentName = "@Det";
                    break;
                case 2:
                    sqlName = "bonds";
                    contentName = "@Det";
                    break;
                case 3:
                    sqlName = "[getADRs]";
                    contentName = "@is_details";
                    break;
                case 6:
                    sqlName = "yield";
                    contentName = "@oper";
                    break;
            }
            string sHtml = "";
            XSLGenerator g = new XSLGenerator("skrin_content_output.." + sqlName, new Dictionary<string, object> { { "@iss", id }, { contentName, 1 } },
                 "tab_content/" + xslpath, new Dictionary<string, object> { { "iss", ticker }, { "iss_name", short_name }, { "PDF", -1 } });
            string xml =  await g.GetXmlAsync();
            if ((type_id == 1) || (type_id == 2))//Облигации и акции
            {
                var XmlTable = "<root>" + xml + "</root>";
                XDocument doc = XDocument.Parse(XmlTable);
                foreach (XElement el in doc.Root.Elements().Where(i => i.Name == "issues").ToList())
                {
                    //get docs          
                    XElement root = new XElement("docs");
                    using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                    {
                        var sql = "skrin_content_output..action_docs '" + id + "'";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        con.Open();
                        using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                        {
                            while (await rd.ReadAsync())
                            {
                                var filename = string.Format(@"{0}{1}\{2}\{3}", Configs.DocPath, (string)rd["issuer_id"], (string)rd["doc_id"], (string)rd["file_name"]);
                                string size = Utilites.GetFileSize(filename).Replace("&nbsp;", "");
                                var ext = Path.GetExtension(filename);
                                sHtml += "<doc doc_id=\"" + (string)rd["doc_id"] + "\" doc_name=\"" + (string)rd["name"] + "\" file_name=\"" +
                                    (string)rd["file_name"] + "\" size=\"" + size + "\" ext=\"" + ext + "\"/>";
                                XElement inroot = new XElement("doc");
                                inroot.Add(new XAttribute("doc_id", (string)rd["doc_id"]));
                                inroot.Add(new XAttribute("doc_name", (string)rd["name"]));
                                inroot.Add(new XAttribute("file_name", (string)rd["file_name"]));
                                inroot.Add(new XAttribute("size", size));
                                inroot.Add(new XAttribute("ext", ext));
                                root.Add(inroot);
                            }
                        }
                    }
                    el.Add(root);
                    for (int i = 0; i < 2; i++)
                    {
                        string name = (i == 0) ? "states" : "repayment";
                        root = new XElement(name);
                        using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                        {
                            var sql = "skrin_content_output..action_lists '" + id + "'," + i;
                            SqlCommand cmd = new SqlCommand(sql, con);
                            con.Open();
                            using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                            {
                                while (await rd.ReadAsync())
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            sHtml += "<state data=\"" + (string)rd["rd"] + "\" state_name=\"" + (string)rd["name"] + "\"/>";
                                            XElement inroot = new XElement("state");
                                            inroot.Add(new XAttribute("data", (string)rd["rd"]));
                                            inroot.Add(new XAttribute("state_name", (string)rd["name"]));
                                            root.Add(inroot);
                                            break;
                                        case 1:
                                            sHtml += "<redemptions data=\"" + (string)rd["rd"] + "\" shares=\"" + (string)rd["shares"] + "\"/>";
                                            inroot = new XElement("redemptions");
                                            inroot.Add(new XAttribute("doc_id", (string)rd["rd"]));
                                            inroot.Add(new XAttribute("doc_name", (string)rd["shares"]));
                                            root.Add(inroot);
                                            break;
                                    }
                                }
                            }
                        }
                        el.Add(root);
                    }
                }
                XmlTable = doc.ToString().Replace("<root>", "").Replace("</root>", "");
                xml = XmlTable;
            }         
            return xml;
        }

        public static async Task<string> ActionSkrinXmlAsync(string id, string ticker,int type_id)
        {
            string ret = "";
            string xslpath = "";
            switch (type_id)
            {
                case 1:
                    xslpath = "actions/action";
                    break;
                case 2:
                    xslpath = "bonds/bond";
                    break;
                case 3:
                    xslpath = "adr/adr";
                    break;
                case 6  :
                    xslpath = "yield/yield";
                    break;
            }
            var ids = id.Split(',');    
            var short_name = SqlUtiltes.GetShortName(ticker);
            foreach (var iIds in ids)
            {
                if (!String.IsNullOrEmpty(iIds))
                {
                    var xml = await ActionXmls(iIds, ticker, type_id,xslpath);
                    var extras = _GetExtraAttrs(new Dictionary<string, object> { { "iss", ticker }, { "iss_name", short_name }, { "PDF", -1 } });
                    HTMLCreator creator = new HTMLCreator("tab_content/" + xslpath);
                    ret += creator.GetHtml(string.Format("<?xml version=\"1.0\" encoding=\"WINDOWS-1251\"?><iss_profile {1}><profile  xmlns:sql='urn:schemas-microsoft-com:xml-sql'>{0}</profile></iss_profile>", xml, extras));
                }
         }
            return ret;
        }

        public static async Task<string> GetPassport(string num)
        {
            string sHtml = "";
            if (num != "" && num.Length > 4)
            {
                sHtml = "Источник данных: Главное управление по вопросам миграции МВД России<br/>Дата выгрузки результатов поиска " + DateTime.Now.ToShortDateString();
                num = num.Replace(" ", "");
                string series = num.Substring(0, 4);
                string number = num.Substring(4);
                using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
                {
                    var sql = "select 1 from OpenData..Passport where series='" + series + "' and number='" + number + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        if (await rd.ReadAsync())
                        {
                            return (sHtml + "<div style=\"padding-top:20px;padding-bottom:30px; text-align:justify;\">Паспорт физического лица - гражданина РФ (серия/номер) " + series + " " + number + " <span style=\"color:#bf0000;font-weight:bold;\">присутствует</span> в списке недействительных (утраченных (похищенных), оформленных на утраченных (похищенных) бланках паспорта гражданина Российской федерации, выданных в нарушении установленного порядка, а также признанных недействительными) паспортов граждан Российской Федерации, удостоверяющих личность гражданина Российской Федерации на территории Российской Федерации.</div>");
                        }
                        return (sHtml + "<div style=\"padding-top:20px;padding-bottom:30px;text-align:justify;\">Паспорт физического лица - гражданина РФ (серия/номер) " + series + " " + number + " <span style=\"color:#bf0000;font-weight:bold;\">отсутствует</span> в списке недействительных (утраченных (похищенных), оформленных на утраченных (похищенных) бланках паспорта гражданина Российской федерации, выданных в нарушении установленного порядка, а также признанных недействительными) паспортов граждан Российской Федерации, удостоверяющих личность гражданина Российской Федерации на территории Российской Федерации.</div>");
                    }
                }
            }

            return sHtml;
        }
    }
}