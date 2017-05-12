using Skrin.BLL.Authorization;
using Skrin.BLL.Cloud;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Cloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Skrin.Controllers.Cloud
{
    public class UserNoteController : Controller
    {

        private static IUserNoteRepository rep = new UserNotePostgreRepository(new PostgreCloudLogger());

        public async Task<ActionResult> Get(string issuer_id)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return new HttpUnauthorizedResult();
            }

            var res = new List<UserNoteContainer>();
            foreach (var item in await rep.GetAsync(us.UserId, issuer_id))
            {
                res.Add(new UserNoteContainer(item));    
            }
            return Json(res,JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Update(UserNoteContainer input)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return new HttpUnauthorizedResult();
            }

            input.user_id = us.UserId;

            UserNote note = new UserNote(input);
            UserNoteContainer res = new UserNoteContainer(await rep.UpdateAsync(note));
            return Json(res);
        }


        public async Task<ActionResult> Delete(string note_id)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return new HttpUnauthorizedResult();
            }
            if(string.IsNullOrWhiteSpace(note_id))
            {
                return HttpNotFound();
            }

            UserNoteContainer res = new UserNoteContainer(await rep.DeleteAsync(Guid.Parse(note_id)));

            return Json(res,JsonRequestBehavior.AllowGet);
        }

    }
}