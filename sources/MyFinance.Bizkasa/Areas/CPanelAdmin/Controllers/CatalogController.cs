using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{
    public class CatalogController : Controller
    {
        //private readonly ICatalogService _catalog;
        //private readonly IDeleteCodeService _delCode;
        //private readonly IModuleConfigService _modulecofig;
        //private readonly ILogActionService _log;
        public CatalogController()
        {
            //var uow = new UnitOfWork<AppFrameworkContext>();
            //_catalog = new CatalogService(uow, new CatalogRepository(uow));
            //_modulecofig = new ModuleConfigService(uow, new ModuleConfigIRepository(uow));
            //_log = new LogActionService(uow, new LogActionlRepository(uow));
            //_delCode = new DeleteCodeService(uow, new DeleteCodelRepository(uow));
        }

        

        //[Authorize(Roles = "Admin,Nhanvien")]
        public ActionResult Index()
        {
            return View();
        }
        //[Authorize(Roles = "Admin,Nhanvien")]
        //[ValidateInput(false)]
        //public ActionResult Create(Catalog cata)
        //{
        //    var reponse = new { Code = 1, Msg = "Lưu không thành công!!" };
        //    addCatalog(cata);
        //    reponse = new { Code = 0, Msg = "Lưu thành công!!" };
        //    return Json(reponse, JsonRequestBehavior.AllowGet);
        //}
        //[Authorize(Roles = "Admin,Nhanvien")]
        //[ValidateInput(false)]
        //public ActionResult Edit(Catalog cata)
        //{
        //    var reponse = new { Code = 1, Msg = "Cập nhật không thành công!!" };
        //    updateCatalog(cata);
        //    reponse = new { Code = 0, Msg = "Cập nhật thành công!!" };
        //    return Json(reponse, JsonRequestBehavior.AllowGet);
        //}
        //[Authorize(Roles = "Admin,Nhanvien")]
        //public JsonResult Delete(string id)
        //{
        //    var reponse = new { Code = 1, Msg = "xóa không thành công!!" };
        //    delCatalog(id);
        //    reponse = new { Code = 0, Msg = "Xóa thành công!!" };
        //    return Json(reponse, JsonRequestBehavior.AllowGet);
        //}
       

    }
}
