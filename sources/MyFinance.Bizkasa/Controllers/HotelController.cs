using System.Web.Mvc;
using MyFinance.Domain.BusinessModel;
using MyFinance.ApiService;

namespace MyFinance.Bizkasa.Controllers
{
    //[SessionFilterAction]
    public class HotelController : Controller
    {
         private readonly IHotelservice HotelService;

         
        public HotelController(IHotelservice hotelService)
         {
             this.HotelService = hotelService;

         }  
        //
        // GET: /Hotel/
        public ActionResult Index()
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
        public ActionResult Create()
        {
            return View();
        }

        public JsonResult AddHotel(HotelModel data)
        {
            var model = new HotelModel() { Name = data.Name, Phone = data.Phone };
           var result= HotelService.AddHotel(model);
           return new JsonResult() { Data=result};
        }

        //public JsonResult GetallBy()
        //{
        //    var result = HotelService.GetallBy();
        //    return new JsonResult() { Data = result };
        //}
        //[HttpPost]
        
        //public JsonResult GetById()
        //{ 
        //    var hotelId=WorkContext.Context.HotelId;
        //    if(hotelId<=0)
        //        return new JsonResult() { Data = null };
        //    var result = HotelService.GetById(hotelId);
        //    return new JsonResult() { Data = result };
        //}

        //
        // POST: /Hotel/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Hotel/Edit/5
        public ActionResult Edit()
        {

            return View();
        }

        //
        // POST: /Hotel/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Hotel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Hotel/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
