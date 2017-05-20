using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Menu
{


    public class Menu
    {
        public int Id { get; set; }

        public List<Menu> SubMenu { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public List<Tab> Tabs { get; set; }

    }
}