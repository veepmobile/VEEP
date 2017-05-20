using Skrin.BLL.Authorization;
using Skrin.BLL.Cloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Cloud;
using System.Threading.Tasks;

namespace Skrin.Controllers.Cloud
{
    public class UserAddressController : Controller
    {
        private static IUserAddressRepository rep = new UserAddressPostgreRepository(new PostgreCloudLogger());

        public async Task<ActionResult> Get(string issuer_id)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return new HttpUnauthorizedResult();
            }

            var res = new List<UserAddressContainer>();
            foreach (var item in await rep.GetAsync(us.UserId, issuer_id))
            {
                res.Add(new UserAddressContainer(item));
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Update(UserAddressContainer input)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return new HttpUnauthorizedResult();
            }

            input.user_id = us.UserId;

            UserAddress address = new UserAddress(input);
            UserAddressContainer res = new UserAddressContainer(await rep.UpdateAsync(address));
            return Json(res);
        }


        public async Task<ActionResult> Delete(string address_id)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return new HttpUnauthorizedResult();
            }
            if (string.IsNullOrWhiteSpace(address_id))
            {
                return HttpNotFound();
            }

            UserAddressContainer res = new UserAddressContainer(await rep.DeleteAsync(Guid.Parse(address_id)));

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}