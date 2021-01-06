using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Proxy
{
    public interface IHotelProxyService : IBusinessBase
    {
        #region Bizkasa

        UserLoginViewModel ChangeViewHotel(HotelModel filter);
        HotelModel GetHotelInfo();
        List<RoomTypeViewModel> GetHotelUtilityBy();
        bool AddHotel(HotelModel model);
        SystemConfigModel GetConfig();
        SystemConfigModel AddOrUpdateConfig(SystemConfigModel model);
        bool CreateHotelFromSystem(HotelModel model);
        bool RegisterHotel(HotelRegisterModel data);
        List<HotelModel> GetHotels(InvoiceFilterModel filter, out int total);
        #endregion
        #region Inside
      
        bool DisableHotel(int HotelId);
        #endregion
    }
    public class HotelProxyService : BaseProxyService, IHotelProxyService
    {
        #region Inside
        
        public bool DisableHotel(int HotelId)
        {
            try
            {
                string url = "api/Hotel/Inside/DisableHotel";
                return PostStructService<bool>(new { HotelId = HotelId }, url);
            }
            catch (Exception ex)
            {
                this.AddError(ex.InnerException);
                return false;
            }
        }

        #endregion

        #region Bizkasa

        public UserLoginViewModel ChangeViewHotel(HotelModel filter)
        {
            string url = "api/Hotel/ChangeViewHotel";
            return PostService<UserLoginViewModel>(filter, url);
        }

        public List<HotelModel> GetHotels(InvoiceFilterModel filter, out int total)
        {
            string url = "api/Hotel/GetHotels";
            return PostOutTotalService<List<HotelModel>>(filter, url, out total);
        }
        public bool RegisterHotel(HotelRegisterModel data)
        {
            string url = "api/Account/RegisterHotel";
            return PostNonTokenStructService<bool>(data, url);
        }
        public bool CreateHotelFromSystem(HotelModel model)
        {
            string url = "api/Hotel/CreateHotelFromSystem";
            return PostStructService<bool>(model, url);
        }
        public SystemConfigModel AddOrUpdateConfig(SystemConfigModel model)
        {
            try
            {
                string url = "api/Hotel/AddOrUpdateConfig";
                return PostService<SystemConfigModel>(model, url);
            }
            catch (Exception ex)
            {
                this.AddError(ex.InnerException);
                return null;
            }
        }

        public SystemConfigModel GetConfig()
        {
            try
            {
                string url = "api/Hotel/GetConfig";
                return GetDataService<SystemConfigModel>(url);
               
            }
            catch (Exception ex)
            {

                this.AddError(ex.InnerException);
                return null;
            }

        }
        public bool AddHotel(HotelModel model)
        {
            try
            {
                string url = "api/Hotel/AddHotel";
                return PostStructService<bool>(model,url);
            }
            catch (Exception ex)
            {
                this.AddError(ex.InnerException);
                return false;
            }
        }
        public HotelModel GetHotelInfo()
        {
                string url="api/Hotel/GetHotelInfo";
                return GetDataService<HotelModel>(url);
        }
        public List<RoomTypeViewModel> GetHotelUtilityBy()
        {
         
                string url = "api/Hotel/GetHotelUtilityBy";
                return GetDataService<List<RoomTypeViewModel>>(url);
         
        }


        #endregion

       
    }
}
