using System.Web.Mvc;
using MyFinance.Domain.BusinessModel;
using MyFinance.Bizkasa.Infractstructure;
using MyFinance.Utils;
using MyFinance.ApiService;
using System.Collections.Generic;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{

    public class HotelController : Controller
    {
        private readonly ITikasaService _Service;


        public HotelController(ITikasaService hotelService)
         {
             this._Service = hotelService;

         }  
        //
        // GET: /Hotel/
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create()
        {

            return View();
        }
        public ActionResult Add()
        {

            return View();
        }
        public ActionResult Camera()
        {
            return View();
        }
        public ActionResult ListCamera()
        {
            var result = _Service.GetCameraByHotel();
            CameraDTO model = new CameraDTO();
            model = result.Data;

            if (model != null)
            {
                for (int i = 1; i <= model.NumCamera; i++)
                {
                    model.LinkTargets.Add(model.Link + "/user=" + model.Username + "&password=" + model.Password + "&channel=" + i + "&stream=1.sdp?real_stream--rtp-caching=100");
                }
                ViewBag.LinkTarget = model.LinkTargets;
            }
            return View(model);
        }
        public ActionResult List()
        {

            return View();
        }
        public ActionResult CameraDetail()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ViewCamera()
        {
            
          var result=  _Service.GetCameraByHotel();
            CameraDTO model = new CameraDTO();
            if (result.Data != null)
            {
              
                if (!result.HasError)
                    model = result.Data;

                if (Session["CameraCurrentView"] != null)
                    model.CameraCurrentView = (int)Session["CameraCurrentView"];
                else
                {
                    model.CameraCurrentView = result.Data.CameraDefault;
                }
                if (model != null)
                {
                    if (model.NumCamera > 1)
                    {
                        List<SelectListItem> camera = new List<SelectListItem>();
                        for (int i = 1; i <= model.NumCamera; i++)
                        {
                            camera.Add(new SelectListItem() { Value = i.ToString(), Text = "Camera " + i.ToString() });
                        }
                        ViewBag.CameraNos = camera;
                    }
                    ViewBag.LinkTarget = model.Link + "/user=" + model.Username + "&password=" + model.Password + "&channel=" + model.CameraCurrentView + "&stream=1.sdp?real_stream--rtp-caching=100";
                }
                return PartialView(model);
            }
            return PartialView();


        }
        [HttpPost]
        public ActionResult ViewCamera(CameraDTO model)
        {
            var url = HttpContext.Request.UrlReferrer.AbsoluteUri;
            if (model.NumCamera > 1)
            {
                List<SelectListItem> camera = new List<SelectListItem>();
                for (int i = 1; i <= model.NumCamera; i++)
                {
                    camera.Add(new SelectListItem() { Value = i.ToString(), Text = "Camera " + i.ToString() });
                }
                ViewBag.CameraNos = camera;
            }
            ViewBag.LinkTarget = model.Link + "/user=" + model.Username + "&password=" + model.Password + "&channel=" + model.CameraCurrentView + "&stream=1.sdp?real_stream--rtp-caching=100";
            Session["CameraCurrentView"] = model.CameraCurrentView;
            //return PartialView(model);
            return Redirect(url);
        }
        //
        // GET: /Hotel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Hotel/Create
       
        [SessionFilterAction]
        public JsonResult AddHotel(HotelModel data)
        {
            var result = _Service.AddHotel(data);
            if (result.HasError)
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            if (WorkContext.BizKasaContext.HotelName!=data.Name)
                WorkContext.BizKasaContext.HotelName = data.Name;
           return new JsonResult() { Data=result};
        }
        [HttpPost]
        public JsonResult ResetDataHotel(int hotelId)
        {
            var result = _Service.ResetDataHotel(hotelId);
            return result.ToJsonResult(result.Data);
        }


        [HttpPost]
        public JsonResult DeleteHotel(List<int> Ids)
        {
            var result = _Service.DeleteHotel(Ids);
            return result.ToJsonResult(result.Data);
        }

        public JsonResult CreateHotelFromSystem(HotelModel data)
        {
            var result = _Service.CreateHotelFromSystem(data);
            if (result.HasError)
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            
            return new JsonResult() { Data = result };
        }

        [HttpPost]
        public JsonResult InsertOrUpdateCamera(CameraDTO data)
        {
            var result = _Service.InsertOrUpdateCamera(data);
            return result.ToJsonResult(result.Data);
        }

        [HttpGet]
        public JsonResult GetCameraByHotel()
        {
            var result = _Service.GetCameraByHotel();
            return result.ToJsonResult(result.Data);
        }


        [HttpPost]
        public JsonResult GetHotels(InvoiceFilterModel filter)
        {
            var result = _Service.GetHotels(filter);
            return new JsonResult() { Data = result };
        }
        [HttpPost]
        public JsonResult GetHotelById(int id)
        {
            var result = _Service.GetHotelById(id);
            return result.ToJsonResult(result.Data);
        }


        [HttpPost]
        public JsonResult GetHotelUtilityBy()
        {
            var result = _Service.GetHotelUtilityBy();
            if (result.HasError)
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            return result.ToJsonResult(result.Data);
        }
    }
}
