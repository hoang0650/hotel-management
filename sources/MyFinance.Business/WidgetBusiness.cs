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
using MyFinance.Core;
using MyFinance.Utils;
using MyFinance.Extention;

namespace MyFinance.Business
{
    public interface IWidgetBusiness : IBusinessBase
    {

        List<WidgetGroupRowModel> GetGroupWidgetBy();
        List<WidgetGroupRowModel> AddGroupWidget(WidgetGroupRowModel data);
        List<WidgetRowResultModel> GetWidgetBy();
        List<WidgetRowResultModel> AddWidget(WidgetRowModel data);
        bool DeleteGroupWidget(int Id);
        bool DeleteWidget(List<int> Ids);
        WidgetRowModel GetWidgetById(int Id);
        List<WidgetRowResultModel> GetWidgetForRecept();
       
    }
    public class WidgetBusiness :BusinessBase, IWidgetBusiness
    {
       
        private readonly IUnitOfWork unitOfWork;
        public WidgetBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
           
        }





        public List<WidgetGroupRowModel> GetGroupWidgetBy()
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var widgetgroupRepository = unitOfWork.Repository<GroupWidget>();
            List<WidgetGroupRowModel> result = new List<WidgetGroupRowModel>();
            var group = widgetgroupRepository.GetMany(a => a.HotelId == hotelId && !a.IsDeteled).ToList(); 
            if (group.Any())
            {
                foreach (var item in group)
                {
                    var map = AutoMapper.Mapper.Map<WidgetGroupRowModel>(item);                   
                    result.Add(map);

                }
            }
            return result;
        }


        public List<WidgetGroupRowModel> AddGroupWidget(WidgetGroupRowModel data)
        {
            var widgetgroupRepository = unitOfWork.Repository<GroupWidget>();
            data.HotelId = WorkContext.BizKasaContext.HotelId;
            List<WidgetGroupRowModel> result = new List<WidgetGroupRowModel>();
            if (data.Id > 0)
            {
                var roomcurrent = widgetgroupRepository.GetById(data.Id);
                roomcurrent.Name = data.Name;
                roomcurrent.Note = data.Note;
                roomcurrent.HotelId = data.HotelId;
                roomcurrent.UpdatedDate = DateTime.Now;
              
                widgetgroupRepository.Update(roomcurrent);

            }
            else
            {
                var model = new GroupWidget()
                {
                    Name = data.Name,
                    Note = data.Note,
                    HotelId = data.HotelId,
                     UpdatedDate = DateTime.Now,
                       CreatedDate = DateTime.Now
                };

                widgetgroupRepository.Add(model);
            }
            unitOfWork.Commit();
            result = GetGroupWidgetBy();

            return result;
        }


        public List<WidgetRowResultModel> AddWidget(WidgetRowModel data)
        {
            var widgetRepository = unitOfWork.Repository<Widget>();
            if (data == null) { this.AddError("Dữ liệu rỗng !"); return null; }
            data.HotelId = WorkContext.BizKasaContext.HotelId;
            List<WidgetRowResultModel> result = new List<WidgetRowResultModel>();
            if (data.Id > 0)
            {
                var roomcurrent = widgetRepository.GetById(data.Id);
                roomcurrent.Name = data.Name;
                roomcurrent.Note = data.Note;
                roomcurrent.HotelId = data.HotelId;
                roomcurrent.UpdatedDate = DateTime.Now;
                roomcurrent.PricePaid = data.PricePaid;
                roomcurrent.GroupId = data.GroupId;
                roomcurrent.IsRecept = data.IsRecept;
                roomcurrent.Price = data.Price;               

                widgetRepository.Update(roomcurrent);

            }
            else
            {
                var model = new Widget()
                {
                    Name = data.Name,
                    Note = data.Note,
                    HotelId = data.HotelId,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    GroupId=data.GroupId,
                    PricePaid = data.PricePaid,
                    NumExport=0,
                    NumImport=0,
                    IsRecept=data.IsRecept,
                    Residual=0,
                    Price=data.Price
                };

                widgetRepository.Add(model);
            }
            unitOfWork.Commit();
            result = GetWidgetBy();

            return result;
        }

        public List<WidgetRowResultModel> GetWidgetBy()
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            List<WidgetRowResultModel> result = new List<WidgetRowResultModel>();
            var group = unitOfWork.Repository<GroupWidget>().GetMany(a => a.HotelId == hotelId&& !a.IsDeteled).ToList();            
            if (group.Any())
            {
                foreach (var item in group)
                {
                    var row = new WidgetRowResultModel() {GroupId=item.Id,GroupName=item.Name};
                    var widgets = unitOfWork.Repository<Widget>().GetMany(a => a.GroupId == item.Id && !a.IsDeteled).ToList();
                    var map = AutoMapper.Mapper.Map<List<Widget>, List<WidgetRowModel>>(widgets);
                    row.Widgets = map;
                    result.Add(row);
                }
            }
            return result;
        }

        public List<WidgetRowResultModel> GetWidgetForRecept()
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            List<WidgetRowResultModel> result = new List<WidgetRowResultModel>();
            var group = unitOfWork.Repository<GroupWidget>().GetMany(a => a.HotelId == hotelId && !a.IsDeteled).ToList();
            if (group.Any())
            {
               
                foreach (var item in group)
                {
                    var widgets = unitOfWork.Repository<Widget>().GetMany(a => a.GroupId == item.Id && !a.IsDeteled && a.IsRecept).ToList();
                    if (widgets!=null)
                     {
                         var row = new WidgetRowResultModel() { GroupId = item.Id, GroupName = item.Name };
                         var map = AutoMapper.Mapper.Map<List<Widget>, List<WidgetRowModel>>(widgets);
                         row.Widgets = map;
                         result.Add(row);
                     }
                }
            }
            return result;
        }


        public WidgetRowModel GetWidgetById(int Id)
        {
            var widget = unitOfWork.Repository<Widget>().GetMany(a => a.Id == Id && !a.IsDeteled).FirstOrDefault();
            var map = AutoMapper.Mapper.Map<Widget, WidgetRowModel>(widget);
            return map;
        }


        public bool DeleteGroupWidget(int Id)
        {
            var groupRepo=unitOfWork.Repository<GroupWidget>();
            var group = groupRepo.GetById(Id);
            bool widget = unitOfWork.Repository<Widget>().GetMany(a => a.GroupId == Id).Any();
            if (widget)
            {
                group.IsDeteled = true;
                groupRepo.Update(group);
            }
            else
            {
                groupRepo.Delete(group);
            }               
          
            unitOfWork.Commit();
            return !this.HasError;
        }

        public bool DeleteWidget(List<int> Ids)
        {
            if (!Ids.Any()) {
                AddError("Bạn chưa chọn dịch vụ cần xóa !");
                return false;
            }

            var widget = unitOfWork.Repository<Widget>().GetMany(a=>Ids.Contains(a.Id)).ToList();
            if (widget.Any())
            {
                foreach (var item in widget)
                {
                    item.IsDeteled=true;
                    unitOfWork.Repository<Widget>().Update(item);
                }
            }
            unitOfWork.Commit();
            return !this.HasError;
        }

    
    }
}
