using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skrin.BLL.Authorization;
using Skrin.BLL.Infrastructure;
using System.Threading.Tasks;

namespace Skrin.Controllers
{
    public class ExternalUsersController : Controller
    {
        /*
        // GET: ExternalUser
        public  ActionResult B2B(string session_key, int token_lifetime)
        {
            if(!string.IsNullOrWhiteSpace(session_key))
            {
                try
                {
                    SkrinUser user =  ExternalUserRepository.ValidateKey(session_key, UserSource.B2B);
                    if (user != null)
                    {
                        UserSession us = HttpContext.GetUserSession();
                        us.AuthenticateExtenal(user, UserSource.B2B);

                    }
                    
                }
                catch(Exception ex)
                {
                    Helper.SendEmail(ex.ToString() + "\n" + string.Format("Запрос: {0}", HttpContext.Request.RawUrl), "Ошибка авторизации b2b");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        */

        public ActionResult RtsTender(string token, string fz, string target)
        {
            if(!string.IsNullOrWhiteSpace(token))
            {
                try
                {
                    token = HttpUtility.UrlEncode(token);
                    UserSource source = fz == "223" ? UserSource.Rts223 : UserSource.Rts44;
                    SkrinUser user = ExternalUserRepository.ValidateKey(token, source, target);
                    if (user != null)
                    {
                        UserSession us = HttpContext.GetUserSession();
                        var auth_result = us.AuthenticateExtenal(user, source);
                    }
                    
                }
                catch (Exception ex)
                {
                    Helper.SendEmail(ex.ToString() + "\n" + string.Format("Запрос: {0}", HttpContext.Request.RawUrl), "Ошибка авторизации rts");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}