using MyFinance.Core;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using MyFinance.Extention;
using System.Collections.Generic;
using System.Linq;

namespace MyFinance.Business.Inside
{
    public interface IInsideUtilityBusiness : IBusinessBase {
        bool AddOrUpdateUtilityGroup(UtilityGroupModel model);
        bool AddOrUpdateUtility(UtilityModel model);
        List<UtilityGroupModel> GetUtilities();
        List<UtilityGroupModel> GetUtilityGroups();
        UtilityModel GetUtilityForEdit(int Id);
    }
    public class InsideUtilityBusiness : BusinessBase, IInsideUtilityBusiness
    {
  
        private readonly IUnitOfWork unitOfWork;
        public InsideUtilityBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public bool AddOrUpdateUtilityGroup(UtilityGroupModel model)
        {
            var groupRepo=unitOfWork.Repository<UtilityGroup>();
            if (model.Id > 0)
            {
                var group = groupRepo.GetById(model.Id);
                group.Name = model.Name;
                group.IsDeleted = model.IsDeleted;
                groupRepo.Update(group);
            }
            else
            {
                var data = new UtilityGroup() {
                    Name=model.Name,
                IsDeleted=model.IsDeleted
                };
                groupRepo.Add(data);
            }
            IoC.Get<IUnitOfWork>().Commit();
            return !this.HasError;
        }

        public bool AddOrUpdateUtility(UtilityModel model)
        {
            var UtilRepo = unitOfWork.Repository<Utility>();
            if (model.Id > 0)
            {
                var util = UtilRepo.GetById(model.Id);
                util.Name = model.Name;
                util.IsDeleted = model.IsDeleted;
                util.GroupId = model.GroupId;
                util.InputType = model.InputType;
                util.UtilityType = model.UtilityType;
                UtilRepo.Update(util);
            }
            else
            {
                var data = new Utility()
                {
                    Name = model.Name,
                    IsDeleted = model.IsDeleted,
                    UtilityType=model.UtilityType,
                    GroupId=model.GroupId,
                    InputType=model.InputType
                };
                UtilRepo.Add(data);
            }
            IoC.Get<IUnitOfWork>().Commit();
            return !this.HasError;
        }

        public UtilityModel GetUtilityForEdit(int Id)
        {
            var result = new UtilityModel() { };
            var UtilRepo = unitOfWork.Repository<Utility>();
            if (Id > 0)
            {
                var util = UtilRepo.GetById(Id);
                result.UtilityType = util.UtilityType;
                result.GroupId = util.GroupId;
                result.Id=util.Id;
                result.Name=util.Name;
                result.IsDeleted = util.IsDeleted;
                
            }
            return result;
        }

      
        public List<UtilityGroupModel> GetUtilities()
        {
            List<UtilityGroupModel> result = new List<UtilityGroupModel>();
            var groups = unitOfWork.Repository<UtilityGroup>().GetAll().ToList();
            if(groups.Any())
            {
                foreach (var item in groups)
                {
                    var util = unitOfWork.Repository<Utility>().GetMany(a => a.GroupId == item.Id).ToList();
                    var utilities = AutoMapper.Mapper.Map<List<UtilityModel>>(util);
                    result.Add(new UtilityGroupModel() {
                        Id=item.Id
                        ,IsDeleted=item.IsDeleted
                    ,Name=item.Name
                    ,Utilities = utilities
                    });
                }
            }
            return result;
        }

        public List<UtilityGroupModel> GetUtilityGroups()
        {
            List<UtilityGroupModel> result = new List<UtilityGroupModel>();
            var groups = unitOfWork.Repository<UtilityGroup>().GetAll().ToList();
            if (groups.Any())
            {
                foreach (var item in groups)
                {
                    var util = unitOfWork.Repository<Utility>().GetMany(a => a.GroupId == item.Id).ToList();
                    var utilities = AutoMapper.Mapper.Map<List<UtilityModel>>(util);
                    result.Add(new UtilityGroupModel()
                    {
                        Id = item.Id
                        ,
                        IsDeleted = item.IsDeleted
                        ,
                        Name = item.Name
                        });
                }
            }
            return result;
        }
    }
}
