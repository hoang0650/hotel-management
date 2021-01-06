using Bizkasa.Api.Infractstructure;

using MyFinance.Domain.BusinessModel;
using MyFinance.ApiService;
using System.Web.Http;
using MyFinance.Utils;
using MyFinance.Domain.RequestModel;
using MyFinance.ApiService.Inside;
using System.Web.Security;
using System.Web.Http.Cors;

namespace Bizkasa.Api.Controllers
{
    //[ApiAuthenticationFilter]
    [RoutePrefix("api/Account")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private readonly ITikasaService _tokenServices;
        private readonly IInsideService _InsideService;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public AccountController(ITikasaService tokenServices, IInsideService InsideService)
        {
            this._tokenServices = tokenServices;
            this._InsideService = InsideService;
        }

        #endregion

          [Route("login")]
          [HttpPost]
          public IHttpActionResult RequestLogin(RequestLogin request)
          {
              return Ok(_tokenServices.Login(request));
          }
        

        [Route("CheckAuthenticate")]
        [HttpPost]
        public IHttpActionResult ICheckAuthenticate(RequestLoginModel request)
        {
            return Ok(CheckAuthenticate(request));
        }
        public Response CheckAuthenticate(RequestLoginModel request)
        {
            var result = _tokenServices.ValidateToken(request.Token);
            return result;
        }

        [Route("AddHotel")]
        [HttpPost]
        public IHttpActionResult IAddHotel(HotelModel request)
        {
            return Ok(AddHotel(request));
        }

        public Response AddHotel(HotelModel request)
        {
            var result = _tokenServices.AddHotel(request);
            return result;
        }

        #region Bizkasa

        [AuthorizationRequired]
          [Route("GetUserByHotel")]
          [HttpGet]
          public IHttpActionResult IGetUserByHotel()
          {
              return Ok(GetUserByHotel());
          }
          public Response GetUserByHotel()
          {
              var result = _tokenServices.GetUserByHotel(WorkContext.BizKasaContext.HotelId);
              return result;
          }

          [AuthorizationRequired]
          [Route("GetUserForEdit")]
          [HttpPost]
          public IHttpActionResult IGetUserForEdit( AccountRequestModel request)
          {
              return Ok(GetUserForEdit(request));
          }
          public Response GetUserForEdit(AccountRequestModel request)
          {
              var result = _tokenServices.GetUserForEdit(request.UserId);
              return result;
          }




          [AuthorizationRequired]
          [Route("AddUser")]
          [HttpPost]
          public IHttpActionResult IAddUser(UserViewModel request)
          {
              return Ok(AddUser(request));
          }
          public Response AddUser(UserViewModel request)
          {
              var result = _tokenServices.AddUser(request);
              return result;
          }


        
          [Route("Relogin")]
          [HttpPost]
          public IHttpActionResult IRelogin(UserViewModel request)
          {
              return Ok(Relogin(request));
          }
          public Response Relogin(UserViewModel request)
          {
              var result = _tokenServices.Relogin(request.Email);
              return result;
          }


          [Route("CheckUserExist")]
          [HttpPost]
          public IHttpActionResult ICheckUserExist(AccountRequestModel request)
          {
              return Ok(CheckUserExist(request));
          }

          public Response CheckUserExist(AccountRequestModel request)
          {
              var result = _tokenServices.CheckUserExist(request.UserName);
              return result;
          }

          [Route("RegisterHotel")]
          [HttpPost]
          public IHttpActionResult IRegisterHotel(HotelRegisterModel request)
          {
              return Ok(RegisterHotel(request));
          }

          public Response RegisterHotel(HotelRegisterModel request)
          {
              var result = _tokenServices.RegisterHotel(request);
              return result;
          }
        #endregion

        #region Tokent
          [Route("CheckStoreToken")]
          [HttpPost]
          public IHttpActionResult ICheckExistStoreToken(StoreTokenModel request)
          {
              return Ok(CheckExistStoreToken(request));
          }
          public Response CheckExistStoreToken(StoreTokenModel request)
          {
              //request.Password = CommonUtil.CreateMD5(request.Password);
              var result = _tokenServices.CheckExistStoreToken(request.StoreName);
              return result;
          }


          [Route("AddStoreToken")]
          [HttpPost]
          public IHttpActionResult IAddStoreToken(StoreTokenModel request)
          {
              return Ok(AddStoreToken(request));
          }
          public Response AddStoreToken(StoreTokenModel request)
          {
              //request.Password = CommonUtil.CreateMD5(request.Password);
              var result = _tokenServices.AddStoreToken(request);
              return result;
          }

        #endregion
        #region inside
        
        [Route("InsideLogin")]
        [HttpPost]
          public IHttpActionResult IInsideLogin(RequestLoginModel request)
        {
            return Ok(InsideLogin(request));
        }
        public Response InsideLogin(RequestLoginModel request)
        {
            //request.Password = CommonUtil.CreateMD5(request.Password);
            var result = _InsideService.Login(request.Email, request.Password);
            return result;
        }


        [Route("LoginAsHotel")]
        [HttpPost]
        public IHttpActionResult ILoginAsHotel(RequestLoginModel request)
        {
            return Ok(LoginAsHotel(request));
        }
        public Response LoginAsHotel(RequestLoginModel request)
        {
            //request.Password = CommonUtil.CreateMD5(request.Password);
            var result = _tokenServices.LoginAsHotel(request.HotelId);
            return result;
        }




      

        [Route("RegisterUser")]
        [HttpPost]
        public IHttpActionResult IRegisterUser(UserViewModel request)
        {
            return Ok(RegisterUser(request));
        }

        public Response RegisterUser(UserViewModel request)
        {
            var result = _tokenServices.RegisterUser(request);
            return result;
        }


        [Route("loginTicket")]
        [HttpPost]
        public IHttpActionResult ILoginTicket(RequestLoginModel request)
        {
            return Ok(LoginTicket(request));
        }
        public Response LoginTicket(RequestLoginModel request)
        {
            request.Password = CommonUtil.CreateMD5(request.Password);
            var result = _tokenServices.LoginTicket(request.Email, request.Password);
            return result;
        }
        #endregion
    }
}
