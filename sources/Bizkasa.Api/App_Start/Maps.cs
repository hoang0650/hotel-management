using AutoMapper;
using MyFinance.Domain;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bizkasa.Api.App_Start
{
    public class Maps
    {
        public static void Boot()
        {
            Mapper.CreateMap<UserViewModel, User>();

            Mapper.CreateMap<ShiftDTO, Shift>();
            Mapper.CreateMap<Shift, ShiftDTO>();

            Mapper.CreateMap<User, UserLoginViewModel>();
            Mapper.CreateMap<User, UserViewModel>()
                 .ForMember(x => x.Password, y => y.Ignore());

            //Mapper.CreateMap<RoomViewClassModel, RoomClassModel>();
            //Mapper.CreateMap<Seller.Tikasa.Areas.CPanelAdmin.Models.Room.RoomAttribute, MyFinance.Domain.Entities.RoomAttribute>();
            Mapper.CreateMap<MyFinance.Domain.Entities.RoomAttribute, RoomAttributeViewModel>();

            Mapper.CreateMap<RoomClass, RoomClassRow>();
            Mapper.CreateMap<RoomClass, RoomClassModel>();
            Mapper.CreateMap<RoomClass, RoomClassViewModel>();
            Mapper.CreateMap<GroupWidget, WidgetGroupRowModel>();
            Mapper.CreateMap<WidgetGroupRowModel, GroupWidget>();
            Mapper.CreateMap<Widget, WidgetRowModel>();

            Mapper.CreateMap<Room, RoomModel>();
            //Order
            Mapper.CreateMap<OrderRowModel, Order>()
                  .ForMember(x => x.Price, y => y.MapFrom(a => a.Price));
            Mapper.CreateMap<CustomerRowModel, Customer>();
            Mapper.CreateMap<Customer, CustomerRowModel>();

            // end
            Mapper.CreateMap<Hotel, HotelModel>();
            Mapper.CreateMap<HotelModel, Hotel>();
            Mapper.CreateMap<HotelRegisterModel, Hotel>();

            //Floor
            Mapper.CreateMap<Floor, FloorModel>();

            //RoomClass
            Mapper.CreateMap<RoomClass, RoomClassModel>();
            Mapper.CreateMap<Utility, RoomTypeViewModel>();
            Mapper.CreateMap<ConfigPriceRowModel, ConfigPrice>();
            Mapper.CreateMap<ConfigPrice, ConfigPriceRowModel>();

            

            //Gallery
            Mapper.CreateMap<Gallery, GalleryModel>();

            //Room
            Mapper.CreateMap<Room, RoomForEditModel>() 
                .ForMember(x => x.RoomId, y => y.MapFrom(a=>a.Id))
                .ForMember(x => x.RoomName, y => y.MapFrom(a => a.Name));

            #region inside
             Mapper.CreateMap<InsideUser, UserLoginViewModel>();
             Mapper.CreateMap<Utility, UtilityModel>();
            #endregion

             Mapper.CreateMap<Ticket, TicketModel>();
             Mapper.CreateMap<TicketModel, Ticket>();

        }
    }
}