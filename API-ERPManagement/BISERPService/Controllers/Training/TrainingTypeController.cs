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
    [RoutePrefix("api/trainingtype")]
    public class TrainingTypeController : ApiController
    {
        ITrainingTypeRepository _TrainingType;
        private static readonly ILogger _loggger = Logger.Register(typeof(TrainingTypeController));

        public TrainingTypeController(ITrainingTypeRepository TrainingType)
        {
            _TrainingType = TrainingType;
        }

         private List<TrainingTypeEntity> AllTrainingType()
         {
             List<TrainingTypeEntity> type = new List<TrainingTypeEntity>();
             TryCatch.Run(() =>
             {
                 if (!MemoryCaching.CacheKeyExist(CachingKeys.AllTrainingType.ToString()))
                 {
                     var list = _TrainingType.GetAllTrainingType();
                     if (list != null && list.Count() > 0)
                         type = list.ToList();
                     MemoryCaching.AddCacheValue(CachingKeys.AllTrainingType.ToString(), type);
                 }
                 else
                 {
                     type = (List<TrainingTypeEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllTrainingType.ToString()));
                 }
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in AllTrainingType method of TrainingTypeController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             return type;
         }

        [Route("GetAllTrainingType")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllTrainingType()
        {
            IEnumerable<TrainingTypeEntity> TrainingType = null;
            TryCatch.Run(() =>
            {
                TrainingType = AllTrainingType();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllTrainingType method of TrainingTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (TrainingType == null)
                  return BadRequest();
            return Ok(TrainingType.ToList());
          
        }

        [Route("GetdailyupdateTrainingType")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetdailyupdateTrainingType()
        {
            IEnumerable<TrainingTypeEntity> dailyTrainingType = null;
            TryCatch.Run(() =>
            {
                dailyTrainingType = _TrainingType.GetdailyupdateTrainingType();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetdailyupdateTrainingType method of TrainingTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (dailyTrainingType == null)
                return BadRequest();
            return Ok(dailyTrainingType.ToList());

        }

        [Route("CreateTrainingType")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateTrainingType(TrainingTypeEntity TrainingType)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _TrainingType.CheckDuplicateItem(TrainingType.TrainingTypeCode);
                if (isDuplicate == false)
                {
                    var newID = _TrainingType.CreateTrainingType(TrainingType);
                    TrainingType.TypeId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllTrainingType.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateTrainingType method of TrainingTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<TrainingTypeEntity>(Request.RequestUri + TrainingType.TypeId.ToString(), TrainingType);
            else
                return BadRequest();
        }
        [Route("UpdateTrainingType")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateTrainingType(TrainingTypeEntity TrainingType)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _TrainingType.CheckDuplicateupdate(TrainingType.TrainingTypeCode, TrainingType.TypeId);
                if (isDuplicate == false)
                {
                    isSucecss = _TrainingType.UpdateTrainingType(TrainingType);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllTrainingType.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateTrainingType method of TrainingTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactiveTrainingType")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveTrainingType()
        {
            List<TrainingTypeEntity> units = new List<TrainingTypeEntity>();
            TryCatch.Run(() =>
            {
                var list = AllTrainingType();
                if (list != null && list.Count() > 0)
                    units = list.Where(m => m.Deactive == false).ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveTrainingType method of TrainingTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
