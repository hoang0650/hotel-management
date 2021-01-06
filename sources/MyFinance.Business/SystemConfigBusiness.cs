using System.Linq;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using MyFinance.Utils;
using MyFinance.Core;

namespace MyFinance.Business
{
    public interface ISystemConfigBusiness : IBusinessBase
    {
        SystemConfigModel AddOrUpdateConfig(SystemConfigModel data);
        SystemConfigModel GetConfig();
      
        // void SaveCategory();
    }
    public class SystemConfigBusiness : BusinessBase, ISystemConfigBusiness
    {
       
      
        private readonly IUnitOfWork unitOfWork;
        public SystemConfigBusiness(IUnitOfWork unitOfWork)
        {
           
            this.unitOfWork = unitOfWork;
          
        }


        public SystemConfigModel AddOrUpdateConfig(SystemConfigModel data)
        {
            data.HotelId = WorkContext.BizKasaContext.HotelId;
            var systemConfigRepository = unitOfWork.Repository<HotelConfig>();
            var config = systemConfigRepository.GetMany(a => a.HotelId == data.HotelId).FirstOrDefault();
            if (config != null)
            {
                config.StartOverNight = data.StartOverNight;
                config.TimeCheckOut = data.TimeCheckOut;
                config.TimeRound = data.TimeRound;
                config.EndOverNight = data.EndOverNight;
                config.OverCustomer = data.OverCustomer;
                config.TimeCheckIn = data.TimeCheckIn;
                config.HasCleaner = data.HasCleaner;
                config.RoomStatusColor = data.RoomStatusColor;
                systemConfigRepository.Update(config);
            }
            else
            {
                var row = new HotelConfig() {
                    EndOverNight=data.EndOverNight,
                    OverCustomer=data.OverCustomer,
                    HotelId=data.HotelId,
                    StartOverNight=data.StartOverNight,
                    TimeCheckOut=data.TimeCheckOut,
                    TimeCheckIn=data.TimeCheckIn,
                    TimeRound=data.TimeRound };
                systemConfigRepository.Add(row);
            }
           
                
            unitOfWork.Commit();

            return data;
        }


        public SystemConfigModel GetConfig()
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var systemConfigRepository = unitOfWork.Repository<HotelConfig>();
            var config = systemConfigRepository.GetMany(a => a.HotelId == hotelId).FirstOrDefault();
            if (config == null) return new SystemConfigModel() { };
            return new SystemConfigModel() {
                OverCustomer=config.OverCustomer,
                EndOverNight=config.EndOverNight,
                HotelId=config.HotelId,
                StartOverNight=config.StartOverNight,
                TimeCheckOut=config.TimeCheckOut,
                TimeCheckIn=config.TimeCheckIn,
                HasCleaner=config.HasCleaner,
                RoomStatusColor=config.RoomStatusColor,
                TimeRound=config.TimeRound};
        }
       

    }
}
