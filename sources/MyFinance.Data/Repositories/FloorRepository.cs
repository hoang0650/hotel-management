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
    public interface IFloorRepository : IRepository<Floor>
    {
        List<Floor> GetFloorBy(int hoteid);
    }
    public class FloorRepository : RepositoryBase<Floor>, IFloorRepository
    {
        public FloorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public List<Floor> GetFloorBy(int hoteid)
        {
            return  GetMany(a => a.HotelId == hoteid && !a.IsDeleted).ToList();
        }

    }
   
} 