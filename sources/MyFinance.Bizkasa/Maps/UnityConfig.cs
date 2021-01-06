using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Seller.Tikasa.IoC;
using MyFinance.Data;
using MyFinance.Service;
using MyFinance.Data.Infrastructure;
using System.Web.Security;
using MyFinance.Data.Repositories;
using Seller.Tikasa.Areas.CPanelAdmin.Controllers;
using MyFinance.Business;


namespace Seller.Tikasa.Maps
{
    
    public static class UnityConfig
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            IServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);        

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
           // GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
           
        }

        public static IUnityContainer BuildUnityContainer()
        {
            IUnityContainer container = new UnityContainer()
        .RegisterInstance<MembershipProvider>(Membership.Provider)
        .RegisterType<IDatabaseFactory, DatabaseFactory>(new HttpContextLifetimeManager<IDatabaseFactory>())
        .RegisterType<IUnitOfWork, UnitOfWork>(new HttpContextLifetimeManager<IUnitOfWork>())
        .RegisterType<ICategoryRepository, CategoryRepository>(new HttpContextLifetimeManager<ICategoryRepository>())
        .RegisterType<ICategoryService, CategoryService>(new HttpContextLifetimeManager<ICategoryService>())
        .RegisterType<IUserRepository, UserRepository>(new HttpContextLifetimeManager<IUserRepository>())
         .RegisterType<IRoleRepository, RoleRepository>(new HttpContextLifetimeManager<IRoleRepository>())
                //.RegisterType<ISecurityService, SecurityService>(new HttpContextLifetimeManager<ISecurityService>())
           .RegisterType<IHotelservice, HotelService>(new HttpContextLifetimeManager<IHotelservice>())
           .RegisterType<IHotelRepository, HotelRepository>(new HttpContextLifetimeManager<IHotelRepository>());
          
           
           #region repository
           container.RegisterType<IUserRepository, UserRepository>();
           container.RegisterType<IRoomClassRepository, RoomClassRepository>();
           container.RegisterType<IRoomAttributeRepository, RoomAttributeRepository>();
           container.RegisterType<IWidgetRepository, WidgetRepository>();
           container.RegisterType<IGroupWidgetRepository, GroupWidgetRepository>();
           container.RegisterType<IRoomRepository, RoomRepository>();
           container.RegisterType<IFloorRepository, FloorRepository>();
           container.RegisterType<IOrderRepository, OrderRepository>();
           container.RegisterType<IOrderCustomerRepository, OrderCustomerRepository>();
           container.RegisterType<IOrderRoomRepository, OrderRoomRepository>();
           container.RegisterType<IOrderServiceRepository, OrderServiceRepository>();
           container.RegisterType<ICustomerRepository, CustomerRepository>();
           container.RegisterType<ISystemConfigRepository, SystemConfigRepository>();
           container.RegisterType<IInvoiceRepository, InvoiceRepository>();
           container.RegisterType<IInvoiceDetailRepository, InvoiceDetailRepository>();
           #endregion

           #region Business
            container.RegisterType<IUserBusiness, UserBusiness>();
            container.RegisterType<IWidgetBusiness, WidgetBusiness>();
            container.RegisterType<IRoomBusiness, RoomBusiness>();
            container.RegisterType<IOrderBusiness, OrderBusiness>();
            container.RegisterType<IInvoiceBusiness, InvoiceBusiness>();
           #endregion


           #region service
           container.RegisterType<ITikasaService, TikasaService>();
           container.RegisterType<ISystemConfigService, SystemConfigService>();
           #endregion

         
            return container;
        }
    }
}