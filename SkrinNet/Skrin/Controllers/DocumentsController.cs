using Skrin.BLL.Infrastructure;
using Skrin.BLL.Root;
using Skrin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Skrin.Controllers
{
    public class DocumentsController : BaseController
    {
        // GET: Documents
        public async Task<ActionResult> Index(string iss,string id, string fn=null,int doc_type=0,int doc_id=0,int page_no=0)
        {
            string issuer_id = null;
            string extention = null;
            string export_name = null;

            if (doc_type == 1 || doc_type == 2)
            {
                if (doc_type == 2)
                {
                    issuer_id = await DocumentRepository.GetAuthorIdAsync(id, 2);
                }
                if(issuer_id==null)
                {
                    issuer_id = await DocumentRepository.GetAuthorIdAsync(id, 1);
                }
            }
            else
            {
                issuer_id = await DocumentRepository.GetIssuerIdAsync(iss);
            }

            if(issuer_id==null)
            {
                return HttpNotFound();
            }

            fn = fn ?? (page_no==0 ? await DocumentRepository.GetFileNameAsync(id): await DocumentRepository.GetFileNameAsync(id,page_no));

            extention=fn.Substring(fn.Length-3,3);
            
            if(doc_id<0){
                export_name=await DocumentRepository.GetExportNameAsync(doc_id,iss,id);
            }else{
                export_name=fn;
            }

            FileType file_type = ContentTypeCollection.GetFileType(extention);
            string content_type = ContentTypeCollection.GetContentType(extention);

            string file_path = string.Format(@"{0}{1}\{2}\{3}", Configs.DocPath, issuer_id, id, fn);

            switch (file_type)
            {
                case FileType.Binary:
                case FileType.Text:
                case FileType.Img:
                    return File(file_path, content_type, export_name);
                case FileType.Xml:
                    try
                    {
                        string xml_content = await Utilites.GetFileContent(file_path, Encoding.GetEncoding("Windows-1251"));
                        HTMLCreator creator = new HTMLCreator(fn.Replace(".xml", ""));
                        return Content(creator.GetHtml(xml_content), ContentTypeCollection.GetContentType("html"));
                    }
                    catch
                    {
                        File(file_path, content_type, export_name);
                    }
                    break;
                    
            }

            return File(file_path, content_type, export_name);
        }
    }
}