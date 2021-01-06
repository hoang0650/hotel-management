using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Web.Security;
using System.IO;
namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{
    public class ServiceController : Controller
    {
        //private readonly IDeleteCodeService _delCode;
        //private readonly IModuleConfigService _modulecofig;
        //private readonly ILogActionService _log;
        //private readonly ITopicService _topic;
        //private readonly IAdminAccountService _acc;
        //private readonly IGalleryImageService _gallery;
        //private readonly ICatalogService _catalog;
        public ServiceController()
        {
            //var uow = new UnitOfWork<AppFrameworkContext>();
            //_modulecofig = new ModuleConfigService(uow, new ModuleConfigIRepository(uow));
            //_catalog = new CatalogService(uow, new CatalogRepository(uow));
            //_log = new LogActionService(uow, new LogActionlRepository(uow));
            //_delCode = new DeleteCodeService(uow, new DeleteCodelRepository(uow));
            //_topic = new TopicService(uow, new TopicRepository(uow));
            //_acc = new AdminAccountService(uow, new AdminAccountlRepository(uow));
            //_gallery = new GalleryImageService(uow, new GalleryImageRepository(uow));
        }
        //
        // GET: /CPanelAdmin/Product/

        //#region Function
        //private string _userID()
        //{
        //    return Request.Cookies["UID"].Values["ID"].ToString();
        //}
        //private void AddLog(Topic _item, string _controller, string _action, string _content, string _userID)
        //{
        //    var _logitem = new LogAction
        //    {
        //        Controller = _controller,
        //        Action = _action,
        //        Body = _content + "<<ID:" + _item.TopicID + " | " + _item.Name + ">>",
        //        UserID = _userID
        //    };
        //    _log.Save(_logitem);

        //}
        //private void AddLog(GalleryImage _item, string _controller, string _action, string _content, string _userID)
        //{
        //    var _logitem = new LogAction
        //    {
        //        Controller = _controller,
        //        Action = _action,
        //        Body = _content + "<<ID:" + _item.ProductID + " | " + _item.ImgUrl + ">>",
        //        UserID = _userID
        //    };
        //    _log.Save(_logitem);

        //}
     
        //private void addTopic(Topic _model)
        //{
        //    int _sort = _topic.GetAll().Count();
        //    string _randKey = XRandom.GenerateKey("p");
        //    string _topicID;
        //    string _dmname;
        //    GetProID(_model, out _topicID, out _dmname);
        //    var _topicitem = new Topic
        //    {
        //        TopicID = _topicID,
        //        Name = _model.Name,
        //        CatalogID = _model.CatalogID,
        //        Content = _model.Content,
        //        Keyword = _model.Keyword,
        //        Tags = _model.Tags,
        //        IsHome = _model.IsHome,
        //        CatalogType = Models.Enum.CatalogType.Dich_Vu,
        //        Sort = _sort++,
        //        IsActive = _model.IsActive,
        //        RandomKey = _randKey,
        //        Description = _model.Description,
        //        View = 0,
        //        ImgURl = _model.ImgURl,
        //        Alias = XString.ToAscii(_model.Name),
        //        Link = String.Concat("/", XString.ToAscii(_dmname).ToLower(), "/", XString.ToAscii(_model.Name).ToLower(), "-", _randKey, ".html"),
        //        UserID = _userID()
        //    };
        //    _topic.Save(_topicitem);
        //    AddLog(_topicitem, "Topic", "Create", "Lưu sản phẩm", _userID());
        //}
     
        //private void GetProID(Topic _model, out string _topicID, out string _dmname)
        //{
        //    _topicID = "";
        //    _dmname = _catalog.Find(_model.CatalogID).Name;
        //    var _idlist = _delCode.GetAll().Where(x => x.Code == "SR").ToList();
        //    if (_idlist.Count > 0)
        //    {
        //        _topicID = _idlist.First().DeleteCodeID;
        //        var _delitem = _delCode.Find(_topicID);
        //        _delCode.Delete(_delitem);
        //    }
        //    else
        //    {
        //        string _idlast = _topic.GetLastID();
        //        _topicID = GenerateID.Create("SR", _idlast);
        //    }
        //}
        //private void addDelcode(Topic _model)
        //{
        //    var _delitem = new DeleteCode
        //    {
        //        DeleteCodeID = _model.TopicID,
        //        Code = _model.TopicID.Substring(0, 2).ToString()
        //    };
        //    _delCode.Save(_delitem);
        //}

        //public JsonResult List()
        //{

        //    var _model = _topic.GetAll().Where(x => x.CatalogType == CatalogType.Dich_Vu ).Select(y => new TopicViewModel()
        //    {
        //        _id = y.TopicID,
        //        _name = y.Name,
        //        _alias = y.Alias,
        //        _link = y.Link,
        //        _imgUrl = y.ImgURl,
        //        _userID = y.UserID,
        //        _username = _acc.Find(y.UserID).UserName,
        //        _catalogyId = y.CatalogID,
        //        _catalogyName = _catalog.Find(y.CatalogID).Name,
        //        _description = y.Description,
        //        _keyword = y.Keyword,
        //        _isActive = y.IsActive,
        //        _isHome = y.IsHome,
        //        _tags = y.Tags,
        //        _sort = y.Sort,
        //        _view = y.View,
        //        _update = y.Update
        //    }).ToList();
        //    return Json(_model, JsonRequestBehavior.AllowGet);
        //}
        //[Authorize(Roles = "Admin,Nhanvien")]
        //public JsonResult FilterRoles(string roles)
        //{
        //    var key = roles.Trim().ToLower();
        //    var _model = _topic.GetAll().Where(x => x.CatalogID.Trim().ToLower().Contains(key))
        //        .Select(y => new TopicViewModel()
        //        {
        //            _id = y.TopicID,
        //            _name = y.Name,
        //            _alias = y.Alias,
        //            _link = y.Link,
        //            _imgUrl = y.ImgURl,
        //            _userID = y.UserID,
        //            _username = _acc.Find(y.UserID).UserName,
        //            _catalogyId = y.CatalogID,
        //            _catalogyName = _catalog.Find(y.CatalogID).Name,
        //            _description = y.Description,
        //            _keyword = y.Keyword,
        //            _isActive = y.IsActive,
        //            _isHome = y.IsHome,
        //            _tags = y.Tags,
        //            _sort = y.Sort,
        //            _view = y.View,
        //            _update = y.Update
        //        })
        //        .ToList();
        //    return Json(_model, JsonRequestBehavior.AllowGet);

        //}

        //[Authorize(Roles = "Admin,Nhanvien")]
        //public JsonResult Filter(string searchKey)
        //{
        //    var getKey = searchKey.Trim().ToLower();
        //    var _model = _topic.GetAll().Where(x => x.Name.Trim().ToLower().Contains(getKey) || x.RandomKey.Trim().ToLower().Contains(getKey) || x.Link.Trim().ToLower().Contains(getKey) || x.Tags.Trim().ToLower().Contains(getKey))
        //        .Select(y => new TopicViewModel()
        //        {
        //            _id = y.TopicID,
        //            _name = y.Name,
        //            _alias = y.Alias,
        //            _link = y.Link,
        //            _imgUrl = y.ImgURl,
        //            _userID = y.UserID,
        //            _username = _acc.Find(y.UserID).UserName,
        //            _catalogyId = y.CatalogID,
        //            _catalogyName = _catalog.Find(y.CatalogID).Name,
        //            _description = y.Description,
        //            _keyword = y.Keyword,
        //            _isActive = y.IsActive,
        //            _isHome = y.IsHome,
        //            _tags = y.Tags,
        //            _sort = y.Sort,
        //            _view = y.View,
        //            _update = y.Update
        //        })
        //        .ToList();
        //    var respone = new { _model = _model, _count = _model.Count() };
        //    return Json(respone, JsonRequestBehavior.AllowGet);

        //}
        //private void delModel(string id)
        //{
        //    string _id = String.Concat("SR", id);
        //    Topic _model = _topic.Find(_id);
        //    _topic.Delete(_model); 
        //    addDelcode(_model);
        //    AddLog(_model, "Topic", "Delete", "Xóa danh mục TT", _userID());
        //    delGallery(_id);
            
        //}

        //private void delGallery(string _id)
        //{
        //    var _img = _gallery.GetAll().Where(x => x.ProductID == _id).ToList();
        //    foreach (var item in _img)
        //    {
        //        GalleryImage i = _gallery.Find((int)item.GalleryImageID);
        //        _gallery.Delete(i);
        //        AddLog(i, "Topic", "Delete", "Xóa danh mục anh", _userID());
        //    }
        //}

        //#endregion

       // [Authorize(Roles = "Admin,Nhanvien")]
        public ActionResult Index()
        {
           
                
                return View();
           
        }

        //[Authorize(Roles = "Admin,Nhanvien")]
        public ActionResult Create()
        {
            return View();
        }

       
        
        //public ActionResult upload()
        //{
        //    string filepathtosave = "";
        //    filepathtosave = "/Upload/car/test.png";
        //    XImages.SaveByteArrayAsImage(Server.MapPath(filepathtosave), brand.Logo_Brand.Replace("data:image/png;base64,", ""));
        //    return Json("", JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult FroalaUploadImage(HttpPostedFileBase file, int? postId) // نام پارامتر فايل را تغيير ندهيد
        {
            var fileName = Path.GetFileName(file.FileName);
            var rootPath = Server.MapPath("~/Upload/");
            file.SaveAs(Path.Combine(rootPath, fileName));
            return Json(new { link = "../../Upload/" + fileName }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// file manager froala
        /// </summary>
     


        [Authorize(Roles = "Admin,Nhanvien")]
        public JsonResult Delete(string id)
        {
            var reponse = new { Code = 1, Msg = "xóa không thành công!!" };
            //delModel(id);
            reponse = new { Code = 0, Msg = "Xóa thành công!!" };
            return Json(reponse, JsonRequestBehavior.AllowGet);
        }

    }
}
