using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using Skrin.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers
{
    public class UserListsController : Controller
    {
        // GET: UserLists
        public ActionResult Index(int id=0)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                id = 0;
            }
            //Проверим, что запрашиваемая группа принадлежит этому юзеру
            if (id != 0 && !UserGroupsRepository.IsGroupForUser(us.UserId, id))
                id = 0;
            return View(id);
        }


        public ActionResult GroupList()
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Json(null, JsonRequestBehavior.AllowGet);
            var group_list = AuthenticateSqlUtilites.GetGroupList(us.UserId).Select(p => new {id=p.lid,name=p.name,cnt=p.cnt,cnt_disp=p.cnt_disp }).ToList();
            return Json(group_list,JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Search(int group_id, int page)
        {
            if (group_id == 0)
                return Json(null, JsonRequestBehavior.AllowGet);

            var search_result = await UserGroupsRepository.GetCompaniesInGroup(group_id, page);
            return Json(search_result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportForm(bool is_new)
        {
            return View(is_new);
        }

        public ActionResult RenameForm()
        {
            return View();
        }

        public async Task<ActionResult> Import(ImportCodes import_codes)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Json(null);

            var result = await UserGroupsRepository.ImportList(import_codes, us.UserId,us.GroupLimit);
            return Json(new { id = result.Item1, res_count = result.Item2 });
        }

        public async Task<ActionResult> DeleteList(int id)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Json(null);
            await UserGroupsRepository.DeleteListAsync(id);
            return Json(true);
        }

        public async Task<ActionResult> RenameList(int id, string name)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Json(null);
            await UserGroupsRepository.RenameListAsync(id,name);
            return Json(true);
        }

        public async Task<ActionResult> Export(CodeType code_type,int id,string issuers_info)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Content("");
            List<string> issuers = string.IsNullOrEmpty(issuers_info) ? new List<string>() : issuers_info.Split(',').ToList();
            var codes = await UserGroupsRepository.ExportListAsync(code_type, id,issuers);
            return View(codes);
        }

        public async Task<ActionResult> DeleteIssuers(int list_id, string issuers_info)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
                return Json(null);
            List<string> issuers = string.IsNullOrEmpty(issuers_info) ? new List<string>() : issuers_info.Split(',').ToList();
            await UserGroupsRepository.DeleteIssuersFromListAsync(list_id, issuers);
            return Json(true);
        }
    }
}