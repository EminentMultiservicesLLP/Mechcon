using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPBusinessLayer.Entities.Training;
using BISERPService.Caching;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/batchtype")]
    public class BatchTypeController : ApiController
    {
        IBatchTypeRepository _BatchType;
        private static readonly ILogger _loggger = Logger.Register(typeof(BatchTypeController));

        public BatchTypeController(IBatchTypeRepository BatchType)
        {
            _BatchType = BatchType;
        }

         private List<BatchTypeEntity> AllBatchType()
         {
             List<BatchTypeEntity> type = new List<BatchTypeEntity>();
             TryCatch.Run(() =>
             {
                 if (!MemoryCaching.CacheKeyExist(CachingKeys.AllBatchType.ToString()))
                 {
                     var list = _BatchType.GetAllBatchType();
                     if (list != null && list.Count() > 0)
                         type = list.ToList();
                     MemoryCaching.AddCacheValue(CachingKeys.AllBatchType.ToString(), type);
                 }
                 else
                 {
                     type = (List<BatchTypeEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllBatchType.ToString()));
                 }
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in AllBatchType method of BatchTypeController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             return type;
         }

        [Route("GetAllBatchType")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllBatchType()
        {
            IEnumerable<BatchTypeEntity> BatchType = null;
            TryCatch.Run(() =>
            {
                BatchType = AllBatchType();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllBatchType method of BatchTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (BatchType == null)
                  return BadRequest();
          return Ok(BatchType.ToList());
        }

        [Route("CreateBatchType")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateBatchType(BatchTypeEntity BatchType)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _BatchType.CheckDuplicateItem(BatchType.BatchTypeCode);
                if (isDuplicate == false)
                {
                    var newID = _BatchType.CreateBatchType(BatchType);
                    BatchType.BatchTypeId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllBatchType.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateBatchType method of BatchTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<BatchTypeEntity>(Request.RequestUri + BatchType.BatchTypeId.ToString(), BatchType);
            else
                return BadRequest();
        }
        [Route("UpdateBatchType")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateBatchType(BatchTypeEntity BatchType)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _BatchType.CheckDuplicateupdate(BatchType.BatchTypeCode, BatchType.BatchTypeId);
                if (isDuplicate == false)
                {
                    isSucecss = _BatchType.UpdateBatchType(BatchType);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllBatchType.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateBatchType method of BatchTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactiveBatchType")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveBatchType()
        {
            List<BatchTypeEntity> units = new List<BatchTypeEntity>();
            TryCatch.Run(() =>
            {
                var list = AllBatchType();
                if (list != null && list.Count() > 0)
                    units = list.Where(m => m.Deactive == false).ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveBatchType method of BatchTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
