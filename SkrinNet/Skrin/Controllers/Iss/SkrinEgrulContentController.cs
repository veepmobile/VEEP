using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Skrin.Models.Iss.Content;
using Skrin.BLL.ISS.Content;

namespace Skrin.Controllers.Iss
{
    public class SkrinEgrulContentController : Controller
    {
        // GET: SkrinEgrulContent
        public ActionResult GetBranches(string ogrn)
        {
            if (ogrn == null || ogrn.Length != 13)
            {
                return Content("");
            }

//            Task<List<EgrulULAddressModel>> t1 =
//                Task<List<EgrulULAddressModel>>.Factory.StartNew(SkrinContentRepository.GetEgrulBranches, ogrn);
//            Task<List<EgrulULAddressModel>> t2 =
//                Task<List<EgrulULAddressModel>>.Factory.StartNew(SkrinContentRepository.GetEgrulOutputs, ogrn);
            Task<HorisonrtalTable> t1 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetEgrulBranches, ogrn);
            Task<HorisonrtalTable> t2 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetEgrulOutputs, ogrn);
            var branches = t1.Result;
            var outputs = t2.Result;

            return View(new BranchesModel(branches,outputs));
        }

        public ActionResult GetLicense(string ogrn)
        {
            if (ogrn == null || ogrn.Length != 13)
            {
                return Content("");
            }
            return View(new LicenseModel(SkrinContentRepository.GetLicense(ogrn)));
        }

        public ActionResult GetCapital(string ogrn)
        {
            if (ogrn == null || ogrn.Length != 13)
            {
                return Content("");
            }

            Task<HorisonrtalTable> t1 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetCapital, ogrn);
            Task<HorisonrtalTable> t2 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetReduce, ogrn);
            var capital = t1.Result;
            var reduce = t2.Result;
            return View(new CapitalModel(capital, reduce));
        }

        public ActionResult GetRegister(string ogrn)
        {
            if (ogrn == null || ogrn.Length != 13)
            {
                return Content("");
            }
            return View(new RegisterModel(SkrinContentRepository.GetRegister(ogrn)));
        }


        public ActionResult GetReorg(string ogrn)
        {
            if (ogrn == null || ogrn.Length != 13)
            {
                return Content("");
            }

            Task<HorisonrtalTable> t1 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetReorg, ogrn);
            Task<HorisonrtalTable> t2 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetPredecessors, ogrn);
            Task<HorisonrtalTable> t3 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetSuccessors, ogrn);
            var reorg = t1.Result;
            var predecessors = t2.Result; ;
            var successors=t3.Result;
            return View(new ReorgModel(reorg, predecessors, successors));
        }

        //public ActionResult GetManagement(string ogrn)
        //{
        //    if (ogrn == null || ogrn.Length != 13)
        //    {
        //        return Content("");
        //    }

        //    Task<HorisonrtalTable> t1 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetManagers, ogrn);
        //    Task<HorisonrtalTable> t2 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetManagersHistory, ogrn);
        //    Task<HorisonrtalTable> t3 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetManagementCompanies, ogrn);
        //    Task<HorisonrtalTable> t4 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetManagementForeignCompanies, ogrn);
        //    Task<HorisonrtalTable> t5 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetManagementForeignCompanyFilials, ogrn);
        //    Task<HorisonrtalTable> t6 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetManagementForeignCompanyFilialsManager, ogrn);
        //    return View(new ManagementMadel(t1.Result, t2.Result, t3.Result, t4.Result, t5.Result, t6.Result));
        //}


        public ActionResult GetConstitutors(string ogrn)
        {
            if (ogrn == null || ogrn.Length != 13)
            {
                return Content("");
            }
            Task<HorisonrtalTable> t1 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetConstitutors, ogrn);
            Task<HorisonrtalTable> t2 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetConstitutorsHistory, ogrn);

            return View(new ConstitutorsModel(t1.Result, t2.Result));
        }

        //public ActionResult  GetRegdata(string ogrn)
        //{
        //    if (ogrn == null || ogrn.Length != 13)
        //    {
        //        return Content("");
        //    }
        //    Task<HorisonrtalTable> t1 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetGegDataMaincodes, ogrn);
        //    Task<HorisonrtalTable> t2 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetRegDataNames, ogrn);
        //    Task<HorisonrtalTable> t3 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetGetRegDataNameHistory, ogrn);
        //    Task<HorisonrtalTable> t4 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetRegDataAddress, ogrn);
        //    Task<HorisonrtalTable> t5 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetGetRegDataAddressHistory, ogrn);
        //    Task<HorisonrtalTable> t6 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetGetRegDataStatus, ogrn);
        //    Task<HorisonrtalTable> t7 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetGetRegDataStoping, ogrn);
        //    Task<HorisonrtalTable> t8 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetGetRegDataReginfo, ogrn);
        //    Task<HorisonrtalTable> t9 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetGetRegDataRegoldinfo, ogrn);
        //    Task<HorisonrtalTable> t10 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetGetRegDataRegorginfo, ogrn);
        //    Task<HorisonrtalTable> t11 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetGetRegDataRecord, ogrn);
        //    Task<HorisonrtalTable> t12 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetGetRegDataPFRegistration, ogrn);
        //    Task<HorisonrtalTable> t13 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetGetRegDataFSSRegistration, ogrn);
        //    Task<HorisonrtalTable> t14 = Task<HorisonrtalTable>.Factory.StartNew(SkrinContentRepository.GetGetRegDataOKVEDs, ogrn);

        //    return View(new RegdataModel(t1.Result, t2.Result, t3.Result, t4.Result, t5.Result, t6.Result, t7.Result, t8.Result, t9.Result, t10.Result, t11.Result, t12.Result, t13.Result, t14.Result));
        //}

        /*
                public ActionResult GetBranchesJ(string ogrn)
                {
                    if (ogrn == null || ogrn.Length != 13)
                    {
                        return Content("");
                    }

                    Task<List<EgrulULAddressModel>> t1 =
                        Task<List<EgrulULAddressModel>>.Factory.StartNew(SkrinContentRepository.GetEgrulBranches, ogrn);
                    Task<List<EgrulULAddressModel>> t2 =
                        Task<List<EgrulULAddressModel>>.Factory.StartNew(SkrinContentRepository.GetEgrulOutputs, ogrn);
                    var branches = t1.Result;
                    var outputs = t2.Result;

                    return View(new BranchesModel(branches, outputs));
                }
        */
    }
}