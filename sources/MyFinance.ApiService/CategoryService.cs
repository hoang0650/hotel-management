using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFinance.Domain;
using MyFinance.Data;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;

namespace MyFinance.ApiService
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
   
}
