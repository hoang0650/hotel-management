using MyFinance.Business.Bizkasa.Inside;
using MyFinance.Business.Inside;
using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Extention;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ApiService.Inside
{
    public interface IInsideUtilityService {
        Response<List<UtilityGroupModel>> GetUtilities();
        Response<List<UtilityGroupModel>> GetUtilityGroups();
        Response<bool> AddOrUpdateUtilityGroup(UtilityGroupModel model);
        Response<bool> AddOrUpdateUtility(UtilityModel model);
        Response<UtilityModel> GetUtilityForEdit(int Id);
    }
    public partial class InsideService : IInsideUtilityService
    {
        public Response<UtilityModel> GetUtilityForEdit(int Id)
        {
            UtilityModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInsideUtilityBusiness>().GetUtilityForEdit(Id);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response< bool >AddOrUpdateUtility(UtilityModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInsideUtilityBusiness>().AddOrUpdateUtility(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> AddOrUpdateUtilityGroup(UtilityGroupModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInsideUtilityBusiness>().AddOrUpdateUtilityGroup(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<UtilityGroupModel>> GetUtilities()
        {
            List<UtilityGroupModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInsideUtilityBusiness>().GetUtilities();
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<List<UtilityGroupModel>> GetUtilityGroups()
        {
            List<UtilityGroupModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInsideUtilityBusiness>().GetUtilityGroups();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
    }
}
