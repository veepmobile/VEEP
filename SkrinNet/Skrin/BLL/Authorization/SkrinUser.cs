using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Skrin.BLL.Infrastructure;


namespace Skrin.BLL.Authorization
{
    public class SkrinUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Пользователь ограничен по IP
        /// </summary>
        public bool HasIpRestriction { get; set; }
        public List<string> AllowedIps { get; set; }

        /// <summary>
        /// ПОльзователь имеет привязку к компу
        /// </summary>
        public bool UsePCBinding { get; set; }
        public string BoundIp { get; set; }        
        public string BoundBrowser { get; set; }

        
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Пользователь согласился использовать облако
        /// </summary>
        public bool UseCloud { get; set; }

        public List<Access> SitesAccess { get; set; }

        
        
        public string FullAccessName
        {
            get { return string.Join(",", SitesAccess.Where(p => p.IsOutOfDate == false).Select(p => p.AccessType.GetDescription())); }
        }

        public int GroupLimit
        {
            get { return SitesAccess.Where(p => p.IsOutOfDate == false).Select(p => p.GroupLimit).DefaultIfEmpty().Max(); }
        }

        public int EgrulMonitorLimit
        {
            get { return SitesAccess.Where(p => p.IsOutOfDate == false).Select(p => p.EgrulMonitorLimit).DefaultIfEmpty().Max(); }
        }

        public int MessageGroupLimit
        {
            get { return SitesAccess.Where(p => p.IsOutOfDate == false).Select(p => p.MessageGroupLimit).DefaultIfEmpty().Max(); }
        }

        public int UpdateGroupLimit
        {
            get { return SitesAccess.Where(p => p.IsOutOfDate == false).Select(p => p.UpdateGroupLimit).DefaultIfEmpty().Max(); }
        }

        public int FileSizeLimit
        {
            get { return SitesAccess.Where(p => p.IsOutOfDate == false).Select(p => p.FileSizeLimit).DefaultIfEmpty().Max(); }
        }

         public int EgrulGroupLimit
        {
            get { return SitesAccess.Where(p => p.IsOutOfDate == false).Select(p => p.EgrulGroupLimit).DefaultIfEmpty().Max(); }
        }
    }
}