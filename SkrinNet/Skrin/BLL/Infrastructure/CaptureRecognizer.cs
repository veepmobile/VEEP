using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;

namespace Skrin.BLL.Infrastructure
{
    public class CaptureRecognizer
    {
        private string _image_path;
        private string _key;
        private string _captcha_id;
        private string _upload_url = "http://antigate.com/in.php";

        public CaptureRecognizer(string image_path, string key)
        {
            _image_path = image_path;
            _key = key;
        }

        /// <summary>
        /// Загружает капчу и выдает результат ее отгадывания
        /// </summary>
        /// <returns></returns>
        public string GetVal()
        {
            Console.WriteLine("Отправляем капчу");
            string upload_result = HttpUploadFile(_upload_url, _image_path, "file", "image/jpeg", InitialiseParametres());
            string result;
            Console.WriteLine(upload_result);
            if (upload_result.Substring(0, 2) == "OK")
            {
                _captcha_id = upload_result.Substring(3);
                Console.WriteLine("Ждем 10 секунд");
                Thread.Sleep(5000);
                Console.WriteLine("Получаем результат");
                result = GetResult();
                Console.WriteLine(result);
                int i_count = 0;
                while (result == "CAPCHA_NOT_READY" && i_count < 30)
                {
                    Console.WriteLine("Еще не готово, ждем 5 сек");
                    Thread.Sleep(5000);
                    result = GetResult();
                }
            }
            else
            {
                result = upload_result;
            }
            return result;
        }

        public string CaptchaId
        {
            get { return _captcha_id; }
        }

        /// <summary>
        /// Отправляет жалобу о неправильно разгаданной капче
        /// </summary>
        public void MakeComplaint()
        {
            string url = string.Format("http://antigate.com/res.php?key={0}&action=reportbad&id={1}", _key, _captcha_id);
            GetPage(url);
        }

        public static decimal GetBalance(string key)
        {
            string url = string.Format("http://antigate.com/res.php?key={0}&action=getbalance", key);
            string balance = GetPage(url);
            var numberFormatInfo = new NumberFormatInfo();
            numberFormatInfo.NumberDecimalSeparator = ".";
            return Decimal.Parse(balance, numberFormatInfo);
        }

        /// <summary>
        /// Разбирает ответ сервера каптч
        /// </summary>
        /// <param name="result"></param>
        /// <param name="parsing_result"></param>
        /// <returns></returns>
        public static bool ParseCapthaResult(string result, out string parsing_result)
        {
            if (result.Substring(0, 2) == "OK")
            {
                parsing_result = result.Substring(3);
                return true;
            }
            switch (result)
            {
                case "ERROR_WRONG_USER_KEY":
                    parsing_result = "неправильный формат ключа учетной записи (длина не равняется 32 байтам)";
                    break;
                case "ERROR_KEY_DOES_NOT_EXIST":
                    parsing_result = "вы использовали неверный captcha ключ в запросе";
                    break;
                case "ERROR_ZERO_BALANCE":
                    parsing_result = "нулевой либо отрицательный баланс";
                    break;
                case "ERROR_NO_SLOT_AVAILABLE":
                    parsing_result = "нет свободных работников в данный момент, попробуйте позже либо повысьте свою максимальную ставку";
                    break;
                case "ERROR_ZERO_CAPTCHA_FILESIZE":
                    parsing_result = "размер капчи которую вы загружаете менее 100 байт";
                    break;
                case "ERROR_TOO_BIG_CAPTCHA_FILESIZE":
                    parsing_result = "ваша капча имеет размер более 100 килобайт";
                    break;
                case "ERROR_WRONG_FILE_EXTENSION":
                    parsing_result = "ваша капча имеет неверное расширение, допустимые расширения jpg,jpeg,gif,png";
                    break;
                case "ERROR_IMAGE_TYPE_NOT_SUPPORTED":
                    parsing_result = "Невозможно определить тип файла капчи, принимаются только форматы JPG, GIF, PNG";
                    break;
                case "ERROR_IP_NOT_ALLOWED":
                    parsing_result = "Невозможно определить тип файла капчи, принимаются только форматы JPG, GIF, PNG";
                    break;
                case "ERROR_WRONG_ID_FORMAT":
                    parsing_result = "некорректный идентификатор капчи, принимаются только цифры";
                    break;
                case "ERROR_CAPTCHA_UNSOLVABLE":
                    parsing_result = "капчу не смогли разгадать 5 разных работников";
                    break;
                default:
                    parsing_result = "Неизвестное сообщение";
                    break;
            }
            return false;
        }

        #region private

        /// <summary>
        /// Инициализует начальные параметры загрузки
        /// </summary>
        /// <returns></returns>
        private NameValueCollection InitialiseParametres()
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("key", _key);
            nvc.Add("method", "post");
            //nvc.Add("numeric", "1");
            return nvc;
        }

        /// <summary>
        /// Выдает результат разгадывания капчи
        /// </summary>
        /// <returns></returns>
        private string GetResult()
        {
            string url = string.Format("http://antigate.com/res.php?key={0}&action=get&id={1}", _key, _captcha_id);
            return GetPage(url);
        }

        /// <summary>
        /// Служебный метод, обеспечивающий get запросы
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetPage(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "ru-ru,ru;q=0.8,en-us;q=0.5,en;q=0.3");
                request.Headers.Add(HttpRequestHeader.AcceptCharset, "windows-1251,utf-8;q=0.7,*;q=0.7");
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US) AppleWebKit/530.0 (KHTML, like Gecko) Chrome/2.0.160.0 Safari/530.0";
                request.AllowAutoRedirect = false;
                request.CookieContainer = new CookieContainer();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string result = reader.ReadToEnd();
                reader.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка связи методом Get:" + ex.Message);
            }
        }

        /// <summary>
        /// Загружает файл с капчей на сервер и выдает результат загрузки
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <param name="paramName"></param>
        /// <param name="contentType"></param>
        /// <param name="nvc"></param>
        /// <returns></returns>
        private string HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {
            //log.Debug(string.Format("Uploading {0} to {1}", file, url));
            Console.WriteLine("Uploading {0} to {1}", file, url);
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                string server_response = reader2.ReadToEnd();
                return server_response;
                //log.Debug(string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd()));
            }
            catch (Exception ex)
            {
                //log.Error("Error uploading file", ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
                throw new Exception("Ошибка загрузка файла с капчей: " + ex);
            }
            finally
            {
                wr = null;
            }
        }

        #endregion
    }
}