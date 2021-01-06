using MyFinance.Domain.BusinessModel;
using MyFinance.Service;
using MyFinance.Utils;
using Seller.Tikasa.Infractstructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seller.Tikasa.Controllers
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
                data.Password = Utils.CreateMD5(data.Password);
            var result = _Service.AddUser(data);
            return new JsonResult() { Data = result };
        }


       

        public JsonResult MapUserHotel(int HotelId ,int UserId)
        {
            var result = _Service.MappingUserHotel(HotelId,UserId);
            return new JsonResult() { Data = result };
        }

        public JsonResult GetallBy()
        {
            var result = _Service.GetallBy();
            return new JsonResult() { Data = result };
        }


        public JsonResult GetUserByHotel()
        {
            var hotelId = WorkContext.BizKasaContext.HotelId;
            var result = _Service.GetUserByHotel(hotelId);
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
