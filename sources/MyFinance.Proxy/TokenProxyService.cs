using MyFinance.Core;
using MyFinance.Domain.BusinessModel;

namespace MyFinance.Proxy
{
    public interface ITokenProxyService : IBusinessBase {
        string CheckStoreToken(string StoreName);
        bool AddStoreToken(StoreTokenModel Model);
        StoreTokenModel GetStoreToken(StoreTokenModel Model);
        bool CheckValidToken(StoreTokenModel Model);
    }
    public class TokenProxyService : BaseProxyService, ITokenProxyService
    {
        public string CheckStoreToken(string StoreName)
        {
            string url = "api/Account/CheckStoreToken";
            return PostNonTokenService<string>(new { StoreName = StoreName }, url);


        }

        public bool AddStoreToken(StoreTokenModel Model)
        {
            string url = "api/Account/AddStoreToken";
            return PostNonTokenStructService<bool>(Model, url);


        }

        public StoreTokenModel GetStoreToken(StoreTokenModel Model)
        {
            string url = "https://"+Model.StoreName+"/admin/oauth/access_token";
            return PostExternalService( Model.Code , url);
        }

        public bool CheckValidToken(StoreTokenModel Model)
        {
            string url = "https://" + Model.StoreName + "/admin/store.json";
            return CheckValidTokenService(Model.access_token, url);
        }
    }
}
