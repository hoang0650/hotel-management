using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using MyFinance.Business;
using MyFinance.Business.Bizkasa.Inside;
using MyFinance.Business.Inside;
using MyFinance.Core;
using MyFinance.Data.Infrastructure;
using MyFinance.Extention;
using MyFinance.ApiService;
using MyFinance.ApiService.Inside;
using System.Web.Http;

namespace Bizkasa.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();



            container.RegisterType<IDatabaseFactory, DatabaseFactory>(new HttpContextLifetimeManager<IDatabaseFactory>());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HttpContextLifetimeManager<IUnitOfWork>());


            container.RegisterType<BusinessProcess>(new HttpContextLifetimeManager<BusinessProcess>());

        
            #region Business
            container.RegisterType<IUserBusiness, UserBusiness>();
            container.RegisterType<IWidgetBusiness, WidgetBusiness>();
            container.RegisterType<IRoomBusiness, RoomBusiness>();
            container.RegisterType<ISystemConfigBusiness, SystemConfigBusiness>();
            container.RegisterType<IOrderBusiness, OrderBusiness>();
            container.RegisterType<ICustomerBusiness, CustomerBusiness>();
            container.RegisterType<IHotelBusiness, HotelBusiness>();
            container.RegisterType<IInvoiceBusiness, InvoiceBusiness>();
            container.RegisterType<IGalleryBusiness, GalleryBusiness>();
            container.RegisterType<IHistoryBusiness, HistoryBusiness>();
            container.RegisterType<IReportBusiness, ReportBusiness>();
            container.RegisterType<ITokenBusiness, TokenBusiness>();
            container.RegisterType<IInsideHotelBusiness, InsideHotelBusiness>();
            container.RegisterType<IInsideUserBusiness, InsideUserBusiness>();
            container.RegisterType<IInsideUtilityBusiness, InsideUtilityBusiness>();
            
            #endregion


            #region service
            container.RegisterType<ITikasaService, TikasaService>();
            container.RegisterType<IInsideService, InsideService>();
            #endregion

            IServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);

            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}