using System.Web;
using System.Web.Optimization;

namespace MyFinance.Bizkasa
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            #region Js




            bundles.Add(new ScriptBundle("~/Areas/CPanelAdmin/Script/bundles/adminjs").Include(
                  "~/Areas/CPanelAdmin/Script/Libs/jquery.2.1.1.js",
                  "~/Areas/CPanelAdmin/Script/Libs/chosen.jquery.js",
                  "~/Areas/CPanelAdmin/Script/Libs/bootstrap.js",                  
                   "~/Areas/CPanelAdmin/Script/Libs/angular.js",
                  "~/Areas/CPanelAdmin/Script/Libs/bootstrap-datepicker.js",
                  "~/Areas/CPanelAdmin/Script/Libs/toastr.js",                  
                  "~/Areas/CPanelAdmin/Script/Libs/ui-bootstrap-tpls-0.10.0.js",
                  "~/Areas/CPanelAdmin/Script/Libs/moment.js",
                  "~/Areas/CPanelAdmin/Script/Libs/bootstrap-timepicker.js",
                  "~/Areas/CPanelAdmin/Script/Libs/jquery.colorbox.js",
                  "~/Areas/CPanelAdmin/Script/Libs/jquery.gritter.js",
                  "~/Areas/CPanelAdmin/Script/Libs/ace-elements.js",
                  "~/Areas/CPanelAdmin/Script/Libs/bootstrap-colorpicker.js",
                   "~/Areas/CPanelAdmin/Script/Libs/daterangepicker.js",
                  "~/Areas/CPanelAdmin/Script/Libs/angucomplete-alt.js",                  
                  "~/Areas/CPanelAdmin/Script/Libs/ace.js",
                  "~/Areas/CPanelAdmin/Script/Libs/global.js",
                  "~/Areas/CPanelAdmin/Script/Libs/ace-extra.js"
                  ));
       
            

            bundles.Add(new ScriptBundle("~/bundles/adminIndexjs").Include(
                "~/Areas/CPanelAdmin/Content/js/jquery-ui.js",
                 "~/Areas/CPanelAdmin/Script/Paging/paging.js",
                 "~/Areas/CPanelAdmin/Script/Shared/AngularChosen.js",
                 "~/Areas/CPanelAdmin/Content/fullcalendar/fullcalendar.min.js",
                 "~/Areas/CPanelAdmin/Content/fullcalendar/scheduler.min.js",                              
                 "~/Areas/CPanelAdmin/Content/fullcalendar/lang/vi.js"
                 
                 ));


            bundles.Add(new ScriptBundle("~/bundles/adminLogonjs").Include(
               "~/Areas/CPanelAdmin/Script/Libs/jquery-{version}.js",
               "~/Areas/CPanelAdmin/Script/Libs/angular.js",
                "~/Areas/CPanelAdmin/Script/Libs/toastr.js",
                "~/Areas/CPanelAdmin/Script/Libs/ui-bootstrap-tpls-0.10.0.js",
                "~/Areas/CPanelAdmin/Script/Libs/daypilot-all.js",
                "~/Areas/CPanelAdmin/Script/Libs/angucomplete-alt.js",
                 "~/Areas/CPanelAdmin/Script/Libs/global.js",
                "~/Areas/CPanelAdmin/Script/Module/appModule.js"
              // "~/Areas/CPanelAdmin/Script/User/User.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Script/Room/Widget").Include(
              "~/Areas/CPanelAdmin/Script/Room/Widget.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/Script/Customer/Customer").Include(
             "~/Areas/CPanelAdmin/Script/Customer/Customer.js"
              ));

            #endregion
            #region Css
            bundles.Add(new StyleBundle("~/Areas/CPanelAdmin/Content/admincss").Include(
                      "~/Areas/CPanelAdmin/Content/css/bootstrap.min.css",
                      "~/Areas/CPanelAdmin/Content/css/font-awesome.css",
                      "~/Areas/CPanelAdmin/Content/css/jquery.gritter.min.css",
                      "~/Areas/CPanelAdmin/Content/css/colorbox.min.css",
                      "~/Areas/CPanelAdmin/Content/css/toastr.css",
                      "~/Areas/CPanelAdmin/Content/css/jquery-ui.custom.min.css",
                      "~/Areas/CPanelAdmin/Content/css/jquery-ui.min.css",
                      "~/Areas/CPanelAdmin/Content/css/select2.min.css",
                      "~/Areas/CPanelAdmin/Content/css/datepicker.min.css",
                      "~/Areas/CPanelAdmin/Content/css/daterangepicker.min.css",
                      "~/Areas/CPanelAdmin/Content/css/bootstrap-datetimepicker.min.css",
                      "~/Areas/CPanelAdmin/Content/css/bootstrap-timepicker.min.css",
                      "~/Areas/CPanelAdmin/Content/css/bootstrap-editable.min.css",
                      "~/Areas/CPanelAdmin/Content/css/chosen.min.css",
                      "~/Areas/CPanelAdmin/Content/css/colorpicker.min.css",
                      "~/Areas/CPanelAdmin/Content/js/AutoComlete/angucomplete-alt.css",
                      "~/Areas/CPanelAdmin/Content/css/css1.css",
                      "~/Areas/CPanelAdmin/Content/css/ace.min.css",
                      "~/Areas/CPanelAdmin/Content/css/css2.css",
                      "~/Areas/CPanelAdmin/Content/css/css3.css"));


            bundles.Add(new StyleBundle("~/Areas/CPanelAdmin/Content/adminIndexcss").Include(
                     "~/Areas/CPanelAdmin/Content/js/AutoComlete/angucomplete-alt.css",
                     "~/Areas/CPanelAdmin/Content/fullcalendar/fullcalendar.min.css"));


            bundles.Add(new StyleBundle("~/Areas/CPanelAdmin/Content/adminLogoncss").Include(
                   "~/Areas/CPanelAdmin/Content/css/bootstrap.min.css",
                   "~/Areas/CPanelAdmin/Content/css/font-awesome.css",
                   "~/Areas/CPanelAdmin/Content/css/fonts.googleapis.com.css",
                   "~/Areas/CPanelAdmin/Content/css/ace.min.css",
                   "~/Areas/CPanelAdmin/Content/css/ace-rtl.min.css",
                   "~/Areas/CPanelAdmin/Content/css/css1.css",
                   "~/Areas/CPanelAdmin/Content/css/toastr.css"
                   ));
            #endregion
            BundleTable.EnableOptimizations = true;
        }
    }
}
