using AutoMapper;
using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Proxy
{
    public interface IUserProxyService : IBusinessBase
    {
        UserLoginViewModel LoginAPI(string username, string password);
        List<UserViewModel> GetUserByHotel();
        UserViewModel GetUserForEdit(int userId);
        bool AddUser(UserViewModel model);
        UserLoginViewModel Relogin(string Email);
        

        #region Interface Inside
        List<UserViewModel> GetUsers(InvoiceFilterModel filter, out int total);
        bool DeleteUser(List<int> Ids);
        bool CheckUserExistInside(string username);
        bool MappingUserHotel(int hotelid, int userid);
        UserLoginViewModel Login(string username, string password);
        List<UserViewModel> GetallBy();
        List<UserViewModel> GetUserByHotel(int hotelId);
        bool CheckUserExist(string username);
        UserLoginViewModel LoginAsHotel(int hotelId);
        #endregion
    }
    public class UserProxyService : BaseProxyService, IUserProxyService
    {
        #region Bizkasa


        public UserLoginViewModel Relogin(string Email)
        {
            string url = "api/Account/Relogin";
            return PostNonTokenService<UserLoginViewModel>(new { Email = Email }, url);

          
        }

        public bool AddUser(UserViewModel model)
        {
            string url = "api/Account/AddUser";
            return PostStructService<bool>(model, url);

        }
        public UserViewModel GetUserForEdit(int userId)
        {
            string url = "api/Account/GetUserForEdit";
            return PostService<UserViewModel>(new { UserId = userId }, url);

        }
        public bool CheckUserExist(string username)
        {
            string url = "api/Account/CheckUserExist";
            return PostNonTokenStructService<bool>(new { UserName = username }, url);
        }
        public List<UserViewModel> GetUserByHotel()
        {

            string url = "api/Account/GetUserByHotel";
            return GetDataService<List<UserViewModel>>( url);

        }
        public UserLoginViewModel LoginAPI(string username, string password)
        {

            string url = "api/Account/Login";
            return PostNonTokenService<UserLoginViewModel>(new { Email = username, Password = password }, url);

        }

        #endregion
        #region Inside
        public List<UserViewModel> GetUserByHotel(int hotelId)
        {
            string url = "api/Inside/User/GetUserByHotel";
            return PostService<List<UserViewModel>>(new { HotelId = hotelId }, url);
        }
        public List<UserViewModel> GetallBy()
        {
            string url = "api/Inside/User/GetallBy";
            return GetDataService<List<UserViewModel>>(url);
        }
        public UserLoginViewModel Login(string username, string password)
        {
            string url = "api/Account/InsideLogin";
            return PostNonTokenService<UserLoginViewModel>(new { Email = username, Password = password }, url);
        }

        public UserLoginViewModel LoginAsHotel(int hotelId)
        {
            string url = "api/Account/LoginAsHotel";
            return PostNonTokenService<UserLoginViewModel>(new { HotelId = hotelId }, url);
        }
        public bool MappingUserHotel(int hotelid, int userid)
        {
            string url = "api/Inside/User/MappingUserHotel";
            return PostStructService<bool>(new { HotelId = hotelid,UserId=userid }, url);
        }
        public bool CheckUserExistInside(string username)
        {
            string url = "api/Inside/User/CheckUserExist";
            return PostStructService<bool>(new { UserName = username }, url);
        }
        public List<UserViewModel> GetUsers(InvoiceFilterModel filter, out int total)
        {
            string url = "api/Inside/User/GetUsers";
            return PostOutTotalService<List<UserViewModel>>(filter, url,out total);
        }

        public bool DeleteUser(List<int> Ids)
        {
            string url = "api/Inside/User/DeleteUsers";
            return PostStructService<bool>(new {Ids=Ids}, url);
        }
        #endregion
    }
}
