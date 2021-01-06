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
    public interface IRoomClassRepository : IRepository<RoomClass>
    {
        List<RoomClass> GetRoomClassBy(int hoteid);
    }
    public class RoomClassRepository : RepositoryBase<RoomClass>, IRoomClassRepository
    {
        public RoomClassRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public List<RoomClass> GetRoomClassBy(int hoteid)
        {
            return  GetMany(a => a.HotelId == hoteid&&!a.IsDeleted).ToList();
        }

    }
   
}