using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skrin.Models.Iss.Content
{
    public class ManagementModel
    {
        private HorisonrtalTable _managers;
        private HorisonrtalTable _managers_history;
        private HorisonrtalTable _management_companies;
        private HorisonrtalTable _management_foreign_companies;
        private HorisonrtalTable _management_foreign_company_filials;
        private HorisonrtalTable _management_foreign_company_filials_manager;


        public ManagementModel(HorisonrtalTable managers, HorisonrtalTable managers_history, HorisonrtalTable management_companies, HorisonrtalTable management_foreign_companies, HorisonrtalTable management_foreign_company_filials, HorisonrtalTable management_foreign_company_filials_manager)
        {
            _managers = managers;
            _managers_history = managers_history;
            _management_companies = management_companies;
            _management_foreign_companies = management_foreign_companies;
            _management_foreign_company_filials = management_foreign_company_filials;
            _management_foreign_company_filials_manager = management_foreign_company_filials_manager;
        }

        public HorisonrtalTable ManagersTable
        {
            get
            {
                return _managers;
            }
        }
        public HorisonrtalTable ManagersHistoryTable
        {
            get
            {
                return _managers_history;
            }
        }
        public HorisonrtalTable ManagementCompaniesTable
        {
            get
            {
                return _management_companies;
            }
        }
        public HorisonrtalTable ManagementForeignCompaniesTable
        {
            get
            {
                return _management_foreign_companies;
            }
        }
        public HorisonrtalTable ManagementForeignCompanyFilialsTable
        {
            get
            {
                return _management_foreign_company_filials;
            }
        }
        public HorisonrtalTable ManagementForeignCompanyFilialsManagerTable
        {
            get
            {
                return _management_foreign_company_filials_manager;
            }
        }
    }
}