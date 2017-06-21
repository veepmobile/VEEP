﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

public enum HttpVerb
{
    GET,
    POST,
    PUT,
    DELETE
}

namespace HttpUtils
{
    public class RestClient
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }

        public RestClient()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = "text/xml";
            PostData = "";
        }
        public RestClient(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = "text/xml";
            PostData = "";
        }
        public RestClient(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            PostData = "";
        }

        public RestClient(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            PostData = postData;
        }

        public RestClient(string endpoint, HttpVerb method, string postData, string contentType)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = postData;
        }


        public string MakeRequest()
        {
            return MakeRequest("");
        }

        public string MakeRequest(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("utf-8").GetBytes(PostData);
                //var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }


            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;
            }
        }

        public string MakeRequest(string parameters, int encode)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

            request.Method = Method.ToString();
            //request.ContentLength = 0;
            request.ContentType = ContentType;

            if (encode == 1)
            {
                if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
                {
                    var encoding = new UTF8Encoding();
                    var bytes = Encoding.GetEncoding("utf-8").GetBytes(PostData);
                    //var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                    request.ContentLength = bytes.Length;

                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;
            }
        }

        public string ACSRequest()
        {
            return ACSRequest("");
        }

        public string ACSRequest(string parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);
            request.Method = Method.ToString();
            request.ContentType = "text/html;charset=UTF-8";

            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
            {
                try
                {
                    //var bytes = Encoding.GetEncoding("utf-8").GetBytes(PostData);
                    //request.ContentLength = bytes.Length;
                    //using (var writeStream = request.GetRequestStream())
                    //{
                    //    writeStream.Write(bytes, 0, bytes.Length);
                    //}
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        string responseValue = string.Empty;

                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                            throw new ApplicationException(message);
                        }

                        //using (StreamReader responseStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        using (StreamReader responseStream = new StreamReader(response.GetResponseStream()))
                        {
                            if (responseStream != null)
                                responseValue = responseStream.ReadToEnd();
                        }

                        return responseValue;
                    }
                }
                catch (WebException we)
                {
                    string msg = we.Response.ToString();
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    } // class

}