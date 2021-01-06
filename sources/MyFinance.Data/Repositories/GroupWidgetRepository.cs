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
    public interface IGroupWidgetRepository : IRepository<GroupWidget>
    {
        List<GroupWidget> GetAllGroupWidgetsBy(int hotelid);
    }
    public class GroupWidgetRepository : RepositoryBase<GroupWidget>, IGroupWidgetRepository
    {
        public GroupWidgetRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public List<GroupWidget> GetAllGroupWidgetsBy(int hotelid)
        {
            return GetMany(a => a.HotelId == hotelid && !a.IsDeteled).ToList();
        }
    }
   
}