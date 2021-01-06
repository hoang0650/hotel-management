using MyFinance.ApiService;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{
    public class GalleryController : Controller
    {
         private readonly ITikasaService _Service;


         public GalleryController(ITikasaService userService)
        {
            this._Service = userService;

        }  
        //
        // GET: /CPanelAdmin/Gallery/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UploadImage(HttpPostedFileBase file)
        {
            Stream fileStream = file.InputStream;
            var result = _Service.UploadFile(fileStream, file.FileName, null);
            return new JsonResult() { Data = result };
        }
       
 
	}
}