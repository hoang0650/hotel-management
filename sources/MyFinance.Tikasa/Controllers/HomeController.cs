using MyFinance.ApiService;
using MyFinance.Domain.BusinessModel;
using MyFinance.Tikasa.Infractstructure;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Tikasa.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly ITikasaService _tokenServices;
        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public HomeController(ITikasaService tokenServices)
        {
            this._tokenServices = tokenServices;
        }

        #endregion
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
         
            return View();
        }
       
        public ActionResult Detail(int hotelId)
        {
            var result = _tokenServices.GetHotelById(hotelId);
            ViewBag.Title = result.Data.Name;
            ViewBag.Address = result.Data.Address;
            ViewBag.Description = result.Data.Description;
            ViewBag.Seo_Image =ConfigKey.URLAPI+ result.Data.Logo;
            return View(result.Data);
        }
        [HttpGet]
        public ActionResult Result(string keyword,int page=1)
        {
            InvoiceFilterModel model = new InvoiceFilterModel()
            {
                Keyword = keyword,
                Page = new PagingModel(),
            };
            model.Page.currentPage = page;

              var result= _tokenServices.GetHotels(model);

            var data = new PagedList<HotelModel>(
                    result.Data.Data,
                    page - 1,
                    model.Page.pageSize,
                    result.Data.TotalRecord
                );
            ViewData["keyword"] = keyword;

            return View(data);
           
        }
        [HttpPost]
        public ActionResult Search(InvoiceFilterModel model )
        {
            var result = _tokenServices.GetHotels(model);

            var data = new PagedList<HotelModel>(
                    result.Data.Data,
                    model.Page.currentPage - 1,
                    model.Page.pageSize,
                    result.Data.TotalRecord
                );
            ViewData["keyword"] = model.Keyword;

            return View("Result",data);

        }


        [HttpPost]
        [Route("tim-quanh-day")]
        public ActionResult GetNearBy(HotelRequestModel model)
        {
            var result = _tokenServices.GetHotelNearBy(model);

            var data = new PagedList<HotelModel>(
                    result.Data.Data,
                    model.Page.currentPage - 1,
                    model.Page.pageSize,
                    result.Data.TotalRecord
                );
            return View("Result", data);

        }


        public ActionResult HotelRelated(string keyword)
        {
            InvoiceFilterModel model = new InvoiceFilterModel()
            {
                Keyword = keyword,
                Page = new PagingModel(),
            };
            model.Page.currentPage = 1;
            model.Page.pageSize = 5;

            var result = _tokenServices.GetHotels(model);
            return PartialView("_HotelRelated",result.Data.Data);
        }
    }
}