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
    public interface IWidgetProxyService : IBusinessBase
    {
        List<WidgetRowResultModel> GetWidgetBy();
        WidgetRowModel GetWidgetById(int Id);
        List<WidgetGroupRowModel> GetGroupWidgetBy();
        List<WidgetRowResultModel> AddWidget(WidgetRowModel data);
        List<WidgetRowResultModel> GetWidgetForRecept();
        bool DeleteGroupWidget(int Id);
        bool DeleteWidget(List<int> Ids);
        List<WidgetGroupRowModel> AddGroupWidget(WidgetGroupRowModel data);
        #region interface UtilityInside
        UtilityModel GetUtilityForEdit(int Id);
        bool AddOrUpdateUtility(UtilityModel model);
        bool AddOrUpdateUtilityGroup(UtilityGroupModel model);
        List<UtilityGroupModel> GetUtilities();
        List<UtilityGroupModel> GetUtilityGroups();
        #endregion
    }

    public class WidgetProxyService : BaseProxyService, IWidgetProxyService
    {
        #region UtilityInside

        public UtilityModel GetUtilityForEdit(int Id)
        {
            string url = "api/Inside/Utility/GetUtilityForEdit";
            return PostService<UtilityModel>(new { Id = Id }, url);
        }


        public bool AddOrUpdateUtility(UtilityModel model)
        {
            string url = "api/Inside/Utility/AddOrUpdateUtility";
            return PostStructService<bool>(model, url);
        }


        public bool AddOrUpdateUtilityGroup(UtilityGroupModel model)
        {
            string url = "api/Inside/Utility/AddOrUpdateUtilityGroup";
            return PostStructService<bool>(model, url);
        }

        public List<UtilityGroupModel> GetUtilities()
        {
            string url = "api/Inside/Utility/GetUtilities";
            return GetDataService<List<UtilityGroupModel>>( url);

        }
       
        public List<UtilityGroupModel> GetUtilityGroups()
        {
            string url = "api/Inside/Utility/GetUtilityGroups";
            return GetDataService<List<UtilityGroupModel>>( url);
        }
         #endregion
        public List<WidgetGroupRowModel> AddGroupWidget(WidgetGroupRowModel data)
        {
            string url = "api/Widget/AddGroupWidget";
            return PostService<List<WidgetGroupRowModel>>(data, url);
        }
        public bool DeleteWidget(List<int> Ids)
        {
            string url = "api/Widget/DeleteWidget";
            return PostStructService<bool>(new { Ids = Ids }, url);
        }
        public bool DeleteGroupWidget(int Id)
        {
            string url = "api/Widget/DeleteGroupWidget";
            return PostStructService<bool>(new { Id=Id}, url);
        }

        public List<WidgetRowResultModel> GetWidgetForRecept()
        {
            string url = "api/Widget/GetWidgetForRecept";
            return GetDataService<List<WidgetRowResultModel>>( url);

        }
        public List<WidgetRowResultModel> AddWidget(WidgetRowModel model)
        {
            string url = "api/Widget/AddWidget";
            return PostService<List<WidgetRowResultModel>>(model,url);

        }
        public List<WidgetGroupRowModel> GetGroupWidgetBy()
        {
            string url = "api/Widget/GetGroupWidget";
            return GetDataService<List<WidgetGroupRowModel>>(url);

        }
        public WidgetRowModel GetWidgetById(int Id)
        {
            string url = "api/Widget/GetWidgetById";
            return PostService<WidgetRowModel>(new { Id = Id },url);

        }
        public List<WidgetRowResultModel> GetWidgetBy()
        {
            string url = "api/Widget/GetWidget";
            return GetDataService<List<WidgetRowResultModel>>( url);

            

        }

        
    }
}
