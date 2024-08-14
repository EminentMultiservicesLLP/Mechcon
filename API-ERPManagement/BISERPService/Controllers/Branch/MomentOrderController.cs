using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.Repositories.Branch.Interface;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers.Branch
{
    [RoutePrefix("api/MomentOrder")]
    public class MomentOrderController : ApiController
    {
        readonly IMomentOrderRepository _momentOrder;
        private static readonly ILogger Loggger = Logger.Register(typeof(MomentOrderController));

        public MomentOrderController(IMomentOrderRepository momentOrder)
        {
            _momentOrder = momentOrder;
        }

        [Route("CreateMomentOrder")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateMomentOrder(MomentOrderEntity entity)
        {
            var isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = true;

                var newId = _momentOrder.CreateMomentOrder(entity);
                entity.OrderId = newId;
                isSucecss = true;
                //}
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CreateMomentOrder method of MomentOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<MomentOrderEntity>(Request.RequestUri + entity.OrderId.ToString(), entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("GetAllMomentOrderList/{userId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMomentOrderList(int userId)
        {
            List<MomentOrderEntity> items = new List<MomentOrderEntity>();
            TryCatch.Run(() =>
            {
                var list = _momentOrder.GetAllMomentOrderList(userId);
                if (list != null && list.Any())
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetAllMomentOrderList method of MomentOrderController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("UpdateMomentOrder")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateMomentOrder(MomentOrderEntity entity)
        {
           
            var isSucecss = false;
            TryCatch.Run(() =>
            {
               
                var newId = _momentOrder.UpdateMomentOrder(entity);
                entity = newId;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in UpdateMomentOrder method of MomentOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Created<MomentOrderEntity>(Request.RequestUri , entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }


        [Route("ClearBatchFullOrderacceptance")]
        [AcceptVerbs("POST")]
      public IHttpActionResult ClearBatchFullOrderacceptance(MomentOrderEntity entity)
        {
           
            var isSucecss = false;
            TryCatch.Run(() =>
            {

                var newId = _momentOrder.ClearBatchFullOrderacceptance(entity);
                entity = newId;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in ClearBatchFullOrderacceptance method of MomentOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Created<MomentOrderEntity>(Request.RequestUri , entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }
    }
}
