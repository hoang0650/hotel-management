using Bizkasa.Api.Infractstructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.ApiService;
using MyFinance.ApiService.Inside;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using MyFinance.Utils;
using MyFinance.Domain.InsideModel;
using MyFinance.Domain.RequestModel;


namespace Bizkasa.Api.Controllers
{
    [AuthorizationRequired]
    [RoutePrefix("api/Inside/User")]
    public class InsideUserController : ApiController
    {
      

            private readonly IInsideService _InsideService;
            public InsideUserController(IInsideService insideService)
            {
                
                _InsideService = insideService;
            }
        //
        // GET: /User/
      
       
        [Route("GetUsers")]
        [HttpPost]
        public IHttpActionResult IGetUsers(InvoiceFilterModel filter)
        {
            return Ok(GetUsers(filter));
        }

        public Response GetUsers(InvoiceFilterModel filter)
        {
            var result = _InsideService.GetUsers(filter);
            return result;
        }


        [Route("DeleteUsers")]
        [HttpPost]
        public IHttpActionResult IDeleteUsers(InsideUserRequestModel request)
        {
            return Ok(DeleteUsers(request));
        }

        public Response DeleteUsers(InsideUserRequestModel request)
        {
            var result = _InsideService.DeleteUsers(request.Ids);
            return result;
        }


        [Route("CheckUserExist")]
        [HttpPost]
        public IHttpActionResult ICheckUserExist(InsideUserRequestModel request)
        {
            return Ok(CheckUserExist(request));
        }

        public Response CheckUserExist(InsideUserRequestModel request)
        {
            var result = _InsideService.CheckUserExist(request.UserName);
            return result;
        }


	}
}