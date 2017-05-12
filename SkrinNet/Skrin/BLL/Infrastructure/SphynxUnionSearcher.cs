using Newtonsoft.Json.Linq;
using Skrin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.BLL.Infrastructure
{
    public class SphynxUnionSearcher
    {
        private string _search_result = null;

        public string Search_Result
        {
            get { return _search_result; }
        }

        public SphynxUnionSearcher(List<string> queries, string port, string charaster_set, string order_field, string order_type, int count, int skip, string type, string server = null)
        {
            if (server == null)
                server = Configs.SphinxServer;

            JArray res_ar = new JArray();
            List<Keyword> keywords = new List<Keyword>();

            int i = 0;

            foreach (var query in queries)
            {
                if (!string.IsNullOrWhiteSpace(query))
                {
                    string sql = query + " limit  10000 OPTION max_matches=10000; show meta;";
                    SphynxSearcher srch = new SphynxSearcher(sql, port, charaster_set, server);
                    string result = srch.SearchJson();
                    JObject o = JObject.Parse("{" + result + "}");
                    List<Keyword> k = GetKeywords(o);
                    //Добавим ключевые слова, которых не было ранее
                    foreach (var key in k)
                    {
                        if (!keywords.Contains(key))
                            keywords.Add(key);
                    }

                    //Пометим те ключевые слова, которые были использованы в этом запросе
                    foreach (var key in keywords)
                    {
                        if (k.Contains(key))
                        {
                            key.Used.Add(i);
                        }
                    }

                    var results = o["results"];
                    foreach (var res in results)
                    {
                        bool exist = false;
                        foreach (var item in res_ar)
                        {
                            if (JToken.DeepEquals(item, res))
                            {
                                exist = true;
                                break;
                            }
                        }

                        if (!exist)
                        {
                            res_ar.Add(res);
                        }
                    }
                }
                i++;
            }

            IEnumerable<JToken> enu_result = null;

            if (string.IsNullOrWhiteSpace(order_field))
            {
                enu_result = res_ar.Skip(skip).Take(count);
            }
            else
            {
                if (order_type.ToLower() == "desc")
                    enu_result = res_ar.OrderByDescending(p => p.SelectToken(order_field).TransformForSorting(type)).Skip(skip).Take(count);
                else
                    enu_result = res_ar.OrderBy(p => p.SelectToken(order_field).TransformForSorting(type)).Skip(skip).Take(count);
            }

            var str_keywords = string.Join(",", new KeywordList(keywords).ToList());

            string str_result = string.Join(",", enu_result);
            _search_result = "\"results\":[" + str_result + "],\"total\":\"" + res_ar.Count + "\",\"total_found\":\"" + res_ar.Count + "\"," + str_keywords + "";
        }


        private List<Keyword> GetKeywords(JObject o)
        {
            List<Keyword> keywords = new List<Keyword>();
            int i = 0;
            while (o[string.Format("keyword[{0}]", i)] != null)
            {
                keywords.Add(new Keyword
                {
                    Key = o[string.Format("keyword[{0}]", i)].ToString(),
                    Docs = o[string.Format("docs[{0}]", i)].ToString(),
                    Hits = o[string.Format("hits[{0}]", i)].ToString()
                });
                i++;
            }

            return keywords;
        }
    }
}