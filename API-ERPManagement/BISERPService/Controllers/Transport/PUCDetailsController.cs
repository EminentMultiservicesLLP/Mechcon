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
    [RoutePrefix("api/puc")]
    public class PUCDetailsController : ApiController
    {
        IPUCDetailsRepository _PUCDetails;
        private static readonly ILogger _loggger = Logger.Register(typeof(PUCDetailsController));

        public PUCDetailsController(IPUCDetailsRepository PUCDetails)
        {
            _PUCDetails = PUCDetails;
        }

        [Route("getPUCDetails")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllPUCDetails()
        {
            List<PUCDetailsEntity> tax = new List<PUCDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _PUCDetails.GetAllPUCDetails();
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllPUCDetails method of PUCDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("getnotification/{DueDays}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult PUCDetailsNotification(int DueDays)
        {
            List<PUCDetailsEntity> tax = new List<PUCDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _PUCDetails.PUCDetailsNotification(DueDays);
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in PUCDetailsNotification method of PUCDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("getPUCDetails/{VehicleId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllPUCDetails(int VehicleId)
        {
            List<PUCDetailsEntity> tax = new List<PUCDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _PUCDetails.GetAllPUCDetails(VehicleId);
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllPUCDetails method of PUCDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("createPUCDetails")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreatePUCDetails(PUCDetailsEntity tax)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                //isDuplicate = _PUCDetails.CheckDuplicatePUCDetails(tax.VehicleId);
                //if (isDuplicate == false)
                //{
                    var newID = _PUCDetails.CreatePUCDetails(tax);
                    tax.PUCId = newID;
                    isSucecss = true;
                //}
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreatePUCDetails method of PUCDetailsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Green Tax already assigned to Vehicle");
            else if (isSucecss)
                return Created<PUCDetailsEntity>(Request.RequestUri + tax.PUCId.ToString(), tax);
            else
                return BadRequest();
        }

        [Route("UpdatePUCDetails")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdatePUCDetails(PUCDetailsEntity tax)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _PUCDetails.UpdatePUCDetails(tax);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdatePUCDetails method of PUCDetailsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();

        }
    }
}
