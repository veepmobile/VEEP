using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skrin.BLL;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.Models.ProfileLinks;

namespace Skrin.BLL.Iss
{
    public class ProfileLinksRepository
    {
        private static string _constring = Configs.ConnectionString;
        protected ProfileLinksModel Prof = new ProfileLinksModel();
        ConcurrentBag<string> error_list = new ConcurrentBag<string>();
        ArangoClient ac = new ArangoClient();

        public ProfileLinksRepository(string iss, int user_id)
        {
            Prof.iss = iss;
            Prof.user_id = user_id;
        }
        public async Task<ProfileLinksModel> GetProfileLinksAsync()
        {
            await getProfileMain();

            if (Prof.profile != null)
            {
                List<Task> tasks = new List<Task>();
                tasks.Add(getManager_From_IP());
                tasks.Add(getProfileFounder_To_neo());
                tasks.Add(getProfileManager_To_neo());
                tasks.Add(getProfileFounder_FromFL_neo());
                tasks.Add(getProfileFounder_FromUL_neo());
                tasks.Add(getProfileFounder_From_ToFL_neo());
                tasks.Add(getProfileFounder_From_ToUL_neo());
                tasks.Add(getProfileFounder_From_Manager_To_neo());
                tasks.Add(getSuccessor_From_neo());
                tasks.Add(getSuccessor_To_neo());
                tasks.Add(getManager_From_neo());
                tasks.Add(getManager_From_Founder_To_neo());
                tasks.Add(getManager_From_To_neo());

                for (int i = 0, i_max = tasks.Count; i < i_max; i++)
                {
                    await tasks[i];
                }
            }

            if (error_list.Count > 0)
            {
                Helper.SendEmail(string.Join("\n", error_list), "Ошибка в связях ЮЛ");
                return null;
            }

            return Prof;
        }


