using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class CapitalModel
    {
        private HorisonrtalTable _capital;
        private HorisonrtalTable _reduce;
        public CapitalModel(HorisonrtalTable capital, HorisonrtalTable reduce)
        {
            _capital = capital;
            _reduce = reduce;
        }

        public HorisonrtalTable CapitalTable
        {
            get
            {
                return _capital;
            }
        }
        public HorisonrtalTable ReduceTable
        {
            get
            {
                return _reduce;
            }
        }
    }
}