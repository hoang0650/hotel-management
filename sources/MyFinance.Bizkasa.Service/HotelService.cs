
using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using MyFinance.Extention;
using MyFinance.Proxy;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFinance.Bizkasa.Service
{
    public interface IHotelservice
    {
        Response<bool> AddHotel(HotelModel model);
      
        Response<HotelModel> GetHotelInfo();
        Response<bool> CreateHotelFromSystem(HotelModel model);
        Response<bool> RegisterHotel(HotelRegisterModel data);
        Response<DataPaging<List<HotelModel>>> GetHotels(InvoiceFilterModel filter);
        Response<List<RoomTypeViewModel>> GetHotelUtilityBy();
        Response<UserLoginViewModel> ChangeViewHotel(HotelModel request);
    }
    public partial class TikasaService
    {
        public Response<UserLoginViewModel> ChangeViewHotel(HotelModel request)
        {
            UserLoginViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelProxyService>().ChangeViewHotel(request);
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<List<RoomTypeViewModel>> GetHotelUtilityBy()
        {
            List<RoomTypeViewModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelProxyService>().GetHotelUtilityBy();//IoC.Get<IHotelBusiness>().GetHotelUtilityBy();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        
        public Response<DataPaging<List<HotelModel>>> GetHotels(InvoiceFilterModel filter)
        {
            List<HotelModel> result = null;
            int total = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelProxyService>().GetHotels(filter, out total);
            });

            return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
        }
        public Response<bool> RegisterHotel(HotelRegisterModel data)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelProxyService>().RegisterHotel(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> AddHotel(HotelModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelProxyService>().AddHotel(model);//IoC.Get<IHotelBusiness>().AddHotel(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

       

        public Response<HotelModel> GetHotelInfo()
        {
            HotelModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelProxyService>().GetHotelInfo();//IoC.Get<IHotelBusiness>().GetById(hotelId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<bool> CreateHotelFromSystem(HotelModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelProxyService>().CreateHotelFromSystem(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
      
    }
}
