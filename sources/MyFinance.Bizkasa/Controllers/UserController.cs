using MyFinance.Domain.BusinessModel;
using MyFinance.Utils;
using System.Web.Mvc;
using MyFinance.ApiService;

namespace MyFinance.Bizkasa.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        private readonly IUserService _Service;


        public UserController(IUserService userService)
         {
             this._Service = userService;

         }

        public JsonResult AddUser(UserViewModel data)
        {
            if(!string.IsNullOrWhiteSpace(data.Password))
                data.Password = CommonUtil.CreateMD5(data.Password);
            var result = _Service.AddUser(data);
            return new JsonResult() { Data = result };
        }

        public JsonResult GetUserByHotel()
        {
            var result = _Service.GetUserByHotel(WorkContext.BizKasaContext.HotelId);
            return new JsonResult() { Data = result };
        }
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create
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
        // GET: /User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5
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
        // GET: /User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5
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
