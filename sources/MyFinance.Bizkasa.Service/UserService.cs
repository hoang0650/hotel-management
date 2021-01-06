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
namespace MyFinance.Bizkasa.Service
{
    

    public interface IUserService
    {
        Response<bool> AddUser(UserViewModel model);
        Response<UserLoginViewModel> Login(string username, string password);
        Response<List<UserViewModel>> GetUserByHotel();
        Response<bool> DeleteUsers(List<int> Ids);
        Response<UserViewModel> GetUserForEdit(int userId);
        Response<bool> CheckUserExist(string username);
        Response<UserLoginViewModel> Relogin(string email);
        Response<UserLoginViewModel> LoginAsHotel(int hotelid);
    }
    public partial class TikasaService 
    {
        public Response<UserLoginViewModel> Relogin(string email)
        {
            UserLoginViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().Relogin(email);// IoC.Get<IUserBusiness>().Relogin(email);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> CheckUserExist(string username)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().CheckUserExist(username);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> AddUser(UserViewModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().AddUser(model);//IoC.Get<IUserBusiness>().AddUser(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

     
        public Response<UserLoginViewModel> Login(string username, string password)
        {
            UserLoginViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().LoginAPI(username, password);
            });
            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<UserLoginViewModel> LoginAsHotel(int hotelid)
        {
            UserLoginViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().LoginAsHotel(hotelid);
            });
            return BusinessProcess.Current.ToResponse(result);
        }
        

       

        public Response<List<UserViewModel>> GetUserByHotel()
        {
            List<UserViewModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().GetUserByHotel();//IoC.Get<IUserBusiness>().GetUserByHotel(hotelId);
            });
            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<UserViewModel> GetUserForEdit(int userId)
        {
            UserViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserProxyService>().GetUserForEdit(userId);//IoC.Get<IUserBusiness>().GetUserForEdit(userId);
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
