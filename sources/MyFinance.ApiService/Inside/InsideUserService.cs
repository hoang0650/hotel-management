using MyFinance.Domain.BusinessModel;
using System.Collections.Generic;
using MyFinance.Utils;
using MyFinance.Core;
using MyFinance.Business;
using MyFinance.Extention;
using MyFinance.Business.Inside;
namespace MyFinance.ApiService.Inside
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
                result = IoC.Get<IInsideUserBusiness>().GetUsers(filter, out total);
            });

            return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
        }
        public Response<bool> CheckUserExist(string username)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInsideUserBusiness>().CheckUserExist(username);
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
        public Response<UserLoginViewModel> Login(string username, string password)
        {
            UserLoginViewModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInsideUserBusiness>().Login(username, password);
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
