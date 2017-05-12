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
    public class ArangoClient
    {
        private static string adb_url = "http://" + ConfigurationManager.AppSettings["arangodb_server"];
        public JObject GetQuery(string base_name, string request)
        {
            string url = adb_url + "/_db/" + base_name + "/_api/cursor";
            RequestResult r = Request("POST", url, request);
            if (r.code != 201) throw new Exception("Ошибка выполнения запроса Arango: " + r.text);

            JObject resp = JObject.Parse(r.text);
            return (resp);
        }
        public async Task<JObject> GetQueryAsync(string base_name, string request)
        {
            string url = adb_url + "/_db/" + base_name + "/_api/cursor";
            RequestResult r = await RequestAsync("POST", url, request);
            if (r.code != 201) throw new Exception("Ошибка выполнения запроса Arango: " + r.text);

            JObject resp = JObject.Parse(r.text);
            return (resp);
        }
        public JObject GetNextPart(string base_name, JObject part)
        {
            if (!(bool)part["hasMore"]) return (null);

            string url = adb_url + "/_db/" + base_name + "/_api/cursor/" + part["id"];
            RequestResult r = Request("PUT", url, "");
            if (r.code != 200) throw new Exception("Ошибка выполнения запроса Arango: " + r.text);

            JObject resp = JObject.Parse(r.text);
            return (resp);
        }
        public JObject GetBaseQuery(string base_name, string query_name, string[] param_name = null, string[] param_value = null)
        {
            string url = adb_url + "/_db/" + base_name + "/_api/cursor";
            string request = "{\"query\":\"FOR q IN Query FILTER q._id=='Query/" + query_name + "' RETURN q.text\"}";
            RequestResult r = Request("POST", url, request);
            if (r.code != 201) throw new Exception(r.text);

            JObject resp = JObject.Parse(r.text);
            string query = (string)resp["result"][0];
            request = "{\"query\":\"" + query + "\"";
            if (param_name !=null)
            {
                request += ",\"bindVars\":{";
                for (int i = 0; i < param_name.Length; i++)
                {
                    if (i > 0) request += ",";
                    request += "\"" + param_name[i] + "\":" + param_value[i] + "";
                }
                request += "}";
            }
            request += ", \"batchSize\":1000}";

            r = Request("POST", url, request);
            if (r.code != 201) throw new Exception("Ошибка выполнения запроса Arango: " + r.text);

            resp = JObject.Parse(r.text);
            return (resp);
        }
        public async Task<JObject> GetBaseQueryAsync(string base_name, string query_name, string[] param_name = null, string[] param_value = null)
        {
            string url = adb_url + "/_db/" + base_name + "/_api/cursor";
            string request = "{\"query\":\"FOR q IN Query FILTER q._id=='Query/" + query_name + "' RETURN q.text\"}";
            RequestResult r = await RequestAsync("POST", url, request);
            if (r.code != 201) throw new Exception(r.text);

            JObject resp = JObject.Parse(r.text);
            string query = (string)resp["result"][0];
            request = "{\"query\":\"" + query + "\"";
            if (param_name != null)
            {
                request += ",\"bindVars\":{";
                for (int i = 0; i < param_name.Length; i++)
                {
                    if (i > 0) request += ",";
                    request += "\"" + param_name[i] + "\":" + param_value[i] + "";
                }
                request += "}";
            }
            request += ", \"batchSize\":1000}";

            r = Request("POST", url, request);
            if (r.code != 201) throw new Exception("Ошибка выполнения запроса: " + r.text);

            resp = JObject.Parse(r.text);
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
                    request.ContentType = "text/json";
                    request.ContentLength = data.Length;
                    dataStream = request.GetRequestStream();
                    dataStream.Write(data, 0, data.Length);
                    dataStream.Close();
                }
                request.Timeout = 180000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                result.code = (int)response.StatusCode;
                result.text = responseFromServer;
            }
            catch (WebException e)
            {
                HttpWebResponse httpResponse = (HttpWebResponse)e.Response;
                if (e.Response != null) result.code = (int)httpResponse.StatusCode;
                else result.code = 0;
                result.text = e.Message;
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
                    request.ContentType = "text/json";
                    request.ContentLength = data.Length;
                    dataStream = request.GetRequestStream();
                    dataStream.Write(data, 0, data.Length);
                    dataStream.Close();
                }
                request.Timeout = 180000;
                HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = await reader.ReadToEndAsync();
                result.code = (int)response.StatusCode;
                result.text = responseFromServer;
            }
            catch (WebException e)
            {
                HttpWebResponse httpResponse = (HttpWebResponse)e.Response;
                if (e.Response != null) result.code = (int)httpResponse.StatusCode;
                else result.code = 0;
                result.text = e.Message;
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