using Skrin.BLL.Authorization;
using Skrin.BLL.Cloud;
using Skrin.BLL.Infrastructure;
using Skrin.Models.Cloud;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers.Cloud
{
    public class UserFileController : BaseController
    {
        private static IUserFileRepository rep = new UserFilePostgreRepository(new PostgreCloudLogger());
        private static string user_file_dir=Configs.UserFilesDir;
        private static string user_deleted_file_dir = Configs.UserDeletedFilesDir;
             

        public async Task<ActionResult> Get(string issuer_id)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return new HttpUnauthorizedResult();
            }

            var res = new List<UserFileContainer>();
            foreach (var item in await rep.GetAsync(us.UserId, issuer_id))
            {
                res.Add(new UserFileContainer(item));
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFileSizeLimit()
        {
            UserSession us = HttpContext.GetUserSession();
            int file_size_limit=0;
            if (us.UserId != 0)
            {
                file_size_limit = us.User.FileSizeLimit;
            }
            return Json(file_size_limit,JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Upload(HttpPostedFileBase file, string issuer_id)
        {

            if (file == null || issuer_id == null)
            {
                return ErrorResponse("Ошибка загрузки файла");
            }

            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return new HttpUnauthorizedResult();
            }

            var exist_file_size = (await rep.GetAsync(us.UserId, issuer_id)).Select(p=>p.file_data.file_size).Sum();
            if (exist_file_size + file.ContentLength > us.User.FileSizeLimit)
            {
                throw new HttpException("Превышен общий размер разрешенной загрузки");
            }
            var user_file = new UserFile
            {
                user_data = new UserData
                {
                    id = Guid.NewGuid(),
                    issuer_id = issuer_id,
                    update_date = DateTime.Now,
                    user_id = us.UserId
                },
                file_data = new UserFileData
                {
                    file_name = file.FileName,
                    file_size = file.ContentLength
                }
            };

            string save_path = string.Format(@"{0}\{1}\{2}\", user_file_dir, us.UserId, user_file.user_data.id);

            if (!Directory.Exists(save_path))
            {
                Directory.CreateDirectory(save_path);
            }
            var path = Path.Combine(save_path, file.FileName);
            file.SaveAs(path);


            UserFileContainer res = new UserFileContainer(await rep.UpdateAsync(user_file));
            return Json(res);

        }

        public async Task<ActionResult> Delete(string file_id)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return new HttpUnauthorizedResult();
            }

            Guid g_id; 

            if (!Guid.TryParse(file_id, out g_id))
            {
                return HttpNotFound();
            }


            UserFileContainer res = new UserFileContainer(await rep.DeleteAsync(g_id));

            string source = string.Format(@"{0}\{1}\{2}\", user_file_dir, us.UserId, res.id);
            string dest = string.Format(@"{0}\{1}\{2}\", user_deleted_file_dir, us.UserId, res.id);

            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
            }

            string sourceFile = Path.Combine(source, res.file_name);
            string destFile = Path.Combine(dest, res.file_name);

            if (System.IO.File.Exists(sourceFile))
            {
                System.IO.File.Move(sourceFile, destFile);
                Directory.Delete(source);
            }


            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Load(string id)
        {
            UserSession us = HttpContext.GetUserSession();
            if (us.UserId == 0)
            {
                return new HttpUnauthorizedResult();
            }

            Guid g_id; 

            if (!Guid.TryParse(id, out g_id))
            {
                return HttpNotFound();
            }

            var user_file = await rep.GetAsync(g_id);

            if(user_file ==null)
            {
                return HttpNotFound();
            }

            if (us.UserId != user_file.user_data.user_id)
            {
                return new HttpUnauthorizedResult();
            }

            string file_path = string.Format(@"{0}\{1}\{2}\", user_file_dir, us.UserId, user_file.user_data.id);

            return File(Path.Combine(file_path, user_file.file_data.file_name), System.Net.Mime.MediaTypeNames.Application.Octet,user_file.file_data.file_name);
        }
    }
}