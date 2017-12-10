using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace RestService.BLL
{
    public class Configs
    {
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
        }

    
        /// <summary>
        /// Host платежного шлюза
        /// </summary>
        public static string MerchantHost
        {
            get
            {
                //return WebConfigurationManager.AppSettings["merchantHost"].ToString();
                return WebConfigurationManager.AppSettings["merchantHost0"].ToString();
            }
        }

        /// <summary>
        /// Логин для подключения к платежному шлюзу
        /// </summary>
        public static string MerchantUser
        {
            get
            {
                return WebConfigurationManager.AppSettings["merchantUser0"].ToString();
            }
        }
        /// <summary>
        /// Пароль для подключения к платежному шлюзу
        /// </summary>
        public static string MerchantPsw
        {
            get
            {
                return WebConfigurationManager.AppSettings["merchantPsw0"].ToString();
            }
        }

        /*
        /// <summary>
        /// Логин для подключения к платежному шлюзу (Luce)
        /// </summary>
        public static string MerchantUser2
        {
            get
            {
                return WebConfigurationManager.AppSettings["merchantUser2"].ToString();
            }
        }
        /// <summary>
        /// Пароль для подключения к платежному шлюзу (Luce)
        /// </summary>
        public static string MerchantPsw2
        {
            get
            {
                return WebConfigurationManager.AppSettings["merchantPsw2"].ToString();
            }
        }

        /// <summary>
        /// Логин для подключения к платежному шлюзу (Vogue)
        /// </summary>
        public static string MerchantUser3
        {
            get
            {
                return WebConfigurationManager.AppSettings["merchantUser3"].ToString();
            }
        }
        /// <summary>
        /// Пароль для подключения к платежному шлюзу (Vogue)
        /// </summary>
        public static string MerchantPsw3
        {
            get
            {
                return WebConfigurationManager.AppSettings["merchantPsw3"].ToString();
            }
        }

        /// <summary>
        /// Логин для подключения к платежному шлюзу (Хлеб и вино - Улица 1905 года)
        /// </summary>
        public static string MerchantUser4
        {
            get
            {
                return WebConfigurationManager.AppSettings["merchantUser4"].ToString();
            }
        }
        /// <summary>
        /// Пароль для подключения к платежному шлюзу (Хлеб и вино - Улица 1905 года)
        /// </summary>
        public static string MerchantPsw4
        {
            get
            {
                return WebConfigurationManager.AppSettings["merchantPsw4"].ToString();
            }
        }
        */

        /// <summary>
        /// Host ACS
        /// </summary>
        public static string ACSHost
        {
            get
            {
                return WebConfigurationManager.AppSettings["acsHost"].ToString();
            }
        }

        //Имя конечной точки Интеграционного модуля
        public static string GetEndpoint(int restaurantID)
        {
            switch (restaurantID)
            {
                case 730410001: /*test*/
                    return "BasicHttpBinding_IIntegrationCMD0";
                case 202930001: /*Luce*/
                    return "BasicHttpBinding_IIntegrationCMD";
                case 209631111: /*Vogue*/
                    return "BasicHttpBinding_IIntegrationCMD1";
                case 136230001: /*Brisket*/
                    return "BasicHttpBinding_IIntegrationCMD3";
                case 880540002: /*Хлеб и вино - Улица 1905 года*/
                    return "BasicHttpBinding_IIntegrationCMD2";
                case 880540005: /*Хлеб и вино - Тверская*/
                    return "BasicHttpBinding_IIntegrationCMD4";
                case 880540004: /*Хлеб и вино - Никитская*/
                    return "BasicHttpBinding_IIntegrationCMD5";
                case 880540003: /*Хлеб и вино - Маросейка*/
                    return "BasicHttpBinding_IIntegrationCMD6";
                case 880540001: /*Хлеб и вино - Патриарший*/
                    return "BasicHttpBinding_IIntegrationCMD7";
                case 368250001: /*ZOO*/
                    return "BasicHttpBinding_IIntegrationCMD8";
                case 125010001: /*White*/
                    return "BasicHttpBinding_IIntegrationCMD9";
                case 784680004: /*Чача-Атриум*/
                    return "BasicHttpBinding_IIntegrationCMD10";
                case 784680001: /*The Noodle House РИО*/
                    return "BasicHttpBinding_IIntegrationCMD11";
                case 784680507: /*Чача-Химки_Мега*/
                    return "BasicHttpBinding_IIntegrationCMD12";
                case 784680002: /*The Noodle House Химки_Мега*/
                    return "BasicHttpBinding_IIntegrationCMD13";
                case 361750001: /*KWAK pub Мичуринский*/
                    return "BasicHttpBinding_IIntegrationCMD14";
                case 361750003: /*KWAK pub Покровка*/
                    return "BasicHttpBinding_IIntegrationCMD15";
            }
            return "";
        }

        //Адрес конечной точки Интеграционного модуля
        public static string GetAddress(int restaurantID)
        {
            switch (restaurantID)
            {
                case 730410001: /*test*/
                    return "http://95.84.162.220:9090/";
                case 202930001: /*Luce*/
                    return "http://92.38.32.63:9090/";
                case 209631111: /*Vogue*/
                    return "http://185.26.193.5:9090/";
                case 136230001: /*Brisket*/
                    return "http://92.38.32.79:9090/";
                case 368250001: /*ZOO*/
                    return "http://195.91.131.141:9090/";
                case 125010001: /*White*/
                    return "http://37.230.253.114:9090/";
                case 880540002: /*Хлеб и вино - Улица 1905 года*/
                    return "http://95.84.146.191:9090/";
                // return "http://95.84.168.113:1780/";
                case 880540005: /*Хлеб и вино - Тверская*/
                    return "http://95.84.195.96:9090/";
                case 880540004: /*Хлеб и вино - Никитская*/
                    return "http://109.173.75.233:9090/";
                case 880540003: /*Хлеб и вино - Маросейка*/
                    return "http://95.84.240.46:9090/";
                case 880540001: /*Хлеб и вино - Патриарший*/
                    return "http://95.84.195.57:9090/";
                case 784680004: /*Чача-Атриум*/
                    return "http://213.33.203.206:57001/";
                case 784680001: /*The Noodle House РИО*/
                    return "http://79.174.68.175:57002/";
                case 784680507: /*Чача-Химки_Мега*/
                    return "http://79.174.68.175:57003/";
                case 784680002: /*The Noodle House Химки_Мега*/
                    return "http://79.174.68.175:57004/";
                case 361750001: /*KWAK pub Мичуринский*/
                    return "http://46.38.47.43:9090/";
                case 361750003: /*KWAK pub Покровка*/
                    return "http://82.204.144.18:9090/";
            }
            return "";
        }

        //MerchatUser
        public static string GetMerchantUser(int restaurantID)
        {
            switch (restaurantID)
            {
                case 730410001: /*test*/
                    return WebConfigurationManager.AppSettings["merchantUser0"].ToString();
                case 202930001: /*Luce*/
                    return WebConfigurationManager.AppSettings["merchantUser2"].ToString();
                case 209631111: /*Vogue*/
                    return WebConfigurationManager.AppSettings["merchantUser3"].ToString();
                case 136230001: /*Brisket*/
                    return WebConfigurationManager.AppSettings["merchantUser5"].ToString();
                case 880540002: /*Хлеб и вино - Улица 1905 года*/
                    return WebConfigurationManager.AppSettings["merchantUser4"].ToString();
                case 880540005: /*Хлеб и вино - Тверская*/
                    return WebConfigurationManager.AppSettings["merchantUser6"].ToString();
                case 880540004: /*Хлеб и вино - Никитская*/
                    return WebConfigurationManager.AppSettings["merchantUser7"].ToString();
                case 880540003: /*Хлеб и вино - Маросейка*/
                    return WebConfigurationManager.AppSettings["merchantUser8"].ToString();
                case 880540001: /*Хлеб и вино - Патриарший*/
                    return WebConfigurationManager.AppSettings["merchantUser9"].ToString();
                case 368250001: /*ZOO*/
                    return WebConfigurationManager.AppSettings["merchantUser10"].ToString();
                case 125010001: /*White*/
                    return WebConfigurationManager.AppSettings["merchantUser11"].ToString();
                case 784680004: /*Чача-Атриум*/
                    return WebConfigurationManager.AppSettings["merchantUser12"].ToString();
                case 784680001: /*The Noodle House РИО*/
                    return WebConfigurationManager.AppSettings["merchantUser13"].ToString();
                case 784680507: /*Чача-Химки_Мега*/
                    return WebConfigurationManager.AppSettings["merchantUser14"].ToString();
                case 784680002: /*The Noodle House Химки_Мега*/
                    return WebConfigurationManager.AppSettings["merchantUser15"].ToString();
                case 361750001: /*KWAK pub Мичуринский*/
                    return WebConfigurationManager.AppSettings["merchantUser16"].ToString();
                case 361750003: /*KWAK pub Покровка*/
                    return WebConfigurationManager.AppSettings["merchantUser17"].ToString();
            }
            return "";
        }

        public static string GetMerchantUserMode(int mode)
        {
            switch (mode)
            {
                case 0: /*test*/
                    return WebConfigurationManager.AppSettings["merchantUser0"].ToString();
                case 1: /*привязка карты*/
                    return WebConfigurationManager.AppSettings["merchantUser"].ToString();
                case 2: /*Luce*/
                    return WebConfigurationManager.AppSettings["merchantUser2"].ToString();
                case 3: /*Vogue*/
                    return WebConfigurationManager.AppSettings["merchantUser3"].ToString();
                case 4: /*Хлеб и вино - Улица 1905 года*/
                    return WebConfigurationManager.AppSettings["merchantUser4"].ToString();
                case 5: /*Brisket*/
                    return WebConfigurationManager.AppSettings["merchantUser5"].ToString();
                case 6: /*Хлеб и вино - Тверская*/
                    return WebConfigurationManager.AppSettings["merchantUser6"].ToString();
                case 7: /*Хлеб и вино - Никитская*/
                    return WebConfigurationManager.AppSettings["merchantUser7"].ToString();
                case 8: /*Хлеб и вино - Маросейка*/
                    return WebConfigurationManager.AppSettings["merchantUser8"].ToString();
                case 9: /*Хлеб и вино - Патриарший*/
                    return WebConfigurationManager.AppSettings["merchantUser9"].ToString();
                case 10: /*ZOO*/
                    return WebConfigurationManager.AppSettings["merchantUser10"].ToString();
                case 11: /*White*/
                    return WebConfigurationManager.AppSettings["merchantUser11"].ToString();
                case 12: /*Чача-Атриум*/
                    return WebConfigurationManager.AppSettings["merchantUser12"].ToString();
                case 13: /*The Noodle House РИО*/
                    return WebConfigurationManager.AppSettings["merchantUser13"].ToString();
                case 14: /*Чача-Химки_Мега*/
                    return WebConfigurationManager.AppSettings["merchantUser14"].ToString();
                case 15: /*The Noodle House Химки_Мега*/
                    return WebConfigurationManager.AppSettings["merchantUser15"].ToString();
                case 16: /*KWAK pub Мичуринский*/
                    return WebConfigurationManager.AppSettings["merchantUser16"].ToString();
                case 17: /*KWAK pub Покровка*/
                    return WebConfigurationManager.AppSettings["merchantUser17"].ToString();
            }
            return "";
        }

        public static string GetMerchantUserTip() //мерчант для чаевых
        {
            return WebConfigurationManager.AppSettings["merchantUserTip"].ToString();
        }

        public static string GetMerchantPswTip()  //мерчант для чаевых
        {
            return WebConfigurationManager.AppSettings["merchantPswTip"].ToString();
        }

        //MerchatPsw
        public static string GetMerchantPsw(int restaurantID)
        {
            switch (restaurantID)
            {
                case 730410001: /*test*/
                    return WebConfigurationManager.AppSettings["merchantPsw0"].ToString();
                case 202930001: /*Luce*/
                    return WebConfigurationManager.AppSettings["merchantPsw2"].ToString();
                case 209631111: /*Vogue*/
                    return WebConfigurationManager.AppSettings["merchantPsw3"].ToString();
                case 880540002: /*Хлеб и вино - Улица 1905 года*/
                    return WebConfigurationManager.AppSettings["merchantPsw4"].ToString();
                case 136230001: /*Brisket*/
                    return WebConfigurationManager.AppSettings["merchantPsw5"].ToString();
                case 880540005: /*Хлеб и вино - Тверская*/
                    return WebConfigurationManager.AppSettings["merchantPsw6"].ToString();
                case 880540004: /*Хлеб и вино - Никитская*/
                    return WebConfigurationManager.AppSettings["merchantPsw7"].ToString();
                case 880540003: /*Хлеб и вино - Маросейка*/
                    return WebConfigurationManager.AppSettings["merchantPsw8"].ToString();
                case 880540001: /*Хлеб и вино - Патриарший*/
                    return WebConfigurationManager.AppSettings["merchantPsw9"].ToString();
                case 368250001: /*ZOO*/
                    return WebConfigurationManager.AppSettings["merchantPsw10"].ToString();
                case 125010001: /*White*/
                    return WebConfigurationManager.AppSettings["merchantPsw11"].ToString();
                case 784680004: /*Чача-Атриум*/
                    return WebConfigurationManager.AppSettings["merchantPsw12"].ToString();
                case 784680001: /*The Noodle House РИО*/
                    return WebConfigurationManager.AppSettings["merchantPsw13"].ToString();
                case 784680507: /*Чача-Химки_Мега*/
                    return WebConfigurationManager.AppSettings["merchantPsw14"].ToString();
                case 784680002: /*The Noodle House Химки_Мега*/
                    return WebConfigurationManager.AppSettings["merchantPsw15"].ToString();
                case 361750001: /*KWAK pub Мичуринский*/
                    return WebConfigurationManager.AppSettings["merchantPsw16"].ToString();
                case 361750003: /*KWAK pub Покровка*/
                    return WebConfigurationManager.AppSettings["merchantPsw17"].ToString();
            }
            return "";
        }

        public static string GetMerchantPswMode(int mode)
        {
            switch (mode)
            {
                case 0: /*test*/
                    return WebConfigurationManager.AppSettings["merchantPsw0"].ToString();
                case 1: /*привязка карты*/
                    return WebConfigurationManager.AppSettings["merchantPsw"].ToString();
                case 2: /*Luce*/
                    return WebConfigurationManager.AppSettings["merchantPsw2"].ToString();
                case 3: /*Vogue*/
                    return WebConfigurationManager.AppSettings["merchantPsw3"].ToString();
                case 4: /*Хлеб и вино - Улица 1905 года*/
                    return WebConfigurationManager.AppSettings["merchantPsw4"].ToString();
                case 5: /*Brisket*/
                    return WebConfigurationManager.AppSettings["merchantPsw5"].ToString();
                case 6: /*Хлеб и вино - Тверская*/
                    return WebConfigurationManager.AppSettings["merchantPsw6"].ToString();
                case 7: /*Хлеб и вино - Никитская*/
                    return WebConfigurationManager.AppSettings["merchantPsw7"].ToString();
                case 8: /*Хлеб и вино - Маросейка*/
                    return WebConfigurationManager.AppSettings["merchantPsw8"].ToString();
                case 9: /*Хлеб и вино - Патриарший*/
                    return WebConfigurationManager.AppSettings["merchantPsw9"].ToString();
                case 10: /*ZOO*/
                    return WebConfigurationManager.AppSettings["merchantPsw10"].ToString();
                case 11: /*White*/
                    return WebConfigurationManager.AppSettings["merchantPsw11"].ToString();
                case 12: /*Чача-Атриум*/
                    return WebConfigurationManager.AppSettings["merchantPsw12"].ToString();
                case 13: /*The Noodle House РИО*/
                    return WebConfigurationManager.AppSettings["merchantPsw13"].ToString();
                case 14: /*Чача-Химки_Мега*/
                    return WebConfigurationManager.AppSettings["merchantPsw14"].ToString();
                case 15: /*The Noodle House Химки_Мега*/
                    return WebConfigurationManager.AppSettings["merchantPsw15"].ToString();
                case 16: /*KWAK pub Мичуринский*/
                    return WebConfigurationManager.AppSettings["merchantPsw16"].ToString();
                case 17: /*KWAK pub Покровка*/
                    return WebConfigurationManager.AppSettings["merchantPsw17"].ToString();
            }
            return "";
        }

        //Mode
        public static int GetMode(int restaurantID)
        {
            switch (restaurantID)
            {
                case 730410001: /*test*/
                    return 0;
                case 202930001: /*Luce*/
                    return 2;
                case 209631111: /*Vogue*/
                    return 3;
                case 880540002: /*Хлеб и вино - Улица 1905 года*/
                    return 4;
                case 136230001: /*Brisket*/
                    return 5;
                case 880540005: /*Хлеб и вино - Тверская*/
                    return 6;
                case 880540004: /*Хлеб и вино - Никитская*/
                    return 7;
                case 880540003: /*Хлеб и вино - Маросейка*/
                    return 8;
                case 880540001: /*Хлеб и вино - Патриарший*/
                    return 9;
                case 368250001: /*ZOO*/
                    return 10;
                case 125010001: /*White*/
                    return 11;
                case 784680004: /*Чача-Атриум*/
                    return 12;
                case 784680001: /*The Noodle House РИО*/
                    return 13;
                case 784680507: /*Чача-Химки_Мега*/
                    return 14;
                case 784680002: /*The Noodle House Химки_Мега*/
                    return 15;
                case 361750001: /*KWAK pub Мичуринский*/
                    return 16;
                case 361750003: /*KWAK pub Покровка*/
                    return 17;
            }
            return 1;
        }
    }
}