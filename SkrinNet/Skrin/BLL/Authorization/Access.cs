using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;

namespace Skrin.BLL.Authorization
{
    /// <summary>
    /// Информация о доступе по конкретной подписке
    /// </summary>
    public class Access
    {

        /// <summary>
        /// Тип подписки
        /// </summary>
        public AccessType AccessType { get; set; }
        /// <summary>
        /// Истек срок действия
        /// </summary>
        public bool IsOutOfDate { get; set; }

        /// <summary>
        /// Является ли Тестовым
        /// </summary>
        public bool IsTest { get; set; }

        /// <summary>
        /// Лимит выписок ЕГРЮЛ
        /// </summary>
        public int EgrulMonitorLimit { get; set; }

        /// <summary>
        /// Лимит кол-ва компаний в группе
        /// </summary>
        public int GroupLimit { get; set; }

        /// <summary>
        /// Лимит групп по сообщениям
        /// </summary>
        public int MessageGroupLimit { get; set; }

        /// <summary>
        /// Лимит групп по обновлениям
        /// </summary>
        public int UpdateGroupLimit { get; set; }

        /// <summary>
        /// Предельный размер файлов облака
        /// </summary>
        public int FileSizeLimit { get; set; }
        
         /// <summary>
        /// Лимит групп по мониторингу ЕГРЮЛ
        /// </summary>
        public int EgrulGroupLimit { get; set; }

        /// <summary>
        /// Имя из описания
        /// </summary>
        public string Name
        {
            get { return AccessType.GetDescription(); }
        }
    }
}