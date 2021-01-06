using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
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
    //[SessionFilterAction]
    public class CategoryController : Controller
    {
        
         private readonly ICategoryService _Service;


         public CategoryController(ICategoryService userService)
         {
             this._Service = userService;

         }  
        //
        // GET: /Category/
        public ActionResult Index(string t)
        {
            return View();
        }

        public JsonResult getCateByType(CategoryEnum Type)
        {           
           
            var result = _Service.GetCategoryByType((int)Type);
            return new JsonResult() { Data=result};
        }

        public JsonResult AddCategory(CategoryModel data)
        {
            data.HotelId = WorkContext.BizKasaContext.HotelId;
            var model = new CategoryModel() { Name = data.Name, CategoryType = data.CategoryType,HotelId=data.HotelId };
            var result = _Service.CreateCategory(model);
            return new JsonResult() { Data = result };
        }
        //
        // GET: /Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Category/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create
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
        // GET: /Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Category/Edit/5
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
        // GET: /Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Category/Delete/5
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
