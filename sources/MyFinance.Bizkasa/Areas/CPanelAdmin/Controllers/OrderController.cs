using MyFinance.Domain.BusinessModel;
using System.Web.Mvc;
using MyFinance.Bizkasa.Infractstructure;
using MyFinance.ApiService;
using MyFinance.Utils;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{
    public class OrderController : Controller
    {

        private readonly ITikasaService _Service;
        public OrderController(ITikasaService userService)
        {
            this._Service = userService;

        }



        [HttpPost]
        public JsonResult GetFolioCustomer(int companyId, int typeCustomer)
        {
            var result = _Service.GetFolioCustomer(companyId, typeCustomer);
            return new JsonResult() { Data = result };
        }

        [HttpPost]
        public JsonResult GetOrderBookingByCompany(OrderFilterModel data)
        {
            var result = _Service.GetOrderBookingByCompany(data);
            return result.ToJsonResult(result.Data);
        }

        [HttpGet]
        public JsonResult GetCountries()
        {
            var result = _Service.GetCountries();
            return result.ToJsonResult(result.Data);
        }
        [HttpPost]
        public JsonResult DeleteOrderDetail(OrderDetailDTO data)
        {
            var result = _Service.DeleteOrderDetail(data);
            return result.ToJsonResult(result.Data);
        }

        [HttpPost]
        public JsonResult AddOrderDetail(OrderDetailDTO data)
        {
            data.ShiftId = WorkContext.BizKasaContext.ShiftId;
            var result = _Service.AddOrderDetail(data);
            return result.ToJsonResult(result.Data);
        }

        [HttpPost]
        public JsonResult ChangCalculatorMode(RequestCheckOutModel request)
        {
            request.hotelId = WorkContext.BizKasaContext.HotelId;
            var result = _Service.ChangCalculatorMode(request);
            return result.ToJsonResult(result.Data);
        }
    }
}