        private async Task getProfileMain()
        {
            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileLinksMain", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iss", Prof.iss);
                try
                {
                    await con.OpenAsync();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (await rd.ReadAsync())
                        {
                            Prof.profile = new MainData();
                            Prof.profile.name = rd.ReadEmptyIfDbNull("name");
                            Prof.profile.short_name = rd.ReadEmptyIfDbNull("short_name");
                            Prof.profile.inn = rd.ReadEmptyIfDbNull("inn");
                            Prof.profile.ogrn = rd.ReadEmptyIfDbNull("ogrn");
                            Prof.profile.address = rd.ReadEmptyIfDbNull("address");
                        }
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    error_list.Add("Ошибка в методе getProfileLinksMain: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private async Task getManager_From_IP()
        {
            Prof.manager_from_ip = new List<ManagerFromIPData>();
            Prof.manager_from_ip_inn_count = 0;
            Prof.manager_from_ip_fio_count = 0;
            using (SqlConnection con = new SqlConnection(_constring))
            using (SqlCommand cmd = new SqlCommand("web_shop..getProfileLinksManager_From_IP", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ogrn", Prof.profile.ogrn);
                cmd.Parameters.AddWithValue("@inn", Prof.profile.inn);
                try
                {
                    await con.OpenAsync();
                    ManagerFromIPData mn = new ManagerFromIPData();
                    IPRecData rec;
                    string id = "";
                    bool flag;
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            flag = false;
                            id = rd.ReadEmptyIfDbNull("fio_mn") + rd.ReadEmptyIfDbNull("inn_mn") + rd.ReadEmptyIfDbNull("position_mn");
                            foreach (ManagerFromIPData m in Prof.manager_from_ip)
                            {
                                if (m.fio + m.inn + m.position == id)
                                {
                                    mn = m;
                                    flag = true;
                                    break;
                                }

                            }
                            if (!flag)
                            {
                                mn = new ManagerFromIPData();
                                mn.fio = rd.ReadEmptyIfDbNull("fio_mn");
                                mn.inn = rd.ReadEmptyIfDbNull("inn_mn");
                                mn.position = rd.ReadEmptyIfDbNull("position_mn");
                                Prof.manager_from_ip.Add(mn);
                            }
                            rec = new IPRecData();
                            rec.fio = rd.ReadEmptyIfDbNull("fio");
                            rec.subrf = rd.ReadEmptyIfDbNull("subrf");
                            rec.inn = rd.ReadEmptyIfDbNull("inn");
                            rec.ogrnip = rd.ReadEmptyIfDbNull("ogrnip");
                            rec.id = rd.ReadEmptyIfDbNull("id");
                            rec.gd = rd.ReadEmptyIfDbNull("gd");
                            rec.sd = rd.ReadEmptyIfDbNull("sd");
                            if ((int)rd["st"] == 1)
                            {
                                mn.ip_inn.Add(rec);
                                Prof.manager_from_ip_inn_count++;
                            }
                            else
                            {
                                mn.ip_fio.Add(rec);
                                Prof.manager_from_ip_fio_count++;
                            }
                        }
                        rd.Close();
                    }
                }
                catch (Exception ex)
                {
                    error_list.Add("Ошибка в методе getProfileLinksManager_From_IP: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private async Task getProfileFounder_To_neo()
        {
            Prof.founder_to = new List<FounderToData>();
            Neo4jClient nc = new Neo4jClient();
            try
            {
                FounderToData fn;
                JObject resp = await nc.GetBaseQueryAsync("ProfileLinks_Founder_To", new string[] { "id" }, new string[] { "\"" + Prof.iss.ToUpper() + "\"" });
                int n = resp["results"][0]["data"].Count();
                for (int i = 0, i_max = n; i < i_max; i++)
                {
                    JObject item = (JObject)resp["results"][0]["data"][i]["row"][0];
                    JObject link = (JObject)resp["results"][0]["data"][i]["row"][1];
                    JObject link2 = null;
                    if (resp["results"][0]["data"][i]["row"][2].Type != JTokenType.Null) link2 = (JObject)resp["results"][0]["data"][i]["row"][2];
                    fn = new FounderToData();
                    fn.ogrn = (string)link["ogrn"];
                    fn.inn = (string)link["inn"];
                    fn.name = (string)link["name"];
                    fn.share = (string)link["share"];
                    fn.share_percent = (string)link["share_percent"];
                    fn.ogrn_to = (string)item["ogrn"];
                    fn.name_to = (string)item["name"];
                    fn.status_to = (string)item["status"];
                    fn.ticker_to = (string)item["ticker"];
                    fn.gd = (string)link["gd"];
                    if (link2 != null) fn.remark = "в этом же юр.лице управляющая организация";
                    else fn.remark = "";
                    Prof.founder_to.Add(fn);
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе getProfileLinksFounder_To_neo: " + ex.ToString());
            }
        }
        private async Task getProfileManager_To_neo()
        {
            Prof.manager_to = new List<ManagerToData>();
            Neo4jClient nc = new Neo4jClient();
            try
            {
                ManagerToData mn;
                JObject resp = await nc.GetBaseQueryAsync("ProfileLinks_Manager_To", new string[] { "id" }, new string[] { "\"" + Prof.iss.ToUpper() + "\"" });
                int n = resp["results"][0]["data"].Count();
                for (int i = 0, i_max = n; i < i_max; i++)
                {
                    JObject item = (JObject)resp["results"][0]["data"][i]["row"][0];
                    JObject link = (JObject)resp["results"][0]["data"][i]["row"][1];
                    JObject link2 = null;
                    if (resp["results"][0]["data"][i]["row"][2].Type != JTokenType.Null) link2 = (JObject)resp["results"][0]["data"][i]["row"][2];
                    mn = new ManagerToData();
                    mn.ogrn = (string)link["ogrn"];
                    mn.inn = (string)link["inn"];
                    mn.name = (string)link["name"];
                    mn.address = (string)link["address"];
                    mn.ogrn_to = (string)item["ogrn"];
                    mn.name_to = (string)item["name"];
                    mn.status_to = (string)item["status"];
                    mn.ticker_to = (string)item["ticker"];
                    mn.gd = (string)link["gd"];
                    if (link2 != null) mn.remark = "в этом же юр.лице учредитель";
                    else mn.remark = "";
                    Prof.manager_to.Add(mn);
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе getProfileLinksManager_To_neo: " + ex.ToString());
            }
        }
        private async Task getProfileFounder_FromFL_neo()
        {
            Prof.founder_from_fl = new List<FounderFromFLData>();
            Neo4jClient nc = new Neo4jClient();
            try
            {
                FounderFromFLData fn;
                JObject resp = await nc.GetBaseQueryAsync("ProfileLinks_Founder_FromFL", new string[] { "id" }, new string[] { "\"" + Prof.iss.ToUpper() + "\"" });
                int n = resp["results"][0]["data"].Count();
                for (int i = 0, i_max = n; i < i_max; i++)
                {
                    JObject item = (JObject)resp["results"][0]["data"][i]["row"][0];
                    JObject link = (JObject)resp["results"][0]["data"][i]["row"][1];
                    fn = new FounderFromFLData();
                    fn.fio = (string)item["fio"];
                    fn.inn = (string)link["inn"];
                    fn.share = (string)link["share"];
                    fn.share_percent = (string)link["share_percent"];
                    fn.gd = (string)link["gd"];
                    Prof.founder_from_fl.Add(fn);
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе getProfileLinksFounder_FromFL_neo: " + ex.ToString());
            }
        }
        private async Task getProfileFounder_FromUL_neo()
        {
            Prof.founder_from_ul = new List<FounderFromULData>();
            Neo4jClient nc = new Neo4jClient();
            try
            {
                FounderFromULData fn;
                JObject resp = await nc.GetBaseQueryAsync("ProfileLinks_Founder_FromUL", new string[] { "id" }, new string[] { "\"" + Prof.iss.ToUpper() + "\"" });
                int n = resp["results"][0]["data"].Count();
                for (int i = 0, i_max = n; i < i_max; i++)
                {
                    JObject item = (JObject)resp["results"][0]["data"][i]["row"][0];
                    JObject link = (JObject)resp["results"][0]["data"][i]["row"][1];
                    fn = new FounderFromULData();
                    fn.ogrn = (string)item["ogrn"];
                    fn.inn = (string)item["inn"];
                    fn.name = (string)item["name"];
                    fn.share = (string)link["share"];
                    fn.share_percent = (string)link["share_percent"];
                    fn.ticker = (string)item["ticker"];
                    fn.status = (string)item["status"];
                    fn.gd = (string)link["gd"];
                    Prof.founder_from_ul.Add(fn);
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе getProfileLinksFounder_FromUL_neo: " + ex.ToString());
            }
        }
        private async Task getProfileFounder_From_ToFL_neo()
        {
            Prof.founder_from_to_fl = new List<FounderFromToFLData>();
            Neo4jClient nc = new Neo4jClient();
            try
            {
                FounderFromToFLData fn = new FounderFromToFLData();
                JObject resp = await nc.GetBaseQueryAsync("ProfileLinks_Founder_From_ToFL", new string[] { "id" }, new string[] { "\"" + Prof.iss.ToUpper() + "\"" });
                int n = resp["results"][0]["data"].Count();
                string id = "";
                bool flag;
                FounderRecFLData rec;
                for (int i = 0, i_max = n; i < i_max; i++)
                {
                    JObject item_fn = (JObject)resp["results"][0]["data"][i]["row"][0];
                    JObject link_fn = (JObject)resp["results"][0]["data"][i]["row"][1];
                    JObject item = (JObject)resp["results"][0]["data"][i]["row"][2];
                    JObject link = (JObject)resp["results"][0]["data"][i]["row"][3];
                    JObject link2 = null;
                    if (resp["results"][0]["data"][i]["row"][4].Type != JTokenType.Null) link2 = (JObject)resp["results"][0]["data"][i]["row"][4];
                    flag = false;
                    id = (string)item_fn["fio"] + (string)link_fn["inn"];
                    foreach (FounderFromToFLData f in Prof.founder_from_to_fl)
                    {
                        if (f.fio + f.inn == id)
                        {
                            fn = f;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        fn = new FounderFromToFLData();
                        fn.fio = (string)item_fn["fio"];
                        fn.inn = (string)link_fn["inn"];
                        Prof.founder_from_to_fl.Add(fn);
                    }
                    rec = new FounderRecFLData();
                    rec.fio = (string)item_fn["fio"];
                    rec.inn = (string)link["inn"];
                    rec.share = (string)link["share"];
                    rec.share_percent = (string)link["share_percent"];
                    rec.ogrn_to = (string)item["ogrn"];
                    rec.name_to = (string)item["name"];
                    rec.status_to = (string)item["status"];
                    rec.ticker_to = (string)item["ticker"];
                    rec.gd = (string)link["gd"];
                    if (link2 != null) rec.remark = "в этом же юр.лице " + (string)link2["position"];
                    else rec.remark = "";
                    if (((string)link["inn"] == (string)link_fn["inn"]) && ((string)link_fn["inn"] != ""))
                        fn.founder_inn.Add(rec);
                    else
                        fn.founder_fio.Add(rec);
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе getProfileLinksFounder_From_ToFL_neo: " + ex.ToString());
            }
        }
        private async Task getProfileFounder_From_ToUL_neo()
        {
            Prof.founder_from_to_ul = new List<FounderFromToULData>();
            Neo4jClient nc = new Neo4jClient();
            try
            {
                FounderFromToULData fn = new FounderFromToULData();
                JObject resp = await nc.GetBaseQueryAsync("ProfileLinks_Founder_From_ToUL", new string[] { "id" }, new string[] { "\"" + Prof.iss.ToUpper() + "\"" });
                int n = resp["results"][0]["data"].Count();
                string id = "";
                bool flag;
                FounderRecULData rec;
                for (int i = 0, i_max = n; i < i_max; i++)
                {
                    JObject item_fn = (JObject)resp["results"][0]["data"][i]["row"][0];
                    JObject link_fn = (JObject)resp["results"][0]["data"][i]["row"][1];
                    JObject item = (JObject)resp["results"][0]["data"][i]["row"][2];
                    JObject link = (JObject)resp["results"][0]["data"][i]["row"][3];
                    JObject link2 = null;
                    if (resp["results"][0]["data"][i]["row"][4].Type != JTokenType.Null) link2 = (JObject)resp["results"][0]["data"][i]["row"][4];
                    flag = false;
                    id = (string)item_fn["name"] + (string)item_fn["inn"] + (string)item_fn["ogrn"];
                    foreach (FounderFromToULData f in Prof.founder_from_to_ul)
                    {
                        if (f.name + f.inn + f.ogrn == id)
                        {
                            fn = f;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        fn = new FounderFromToULData();
                        fn.name = (string)item_fn["name"];
                        fn.inn = (string)item_fn["inn"];
                        fn.ogrn = (string)item_fn["ogrn"];
                        Prof.founder_from_to_ul.Add(fn);
                    }
                    rec = new FounderRecULData();
                    rec.name = (string)link["name"];
                    rec.inn = (string)link["inn"];
                    rec.ogrn = (string)link["ogrn"];
                    rec.share = (string)link["share"];
                    rec.share_percent = (string)link["share_percent"];
                    rec.ogrn_to = (string)item["ogrn"];
                    rec.name_to = (string)item["name"];
                    rec.status_to = (string)item["status"];
                    rec.ticker_to = (string)item["ticker"];
                    rec.gd = (string)link["gd"];
                    if (link2 != null) rec.remark = "в этом же юр.лице управляющая организация";
                    else rec.remark = "";
                    fn.founder.Add(rec);
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе getProfileLinksFounder_From_ToUL_neo: " + ex.ToString());
            }
        }
        private async Task getProfileFounder_From_Manager_To_neo()
        {
            Prof.founder_from_manager_to = new List<FounderFromManagerToData>();
            Neo4jClient nc = new Neo4jClient();
            try
            {
                FounderFromManagerToData fn = new FounderFromManagerToData();
                JObject resp = await nc.GetBaseQueryAsync("ProfileLinks_Founder_From_Manager_To", new string[] { "id" }, new string[] { "\"" + Prof.iss.ToUpper() + "\"" });
                int n = resp["results"][0]["data"].Count();
                string id = "";
                bool flag;
                ManagerRecData rec;
                for (int i = 0, i_max = n; i < i_max; i++)
                {
                    JObject item_fn = (JObject)resp["results"][0]["data"][i]["row"][0];
                    JObject link_fn = (JObject)resp["results"][0]["data"][i]["row"][1];
                    JObject item = (JObject)resp["results"][0]["data"][i]["row"][2];
                    JObject link = (JObject)resp["results"][0]["data"][i]["row"][3];
                    JObject link2 = null;
                    if (resp["results"][0]["data"][i]["row"][4].Type != JTokenType.Null) link2 = (JObject)resp["results"][0]["data"][i]["row"][4];
                    flag = false;
                    id = (string)item_fn["fio"] + (string)link_fn["inn"];
                    foreach (FounderFromManagerToData f in Prof.founder_from_manager_to)
                    {
                        if (f.fio + f.inn == id)
                        {
                            fn = f;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        fn = new FounderFromManagerToData();
                        fn.fio = (string)item_fn["fio"];
                        fn.inn = (string)link_fn["inn"];
                        Prof.founder_from_manager_to.Add(fn);
                    }
                    rec = new ManagerRecData();
                    rec.fio = (string)item_fn["fio"];
                    rec.inn = (string)link["inn"];
                    rec.position = (string)link["position"];
                    rec.ogrn = (string)item["ogrn"];
                    rec.name = (string)item["name"];
                    rec.status = (string)item["status"];
                    rec.ticker = (string)item["ticker"];
                    rec.gd = (string)link["gd"];
                    if (link2 != null) rec.remark = "в этом же юр.лице учредитель";
                    else rec.remark = "";
                    if (((string)link["inn"] == (string)link_fn["inn"]) && ((string)link_fn["inn"] != ""))
                        fn.manager_inn.Add(rec);
                    else
                        fn.manager_fio.Add(rec);
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе getProfileLinksFounder_From_Manager_To_neo: " + ex.ToString());
            }
        }
        private async Task getSuccessor_From_neo()
        {
            Prof.successor_from = new List<SuccessorData>();
            Neo4jClient nc = new Neo4jClient();
            try
            {
                JObject resp = await nc.GetBaseQueryAsync("ProfileLinks_Successor_From", new string[] { "id" }, new string[] { "\"" + Prof.iss.ToUpper() + "\"" });
                int n = resp["results"][0]["data"].Count();
                SuccessorData sc;
                for (int i = 0, i_max = n; i < i_max; i++)
                {
                    JObject item = (JObject)resp["results"][0]["data"][i]["row"][0];
                    JObject link = (JObject)resp["results"][0]["data"][i]["row"][1];

                    sc = new SuccessorData();
                    sc.name = (string)item["name"];
                    sc.inn = (string)item["inn"];
                    sc.ogrn = (string)item["ogrn"];
                    sc.ticker = (string)item["ticker"];
                    sc.status = (string)item["status"];
                    sc.gd = (string)link["gd"];
                    Prof.successor_from.Add(sc);
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе getProfileLinksSuccessor_From_neo: " + ex.ToString());
            }
        }
        private async Task getSuccessor_To_neo()
        {
            Prof.successor_to = new List<SuccessorData>();
            Neo4jClient nc = new Neo4jClient();
            try
            {
                JObject resp = await nc.GetBaseQueryAsync("ProfileLinks_Successor_To", new string[] { "id" }, new string[] { "\"" + Prof.iss.ToUpper() + "\"" });
                int n = resp["results"][0]["data"].Count();
                SuccessorData sc;
                for (int i = 0, i_max = n; i < i_max; i++)
                {
                    JObject item = (JObject)resp["results"][0]["data"][i]["row"][0];
                    JObject link = (JObject)resp["results"][0]["data"][i]["row"][1];

                    sc = new SuccessorData();
                    sc.name = (string)item["name"];
                    sc.inn = (string)item["inn"];
                    sc.ogrn = (string)item["ogrn"];
                    sc.ticker = (string)item["ticker"];
                    sc.status = (string)item["status"];
                    sc.gd = (string)link["gd"];
                    Prof.successor_to.Add(sc);
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе getProfileLinksSuccessor_To_neo: " + ex.ToString());
            }
        }
        private async Task getManager_From_neo()
        {
            Prof.manager_from = new List<ManagerFromData>();
            Neo4jClient nc = new Neo4jClient();
            try
            {
                JObject resp = await nc.GetBaseQueryAsync("ProfileLinks_Manager_From", new string[] { "id" }, new string[] { "\"" + Prof.iss.ToUpper() + "\"" });
                int n = resp["results"][0]["data"].Count();
                ManagerFromData mn;
                for (int i = 0, i_max = n; i < i_max; i++)
                {
                    JObject item = (JObject)resp["results"][0]["data"][i]["row"][0];
                    JObject link = (JObject)resp["results"][0]["data"][i]["row"][1];
                    mn = new ManagerFromData();
                    mn.fio = (string)item["fio"];
                    mn.inn = (string)link["inn"];
                    mn.position = (string)link["position"];
                    mn.gd = (string)link["gd"];
                    Prof.manager_from.Add(mn);
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе getProfileLinksManager_From_neo: " + ex.ToString());
            }
        }
        private async Task getManager_From_Founder_To_neo()
        {
            Prof.manager_from_founder_to = new List<ManagerFromFounderToData>();
            Prof.manager_from_founder_to_inn_count = 0;
            Prof.manager_from_founder_to_fio_count = 0;

            Neo4jClient nc = new Neo4jClient();
            try
            {
                ManagerFromFounderToData mn = new ManagerFromFounderToData();
                JObject resp = await nc.GetBaseQueryAsync("ProfileLinks_Manager_From_Founder_To", new string[] { "id" }, new string[] { "\"" + Prof.iss.ToUpper() + "\"" });
                int n = resp["results"][0]["data"].Count();
                string id = "";
                bool flag;
                FounderRecFLData rec;
                for (int i = 0, i_max = n; i < i_max; i++)
                {
                    JObject item_mn = (JObject)resp["results"][0]["data"][i]["row"][0];
                    JObject link_mn = (JObject)resp["results"][0]["data"][i]["row"][1];
                    JObject item = (JObject)resp["results"][0]["data"][i]["row"][2];
                    JObject link = (JObject)resp["results"][0]["data"][i]["row"][3];
                    JObject link2 = null;
                    if (resp["results"][0]["data"][i]["row"][4].Type != JTokenType.Null) link2 = (JObject)resp["results"][0]["data"][i]["row"][4];
                    flag = false;
                    id = (string)item_mn["fio"] + (string)link_mn["inn"];
                    foreach (ManagerFromFounderToData m in Prof.manager_from_founder_to)
                    {
                        if (m.fio + m.inn == id)
                        {
                            mn = m;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        mn = new ManagerFromFounderToData();
                        mn.fio = (string)item_mn["fio"];
                        mn.inn = (string)link_mn["inn"];
                        mn.position = (string)link_mn["position"];
                        Prof.manager_from_founder_to.Add(mn);
                    }
                    rec = new FounderRecFLData();
                    rec.fio = ((string)item_mn["fio"]).Trim();
                    rec.inn = (string)link["inn"];
                    rec.share = (string)link["share"];
                    rec.share_percent = (string)link["share_percent"];
                    rec.ogrn_to = (string)item["ogrn"];
                    rec.name_to = (string)item["name"];
                    rec.status_to = (string)item["status"];
                    rec.ticker_to = (string)item["ticker"];
                    rec.gd = (string)link["gd"];
                    if (link2 != null) rec.remark = "в этом же юр.лице " + (string)link2["position"];
                    else rec.remark = "";
                    if (((string)link["inn"] == (string)link_mn["inn"]) && ((string)link_mn["inn"] != ""))
                    {
                        mn.founder_inn.Add(rec);
                        Prof.manager_from_founder_to_inn_count++;
                    }
                    else
                    {
                        mn.founder_fio.Add(rec);
                        Prof.manager_from_founder_to_fio_count++;
                    }
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе getProfileLinksManager_From_Founder_To_neo: " + ex.ToString());
            }
        }
        private async Task getManager_From_To_neo()
        {
            Prof.manager_from_to = new List<ManagerFromToData>();
            Prof.manager_from_to_inn_count = 0;
            Prof.manager_from_to_fio_count = 0;

            Neo4jClient nc = new Neo4jClient();
            try
            {
                ManagerFromToData mn = new ManagerFromToData();
                JObject resp = await nc.GetBaseQueryAsync("ProfileLinks_Manager_From_To", new string[] { "id" }, new string[] { "\"" + Prof.iss.ToUpper() + "\"" });
                int n = resp["results"][0]["data"].Count();
                string id = "";
                bool flag;
                ManagerRecData rec;
                for (int i = 0, i_max = n; i < i_max; i++)
                {
                    JObject item_mn = (JObject)resp["results"][0]["data"][i]["row"][0];
                    JObject link_mn = (JObject)resp["results"][0]["data"][i]["row"][1];
                    JObject item = (JObject)resp["results"][0]["data"][i]["row"][2];
                    JObject link = (JObject)resp["results"][0]["data"][i]["row"][3];
                    JObject link2 = null;
                    if (resp["results"][0]["data"][i]["row"][4].Type != JTokenType.Null) link2 = (JObject)resp["results"][0]["data"][i]["row"][4];

                    flag = false;
                    id = (string)item_mn["fio"] + (string)link_mn["inn"];
                    foreach (ManagerFromToData m in Prof.manager_from_to)
                    {
                        if (m.fio + m.inn == id)
                        {
                            mn = m;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        mn = new ManagerFromToData();
                        mn.fio = (string)item_mn["fio"];
                        mn.inn = (string)link_mn["inn"];
                        mn.position = (string)link_mn["position"];
                        Prof.manager_from_to.Add(mn);
                    }
                    rec = new ManagerRecData();
                    rec.fio = (string)item_mn["fio"];
                    rec.inn = (string)link["inn"];
                    rec.position = (string)link["position"];
                    rec.ogrn = (string)item["ogrn"];
                    rec.name = (string)item["name"];
                    rec.status = (string)item["status"];
                    rec.ticker = (string)item["ticker"];
                    rec.gd = (string)link["gd"];
                    if (link2 != null) rec.remark = "в этом же юр.лице учредитель";
                    else rec.remark = "";
                    if (((string)link["inn"] == (string)link_mn["inn"]) && ((string)link_mn["inn"] != ""))
                    {
                        mn.manager_inn.Add(rec);
                        Prof.manager_from_to_inn_count++;
                    }
                    else
                    {
                        mn.manager_fio.Add(rec);
                        Prof.manager_from_to_fio_count++;
                    }
                }
            }
            catch (Exception ex)
            {
                error_list.Add("Ошибка в методе getProfileLinksManager_From_To_neo: " + ex.ToString());
            }
        }
    }
}