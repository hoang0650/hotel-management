using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFinance.Domain;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain.Enum;
using MyFinance.Domain.Entities;
namespace MyFinance.Data
{
    public interface IWidgetRepository : IRepository<Widget>
    {
        List<Widget> GetAllWidgetsBy(int hotelid);
    }
    public class WidgetRepository : RepositoryBase<Widget>, IWidgetRepository
    {
        public WidgetRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public List<Widget> GetAllWidgetsBy(int hotelid)
        {
            return GetMany(a=>a.HotelId==hotelid).ToList();
        }
    }
   
}