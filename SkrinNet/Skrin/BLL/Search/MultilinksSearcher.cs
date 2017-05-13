using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skrin.Models.Search;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Search;
using Skrin.BLL.Root;
using Skrin.Models.Authentication;

namespace Skrin.BLL.Search
{
    public class MultilinksSearcher
    {
        string _constring = Configs.ConnectionString;
        int MaxProfCount = 100;
        int CountUL = 0;
        int CountFL = 0;
        int SearchCountUL = 0;
        int SearchCountFL = 0;
        int variant_limit = 5;
        List<ProfData> prof_list = new List<ProfData>();
        List<ChainData> chain_list = new List<ChainData>();
        Neo4jClient neo = new Neo4jClient();
        string codes;
        int listid;

        public async Task<JObject> GetLinks(string acodes, int alistid)
        {
            codes = acodes;
            listid = alistid;

            await DoSearch();

            JObject res = new JObject();
            JsonSerializer serializer = new JsonSerializer();
            JArray list = new JArray();
            for (int i = 0; i < chain_list.Count(); i++)
            {
                ChainData chain = chain_list[i];
                JObject jo = JObject.Parse(JsonConvert.SerializeObject(chain));
                list.Add(jo);
            }
            res.Add("chain", list);
            res.Add("countul", CountUL);
            res.Add("search_countul", SearchCountUL);
            res.Add("countfl", CountFL);
            res.Add("search_countfl", SearchCountFL);

            return (res);
        }
        private async Task DoSearch()
        {
            List<string> code1 = new List<string>(); // ЮЛ 
            List<string> code2 = new List<string>(); // ФЛ с ИНН
            List<string> code3 = new List<string>(); // ФЛ без ИНН
            Dictionary<string, string> codefl = new Dictionary<string, string>();
            JObject res;

            if (listid != 0)
            {
                SqlConnection con = new SqlConnection(_constring);
                SqlCommand cmd = new SqlCommand("SELECT a.id, u.ticker " +
                    "FROM security.dbo.secUserListItems_join a " +
                    " left join searchdb2..union_search u on u.issuer_id = a.issuerid " +
                    "WHERE a.listid=" + listid +
                    "ORDER BY a.id", con);

                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (rd.ReadEmptyIfDbNull("ticker") != "")
                    {
                        code1.Add(rd.ReadEmptyIfDbNull("ticker").ToUpper());
                    }
                }
                con.Close();
            }

            if (codes != "")
            {
                string codeUL = "";
                string[] codes_arr = codes.Split(',');
                List<string> fl_arr = new List<string>();
                string fl_inn = "";
                for (int i = 0; i < codes_arr.Length; i++)
                {
                    string code = codes_arr[i].Trim().ToUpper();
                    if (code != "")
                    {
                        long l;
                        if (code.IndexOf(" ") < 0 && ((((code.Length == 10) || (code.Length == 13)) && (long.TryParse(code, out l))) || IsTicker(code)))
                        {
                            codeUL += (codeUL == "" ? "" : ",") + "'" + code + "'";
                        }
                        else
                        {
                            if (code.Length == 12 && long.TryParse(code, out l))
                                fl_inn += (fl_inn == "" ? "" : ",") + "'" + code + "'";
                            else
                                fl_arr.Add(code);
                        }
                    }
                }

                if (codeUL != "")
                {
                    SqlConnection con = new SqlConnection(_constring);
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT ticker FROM (" +
                        "SELECT u.ticker " +
                        "FROM searchdb2..union_search u " +
                        "WHERE u.ticker in (" + codeUL + ") " +
                        "union all " +
                        "SELECT u.ticker " +
                        "FROM searchdb2..union_search u " +
                        "WHERE u.ul2_ogrn in (" + codeUL + ") " +
                        "union all " +
                        "SELECT u.ticker " +
                        "FROM searchdb2..union_search u " +
                        "WHERE u.inn in (" + codeUL + ") and u.uniq_inn=1) as TMP ", con);

                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        if (code1.IndexOf(rd.ReadEmptyIfDbNull("ticker").ToUpper()) < 0)
                        {
                            code1.Add(rd.ReadEmptyIfDbNull("ticker").ToUpper());
                        }
                    }
                    con.Close();
                }

