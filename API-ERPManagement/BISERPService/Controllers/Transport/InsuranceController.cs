using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Transport
{
    [RoutePrefix("api/insurance")]
    public class InsuranceController : ApiController
    {
        IInsuranceRepository _insurance;
        private static readonly ILogger _loggger = Logger.Register(typeof(InsuranceController));

        public InsuranceController(IInsuranceRepository insurance)
        {
            _insurance = insurance;
        }

        [Route("getinsurance")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllInsurance()
        {
            List<InsuranceEntity> tax = new List<InsuranceEntity>();
            TryCatch.Run(() =>
            {
                var list = _insurance.GetAllInsurance();
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllInsurance method of InsuranceController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("getnotification/{DueDays}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult InsuranceNotification(int DueDays)
        {
            List<InsuranceEntity> tax = new List<InsuranceEntity>();
            TryCatch.Run(() =>
            {
                var list = _insurance.InsuranceNotification(DueDays);
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in InsuranceNotification method of InsuranceController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("vehicleinsurance/{VehicleId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllInsurance(int VehicleId)
        {
            List<InsuranceEntity> tax = new List<InsuranceEntity>();
            TryCatch.Run(() =>
            {
                var list = _insurance.GetAllInsurance(VehicleId);
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllInsurance method of InsuranceController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("createInsurance")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateInsurance(InsuranceEntity tax)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                //isDuplicate = _insurance.CheckDuplicateInsurance(tax.VehicleId);
                //if (isDuplicate == false)
                //{
                    var newID = _insurance.CreateInsurance(tax);
                    tax.InsuranceId = newID;
                    isSucecss = true;
                //}
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateInsurance method of InsuranceController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Insurance already assigned to Vehicle");
            else if (isSucecss)
                return Created<InsuranceEntity>(Request.RequestUri + tax.InsuranceId.ToString(), tax);
            else
                return BadRequest();
        }

        [Route("UpdateInsurance")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateInsurance(InsuranceEntity tax)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _insurance.UpdateInsurance(tax);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateInsurance method of InsuranceController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();

        }
    }
}
