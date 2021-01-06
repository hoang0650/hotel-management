using System.Web.Mvc;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin
{
    public class CPanelAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CPanelAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {



            context.MapRoute(
                "CPanelAdmin_default",
                "CPanelAdmin/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional },
                new string[] {"MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers" }
            );
        }
    }
}
