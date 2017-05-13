using Skrin.BLL.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Menu
{
    public class Tab
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public IEnumerable<AccessType> Accesses { get; set; }
    }
}