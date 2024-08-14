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
using BISERPService.Filters;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/trainer")]
    public class TrainerController : ApiController
    {
        ITrainerRepository _Trainer;
        private static readonly ILogger _loggger = Logger.Register(typeof(TrainerController));

        public TrainerController(ITrainerRepository Trainer)
        {
            _Trainer = Trainer;
        }

        private List<TrainerEntity> AllTrainer()
         {
             List<TrainerEntity> type = new List<TrainerEntity>();
             TryCatch.Run(() =>
             {
                 if (!MemoryCaching.CacheKeyExist(CachingKeys.AllTrainer.ToString()))
                 {
                     var list = _Trainer.GetAllTrainer();
                     if (list != null && list.Count() > 0)
                         type = list.ToList();
                     MemoryCaching.AddCacheValue(CachingKeys.AllTrainer.ToString(), type);
                 }
                 else
                 {
                     type = (List<TrainerEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllTrainer.ToString()));
                 }
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in AllTrainer method of TrainerController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             return type;
         }

        [Route("GetAllTrainer")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllTrainer()
        {
            List<TrainerEntity> Trainer = new List<TrainerEntity>();
            TryCatch.Run(() =>
            {
                Trainer = AllTrainer();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllTrainer method of TrainerController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (Trainer.IsNotNull())
                return Ok(Trainer);
            else
                return BadRequest();
        }

        [Route("CreateTrainer")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateTrainer(TrainerEntity Trainer)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _Trainer.CheckDuplicateItem(Trainer.TrainerName);
                if (isDuplicate == false)
                {
                    var newID = _Trainer.CreateTrainer(Trainer);
                    Trainer.TrainerId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllTrainer.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateTrainer method of TrainerController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Trainer Name Already Exist");
            else if (isSucecss)
                return Created<TrainerEntity>(Request.RequestUri + Trainer.TrainerId.ToString(), Trainer);
            else
                return BadRequest("Unknown Error! Failed to save Rating, Please contact system Administrator");
        }

        [Route("UpdateTrainer")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult UpdateTrainer(TrainerEntity Trainer)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _Trainer.CheckDuplicateupdate(Trainer.TrainerName, Trainer.TrainerId);
                if (isDuplicate == false)
                {
                    isSucecss = _Trainer.UpdateTrainer(Trainer);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllTrainer.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateTrainer method of TrainerController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactiveTrainer")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveTrainer()
        {
            List<TrainerEntity> units = new List<TrainerEntity>();
            TryCatch.Run(() =>
            {
                var list = AllTrainer();
                if (list != null && list.Count() > 0)
                    units = list.Where(m => m.Deactive == false).ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveTrainer method of TrainerController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
