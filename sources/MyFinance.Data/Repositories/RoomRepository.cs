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
    public interface IRoomRepository : IRepository<Room>
    {
        List<Room> GetRoomBy(int hoteid);
        List<Room> GetRoomByFloor(int floorid);
        List<Room> GetRoomByRoomClass(int roomclassid);
    }
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public List<Room> GetRoomBy(int hoteid)
        {
            return  GetMany(a => a.HotelId == hoteid).ToList();
        }

        public List<Room> GetRoomByFloor(int floorid)
        {
            return GetMany(a => a.FloorId == floorid&&!a.IsDeleted).ToList();
        }

        public List<Room> GetRoomByRoomClass(int roomclassid)
        {
            return GetMany(a => a.RoomClassId == roomclassid && !a.IsDeleted).ToList();
            
        }
    }
   
}