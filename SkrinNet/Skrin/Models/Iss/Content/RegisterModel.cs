using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class RegisterModel
    {
        private HorisonrtalTable _register;

        public RegisterModel(HorisonrtalTable register)
        {
            _register = register;
        }

        public HorisonrtalTable RegisterTable
        {
            get
            {
                return _register;
            }
        }
    }
}