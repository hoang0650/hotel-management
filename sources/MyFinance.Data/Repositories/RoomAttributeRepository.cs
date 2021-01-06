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
    public interface IRoomAttributeRepository : IRepository<RoomAttribute>
    {
        List<RoomAttribute> GetRoomAttributeBy(int roomid);
    }
    public class RoomAttributeRepository : RepositoryBase<RoomAttribute>, IRoomAttributeRepository
    {
        public RoomAttributeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public List<RoomAttribute> GetRoomAttributeBy(int configId)
        {
            return GetMany(a => a.ConfigPriceId == configId).ToList();
        }

    }
   
}