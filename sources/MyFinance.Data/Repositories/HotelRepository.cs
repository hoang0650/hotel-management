using MyFinance.Data.Infrastructure;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFinance.Data.Repositories
{
    public interface IHotelRepository : IRepository<Hotel>
    {


        List<Hotel> Gethotelbycate(int cateid);

    }


    public class HotelRepository : RepositoryBase<Hotel>, IHotelRepository
    {
        public HotelRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
            } 

        public List<Hotel> Gethotelbycate(int cateid)
        {
            return GetMany(a => a.Id == cateid).ToList();
        }

    }
}
