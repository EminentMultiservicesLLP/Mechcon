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
    [RoutePrefix("api/enquiryAllocation")]
    public class EnquiryAllocationController : ApiController
    {
        IEnquiryAllocationRepository _enquiryAllocation;
        private static readonly ILogger _loggger = Logger.Register(typeof(EnquiryAllocationController));

        public EnquiryAllocationController(IEnquiryAllocationRepository enquiryAllocation)
        {
            _enquiryAllocation = enquiryAllocation;
        }


        [Route("getEnqForAllocation")]
        [Route("getEnqForAllocation/{statusID}")]
        [HttpGet]
        public IHttpActionResult GetEnqForAllocation(int? statusID = null)
        {
            try
            {
                var details = _enquiryAllocation.GetEnqForAllocation(statusID);

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnqForAllocation of EnquiryRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }


        [Route("saveAllocation")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveAllocation(EnquiryAllocationEntities model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var Allocation = _enquiryAllocation.SaveAllocation(model);
                model = Allocation;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save Enquiry method of EnquiryAllocationController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<EnquiryAllocationEntities>(Request.RequestUri + model.EnquiryID.ToString(), model);
            else
                return BadRequest();
        }

        [Route("getAllocation")]
        [HttpGet]
        public IHttpActionResult GetAllocation()
        {
            try
            {
                var details = _enquiryAllocation.GetAllocation();

                if (details == null)
                {
                    return NotFound(); // or return BadRequest("No details found for the specified enquiry ID.");
                }

                return Ok(details.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetEnqForAllocation of EnquiryAllocationController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

    }
}