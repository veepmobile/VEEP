using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class LicenseModel
    {
        private HorisonrtalTable _license;
        public LicenseModel(HorisonrtalTable license)
        {
            _license = license;
        }

        public HorisonrtalTable LicenseTable
        {
            get
            {
                return _license;
            }
        }
    }
}