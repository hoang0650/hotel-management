using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFinance.Service;
using System.Web.Mvc;
using MyFinance.Domain.Entities;
using MyFinance.Domain.BusinessModel;
using Seller.Tikasa.Infractstructure;
using MyFinance.Utils;


namespace Seller.Tikasa.Areas.CPanelAdmin.Controllers
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
        public ActionResult List()
        {

            return View();
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

        public JsonResult GetallBy()
        {
            var result = _Service.GetallBy();
            return new JsonResult() { Data = result };
        }
      

        public JsonResult GetHotelInfo()
        { 
            var hotelId=WorkContext.BizKasaContext.HotelId;
            if(hotelId<=0)
                return new JsonResult() { Data = null };
            var result = _Service.GetHotelById(hotelId);
            return  new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult CreateHotelFromSystem(HotelModel data)
        {
            var result = _Service.CreateHotelFromSystem(data);
            if (result.HasError)
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            
            return new JsonResult() { Data = result };
        }

        [HttpPost]
        public JsonResult GetHotels(InvoiceFilterModel filter)
        {
            var result = _Service.GetHotels(filter);
            return new JsonResult() { Data = result };
        }


        public JsonResult GetHotelUtilityBy()
        {
            var result = _Service.GetHotelUtilityBy();
            if (result.HasError)
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
