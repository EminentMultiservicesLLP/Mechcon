using BISERPBusinessLayer.Entities.SM;
using BISERPBusinessLayer.Repositories.SM.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.SM
{
    [RoutePrefix("api/sm_WorkOrder")]
    public class SM_WorkOrderController : ApiController
    {
        ISM_WorkOrderRepository _workOrder;
        private static readonly ILogger _loggger = Logger.Register(typeof(SM_WorkOrderController));

        public SM_WorkOrderController(ISM_WorkOrderRepository workOrder)
        {
            _workOrder = workOrder;
        }

        [Route("getEnqForWorkOrder/{UserID}")]
        [HttpGet]
        public IHttpActionResult GetEnqForWorkOrder(int UserID)
        {
            try
            {
                var details = _workOrder.GetEnqForWorkOrder(UserID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnqForWorkOrder of EnquiryRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("saveWorkOrder")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveWorkOrder(WorkOrderEntities model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var WorkOrder = _workOrder.SaveWorkOrder(model);
                model = WorkOrder;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save Enquiry method of WorkOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<WorkOrderEntities>(Request.RequestUri + model.EnquiryID.ToString(), model);
            else
                return BadRequest();
        }

        [Route("getWorkOrder/{UserID}")]
        [HttpGet]
        public IHttpActionResult GetWorkOrder(int UserID)
        {
            try
            {
                var details = _workOrder.GetWorkOrder(UserID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnqForWorkOrder of WorkOrderController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getWorkOrderDetails/{WorkOrderID}")]
        [HttpGet]
        public IHttpActionResult GetWorkOrderDetails(int WorkOrderID)
        {
            try
            {
                var details = _workOrder.GetWorkOrderDetails(WorkOrderID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetWorkOrderDetails of EnquiryRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getWOOtherDetails/{WorkOrderID}")]
        [HttpGet]
        public IHttpActionResult GetWOOtherDetails(int WorkOrderID)
        {
            try
            {
                var details = _workOrder.GetWOOtherDetails(WorkOrderID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetWOOtherDetails of OfferRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

    }
}