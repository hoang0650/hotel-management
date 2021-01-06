using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFinance.Domain;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;

namespace MyFinance.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        void CreateCategory(Category category);
        void DeleteCategory(int id);
        List<Category> GetCategoryByType(int type);
        List<Category> CreateCategory(CategoryModel category);
        void SaveCategory();
    }
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;
        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }  
        #region ICategoryService Members

        public IEnumerable<Category> GetCategories()
        {
            var categories = categoryRepository.GetAll();
            return categories;
        }

        public Category GetCategory(int id)
        {
            var category = categoryRepository.GetById(id);
            return category;
        }

        public List<Category> GetCategoryByType(int type)
        {
            var category = categoryRepository.GetCateByType(type);
            return category;
        }

        public void CreateCategory(Category category)
        {
            categoryRepository.Add(category);
            SaveCategory();
        }

        public List<Category> CreateCategory(CategoryModel category)
        {
            var data=new Category(){
                CategoryType=(int)category.CategoryType,
                HotelId=category.HotelId,
                Name=category.Name
            };
            categoryRepository.Add(data);
            SaveCategory();
            var result = categoryRepository.GetCateByType((int)category.CategoryType);
            return result;
        }

        public void DeleteCategory(int id)
        {
            var category = categoryRepository.GetById(id);
            categoryRepository.Delete(category);
            SaveCategory();
        }

        public void SaveCategory()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}
