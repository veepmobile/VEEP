using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class BranchesModel
    {
        private HorisonrtalTable _branches;
        private HorisonrtalTable _outputs;
        public BranchesModel(HorisonrtalTable branches, HorisonrtalTable outputs)
        {
            _branches = branches;
            _outputs = outputs;
        }

        public HorisonrtalTable BranchesTable
        {
            get
            {
                return _branches;
            }
        }

        public HorisonrtalTable OutputsTable
        {
            get
            {
                return _outputs;
            }
        }
    }
}