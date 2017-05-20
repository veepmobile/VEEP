using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class ConstitutorsModel
    {
        private HorisonrtalTable _constitutors;
        private HorisonrtalTable _constitutors_history;
        public ConstitutorsModel(HorisonrtalTable constitutors, HorisonrtalTable constitutors_history)
        {
            _constitutors = constitutors;
            _constitutors_history = constitutors_history;
        }

        public HorisonrtalTable ConstitutorsTable
        {
            get
            {
                return _constitutors;
            }
        }
        public HorisonrtalTable ConstitutorsHistoryTable
        {
            get
            {
                return _constitutors_history;
            }
        }
    }
}