                if (fl_inn != "")
                {
                    res = SearchFLByINN_neo(neo, fl_inn);
                    if (res != null)
                    {
                        int n = (int)res["results"][0]["data"].Count();
                        for (int i = 0; i < n; i++)
                        {
                            if (code2.IndexOf((string)res["results"][0]["data"][i]["row"][0]) < 0)
                            {
                                code2.Add((string)res["results"][0]["data"][i]["row"][0]);
                            }
                        }
                    }
                }

                for (int i = 0; i < fl_arr.Count(); i++)
                {
                    string s = fl_arr[i].Trim().ToUpper();
                    res = SearchFLByFIO_elastic(s);
                    if (res != null)
                    {
                        int n = (int)res["hits"]["hits"].Count();
                        for (int j = 0; j < n; j++)
                        {
                            string flid = ((string)res["hits"]["hits"][j]["_source"]["fio"]).ToUpper().Replace(" ", "_");
                            if (code3.IndexOf(flid) < 0)
                                code3.Add(flid);
                            /*if ((string)res["hits"]["hits"][j]["_source"]["inn"] == "")
                            {
                                if (code3.IndexOf((string)res["hits"]["hits"][j]["_source"]["FLID"]) < 0)
                                {
                                    code3.Add((string)res["hits"]["hits"][j]["_source"]["FLID"]);
                                    codefl.Add((string)res["hits"]["hits"][j]["_source"]["FLID"], s);
                                }
                            }
                            else
                            {
                                if (code2.IndexOf((string)res["hits"]["hits"][j]["_source"]["FLID"]) < 0)
                                {
                                    code2.Add((string)res["hits"]["hits"][j]["_source"]["FLID"]);
                                }
                            }*/
                        }
                    }
                }
            }

            List<string> code4 = new List<string>(); // список для поиска
            CountUL = code1.Count();
            CountFL = code2.Count() + code3.Count();
            for (int i = 0; i < code1.Count(); i++)
            {
                if (code4.Count() >= MaxProfCount) break;
                code4.Add(code1[i]);
                SearchCountUL++;
            }
            for (int i = 0; i < code2.Count(); i++)
            {
                if (code4.Count() >= MaxProfCount) break;
                code4.Add(code2[i]);
                SearchCountFL++;
            }
            for (int i = 0; i < code3.Count(); i++)
            {
                if (code4.Count() >= MaxProfCount) break;
                code4.Add(code3[i]);
                SearchCountFL++;
            }

            List<Task<JObject>> tasks = new List<Task<JObject>>();
            //if (code4.Count() > 50) variant_limit = 5;
            for (int i = 0; i < code4.Count(); i++)
            {
                for (int j = i + 1; j < code4.Count(); j++)
                {
                    bool flag = true;
                    // физики с одним источником
                    if (codefl.ContainsKey(code4[i]) && codefl.ContainsKey(code4[j]))
                        if (codefl[code4[i]] == codefl[code4[j]]) flag = false;

                    if (flag)
                    {
                        int c = 0;
                        int first_task = 0;
                        for (int k = 0; k < tasks.Count(); k++)
                        {
                            if (!tasks[k].IsCompleted)
                            {
                                if (c == 0) first_task = k;
                                c++;
                            }
                        }
                        if (c >= 4) await tasks[first_task];

                        tasks.Add(SearchMultiLinkNeo(code4[i], code4[j], variant_limit));
                    }
                }
            }

