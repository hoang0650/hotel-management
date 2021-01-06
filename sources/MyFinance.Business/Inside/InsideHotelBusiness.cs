using MyFinance.Core;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain;
using MyFinance.Domain.Entities;
using MyFinance.Extention;
using MyFinance.Utils;
using System;

namespace MyFinance.Business.Bizkasa.Inside
{
    public interface IInsideHotelBusiness : IBusinessBase
    {
        bool DisableHotel(int hotelId);
    }
    public class InsideHotelBusiness : BusinessBase,IInsideHotelBusiness
    {
       
        private readonly IUnitOfWork unitOfWork;
        public InsideHotelBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public bool DisableHotel(int hotelId)
        {
            if(hotelId<=0)
            {
                this.AddError("Chưa chọn khách sạn !");
                return false;
            }
            var hotelRepo=unitOfWork.Repository<Hotel>();
            var hotel = hotelRepo.GetById(hotelId);
            hotel.IsActive = hotel.IsActive?false:true;
            hotel.LastUpdate = DateTime.Now;
            hotel.UpdateByUser = WorkContext.BizKasaContext.UserName;
            hotelRepo.Update(hotel);

            var userRepo = unitOfWork.Repository<User>();
            var users = userRepo.GetMany(a => a.HotelId == hotelId);
            foreach (var item in users)
            {
                item.IsActive = hotel.IsActive ? true : false;
                userRepo.Update(item);
            }
            IoC.Get<IUnitOfWork>().Commit();
            return !this.HasError;
        }
    }
}
