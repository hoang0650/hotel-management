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
    [RoutePrefix("api/Widget")]
    public class WidgetController : ApiController
    {
        private readonly ITikasaService _Services;

           #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public WidgetController(ITikasaService tokenServices)
        {
            this._Services = tokenServices;
        }
           #endregion


        [Route("GetWidget")]
        [HttpGet]
        public IHttpActionResult IGetWidget()
        {
            return Ok(GetWidget());
        }
        public Response GetWidget()
        {
            var result = _Services.GetWidgetBy();
            return result;
        }



        [Route("GetWidgetById")]
        [HttpPost]
        public IHttpActionResult IGetWidgetById(WidgetRequestModel request)
        {
            return Ok(GetWidgetById(request));
        }
        public Response GetWidgetById(WidgetRequestModel request)
        {
            var result = _Services.GetWidgetById(request.Id);
            return result;
        }

        [Route("GetGroupWidget")]
        [HttpGet]
        public IHttpActionResult IGetGroupWidget()
        {
            return Ok(GetGroupWidget());
        }
        public Response GetGroupWidget()
        {
            var result = _Services.GetGroupWidgetBy();
            return result;
        }



        [Route("AddWidget")]
        [HttpPost]
        public IHttpActionResult IAddWidget(WidgetRowModel request)
        {
            return Ok(AddWidget(request));
        }
        public Response AddWidget(WidgetRowModel request)
        {
            var result = _Services.AddWidget(request);
            return result;
        }




        [Route("GetWidgetForRecept")]
        [HttpGet]
        public IHttpActionResult IGetWidgetForRecept()
        {
            return Ok(GetWidgetForRecept());
        }
        public Response GetWidgetForRecept()
        {
            var result = _Services.GetWidgetForRecept();
            return result;
        }



        [Route("DeleteWidget")]
        [HttpPost]
        public IHttpActionResult IDeleteWidget(WidgetRequestModel request)
        {
            return Ok(DeleteWidget(request));
        }
        public Response DeleteWidget(WidgetRequestModel request)
        {
            var result = _Services.DeleteWidget(request.Ids);
            return result;
        }



        [Route("AddGroupWidget")]
        [HttpPost]
        public IHttpActionResult IAddGroupWidget(WidgetGroupRowModel request)
        {
            return Ok(AddGroupWidget(request));
        }
        public Response AddGroupWidget(WidgetGroupRowModel request)
        {
            var result = _Services.AddGroupWidget(request);
            return result;
        }


        [Route("DeleteGroupWidget")]
        [HttpPost]
        public IHttpActionResult IDeleteGroupWidget(WidgetGroupRowModel request)
        {
            return Ok(DeleteGroupWidget(request));
        }
        public Response DeleteGroupWidget(WidgetGroupRowModel request)
        {
            var result = _Services.DeleteGroupWidget(request.Id);
            return result;
        }


        

        #region Inside


        #endregion

    }
}
