using MyFinance.Bizkasa.Inside.Infractstructure;
using MyFinance.Bizkasa.Service;
using MyFinance.Bizkasa.Service.Inside;
using MyFinance.Domain.BusinessModel;
using System.Web.Mvc;

namespace MyFinance.Bizkasa.Inside.Controllers
{

    public class HotelController : Controller
        {
            private readonly ITikasaService _Service;

            private readonly IInsideService _InsideService;
            public HotelController(ITikasaService hotelService, IInsideService insideService)
            {
                this._Service = hotelService;
                _InsideService = insideService;
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

            public ActionResult History()
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

          //  [SessionFilterAction]
            public JsonResult AddHotel(HotelModel data)
            {
                var result = _Service.AddHotel(data);
                if (result.HasError)
                    return JsonCommonResult.CreateError(result.ToErrorMsg());
               
                return new JsonResult() { Data = result };
            }

            //public JsonResult GetallBy()
            //{
            //    var result = _Service.GetallBy();
            //    return new JsonResult() { Data = result };
            //}


            public JsonResult GetHistoriesByInside(InvoiceFilterModel filter)
            {
                var result = _Service.GetHistoriesByInside(filter);
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
            public JsonResult GetHotels(InvoiceFilterModel filter)
            {
                var result = _Service.GetHotels(filter);
                return new JsonResult() { Data = result };
            }
            [HttpPost]
            public JsonResult DisableHotel(int hotelId)
            {
                var result = _InsideService.DisableHotel(hotelId);
                return new JsonResult() { Data = result };
            }
        }
	
}