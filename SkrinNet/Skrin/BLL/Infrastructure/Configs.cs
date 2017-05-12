using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Skrin.BLL.Infrastructure
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
                return WebConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;
            }
        }

        /// <summary>
        /// Ip адрес сервиса Redis
        /// </summary>
        public static string RedisServer
        {
            get
            {
                return WebConfigurationManager.AppSettings["redis_server"];
            }
        }

        /// <summary>
        /// Путь лога с ошибками
        /// </summary>
        public static string LogPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["log_path"];
            }
        }


        /// <summary>
        /// Тестовая версия сайта или боевая
        /// </summary>
        public static bool IsTest
        {
            get
            {
                try
                {
                    return bool.Parse(WebConfigurationManager.AppSettings["is_test"]);
                }
                catch
                {
                    return false;
                }
            }
        }

        #region Sphinx config old

        /// <summary>
        /// Ip адрес сервера Сфинкс
        /// </summary>
        public static string SphinxServer
        {
            get
            {
                return WebConfigurationManager.AppSettings["sphinx_server"];
            }
        }
        /// <summary>
        /// Ip адрес второго сервера Сфинкс
        /// </summary>
        public static string SphinxServer2
        {
            get
            {
                return WebConfigurationManager.AppSettings["sphinx_server2"];
            }
        }
        /// <summary>
        /// Порт сервера Сфинкс
        /// </summary>
        public static string SphinxPort
        {
            get
            {
                return WebConfigurationManager.AppSettings["sphinx_port"];
            }
        }
        /// <summary>
        /// Порт второго сервера Сфинкс
        /// </summary>
        public static string SphinxPort2
        {
            get
            {
                return WebConfigurationManager.AppSettings["sphinx_port2"];
            }
        }

        #endregion

        /// <summary>
        /// Sphinx - поиск ЮЛ по реквизитам (searchreq)
        /// </summary>
        public static string SphinxSearchreqServer
        {
            get { return WebConfigurationManager.AppSettings["searchreq_server"]; }
        }
        public static string SphinxSearchreqPort
        {
            get { return WebConfigurationManager.AppSettings["searchreq_port"]; }
        }

        /// <summary>
        /// Sphinx - поиск ИП по реквизитам (searchip)
        /// </summary>
        public static string SphinxSearchIPServer
        {
            get { return WebConfigurationManager.AppSettings["searchip_server"]; }
        }
        public static string SphinxSearchIPPort
        {
            get { return WebConfigurationManager.AppSettings["searchip_port"]; }
        }

        /// <summary>
        /// Sphinx - исполнительное производство (debt)
        /// </summary>
        public static string SphinxDebtServer
        {
            get { return WebConfigurationManager.AppSettings["debt_server"]; }
        }
        public static string SphinxDebtPort
        {
            get { return WebConfigurationManager.AppSettings["debt_port"]; }
        }

        /// <summary>
        /// Sphinx - сообщения о банкротстве (bankruptcy)
        /// </summary>
        public static string SphinxBankruptcyServer
        {
            get { return WebConfigurationManager.AppSettings["bankruptcy_server"]; }
        }
        public static string SphinxBankruptcyPort
        {
            get { return WebConfigurationManager.AppSettings["bankruptcy_port"]; }
        }

        /// <summary>
        /// Sphinx - существенные факты (disclosure)
        /// </summary>
        public static string SphinxDisclosureServer
        {
            get { return WebConfigurationManager.AppSettings["disclosure_server"]; }
        }
        public static string SphinxDisclosurePort
        {
            get { return WebConfigurationManager.AppSettings["disclosure_port"]; }
        }

        /// <summary>
        /// Sphinx - факты деятельности (fedresurs)
        /// </summary>
        public static string SphinxFedresursServer
        {
            get { return WebConfigurationManager.AppSettings["fedresurs_server"]; }
        }
        public static string SphinxFedresursPort
        {
            get { return WebConfigurationManager.AppSettings["fedresurs_port"]; }
        }

        /// <summary>
        /// Sphinx - арбитраж (pravo)
        /// </summary>
        public static string SphinxPravoServer
        {
            get { return WebConfigurationManager.AppSettings["pravo_server"]; }
        }
        public static string SphinxPravoPort
        {
            get { return WebConfigurationManager.AppSettings["pravo_port"]; }
        }

        /// <summary>
        /// Sphinx - сообщения о госрегистрации (vestnik)
        /// </summary>
        public static string SphinxVestnikServer
        {
            get { return WebConfigurationManager.AppSettings["vestnik_server"]; }
        }
        public static string SphinxVestnikPort
        {
            get { return WebConfigurationManager.AppSettings["vestnik_port"]; }
        }

        /// <summary>
        /// Sphinx - поиск Украина (ua_skrin)
        /// </summary>
        public static string SphinxSearchUAServer
        {
            get { return WebConfigurationManager.AppSettings["searchua_server"]; }
        }
        public static string SphinxSearchUAPort
        {
            get { return WebConfigurationManager.AppSettings["searchua_port"]; }
        }



        /// <summary>
        /// Путь на облако
        /// </summary>
        public static string Cloud
        {
            get
            {
                return WebConfigurationManager.AppSettings["cloud"];
            }
        }

        /// <summary>
        /// Символы заменители
        /// </summary>
        public static string Replacer
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["replacer"];
                }
                catch
                {
                    return "*****";
                }
            }
        }

        /// <summary>
        /// Символы заменители даты
        /// </summary>
        public static string DateReplacer
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["date_replacer"];
                }
                catch
                {
                    return "**.**.****";
                }
            }
        }

        /// <summary>
        /// Путь к папке с документами
        /// </summary>
        public static string DocPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["doc_path"];
            }
        }

        /// <summary>
        /// Путь к папке с документами
        /// </summary>
        public static string UADocPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["ua_doc_path"];
            }
        }

        /// <summary>
        /// Путь к папке с документами
        /// </summary>
        public static string EgrulPdfDocPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["egrulpdf_doc_path"];
            }
        }
        /// <summary>
        /// Путь к папке с документами
        /// </summary>
        public static string EgripPdfDocPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["egrippdf_doc_path"];
            }
        }
        /// <summary>
        /// Адрес сервера smtp
        /// </summary>
        public static string Smtp
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["smtp"];
                }
                catch
                {
                    return "mail2.skrin.ru";
                }
            }
        }

        /// <summary>
        /// Адреса копий писем
        /// </summary>
        public static List<string> EmailCopies
        {
            get
            {
                try
                {
                    return WebConfigurationManager.AppSettings["email_copy"].Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                catch
                {
                    return new List<string>();
                }
            }
        }

        /// <summary>
        /// Нужно ли следить за Timout
        /// </summary>
        public static bool NeedWatchingTimout
        {
            get
            {
                try
                {
                    return Boolean.Parse(WebConfigurationManager.AppSettings["watching_timeout"]);
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Строка подключения к серверу Postgre
        /// </summary>
        public static string PostgreConnectionString
        {
            get
            {
                return WebConfigurationManager.AppSettings["postgre_connection"];
            }
        }

        public static string ElasticsearcherServer
        {
            get
            {
                return WebConfigurationManager.AppSettings["elasticsearcher_server"];
            }
        }

        public static string ElasticDBsearchServerUri
        {
            get
            {
                return "http://"+WebConfigurationManager.AppSettings["elasticsearch_dbsearch_server"];
            }
        }

        public static string ElasticDBsearchServerIndex
        {
            get
            {
                return WebConfigurationManager.AppSettings["elasticsearch_dbsearch_index"];
            }
        }

        public static string ElasticDebtServerUri
        {
            get
            {
                return "http://" + WebConfigurationManager.AppSettings["elasticsearch_debt_server"];
            }
        }

        /// <summary>
        /// Место хранения  файлов пользователя
        /// </summary>
        public static string UserFilesDir
        {
            get
            {
                return WebConfigurationManager.AppSettings["user_files_dir"];
            }
        }
        /// <summary>
        /// Место хранения удаленных файлов пользователя
        /// </summary>
        public static string UserDeletedFilesDir
        {
            get
            {
                return WebConfigurationManager.AppSettings["user_deleted_files_dir"];
            }
        }
    }
}