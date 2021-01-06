using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFinance.Domain;

using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Domain.Entities;

using MyFinance.Utils;
using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Proxy;

namespace MyFinance.Bizkasa.Service
{
    public interface ISystemConfigService
    {
        Response<SystemConfigModel> AddOrUpdateConfig(SystemConfigModel data);
        Response<SystemConfigModel> GetConfig();
      
        // void SaveCategory();
    }
    public partial class TikasaService
    {
        public Response<SystemConfigModel> GetConfig()
        {
            SystemConfigModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelProxyService>().GetConfig();//IoC.Get<ISystemConfigBusiness>().GetConfig();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<SystemConfigModel> AddOrUpdateConfig(SystemConfigModel data)
        {
            SystemConfigModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IHotelProxyService>().AddOrUpdateConfig(data);// IoC.Get<ISystemConfigBusiness>().AddOrUpdateConfig(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        

    }
}
