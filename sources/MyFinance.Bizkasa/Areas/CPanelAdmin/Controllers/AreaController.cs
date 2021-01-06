using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{
    public class AreaController : Controller
    {

        //private readonly IAreaService _area;
        //private readonly ILogActionService _log;
        //private readonly IDeleteCodeService _delCode;
        public AreaController()
        {
            //var uow = new UnitOfWork<AppFrameworkContext>();
            //_area = new AreaService(uow, new ArealRepository(uow));
            //_log = new LogActionService(uow, new LogActionlRepository(uow));
            //_delCode = new DeleteCodeService(uow, new DeleteCodelRepository(uow));
        }

        //#region Function
        //private void AddLog(Area _areaitem, string _controller, string _action, string _content, string _userID)
        //{
        //    var _logitem = new LogAction
        //    {
        //        Controller = _controller,
        //        Action = _action,
        //        Body = _content + "<<id:" + _areaitem.AreaID + " | " + _areaitem.Name + ">>",
        //        UserID = _userID
        //    };
        //    _log.Save(_logitem);

        //}
        //private Area AddArea(Area model)
        //{
        //    string _areaID="";
        //    int _sort = _area.GetAll().Count();
        //    var _idlist = _delCode.GetAll().Where(x=>x.Code =="VU").ToList();
         
          
        //    if (_idlist.Count > 0)
        //    {
        //        _areaID = _idlist.First().DeleteCodeID;
        //        var _delitem = _delCode.Find(_areaID);
        //        _delCode.Delete(_delitem);
        //    }
        //    else
        //    {
        //        string _idlast = _area.GetLastID();
        //        _areaID = GenerateID.Create("VU", _idlast);
        //    }

        //    var _areaitem = new Area
        //    {
        //        AreaID = _areaID,
        //        Name = model.Name,
        //        Note= model.Note,
        //        Status = model.Status
               
        //    };
        //    _area.Save(_areaitem);
        //    return _areaitem;
        //}
        //private Area UpdateArea(Area model)
        //{
        //    var _areaitem = new Area
        //    {
        //        AreaID = model.AreaID,
        //        Name = model.Name,
        //        Note = model.Note,
        //        Status = model.Status

        //    };
        //    _area.Update(_areaitem);
        //    return _areaitem;
        //}
        //public JsonResult List()
        //{
           
        //    return Json(_area.GetAll().OrderByDescending(x=>x.Created).ToList(), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult Get(string id)
        //{
        //    string _id = String.Concat("VU", id);
        //    Area _cat = _area.Find(_id);
        //    return Json(_cat, JsonRequestBehavior.AllowGet);
        //}
        //private void AddDeleteCode(Area _areaitem)
        //{
        //    var _deleteitem = new DeleteCode
        //    {
        //        DeleteCodeID = _areaitem.AreaID,
        //        Code = _areaitem.AreaID.Substring(0, 2).ToString()
        //    };
        //    _delCode.Save(_deleteitem);
        //}
        //#endregion

        #region View
        //[Authorize(Roles = "Admin,Nhanvien")]
        public ActionResult Index()
        {
            if (Request.Cookies["UID"] != null)
            {

                return View();
            }
            return RedirectToRoute("LogOn");

        }

        //[Authorize(Roles = "Admin,Nhanvien")]
        //[ValidateInput(false)]
        //public ActionResult Create(Area model)
        //{
        //    var reponse = new { Code = 1, Msg = "Lưu không thành công!!" };
        //    var _areaitem = AddArea(model);
        //    AddLog(_areaitem, "Area", "Create", "Thêm mới khu vực", Request.Cookies["UID"].Values["ID"].ToString());
        //    reponse = new { Code = 0, Msg = "Lưu thành công!!" };
        //    return Json(reponse, JsonRequestBehavior.AllowGet);
        //}

        //[Authorize(Roles = "Admin,Nhanvien")]
        //[ValidateInput(false)]
        //public ActionResult Edit(Area model)
        //{
        //    var reponse = new { Code = 1, Msg = "Cập nhật không thành công!!" };
        //    var _areaitem = UpdateArea(model);
        //    AddLog(_areaitem, "Area", "Edit", "Sửa khu vực", Request.Cookies["UID"].Values["ID"].ToString());
        //    reponse = new { Code = 0, Msg = "Cập nhật thành công!!" };
        //    return Json(reponse, JsonRequestBehavior.AllowGet);
        //}
        //[Authorize(Roles = "Admin,Nhanvien")]
        //public JsonResult Delete(string id)
        //{
        //    string _id = String.Concat("VU", id);
        //    Area _areaitem = _area.Find(_id);
        //    _area.Delete(_areaitem);
        //    AddLog(_areaitem, "Area", "Delete", "Xóa khu vực", Request.Cookies["UID"].Values["ID"].ToString());
        //    AddDeleteCode(_areaitem);
        //    return Json("ID: " + _areaitem.AreaID + " đã được xóa thành công", JsonRequestBehavior.AllowGet);
        //}

        #endregion

    }
}
