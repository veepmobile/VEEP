using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class ReorgModel
    {
        private HorisonrtalTable _reorg;
        private HorisonrtalTable _predecessors;
        private HorisonrtalTable _successors;
        public ReorgModel(HorisonrtalTable reorg, HorisonrtalTable predecessors, HorisonrtalTable successors)
        {
            _reorg = reorg;
            _predecessors = predecessors;
            _successors = successors;
        }

        public HorisonrtalTable ReorgTable
        {
            get
            {
                return _reorg;
            }
        }
        public HorisonrtalTable PredecessorsTable
        {
            get
            {
                return _predecessors;
            }
        }
        public HorisonrtalTable SuccessorsTable
        {
            get
            {
                return _successors;
            }
        }
    }
}