            for (int t = 0; t < tasks.Count; t++)
            {
                res = await tasks[t];

                Dictionary<ChainData, int> d = new Dictionary<ChainData, int>();
                int nn = (int)res["results"][0]["data"].Count();
                for (int i = 0; i < nn; i++)
                {
                    int v = 0;
                    JObject item = (JObject)res["results"][0]["data"][i];

                    //связи только по ФИО не берем
                    if (((int)item["graph"]["relationships"].Count() == 1) && ((int)item["graph"]["relationships"][0]["properties"]["LinkType"] == 6)) continue;
                    if ((int)item["graph"]["relationships"].Count() == 2) if (((int)item["graph"]["relationships"][0]["properties"]["LinkType"] == 6) && ((int)item["graph"]["relationships"][1]["properties"]["LinkType"] == 6)) continue;

                    ChainData cd = new ChainData();
                    int n;

                    int direction = 0;
                    string nodeid = "";
                    int l = (int)item["row"][0].Count();
                    for (int j = 0; j < l; j++)
                    {
                        if ((string)item["meta"][0][j]["type"] == "node")
                        {
                            nodeid = (string)item["meta"][0][j]["id"];
                            int m = (int)item["graph"]["nodes"].Count();
                            n = 0;
                            for (int k = 0; k < m; k++)
                            {
                                if ((string)item["graph"]["nodes"][k]["id"] == nodeid)
                                {
                                    n = GetProfileJson((JObject)item["graph"]["nodes"][k]["properties"]);
                                    break;
                                }
                            }
                            if ((j == 0) || (j == l - 1)) prof_list[n].is_src = true;
                            cd.prof.Add(prof_list[n]);
                            cd.chain1 = cd.chain1 + "[" + n + "]";
                            cd.chain2 = "[" + n + "]" + cd.chain2;
                        }
                        else
                        {
                            int m = (int)item["graph"]["relationships"].Count();
                            n = 0;
                            for (int k = 0; k < m; k++)
                            {
                                if ((string)item["graph"]["relationships"][k]["id"] == (string)item["meta"][0][j]["id"])
                                {
                                    n = k;
                                    break;
                                }
                            }
                            LinkData link = LoadLinkJson((JObject)item["graph"]["relationships"][n]["properties"]);
                            if ((string)item["graph"]["relationships"][n]["startNode"] == (string)item["meta"][0][j - 1]["id"])
                                link.direction = 1;
                            else
                                link.direction = 2;
                            cd.link.Add(link);
                            cd.chain1 = cd.chain1 + "<" + link.LinkType + ">";
                            cd.chain2 = "<" + link.LinkType + ">" + cd.chain2;

                            if (direction != link.direction && direction != 0) v++;
                            direction = link.direction;
                        }
                    }

                    bool error = false;
                    l = cd.prof.Count();
                    for (int j = 0; j < l; j++)
                    {
                        ProfData prof = cd.prof[j];
                        if (prof.ProfType == 2)
                        {
                            if (j == 0)
                            {
                                LinkData link = cd.link[0];
                                if (link.LinkType != 6)
                                {
                                    string inn = link.inn;
                                    if ((inn != "") && (prof.fl.inn == "")) prof = prof_list[GetProfileFL(prof.fl.fio.ToUpper().Replace(" ", "_") + "_" + inn, prof.fl.fio, inn)];
                                    else link.tmplink = true;
                                    prof.is_src = true;
                                }
                            }

                            if (j == l - 1)
                            {
                                LinkData link = cd.link[l - 2];
                                if (link.LinkType != 6)
                                {
                                    string inn = link.inn;
                                    if ((inn != "") && (prof.fl.inn == "")) prof = prof_list[GetProfileFL(prof.fl.fio.ToUpper().Replace(" ", "_") + "_" + inn, prof.fl.fio, inn)];
                                    else link.tmplink = true;
                                    prof.is_src = true;
                                }
                            }

                            if ((j > 0) && (j < l - 1))
                            {
                                LinkData link1 = cd.link[j - 1];
                                LinkData link2 = cd.link[j];
                                string inn1 = link1.inn;
                                string inn2 = link2.inn;
                                if (link1.LinkType == 6) inn1 = cd.prof[j - 1].fl.inn;
                                if (link2.LinkType == 6) inn2 = cd.prof[j + 1].fl.inn;

                                // несовпадение ИНН
                                if ((inn1 != "") && (inn2 != "") && (inn1 != inn2)) error = true;
                                if (error) break;

                                if ((inn1 != "") && (prof.fl.inn == "")) prof = prof_list[GetProfileFL(prof.fl.fio.ToUpper().Replace(" ", "_") + "_" + inn1, prof.fl.fio, inn1)];
                                if ((inn2 != "") && (prof.fl.inn == "")) prof = prof_list[GetProfileFL(prof.fl.fio.ToUpper().Replace(" ", "_") + "_" + inn2, prof.fl.fio, inn2)];

                                if (inn1 == "") link1.tmplink = true;
                                if (inn2 == "") link2.tmplink = true;
                            }
                            cd.prof[j] = prof;
                        }
                    }

                    // убираем связи по ФИО
                    {
                        LinkData link = cd.link[0];
                        if (link.LinkType == 6)
                        {
                            cd.prof.RemoveAt(0);
                            cd.link.RemoveAt(0);
                            cd.prof[0].is_src = true;
                            ResetChain(cd);
                        }
                        link = cd.link[cd.link.Count() - 1];
                        if (link.LinkType == 6)
                        {
                            cd.prof.RemoveAt(cd.link.Count());
                            cd.link.RemoveAt(cd.link.Count() - 1);
                            cd.prof[cd.link.Count()].is_src = true;
                            ResetChain(cd);
                        }
                    }

                    if (!error) d.Add(cd, v);
                }

                d = d.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

                foreach (var chainid in d)
                {
                    chain_list.Add(chainid.Key);
                }
            }

