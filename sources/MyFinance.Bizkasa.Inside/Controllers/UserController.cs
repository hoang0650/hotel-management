using MyFinance.Bizkasa.Service;
using MyFinance.Bizkasa.Service.Inside;
using MyFinance.Domain.BusinessModel;
using System.Web.Mvc;

namespace MyFinance.Bizkasa.Inside.Controllers
{
    public class UserController : Controller
    {
        private readonly ITikasaService _Service;

            private readonly IInsideService _InsideService;
            public UserController(ITikasaService hotelService, IInsideService insideService)
            {
                this._Service = hotelService;
                _InsideService = insideService;
            }
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetUsers(InvoiceFilterModel filter)
        {
            var result = _InsideService.GetUsers(filter);
            return new JsonResult() { Data = result };
        }
	}
}