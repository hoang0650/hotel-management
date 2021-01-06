using AutoMapper;
using MyFinance.Domain;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using MyFinance.Bizkasa.Areas.CPanelAdmin.Models.Room;

namespace MyFinance.Bizkasa.Maps
{
    public class Maps
    {
        public static void Boot()
        {
            Mapper.CreateMap<UserViewModel, User>();

            Mapper.CreateMap<User, UserLoginViewModel>();
            Mapper.CreateMap<User, UserViewModel>()
                 .ForMember(x => x.Password, y => y.Ignore());

            Mapper.CreateMap<RoomViewClassModel, RoomClassModel>();
            Mapper.CreateMap<MyFinance.Bizkasa.Areas.CPanelAdmin.Models.Room.RoomAttribute, MyFinance.Domain.Entities.RoomAttribute>();
            Mapper.CreateMap<MyFinance.Domain.Entities.RoomAttribute, RoomAttributeViewModel>();

            Mapper.CreateMap<RoomClass, RoomClassRow>();
            Mapper.CreateMap<RoomClass, RoomClassModel>();
            Mapper.CreateMap<RoomClass, RoomClassViewModel>();
            Mapper.CreateMap<GroupWidget, WidgetGroupRowModel>();
            Mapper.CreateMap<WidgetGroupRowModel, GroupWidget>();
            Mapper.CreateMap<Widget, WidgetRowModel>();

            Mapper.CreateMap<Room, RoomModel>();
            //Order
            Mapper.CreateMap<OrderRowModel, Order>();
            Mapper.CreateMap<CustomerRowModel, Customer>();
            Mapper.CreateMap<Customer, CustomerRowModel>();

            // end
            Mapper.CreateMap<Hotel, ShiftDTO>();
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
            Mapper.CreateMap<Shift, Shift>();
            
        }
    }
}