            // удаляем цепочки, входящие в другие
            for (int i = 0; i < chain_list.Count(); i++)
            {
                ChainData chain1 = chain_list[i];
                int j = 0;
                while (j < chain_list.Count())
                {
                    if (j != i)
                    {
                        ChainData chain2 = chain_list[j];
                        if ((chain1.chain1.IndexOf(chain2.chain1) >= 0) || (chain1.chain2.IndexOf(chain2.chain1) >= 0) || (chain1.chain1.IndexOf(chain2.chain2) >= 0) || (chain1.chain2.IndexOf(chain2.chain2) >= 0))
                        {
                            chain_list.RemoveAt(j);
                            if (j < i) i--;
                            continue;
                        }
                    }
                    j++;
                }
            }
        }
        private string GetCodeList(List<string> list)
        {
            string res = "";
            for (int i = 0; i < list.Count(); i++)
            {
                res += (i == 0 ? "" : ",") + "'" + list[i] + "'";
            }
            return res;
        }
        private void ResetChain(ChainData chain)
        {
            chain.chain1 = "";
            chain.chain2 = "";
            for (int i = 0; i < chain.prof.Count; i++)
            {
                chain.chain1 = chain.chain1 + "[" + FindProfile(chain.prof[i].ProfType, chain.prof[i].ProfID) + "]";
                chain.chain2 = "[" + FindProfile(chain.prof[i].ProfType, chain.prof[i].ProfID) + "]" + chain.chain2;
                if (i < chain.link.Count)
                {
                    chain.chain1 = chain.chain1 + "<" + chain.link[i].LinkType + ">";
                    chain.chain2 = "<" + chain.link[i].LinkType + ">" + chain.chain2;
                }
            }
        }
        public JObject SearchFLByINN_neo(Neo4jClient neo, string code)
        {
            string json = "{\"statements\": [{\"statement\":\"MATCH (p:FL) " +
                "WHERE p.inn IN [" + code + "] " +
                "RETURN p.ProfID " +
                "LIMIT 10\"}]}";
            JObject r = neo.GetQuery(json);
            return (r);
        }
        public JObject SearchFLByFIO_elastic(string fio)
        {
            ElasticClient elastic = new ElasticClient();
            string url = "/profilefl/_search";
            string json = "{\"query\": {\"bool\": {\"must\": {\"multi_match\": {" +
                "  \"query\": \"" + fio + "\"," +
                "  \"fields\": [ \"fio\" ]," +
                "  \"operator\": \"and\"} } } }, " +
                "\"size\": \"100\"," +
                "\"sort\": [{\"FLID\": {\"order\": \"asc\"}}] }";

            JObject r = elastic.GetQuery(url, json);
            return (r);
        }
        private async Task<JObject> SearchMultiLinkNeo(string code1, string code2, int variant_limit)
        {
            string json = "{\"statements\": [{\"statement\":\"MATCH (p1:Profile {ProfID:'" + code1 + "'}),(p2:Profile {ProfID:'" + code2 + "'})," +
                " p = allShortestPaths((p1)-[*..15]-(p2)) " +
                "RETURN p " +
                "LIMIT " + variant_limit + "\"," +
                "\"resultDataContents\": [\"row\",\"graph\"]}]}";
            JObject r = await neo.GetQueryAsync(json);
            return (r);
        }
        private int GetProfileJson(JObject res)
        {
            int n;
            if ((int)res["ProfType"] == 1)
                n = GetProfileUL((string)res["ProfID"], (string)res["inn"], (string)res["ogrn"], (string)res["name"], (string)res["short"], (string)res["ticker"]);
            else
                n = GetProfileFL((string)res["ProfID"], (string)res["fio"], (string)res["inn"]);
            return (n);
        }
        private LinkData LoadLinkJson(JObject res)
        {
            LinkData link = new LinkData();
            link.LinkType = (int)res["LinkType"];
            if (link.LinkType == 1 || link.LinkType == 3)
            {
                link.share = (string)res["share"];
                link.share_percent = (string)res["share_percent"];
            }

            if (link.LinkType == 4)
            {
                link.position = (string)res["position"];
            }

            if (res.Property("inn") != null)
            {
                link.inn = (string)res["inn"];
            }
            else link.inn = "";

            link.tmplink = false;
            return (link);
        }
        private int GetProfileUL(string ProfID, string inn, string ogrn, string name, string short_name, string ticker)
        {
            ULData ul;
            int n = FindProfileUL(ProfID);
            if (n < 0)
            {
                ul = new ULData();
                ul.inn = inn;
                ul.ogrn = ogrn;
                ul.name = name;
                ul.short_name = short_name;
                ul.ticker = ticker;

                ProfData prof = new ProfData();
                prof.ProfType = 1;
                prof.ProfID = ProfID;
                prof.ul = ul;
                prof_list.Add(prof);
                n = prof_list.Count - 1;
            }
            return n;
        }
        private int FindProfileUL(string ProfID)
        {
            int res = -1;
            for (int i = 0, i_max = prof_list.Count; i < i_max; i++)
            {
                ProfData prof = prof_list[i];
                if (prof.ProfType == 1 && prof.ProfID == ProfID)
                {
                    res = i;
                    break;
                }
            }
            return res;
        }
        private int GetProfileFL(string ProfID, string fio, string inn)
        {
            FLData fl;
            int n = FindProfileFL(ProfID);
            if (n < 0)
            {
                fl = new FLData();
                fl.fio = fio;
                fl.inn = inn;

                ProfData prof = new ProfData();
                prof.ProfType = 2;
                prof.ProfID = ProfID;
                prof.fl = fl;
                prof_list.Add(prof);
                n = prof_list.Count - 1;
            }
            return n;
        }
        private int FindProfileFL(string ProfID)
        {
            int res = -1;
            for (int i = 0, i_max = prof_list.Count; i < i_max; i++)
            {
                ProfData prof = prof_list[i];
                if (prof.ProfType == 2 && prof.ProfID == ProfID)
                {
                    res = i;
                    break;
                }
            }
            return res;
        }
        private int FindProfile(int ProfType, string ProfID)
        {
            int res = -1;
            for (int i = 0, i_max = prof_list.Count; i < i_max; i++)
            {
                ProfData prof = prof_list[i];
                if (prof.ProfType == ProfType && prof.ProfID == ProfID)
                {
                    res = i;
                    break;
                }
            }
            return res;
        }
        private bool IsTicker(String s)
        {
            bool res = true;
            s = s.ToUpper();
            for (int i = 0; i < s.Length; i++)
            {
                if (string.Compare(s.Substring(i, 1), "A") < 0 || string.Compare(s.Substring(i, 1), "Z") > 0)
                {
                    res = false;
                    break;
                }
            }
            return (res);
        }
    }
    public class ProfData
    {
        public int ProfType;
        public string ProfID;
        public bool is_src = false;
        public ULData ul;
        public FLData fl;
    }
    public class ULData
    {
        public string name { get; set; }
        public string short_name { get; set; }
        public string inn { get; set; }
        public string ogrn { get; set; }
        public string ticker { get; set; }
    }
    public class FLData
    {
        public string fio;
        public string inn;
    }
    public class ChainData
    {
        public ChainData()
        {
            prof = new List<ProfData>();
            link = new List<LinkData>();
        }
        public List<ProfData> prof;
        public List<LinkData> link;
        public string chain1 = "";
        public string chain2 = "";
    }
    public class LinkData
    {
        public int LinkType;
        public int direction;
        public string share;
        public string share_percent;
        public string position;
        public string inn;
        public bool tmplink;
    }
}