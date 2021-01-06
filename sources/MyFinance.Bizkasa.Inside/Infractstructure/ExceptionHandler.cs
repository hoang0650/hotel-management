
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using System.Web.Security;

using log4net;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Bizkasa.Service;

namespace MyFinance.Bizkasa.Inside.Infractstructure
{
    public class SessionFilterAction : System.Web.Mvc.ActionFilterAttribute
    {
        
        public string ActionName { get; set; }
        public IDictionary<string,object> Parameter { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Parameter = filterContext.ActionParameters;
            ActionName = filterContext.ActionDescriptor.ActionName;
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ITikasaService _service=new TikasaService();
            
            var row =new HistoryModel(){
                Content = GeneralContent(),
                CreatedDate=DateTime.Now,
                UserName=MyFinance.Utils.WorkContext.BizKasaContext.UserName,
                UserId = MyFinance.Utils.WorkContext.BizKasaContext.UserId
            };
            _service.InsertHistory(row);
        }

  
        private string GeneralContent()
        {
            string result = string.Empty;
            switch (ActionName)
            {
                case "Login":
                    result = "đăng nhập";
                    break;
                case "AddOrder":
                  var model=  (OrderRowModel)Parameter.Where(a=>a.Key=="model").FirstOrDefault().Value;
                  if (model.Id == 0)
                  {
                      result = string.Format("cho khách hàng [{0}] nhận phòng [{1}]", model.Customers.Where(a=>a.IsPrimary).FirstOrDefault().Name, model.RoomName);
                  }                    
                    break;
                case "BookingOrder":
                    var row = (OrderRowModel)Parameter.Where(a => a.Key == "model").FirstOrDefault().Value;
                    result = string.Format("cho khách hàng [{0}] đặt phòng [{1}]", row.Customers.Where(a => a.IsPrimary).FirstOrDefault().Name, row.RoomName);
                    break;
                case "UpdateOrder":
                    var row1 = (OrderRowModel)Parameter.Where(a => a.Key == "model").FirstOrDefault().Value;
                    if (row1.OrderStatus == (int)OrderStatus.Paid || row1.OrderStatus == (int)OrderStatus.NotPaid)
                        result = string.Format("cho khách hàng [{0}] trả phòng [{1}]", row1.CustomerName, row1.RoomName);
                    break;
                case "TranferBookingToCheckIn":
                    var row2 = Parameter.Where(a => a.Key == "model").FirstOrDefault().Value;
                    result = string.Format("chuyển từ đặt phòng sang nhận phòng [{0}]", row2);
                    break;
                case "AddHotel":
                    result = "Cập nhật thông tin khách sạn ";
                    break;
                case "AddConfig":
                    result = "Cập nhật cấu hình khách sạn ";
                    break;
                case "DeleteWidget":
                    result = "Xóa dịch vụ ";
                    break;
                case "AddWidget":
                    var row3 = (WidgetRowModel)Parameter.Where(a => a.Key == "model").FirstOrDefault().Value;
                    result =string.Format("thêm dịch vụ [{0}]",row3.Name);
                    break;
                case "InsertOrUpdateInvoice":
                    var row4 = (InvoiceRowModel)Parameter.Where(a => a.Key == "data").FirstOrDefault().Value;
                    if(row4.Id<=0&&row4.InvoiceType==(int)InvoiceType.Receipt)
                        result = string.Format("thêm mới phiếu thu ", row4.RoomName);
                    if (row4.Id <= 0 && row4.InvoiceType == (int)InvoiceType.Payment)
                        result = string.Format("thêm mới phiếu chi ", row4.RoomName);
                    break;
                case "AddRoomClass":
                    var row5 = (RoomClassModel)Parameter.Where(a => a.Key == "data").FirstOrDefault().Value;
                    if (row5.RoomClass.Id <= 0)
                        result = string.Format("Thêm mới loại phòng [{0}]", row5.RoomClass.Name);
                    else
                        result = string.Format("Cập nhật loại phòng [{0}]", row5.RoomClass.Name);
                    break;
            }
            return result;
        }


    }


    public class ExceptionHandler : System.Web.Mvc.IExceptionFilter
    {
        private static readonly ILog logger =
           LogManager.GetLogger(typeof(ExceptionHandler));
        public static bool IsAjaxRequest()
        {
            var request = HttpContext.Current.Request;
            return ((request["X-Requested-With"] == "XMLHttpRequest") || ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest")));
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {

                logger.Error(filterContext.Exception);
                if (filterContext.Exception is SecurityAccessDeniedException)
                {
                    if (filterContext.HttpContext.Request.IsAuthenticated)
                    {
                        //Log out this session
                        //if (!MyFinance.Extention.IoC.Get<ITikasaService>().CheckLogin())
                        //{
                        //    FormsAuthentication.SignOut();
                        //}
                        FormsAuthentication.SignOut();
                        if (!IsAjaxRequest())
                        {
                            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
                            return;
                        }
                    }

                    if (IsAjaxRequest())
                    {
                        HttpContext.Current.Response.Redirect("~/Home/Error?code=999");
                        return;
                    }
                }

                if (!HttpContext.Current.IsDebuggingEnabled)
                {
                    if (filterContext.Exception is System.Web.HttpRequestValidationException)
                    {
                        HttpContext.Current.Response.Redirect("~/Home/Error?code=9000");
                        return;
                    }
                }

                HttpContext.Current.Response.Redirect("~/Home/Logon");
                //HttpContext.Current.Response.Redirect("~/CPanelAdmin/Home/Error?code=" + HttpContext.Current.Response.StatusCode.ToString());
            }
        }
    }


    public class KasaPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public KasaPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] roles { get; set; }
    }

    public class KasaAuthorizeAttribute : AuthorizeAttribute
    {
        public ITikasaService _service { get; set; }
        public string Description { get; set; }
        public string RolesConfigKey { get; set; }

        protected virtual KasaPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as KasaPrincipal; }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new HttpUnauthorizedResult(); // Try this but i'm not sure
            filterContext.Result = new RedirectResult("~/CPanelAdmin/Home/LogOn");
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            base.OnAuthorization(filterContext); //returns to login url
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                var authorizedUsers =string.Empty ;//ConfigurationManager.AppSettings[UsersConfigKey];
                var authorizedRoles = string.Empty;// ConfigurationManager.AppSettings[RolesConfigKey];

                Users = String.IsNullOrEmpty(Users) ? authorizedUsers : Users;
                Roles = String.IsNullOrEmpty(Roles) ? authorizedRoles : Roles;

                //if (!String.IsNullOrEmpty(Roles))
                //{
                //    if (!CurrentUser.IsInRole(Roles))
                //    {
                //        filterContext.Result = new RedirectToRouteResult(new
                //        RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));

                //        // base.OnAuthorization(filterContext); //returns to login url
                //    }
                //}

                //if (!String.IsNullOrEmpty(Users))
                //{
                //    if (!Users.Contains(CurrentUser.UserId.ToString()))
                //    {
                //        filterContext.Result = new RedirectToRouteResult(new
                //        RouteValueDictionary(new { controller = "Home", action = "Logon" }));

                //         base.OnAuthorization(filterContext); //returns to login url
                //    }
                //}
            }
           
           
        }

     
    }
}