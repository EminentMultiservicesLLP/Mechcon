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
    [RoutePrefix("api/permit")]
    public class PermitDetailsController : ApiController
    {
        IPermitDetailsRepository _PermitDetails;
        private static readonly ILogger _loggger = Logger.Register(typeof(PermitDetailsController));

        public PermitDetailsController(IPermitDetailsRepository PermitDetails)
        {
            _PermitDetails = PermitDetails;
        }

        [Route("getPermitDetails")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllPermitDetails()
        {
            List<PermitDetailsEntity> tax = new List<PermitDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _PermitDetails.GetAllPermitDetails();
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllPermitDetails method of PermitDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("getnotification/{DueDays}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult PermitDetailsNotification(int DueDays)
        {
            List<PermitDetailsEntity> tax = new List<PermitDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _PermitDetails.PermitDetailsNotification(DueDays);
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in PermitDetailsNotification method of PermitDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("vehiclepermit/{VehicleId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllPermitDetails(int VehicleId)
        {
            List<PermitDetailsEntity> tax = new List<PermitDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _PermitDetails.GetAllPermitDetails(VehicleId);
                if (list != null && list.Count() > 0)
                    tax = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllPermitDetails method of PermitDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (tax.Any())
                return Ok(tax);
            else
                return BadRequest();
        }

        [Route("createPermitDetails")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreatePermitDetails(PermitDetailsEntity tax)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                //isDuplicate = _PermitDetails.CheckDuplicatePermitDetails(tax.VehicleId);
                //if (isDuplicate == false)
                //{
                    var newID = _PermitDetails.CreatePermitDetails(tax);
                    tax.PermitId = newID;
                    isSucecss = true;
                //}
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreatePermitDetails method of PermitDetailsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Green Tax already assigned to Vehicle");
            else if (isSucecss)
                return Created<PermitDetailsEntity>(Request.RequestUri + tax.PermitId.ToString(), tax);
            else
                return BadRequest();
        }

        [Route("UpdatePermitDetails")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdatePermitDetails(PermitDetailsEntity tax)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _PermitDetails.UpdatePermitDetails(tax);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdatePermitDetails method of PermitDetailsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();

        }
    }
}
