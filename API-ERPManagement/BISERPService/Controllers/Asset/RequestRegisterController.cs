using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPBusinessLayer.Entities.Asset;

namespace BISERPService.Controllers.Asset
{
    [RoutePrefix("api/requestreg")]
    public class RequestRegisterController : ApiController
    {
        IRequestRegisterRepository _requestreg;
        private static readonly ILogger _loggger = Logger.Register(typeof(RequestRegisterController));

        public RequestRegisterController(IRequestRegisterRepository requestreg)
        {
            _requestreg = requestreg;
        }

        [Route("GetRequestno/{BranchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetRequestno(int BranchId)
        {
            List<RequestRegisterEntity> units = new List<RequestRegisterEntity>();
            TryCatch.Run(() =>
            {
                var list = _requestreg.GetRequestno(BranchId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetRequestno method of RequestRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }

        [Route("getrequestdt/{RequisitionId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetRequestDetail(int RequisitionId)
        {
            RequestRegisterEntity request = new RequestRegisterEntity();
            TryCatch.Run(() =>
            {
                request = _requestreg.GetRequestDetail(RequisitionId);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetRequestDetail method of RequestRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (!request.IsNull())
                return Ok(request);
            else
                return BadRequest();
        }

        [Route("GetRequestNoDeletion/{BranchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetRequestNoDeletion(int BranchId)
        {
            List<RequestRegisterEntity> units = new List<RequestRegisterEntity>();
            TryCatch.Run(() =>
            {
                var list = _requestreg.GetRequestNoDeletion(BranchId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetRequestNoDeletion method of RequestRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
     
        [Route("CreateRequestRegister")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateRequestRegister(RequestRegisterEntity Entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {                
                    var newEntity = _requestreg.CreateRequestRegister(Entity);
                    Entity.RequestId = newEntity.RequestId;
                    Entity.RequestCode = newEntity.RequestCode;
                    isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateRequestRegister method of RequestRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<RequestRegisterEntity>(Request.RequestUri + Entity.RequestId.ToString(), Entity);
            else
                return BadRequest();
        }

        [Route("UpdRequestAcceptance")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdRequestAcceptance(RequestRegisterEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
              //  var entity = tax.fuelDetail;
                isSucecss = _requestreg.UpdRequestAcceptance(entity);

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateFuelDetails method of DriverScheduleController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Created<RequestRegisterEntity>(Request.RequestUri + entity.RequestId.ToString(), entity);
            else
                return BadRequest();
        }

        [Route("getregrequest/{FromDate}/{ToDate}/{BranchId}/{Status}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetRequestRegister(DateTime FromDate, DateTime ToDate, int BranchId, int Status)
        {
            List<RequestRegisterEntity> inhouse = new List<RequestRegisterEntity>();
            TryCatch.Run(() =>
            {
                var list = _requestreg.GetRequestRegister(FromDate, ToDate, BranchId, Status);
                if (list != null && list.Count() > 0)
                    inhouse = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetRequestRegister method of RequestRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (inhouse.Any())
                return Ok(inhouse);
            else
                return Ok(inhouse);
        }

        [Route("getallregrequest")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllRequestRegister()
        {
            List<RequestRegisterEntity> inhouse = new List<RequestRegisterEntity>();
            TryCatch.Run(() =>
            {
                var list = _requestreg.GetAllRequestRegister();
                if (list != null && list.Count() > 0)
                    inhouse = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllRequestRegister method of RequestRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (inhouse.Any())
                return Ok(inhouse);
            else
                return Ok(inhouse);
        }

        [Route("UpdateRequestRegister")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateRequestRegister(RequestRegisterEntity Entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newId = _requestreg.UpdateRequestRegister(Entity);
                if (newId > 0)
                    isSucecss = true;
                else
                    isSucecss = false;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateRequestRegister method of RequestRegisterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("regrequestreport/{FromDate}/{ToDate}/{BranchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult RequestRegisterReport(DateTime FromDate, DateTime ToDate, int BranchId)
        {
            List<RequestRegisterEntity> TYPE = new List<RequestRegisterEntity>();
            TryCatch.Run(() =>
            {
                var list = _requestreg.RequestRegisterReport(FromDate, ToDate, BranchId);
                if (list != null && list.Count() > 0)
                    TYPE = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in RequestRegisterReport method of RequestRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (TYPE.Any())
                return Ok(TYPE);
            else
                  return Ok(TYPE);
        }


        [Route("getRequestNotification/{DueDays}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult RequestNotification(int DueDays)
        {
            List<RequestRegisterEntity> TYPE = new List<RequestRegisterEntity>();
            TryCatch.Run(() =>
            {
                var list = _requestreg.RequestNotification(DueDays);
                if (list != null && list.Count() > 0)
                    TYPE = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in RequestNotification method of RequestRegisterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (TYPE.Any())
                return Ok(TYPE);
            else
                return Ok(TYPE);
        }
     
    }
}
