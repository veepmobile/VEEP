using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.BLL.Authorization
{
    public class AuthLog
    {
        public string login { get; set; }
        public string password { get; set; }
        public string ip { get; set; }
        public List<string> stages { get; set; }
        public AuthenticationType? auth_result { get; set; }
        public string session_id { get; set; }
        public string exception { get; set; }
        public string browser { get; set; }

        public AuthLog()
        {
            stages = new List<string>();
        }

        public AuthLog(string login, string password,string ip,string browser)
        {
            this.login = login;
            this.password = password;
            this.ip = ip;
            this.browser = browser;
            stages = new List<string>();
        }
    }
}