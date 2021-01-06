using Bizkasa.Api.Infractstructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.ApiService;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bizkasa.Api.Controllers
{
    [AuthorizationRequired]
    [RoutePrefix("api/Floor")]
    public class FloorController : ApiController
    {
         private readonly ITikasaService _Services;

           #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
         public FloorController(ITikasaService tokenServices)
        {
            this._Services = tokenServices;
        }
           #endregion

         [Route("GetListFloor")]
         [HttpGet]
         public IHttpActionResult IGetListFloor()
         {
             return Ok(GetListFloor());
         }
         public Response GetListFloor()
         {
             var result = _Services.GetListFloor();
             return result;
         }

         [Route("InsertOrUpdateFloor")]
         [HttpPost]
         public IHttpActionResult IInsertOrUpdateFloor(FloorModel request)
         {
             return Ok(InsertOrUpdateFloor(request));
         }
         public Response InsertOrUpdateFloor(FloorModel request)
         {
             var result = _Services.InsertOrUpdateFloor(request);
             return result;
         }



         [Route("GetFloorBy")]
         [HttpPost]
         public IHttpActionResult IGetFloorBy(FloorModel request)
         {
             return Ok(GetFloorBy(request));
         }
         public Response GetFloorBy(FloorModel request)
         {
             var result = _Services.GetFloorBy(request.Id);
             return result;
         }


         [Route("DeleteFloor")]
         [HttpPost]
         public IHttpActionResult IDeleteFloor(FloorRequestModel request)
         {
             return Ok(DeleteFloor(request));
         }
         public Response DeleteFloor(FloorRequestModel request)
         {
             var result = _Services.DeleteFloor(request.Ids);
             return result;
         }
       
         

    }
}
