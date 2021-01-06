using Bizkasa.Api.Infractstructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.ApiService;
using MyFinance.ApiService.Inside;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace Bizkasa.Api.Controllers
{
    [AuthorizationRequired]
    [RoutePrefix("api/Inside/Utility")]
    public class InsideUtilityController : ApiController
    {

            private readonly IInsideService _InsideService;
            public InsideUtilityController(IInsideService insideService)
            {
                _InsideService = insideService;
            }
            //



            [Route("GetUtilities")]
            [HttpGet]
            public IHttpActionResult IGetUtilities()
            {
                return Ok(GetUtilities());
            }

            public Response GetUtilities()
            {
                var result = _InsideService.GetUtilities();
                return result;
            }


            [Route("GetUtilityGroups")]
            [HttpGet]
            public IHttpActionResult IGetUtilityGroups()
            {
                return Ok(GetUtilityGroups());
            }

            public Response GetUtilityGroups()
            {
                var result = _InsideService.GetUtilityGroups();
                return result;
            }




            [Route("AddOrUpdateUtility")]
            [HttpPost]
            public IHttpActionResult IAddOrUpdateUtility(UtilityModel request)
            {
                return Ok(AddOrUpdateUtility(request));
            }

            public Response AddOrUpdateUtility(UtilityModel request)
            {
                var result = _InsideService.AddOrUpdateUtility(request);
                return result;
            }



            [Route("AddOrUpdateUtilityGroup")]
            [HttpPost]
            public IHttpActionResult IAddOrUpdateUtilityGroup(UtilityGroupModel request)
            {
                return Ok(AddOrUpdateUtilityGroup(request));
            }

            public Response AddOrUpdateUtilityGroup(UtilityGroupModel request)
            {
                var result = _InsideService.AddOrUpdateUtilityGroup(request);
                return result;
            }


            [Route("GetUtilityForEdit")]
            [HttpPost]
            public IHttpActionResult IGetUtilityForEdit(UtilityModel request)
            {
                return Ok(GetUtilityForEdit(request));
            }

            public Response GetUtilityForEdit(UtilityModel request)
            {
                var result = _InsideService.GetUtilityForEdit(request.Id);
                return result;
            }

       

       

	}
}