using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using MyFinance.Data.Infrastructure;
using Seller.Tikasa.IoC;
using MyFinance.Data;
using MyFinance.Data.Repositories;
using MyFinance.Business;
using MyFinance.Service;
using MyFinance.Core;
using MyFinance.Domain.Entities;


namespace Seller.Tikasa.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();


            container.RegisterType<IDatabaseFactory, DatabaseFactory>(new HttpContextLifetimeManager<IDatabaseFactory>());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HttpContextLifetimeManager<IUnitOfWork>());

         

            container.RegisterType<ICategoryRepository, CategoryRepository>(new HttpContextLifetimeManager<ICategoryRepository>());
       container .RegisterType<ICategoryService, CategoryService>(new HttpContextLifetimeManager<ICategoryService>());
        container.RegisterType<IUserRepository, UserRepository>(new HttpContextLifetimeManager<IUserRepository>());
        container .RegisterType<IRoleRepository, RoleRepository>(new HttpContextLifetimeManager<IRoleRepository>());
        
        container.RegisterType<BusinessProcess>(new HttpContextLifetimeManager<BusinessProcess>());
        
          
          
           
           #region repository
        container.RegisterType<IHotelRepository, HotelRepository>(new HttpContextLifetimeManager<IHotelRepository>());
           container.RegisterType<IUserRepository, UserRepository>(new HttpContextLifetimeManager<IUserRepository>());
           container.RegisterType<IRoomClassRepository, RoomClassRepository>(new HttpContextLifetimeManager<IRoomClassRepository>());
           container.RegisterType<IRoomAttributeRepository, RoomAttributeRepository>(new HttpContextLifetimeManager<IRoomAttributeRepository>());
           container.RegisterType<IWidgetRepository, WidgetRepository>(new HttpContextLifetimeManager<IWidgetRepository>());
           container.RegisterType<IGroupWidgetRepository, GroupWidgetRepository>(new HttpContextLifetimeManager<IUserBusiness>());
           container.RegisterType<IRoomRepository, RoomRepository>(new HttpContextLifetimeManager<IRoomRepository>());
           container.RegisterType<IFloorRepository, FloorRepository>(new HttpContextLifetimeManager<IFloorRepository>());
           container.RegisterType<IOrderRepository, OrderRepository>(new HttpContextLifetimeManager<IOrderRepository>());
           container.RegisterType<IOrderCustomerRepository, OrderCustomerRepository>(new HttpContextLifetimeManager<IOrderCustomerRepository>());
           container.RegisterType<IOrderRoomRepository, OrderRoomRepository>(new HttpContextLifetimeManager<IOrderRoomRepository>());
           container.RegisterType<IOrderServiceRepository, OrderServiceRepository>(new HttpContextLifetimeManager<IOrderServiceRepository>());
           container.RegisterType<ICustomerRepository, CustomerRepository>(new HttpContextLifetimeManager<ICustomerRepository>());
           container.RegisterType<ISystemConfigRepository, SystemConfigRepository>(new HttpContextLifetimeManager<ISystemConfigRepository>());
           container.RegisterType<IInvoiceRepository, InvoiceRepository>(new HttpContextLifetimeManager<IInvoiceRepository>());
           container.RegisterType<IInvoiceDetailRepository, InvoiceDetailRepository>(new HttpContextLifetimeManager<IInvoiceDetailRepository>());
           container.RegisterType<IUtilityReponsitory,UtilityReponsitory>(new HttpContextLifetimeManager<IUtilityReponsitory>());
           container.RegisterType<IUtilityMappingReponsitory, UtilityMappingReponsitory>(new HttpContextLifetimeManager<IUtilityMappingReponsitory>());
           container.RegisterType<IGalleryReponsitory, GalleryReponsitory>(new HttpContextLifetimeManager<IGalleryReponsitory>());
           container.RegisterType<IHistoryReponsitory, HistoryReponsitory>();
           container.RegisterType<IConfigPriceReponsitory, ConfigPriceReponsitory>();
           #endregion

           #region Business
           container.RegisterType<IUserBusiness, UserBusiness>(new HttpContextLifetimeManager<IUserBusiness>());
           container.RegisterType<IWidgetBusiness, WidgetBusiness>(new HttpContextLifetimeManager<IWidgetBusiness>());
           container.RegisterType<IRoomBusiness, RoomBusiness>(new HttpContextLifetimeManager<IRoomBusiness>());
           container.RegisterType<ISystemConfigBusiness, SystemConfigBusiness>(new HttpContextLifetimeManager<ISystemConfigBusiness>());
           container.RegisterType<IOrderBusiness, OrderBusiness>();
           container.RegisterType<ICustomerBusiness, CustomerBusiness>();
           container.RegisterType<IHotelBusiness, HotelBusiness>();
           container.RegisterType<IInvoiceBusiness, InvoiceBusiness>(new HttpContextLifetimeManager<IInvoiceBusiness>());
           container.RegisterType<IGalleryBusiness, GalleryBusiness>();
           container.RegisterType<IHistoryBusiness, HistoryBusiness>();
           container.RegisterType<IReportBusiness, ReportBusiness>();
           #endregion


           #region service
           container.RegisterType<ITikasaService, TikasaService>(new HttpContextLifetimeManager<ITikasaService>());
           #endregion

         
        }
    }
}
