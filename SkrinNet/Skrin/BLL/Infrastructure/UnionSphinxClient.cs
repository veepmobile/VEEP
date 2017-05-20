using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;

namespace Skrin.BLL.Infrastructure
{
    public class UnionSphinxClient
    {
        private QueryObject _qo;
        private string _url = "http://services.skrin.ru/Sphynx/UnionSearch";

        public UnionSphinxClient(QueryObject qo)
        {
            _qo = qo;
        }

        public string SearchResult()
        {
            byte[] byteArr = Encoding.UTF8.GetBytes(_qo.Query);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_url);
            request.Method = "POST";
            request.ContentLength = byteArr.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            request.GetRequestStream().Write(byteArr, 0, byteArr.Length);

            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);

            string result = sr.ReadToEnd();
            sr.Close();

            return result;
        }
    }


    public class QueryObject
    {
        public List<string> Queries { get; set; }
        public string OrderField { get; set; }
        public string OrderType { get; set; }
        public int Count { get; set; }
        public int Skip { get; set; }
        public string OrderFieldType { get; set; }
        public int Port { get; set; }
        public string CharasterSet { get; set; }


        public QueryObject()
        {
            Queries = new List<string>();
            Count = 20;
            Skip = 0;
            OrderType = "asc";
            OrderFieldType = "string";
            Port = 9306;
            CharasterSet = "cp1251";
        }

        public string SqlQuery
        {
            get
            {
                return string.Join(" ", Queries.ToArray());
            }
        }

        public string Query
        {
            get
            {
                string q = "";
                foreach(string query in Queries)
                {
                    q += "queries=" + HttpUtility.UrlEncode(query) + "&";
                }
                if (q.Length > 0)
                    q = q.Substring(0, q.Length - 1);
                return q + "&order_field=" + OrderField + "&order_type=" + OrderType + "&count=" + Count.ToString() + "&skip=" + Skip.ToString() + "&port=" + Port.ToString() + "&charaster_set=" + CharasterSet + "&order_field_type=" + OrderFieldType;
            }
        }
    }

   
}