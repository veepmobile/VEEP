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
    public class ElasticClient
    {
        private static string es_url;

        public ElasticClient(string var_es_url)
        {
            es_url = "http://" + ConfigurationManager.AppSettings[var_es_url];
        }
        public ElasticClient()
        {
            es_url = "http://" + ConfigurationManager.AppSettings["elasticsearcher_server"];
        }

        public JObject GetQuery(string url, string json)
        {
            url = es_url + url;

            RequestResult r = Request("POST", url, json);
            if (r.code != 200) throw new Exception("Ошибка выполнения запроса ElasticSearcher: " + r.text);

            JObject resp = JObject.Parse(r.text);
            return (resp);
        }
        public async Task<JObject> GetQueryAsync(string url, string json)
        {
            url = es_url + url;

            RequestResult r = await RequestAsync("POST", url, json);
            if (r.code != 200) throw new Exception("Ошибка выполнения запроса ElasticSearcher: " + r.text);

            JObject resp = JObject.Parse(r.text);
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
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                result.code = (int)response.StatusCode; ;
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
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = await reader.ReadToEndAsync();
                result.code = (int)response.StatusCode; ;
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