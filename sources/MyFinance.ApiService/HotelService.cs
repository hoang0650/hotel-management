using MyFinance.Business;
using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using MyFinance.Extention;
using MyFinance.Utils;
using System.Collections.Generic;

namespace MyFinance.ApiService
{
    public interface IHotelservice
    {
        Response<bool> AddHotel(HotelModel model);
        Response<List<Hotel>> GetallHotelBy();
        Response<HotelModel> GetHotelById(int hotelId);
        Response<bool> CreateHotelFromSystem(HotelModel model);
        Response<bool> RegisterHotel(HotelRegisterModel data);
        Response<DataPaging<List<HotelModel>>> GetHotels(InvoiceFilterModel filter);
        Response<List<RoomTypeViewModel>> GetHotelUtilityBy();
        Response<bool> AddTicket(TicketModel model);
        Response<List<TicketModel>> ListTicket();
        Response<bool> RegisterUser(UserViewModel data);
        Response<UserLoginViewModel> ChangeViewHotel(int hotelId);
        Response<DataPaging<IList<HotelModel>>> GetHotelNearBy(HotelRequestModel filter);
        Response<bool> DeleteHotel(List<int> Ids);
        Response<bool> InsertOrUpdateCamera(CameraDTO dto);
        Response<CameraDTO> GetCameraByHotel();
        Response<bool> ResetDataHotel(int hotelId);
    }
    public partial class TikasaService
    {
        public Response<bool> ResetDataHotel(int hotelId)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().ResetDataHotel(hotelId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<CameraDTO> GetCameraByHotel()
        {
            CameraDTO result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().GetCameraByHotel();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> InsertOrUpdateCamera(CameraDTO dto)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().InsertOrUpdateCamera(dto);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<bool> DeleteHotel(List<int> Ids)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().DeleteHotel(Ids);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<UserLoginViewModel> ChangeViewHotel(int hotelId)
        {
            UserLoginViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().ChangeViewHotel(hotelId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<bool> RegisterUser(UserViewModel data)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().RegisterUser(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response< List<TicketModel>> ListTicket()
        {
            List<TicketModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().ListTicket();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> AddTicket(TicketModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().AddTicket(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
      
        public Response<List<RoomTypeViewModel>> GetHotelUtilityBy()
        {
            List<RoomTypeViewModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().GetHotelUtilityBy();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        
        public Response<DataPaging<List<HotelModel>>> GetHotels(InvoiceFilterModel filter)
        {
            List<HotelModel> result = null;
            int total = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().GetHotels(filter, out total);
            });

            return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
        }


        public Response<DataPaging<IList<HotelModel>>> GetHotelNearBy(HotelRequestModel filter)
        {
            IList<HotelModel> result = null;
            int total = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().GetHotelNearBy(filter, out total);
            });

            return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
        }
        public Response<bool> RegisterHotel(HotelRegisterModel data)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().RegisterHotel(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> AddHotel(HotelModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().AddHotel(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<List<Hotel>> GetallHotelBy()
        {
            List<Hotel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().GetallBy();
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<HotelModel> GetHotelById(int hotelId)
        {
            HotelModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().GetById(hotelId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<bool> CreateHotelFromSystem(HotelModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelBusiness>().CreateHotelFromSystem(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
      
    }
}
