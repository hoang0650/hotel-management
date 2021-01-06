using MyFinance.Domain.BusinessModel;
using System.Collections.Generic;
using MyFinance.Utils;
using MyFinance.Core;
using MyFinance.Business;
using MyFinance.Extention;
namespace MyFinance.ApiService
{


    public interface IUserService
    {
        Response<bool> AddUser(UserViewModel model);
        Response<List<UserViewModel>> GetallBy();
        Response<bool> MappingUserHotel(int hotelid, int userid);
        Response<UserLoginViewModel> Login(RequestLogin model);
        Response<List<UserViewModel>> GetUserByHotel(int hotelId);
        Response<bool> DeleteUsers(List<int> Ids);
        Response<UserViewModel> GetUserForEdit(int userId);
        Response<bool> CheckUserExist(string username);
        Response<UserLoginViewModel> Relogin(string email);
        Response<UserLoginViewModel> LoginAsHotel(int hotelId);
       int Authenticate(string userName, string password);
       Response<UserLoginViewModel> LoginTicket(string username, string password);
        Response<bool> Logout();
        Response<List<UserViewModel>> GetAdminByHotel(int hotelId);
    }
    public partial class TikasaService 
    {
        public Response<List<UserViewModel>> GetAdminByHotel(int hotelId)
        {
            List<UserViewModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().GetAdminByHotel(hotelId);
            });
            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> Logout()
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().Logout();
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<UserLoginViewModel> LoginTicket(string username, string password)
        {
            UserLoginViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().LoginTicket(username, password);
            });
            return BusinessProcess.Current.ToResponse(result);
        }
        public int Authenticate(string userName, string password)
        {
            return IoC.Get<IUserBusiness>().Authenticate(userName, password);
        }


        public Response<UserLoginViewModel> Relogin(string email)
        {
            UserLoginViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().Relogin(email);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> CheckUserExist(string username)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().CheckUserExist(username);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> AddUser(UserViewModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().AddUser(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<bool> MappingUserHotel(int hotelid, int userid)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().MappingUserHotel(hotelid, userid);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<UserLoginViewModel> Login(RequestLogin model)
        {
            UserLoginViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().Login(model);
            });
            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<UserLoginViewModel> LoginAsHotel(int hotelId)
        {
            UserLoginViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().LoginAsHotel(hotelId);
            });
            return BusinessProcess.Current.ToResponse(result);
        }
        

        public Response< List<UserViewModel>> GetallBy()
        {
            List<UserViewModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().GetallBy();
            });
            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<List<UserViewModel>> GetUserByHotel(int hotelId)
        {
            List<UserViewModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().GetUserByHotel(hotelId);
            });
            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<UserViewModel> GetUserForEdit(int userId)
        {
            UserViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().GetUserForEdit(userId);
            });
            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<bool> DeleteUsers(List<int> Ids)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IUserBusiness>().DeleteUser(Ids);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
    }
}
