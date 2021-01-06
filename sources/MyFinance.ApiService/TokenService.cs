using MyFinance.Business;
using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Extention;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ApiService
{
    public interface ITokenService
    {
        Response<TokenModel> GenerateToken(int userId);
        Response<bool> DeleteByUserId(int userId);
        Response<bool> Kill(string tokenId);
        Response<bool> ValidateToken(string tokenId);
        Response<string> CheckExistStoreToken(string storeName);
        Response<bool> AddStoreToken(StoreTokenModel model);
    }
    public partial class TikasaService
    {

        public Response<bool> AddStoreToken(StoreTokenModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ITokenBusiness>().AddStoreToken(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<string> CheckExistStoreToken(string storeName)
        {
            string result = string.Empty;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ITokenBusiness>().CheckExistStoreToken(storeName);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> ValidateToken(string tokenId)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ITokenBusiness>().ValidateToken(tokenId);
            });

            return BusinessProcess.Current.ToResponse(result);

        }
        public Response<bool> Kill(string tokenId)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ITokenBusiness>().Kill(tokenId);
            });

            return BusinessProcess.Current.ToResponse(result);

        }


        public Response<bool> DeleteByUserId(int userId)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ITokenBusiness>().DeleteByUserId(userId);
            });

            return BusinessProcess.Current.ToResponse(result);

        }
        public Response<TokenModel> GenerateToken(int userId)
        {
            TokenModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ITokenBusiness>().GenerateToken(userId);
            });

            return BusinessProcess.Current.ToResponse(result);

        }
    }
}
