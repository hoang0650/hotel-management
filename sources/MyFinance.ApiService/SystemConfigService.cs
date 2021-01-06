using MyFinance.Domain.BusinessModel;
using MyFinance.Utils;
using MyFinance.Core;
using MyFinance.Business;
using MyFinance.Extention;

namespace MyFinance.ApiService
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
                result = IoC.Get<ISystemConfigBusiness>().GetConfig();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<SystemConfigModel> AddOrUpdateConfig(SystemConfigModel data)
        {
            SystemConfigModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ISystemConfigBusiness>().AddOrUpdateConfig(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        

    }
}
