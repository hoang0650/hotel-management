using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFinance.Domain;
using MyFinance.Data;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Core;

namespace MyFinance.Business
{
    public interface ICategoryBusiness : IBusinessBase
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        void CreateCategory(Category category);
        void DeleteCategory(int id);
        List<Category> GetCategoryByType(int type);
        List<Category> CreateCategory(CategoryModel category);
        void SaveCategory();
    }
    public class CategoryBusiness : BusinessBase, ICategoryBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        public CategoryBusiness( IUnitOfWork unitOfWork)
        {
            
            this.unitOfWork = unitOfWork;
        }  
        #region ICategoryService Members

        public IEnumerable<Category> GetCategories()
        {
            var categories = unitOfWork.Repository<Category>().GetAll();
            return categories;
        }

        public Category GetCategory(int id)
        {
            var category = unitOfWork.Repository<Category>().GetById(id);
            return category;
        }

        public List<Category> GetCategoryByType(int type)
        {
            var category = unitOfWork.Repository<Category>().GetMany(a => a.CategoryType == type).ToList();
            return category;
        }

        public void CreateCategory(Category category)
        {
            unitOfWork.Repository<Category>().Add(category);
            SaveCategory();
        }

        public List<Category> CreateCategory(CategoryModel category)
        {
            var data=new Category(){
                CategoryType=(int)category.CategoryType,
                HotelId=category.HotelId,
                Name=category.Name
            };
            unitOfWork.Repository<Category>().Add(data);
            SaveCategory();
            var result = unitOfWork.Repository<Category>().GetMany(a => a.CategoryType == (int)category.CategoryType).ToList();
            return result;
        }

        public void DeleteCategory(int id)
        {
            var category = unitOfWork.Repository<Category>().GetById(id);
            unitOfWork.Repository<Category>().Delete(category);
            SaveCategory();
        }

        public void SaveCategory()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}
