using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class RegDataModel
    {
        private HorisonrtalTable _maincodes;
        private HorisonrtalTable _names;
        private HorisonrtalTable _name_history;
        private HorisonrtalTable _address;
        private HorisonrtalTable _address_history;
        private HorisonrtalTable _status;
        private HorisonrtalTable _stoping;
        private HorisonrtalTable _reginfo;
        private HorisonrtalTable _regoldinfo;
        private HorisonrtalTable _regorginfo;
        private HorisonrtalTable _record;
        private HorisonrtalTable _pfregistration;
        private HorisonrtalTable _fssregistration;
        private HorisonrtalTable _okveds;


        public RegDataModel(HorisonrtalTable maincodes, HorisonrtalTable names, HorisonrtalTable name_history, HorisonrtalTable address, HorisonrtalTable address_history, HorisonrtalTable status, HorisonrtalTable stoping, HorisonrtalTable reginfo, HorisonrtalTable regoldinfo, HorisonrtalTable regorginfo, HorisonrtalTable record, HorisonrtalTable pfregistration, HorisonrtalTable fssregistration, HorisonrtalTable okveds)
        {
            _maincodes = maincodes;
            _names = names;
            _name_history = name_history;
            _address = address;
            _address_history = address_history;
            _status = status;
            _stoping = stoping;
            _reginfo = reginfo;
            _regoldinfo = regoldinfo;
            _regorginfo = regorginfo;
            _record = record;
            _pfregistration = pfregistration;
            _fssregistration = fssregistration;
            _okveds = okveds;
        }

        public HorisonrtalTable MaincodesTable
        {
            get
            {
                return _maincodes;
            }
        }
        public HorisonrtalTable NamesTable
        {
            get
            {
                return _names;
            }
        }
        public HorisonrtalTable NameHistoryTable
        {
            get
            {
                return _name_history;
            }
        }
        public HorisonrtalTable AddressTable
        {
            get
            {
                return _address;
            }
        }
        public HorisonrtalTable AddressHistoryTable
        {
            get
            {
                return _address_history;
            }
        }
        public HorisonrtalTable StatusTable
        {
            get
            {
                return _status;
            }
        }
        public HorisonrtalTable StopingTable
        {
            get
            {
                return _stoping;
            }
        }
        public HorisonrtalTable ReginfoTable
        {
            get
            {
                return _reginfo;
            }
        }
        public HorisonrtalTable RegoldinfoTable
        {
            get
            {
                return _regoldinfo;
            }
        }

        public HorisonrtalTable RegorginfoTable
        {
            get
            {
                return _regorginfo;
            }
        }
        public HorisonrtalTable RecordTable
        {
            get
            {
                return _record;
            }
        }

        public HorisonrtalTable PFRegistrationTable
        {
            get
            {
                return _pfregistration;
            }
        }
        public HorisonrtalTable FSSRegistrationTable
        {
            get
            {
                return _fssregistration;
            }
        }
        public HorisonrtalTable OKVRDsTable
        {
            get
            {
                return _okveds;
            }
        }
    }
}