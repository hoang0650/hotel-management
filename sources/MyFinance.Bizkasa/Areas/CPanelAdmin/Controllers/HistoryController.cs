using MyFinance.Domain.BusinessModel;
using System.Web.Mvc;
using MyFinance.ApiService;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{
    public class HistoryController : Controller
    {
          private readonly ITikasaService _Service;


          public HistoryController(ITikasaService userService)
        {
            this._Service = userService;

        }  
        //
        // GET: /CPanelAdmin/History/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetHistories(InvoiceFilterModel filter)
        {
            var result = _Service.GetHistories(filter);
            return new JsonResult() { Data = result };
        }
	}
}