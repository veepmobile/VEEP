using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skrin.BLL.Infrastructure
{
    public class Neo4jClient
    {
        private static string neo_url = "http://" + ConfigurationManager.AppSettings["neo4j_server"];
        public JObject GetQuery(string request)
        {
            string url = neo_url + "/db/data/transaction/commit";
            RequestResult r = Request("POST", url, request);

            if (r.code != 200) throw new Exception("Ошибка выполнения запроса neo4j: " + r.text);

            JObject resp = JObject.Parse(r.text);
            if (resp.Property("errors") != null)
                if (resp["errors"].Count() > 0)
                {
                    throw new Exception("Ошибка выполнения запроса neo4j: " + (string)resp["errors"][0]["code"] + ": " + (string)resp["errors"][0]["message"]);
                }

            return (resp);
        }
        public async Task<JObject> GetQueryAsync(string request)
        {
            string url = neo_url + "/db/data/transaction/commit";
            RequestResult r = await RequestAsync("POST", url, request);

            if (r.code != 200) throw new Exception("Ошибка выполнения запроса neo4j: " + r.text);

            JObject resp = JObject.Parse(r.text);
            if (resp.Property("errors") != null)
                if (resp["errors"].Count() > 0)
                {
                    throw new Exception("Ошибка выполнения запроса neo4j: " + (string)resp["errors"][0]["code"] + ": " + (string)resp["errors"][0]["message"]);
                }

            return (resp);
        }
        public async Task<JObject> GetBaseQueryAsync(string query_name, string[] param_name = null, string[] param_value = null)
        {
            string request = "{\"statements\": [{\"statement\": \" MATCH (p:Query {name:'" + query_name + "'}) RETURN p.text \"}]}";
            JObject q = GetQuery(request);

            string query = (string)q["results"][0]["data"][0]["row"][0];
            request = "{\"statements\": [{\"statement\": \"" + query + "\"";
            if (param_name != null)
            {
                request += ",\"parameters\":{";
                for (int i = 0; i < param_name.Length; i++)
                {
                    if (i > 0) request += ",";
                    request += "\"" + param_name[i] + "\":" + param_value[i] + "";
                }
                request += "}";
            }
            request += "}]}";

            JObject resp = await GetQueryAsync(request);
            return (resp);
        }
        public JObject GetBaseQuery(string query_name, string[] param_name = null, string[] param_value = null)
        {
            string request = "{\"statements\": [{\"statement\": \" MATCH (p:Query {name:'" + query_name + "'}) RETURN p.text \"}]}";
            JObject q = GetQuery(request);

            string query = (string)q["results"][0]["data"][0]["row"][0];
            request = "{\"statements\": [{\"statement\": \"" + query + "\"";
            if (param_name != null)
            {
                request += ",\"parameters\":{";
                for (int i = 0; i < param_name.Length; i++)
                {
                    if (i > 0) request += ",";
                    request += "\"" + param_name[i] + "\":" + param_value[i] + "";
                }
                request += "}";
            }
            request += "}]}";

            JObject resp = GetQuery(request);
            return (resp);
        }
        static RequestResult Request(string method, string url, string document)
        {
            RequestResult result = new RequestResult();
            try
            {
                byte[] data = new UTF8Encoding().GetBytes(document);
                var request = (HttpWebRequest)WebRequest.Create(url);
                Stream dataStream;
                request.Method = method;
                if (!string.IsNullOrEmpty(document))
                {
                    request.ContentType = "application/json";
                    request.ContentLength = data.Length;
                    dataStream = request.GetRequestStream();
                    dataStream.Write(data, 0, data.Length);
                    dataStream.Close();
                }
                request.Accept = "application/json";
                request.Timeout = 300000;
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                dataStream.ReadTimeout = 300000;
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                result.code = (int)((HttpWebResponse)response).StatusCode;
                result.text = responseFromServer;
            }
            catch (WebException e)
            {
                HttpWebResponse httpResponse = (HttpWebResponse)e.Response;
                if (e.Response != null) result.code = (int)httpResponse.StatusCode;
                else result.code = 0;
                result.text = e.Message;
                //System.IO.File.AppendAllText("c:\\net\\error.log", document + Environment.NewLine + Environment.NewLine, Encoding.GetEncoding("Windows-1251"));
            }
            return (result);
        }
        static async Task<RequestResult> RequestAsync(string method, string url, string document)
        {
            RequestResult result = new RequestResult();
            try
            {
                byte[] data = new UTF8Encoding().GetBytes(document);
                var request = (HttpWebRequest)WebRequest.Create(url);
                Stream dataStream;
                request.Method = method;
                if (!string.IsNullOrEmpty(document))
                {
                    request.ContentType = "application/json";
                    request.ContentLength = data.Length;
                    dataStream = request.GetRequestStream();
                    dataStream.Write(data, 0, data.Length);
                    dataStream.Close();
                }
                request.Accept = "application/json";
                request.Timeout = 300000;
                WebResponse response = await request.GetResponseAsync();
                dataStream = response.GetResponseStream();
                dataStream.ReadTimeout = 300000;
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = await reader.ReadToEndAsync();
                result.code = (int)((HttpWebResponse)response).StatusCode;
                result.text = responseFromServer;
            }
            catch (WebException e)
            {
                HttpWebResponse httpResponse = (HttpWebResponse)e.Response;
                if (e.Response != null) result.code = (int)httpResponse.StatusCode;
                else result.code = 0;
                result.text = e.Message;
                //System.IO.File.AppendAllText("c:\\net\\error.log", document + Environment.NewLine + Environment.NewLine, Encoding.GetEncoding("Windows-1251"));
            }
            return (result);
        }
        class RequestResult
        {
            public int code = 0;
            public string text = "";
        }
    }
}