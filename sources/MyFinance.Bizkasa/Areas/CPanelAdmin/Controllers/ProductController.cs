using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /CPanelAdmin/Product/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CPanelAdmin/Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CPanelAdmin/Product/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CPanelAdmin/Product/Create
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
        // GET: /CPanelAdmin/Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /CPanelAdmin/Product/Edit/5
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
        // GET: /CPanelAdmin/Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CPanelAdmin/Product/Delete/5
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
