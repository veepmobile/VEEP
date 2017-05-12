using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    /// <summary>
    /// Класс-контейнер для хранения сгенерированных условий для запросов Sphinx
    /// </summary>
    public class SphinxCondition
    {
        public string Match { get; set; }

        public string Where { get; set; }
    }
}