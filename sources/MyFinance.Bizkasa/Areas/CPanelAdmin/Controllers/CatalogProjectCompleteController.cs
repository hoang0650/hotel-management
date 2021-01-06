using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{
    public class CatalogProjectCompleteController : Controller
    {
        public CatalogProjectCompleteController()
        {
        }

        

        //[Authorize(Roles = "Admin,Nhanvien")]
        public ActionResult Index()
        {
            return View();
        }
        //[Authorize(Roles = "Admin,Nhanvien")]
        [ValidateInput(false)]
        public ActionResult Create()
        {
            return View();
        }
       // [Authorize(Roles = "Admin,Nhanvien")]
        [ValidateInput(false)]
        public ActionResult Edit()
        {
            return View();
        }
       
       

    }
}
