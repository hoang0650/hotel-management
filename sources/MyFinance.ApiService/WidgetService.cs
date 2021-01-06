using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFinance.Domain;
using MyFinance.Data;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Domain.Entities;
using MyFinance.Business;
using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Utils;

namespace MyFinance.ApiService
{
    public interface IWidgetService
    {

         Response<List<WidgetGroupRowModel>> GetGroupWidgetBy();
         Response<List<WidgetGroupRowModel>> AddGroupWidget(WidgetGroupRowModel data);
         Response<List<WidgetRowResultModel>> GetWidgetBy();
         Response<List<WidgetRowResultModel>> AddWidget(WidgetRowModel data);
         Response<bool> DeleteGroupWidget(int id);
         Response<bool> DeleteWidget(List<int> Ids);
        Response<WidgetRowModel> GetWidgetById(int Id);
        Response<List<WidgetRowResultModel>> GetWidgetForRecept();
    }
    public partial class TikasaService 
    {
       

        public Response<List<WidgetGroupRowModel>> GetGroupWidgetBy()
        {
            List<WidgetGroupRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWidgetBusiness>().GetGroupWidgetBy();
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response< List<WidgetGroupRowModel>>AddGroupWidget(WidgetGroupRowModel data)
        {
            List<WidgetGroupRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWidgetBusiness>().AddGroupWidget(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<List<WidgetRowResultModel>> AddWidget(WidgetRowModel data)
        {
            List<WidgetRowResultModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWidgetBusiness>().AddWidget(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<List<WidgetRowResultModel>> GetWidgetBy()
        {
            List<WidgetRowResultModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWidgetBusiness>().GetWidgetBy();
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<bool> DeleteGroupWidget(int id)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWidgetBusiness>().DeleteGroupWidget(id);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<bool> DeleteWidget(List<int> Ids)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWidgetBusiness>().DeleteWidget(Ids);
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<WidgetRowModel> GetWidgetById(int Id)
        {
            WidgetRowModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWidgetBusiness>().GetWidgetById(Id);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<List<WidgetRowResultModel>> GetWidgetForRecept()
        {
            List<WidgetRowResultModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IWidgetBusiness>().GetWidgetForRecept();
            });

            return BusinessProcess.Current.ToResponse(result);
        }

    }
}
