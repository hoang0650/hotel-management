using MyFinance.ApiService;
using System.Web.Mvc;

namespace MyFinance.Bizkasa.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _Service;


          public RoomController(IRoomService userService)
         {
             this._Service = userService;

         }  
        //
        // GET: /Room/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Room/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Room/Create
        public ActionResult Create()
        {
            return View();
        }

        

        //
        // GET: /Room/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Room/Edit/5
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
        // GET: /Room/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Room/Delete/5
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
