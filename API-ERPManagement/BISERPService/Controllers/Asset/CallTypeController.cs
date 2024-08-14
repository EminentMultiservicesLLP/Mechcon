using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Entities.Asset;
using BISERPService.Caching;

namespace BISERPService.Controllers.Asset
{
    [RoutePrefix("api/calltype")]
    public class CallTypeController : ApiController
    {
        readonly ICallTypeRepository _callType;
        private static readonly ILogger Loggger = Logger.Register(typeof(CallTypeController));

         public CallTypeController(ICallTypeRepository callType)
        {
            _callType = callType;
        }

         private List<CallTypeEntity> AllCallType()
         {
             List<CallTypeEntity> type = new List<CallTypeEntity>();
             TryCatch.Run(() =>
             {
                 if (!MemoryCaching.CacheKeyExist(CachingKeys.AllCallType.ToString()))
                 {
                     var list = _callType.GetAllCallType();
                     if (list != null && list.Any())
                         type = list.ToList();
                     MemoryCaching.AddCacheValue(CachingKeys.AllCallType.ToString(), type);
                 }
                 else
                 {
                     type = (List<CallTypeEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllCallType.ToString()));
                 }
             }).IfNotNull(ex =>
             {
                 Loggger.LogError("Error in AllCallType method of AssetTypeController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             return type;
         }

        [Route("GetAllCallType")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllCallType()
        {
            List<CallTypeEntity> calltype = new List<CallTypeEntity>();
            TryCatch.Run(() =>
            {
                calltype = AllCallType();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetAllCallType method of CallTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (calltype.IsNotNull())
                return Ok(calltype);
            else
                return BadRequest();
        }

        [Route("CreateCallType")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateCallType(CallTypeEntity callType)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _callType.CheckDuplicateItem(callType.CallTypeCode);
                if (isDuplicate == false)
                {
                    var newId = _callType.CreateCallType(callType);
                    callType.CallTypeId = newId;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllCallType.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CreateCallType method of CallTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<CallTypeEntity>(Request.RequestUri + callType.CallTypeId.ToString(), callType);
            else
                return BadRequest();
        }
        [Route("UpdateCallType")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateCallType(CallTypeEntity callType)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                //isDuplicate = _CallType.CheckDuplicateupdate(CallType.CallTypeCode, CallType.CallTypeId);
                if (isDuplicate == false)
                {
                    isSucecss = _callType.UpdateCallType(callType);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllCallType.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in UpdateCallType method of CallTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactiveCallType")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveCallType()
        {
            var units = new List<CallTypeEntity>();
            TryCatch.Run(() =>
            {
                var list = _callType.GetActiveCallType();
                if (list != null && list.Any())
                    units = list.ToList();


            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetActiveCallType method of CallTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
