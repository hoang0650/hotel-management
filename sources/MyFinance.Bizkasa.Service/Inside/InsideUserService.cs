using AutoMapper;
using MyFinance.Domain;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFinance.Utils;
using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Proxy;
namespace MyFinance.Bizkasa.Service.Inside
{


    public interface IInsideUserService
    {
        Response<bool> AddUser(UserViewModel model);
        Response<List<UserViewModel>> GetallBy();
        Response<bool> MappingUserHotel(int hotelid, int userid);
        Response<UserLoginViewModel> Login(string username, string password);
        Response<List<UserViewModel>> GetUserByHotel(int hotelId);
        Response<bool> DeleteUsers(List<int> Ids);
        Response<UserViewModel> GetUserForEdit(int userId);
        Response<bool> CheckUserExist(string username);
        Response<DataPaging<List<UserViewModel>>> GetUsers(InvoiceFilterModel filter);
    }
    public partial class InsideService 
    {
        public Response<DataPaging< List<UserViewModel>>> GetUsers(InvoiceFilterModel filter)
        {
            List<UserViewModel> result = null;
            int total = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().GetUsers(filter, out total);
            });

            return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
        }
        public Response<bool> CheckUserExist(string username)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().CheckUserExistInside(username);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> AddUser(UserViewModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().AddUser(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<bool> MappingUserHotel(int hotelid, int userid)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().MappingUserHotel(hotelid, userid);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<UserLoginViewModel> Login(string username, string password)
        {
            UserLoginViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().Login(username, password);
            });
            return BusinessProcess.Current.ToResponse(result);
        }
        

        public Response< List<UserViewModel>> GetallBy()
        {
            List<UserViewModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().GetallBy();
            });
            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<List<UserViewModel>> GetUserByHotel(int hotelId)
        {
            List<UserViewModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().GetUserByHotel(hotelId);
            });
            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<UserViewModel> GetUserForEdit(int userId)
        {
            UserViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().GetUserForEdit(userId);
            });
            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<bool> DeleteUsers(List<int> Ids)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().DeleteUser(Ids);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
    }
}
