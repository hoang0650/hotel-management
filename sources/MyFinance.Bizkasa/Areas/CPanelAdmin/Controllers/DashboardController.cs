using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /CPanelAdmin/TestSPA/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Widget()
        {
            return View();
        }
	}
}