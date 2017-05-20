using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss
{
    public class DisfindSearchResult
    {
        /// <summary>
        /// 0 - не найдено, 1 - найдено, -1 - недостаточно информации -3 - Не готов
        /// </summary>
        public int IsFinded { get; set; }

        public string Name { get; set; }
        public string Ogrn { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Address { get; set; }
        public string FormationDate { get; set; }


        public string ErrorMessage { get; set; }
        public int ErrorType { get; set; }
        public string SearchOgrn { get; set; }
    }
}