using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFinance.Domain;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain.Enum;
namespace MyFinance.Data
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public List<Category> GetCateByType(int type)
        {
            return GetMany(a => a.CategoryType == type).ToList();
        }



        public List<Category> GetCateByHotelId(int hotelid)
        {
            return GetMany(a => a.HotelId == hotelid).ToList();
        }
    
        public List<Category> GetCateByHotelId3(int hotelid)
        {
            return GetMany(a => a.HotelId == hotelid).ToList();
        }


        public List<Category> GetCateByHotelId1(int hotelid)
        {
            return GetMany(a => a.HotelId == hotelid).ToList();
        }

    }
    public interface ICategoryRepository : IRepository<Category>
    {
        List<Category> GetCateByType(int type);
        List<Category> GetCateByHotelId(int hotelid);
       List<Category> GetCateByHotelId3(int hotelid);
   List<Category> GetCateByHotelId1(int hotelid);
    }
}