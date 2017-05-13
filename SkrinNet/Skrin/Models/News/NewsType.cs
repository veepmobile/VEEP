using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.News
{
    public enum NewsType
    {
        /// <summary>
        /// 1-Статитстика предприятий и ИП
        /// </summary>
        Statistc = 1,
        /// <summary>
        /// 2-Новое на сайте
        /// </summary>
        SiteUpdates = 2,
        /// <summary>
        /// 3-Последние сообщения
        /// </summary>
        LastReports = 3,
        /// <summary>
        /// 4-Корпоративные события
        /// </summary>
        LastEvents = 4,
        /// <summary>
        /// 5-Аналитика
        /// </summary>
        Analitics = 5,
        /// <summary>
        /// 6-УЦ
        /// </summary>
        UC = 6,
        /// <summary>
        /// 7-Последние обновления компаний
        /// </summary>
        ULUpdates = 7
    }
}