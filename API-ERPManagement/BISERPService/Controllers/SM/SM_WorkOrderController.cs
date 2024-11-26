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

    }
}