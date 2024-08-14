using BISERPBusinessLayer.Repositories.Training.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPBusinessLayer.Entities.Training;
using BISERPCommon.Extensions;
using BISERPService.Caching;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/TrainingCategory")]
    public class TrainingCategoryController : ApiController
    {
        ITrainingCategoryRepository _TrainingCategory;
        private static readonly ILogger Loggger = Logger.Register(typeof(TrainingTypeController));

        public TrainingCategoryController(ITrainingCategoryRepository trainingCategory) 
        {
            _TrainingCategory = trainingCategory;
        }


        private List<TrainingCategoryEntity> AllTrainingCategory()
        {
            List<TrainingCategoryEntity> type = new List<TrainingCategoryEntity>();
            TryCatch.Run(() =>
            {

                //var list = _TrainingCategory.GetAllTrainingCategory();
                //if (list != null && list.Count() > 0)
                //    type = list.ToList();
                    if (!MemoryCaching.CacheKeyExist(CachingKeys.AllTrainingCategory.ToString()))
                    {
                        var list = _TrainingCategory.GetAllTrainingCategory();
                        if (list != null && list.Count() > 0)
                            type = list.ToList();
                        MemoryCaching.AddCacheValue(CachingKeys.AllTrainingCategory.ToString(), type);
                    }
                    else
                    {
                        type = (List<TrainingCategoryEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllTrainingCategory.ToString()));
                    }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in AllTrainingCategory method of TrainingCategoryController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return type;
        }

        [Route("GetAllTrainingCategory")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllTrainingCategory()
        {
            List<TrainingCategoryEntity> training = new List<TrainingCategoryEntity>();
            TryCatch.Run(() =>
            {
                training = AllTrainingCategory();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetAllTrainingCategory method of TrainingCategoryController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (training.IsNotNull())
                return Ok(training);
            else
                return Ok(training);
        }

        [Route("CreateTrainingCategory")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateTrainingCategory(TrainingCategoryEntity trainingCategory)  
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _TrainingCategory.CheckDuplicateItem(trainingCategory.Code);
                if (isDuplicate == false)
                {
                    var newId = _TrainingCategory.CreateTrainingCategory(trainingCategory);
                    trainingCategory.CategoryId = newId;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllTrainingCategory.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CreateTrainingCategory method of TrainingCategoryController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Training Category already exist in system, please use another code");
            else if (isSucecss)
                return Created<TrainingCategoryEntity>(Request.RequestUri + trainingCategory.CategoryId.ToString(), trainingCategory);
            else
                return BadRequest("Unknown EError! Failed to save Training Category, Please contact system Administrator");
        }

        [Route("UpdateTrainingCategory")]
        [AcceptVerbs("POST", "PUT")]
        public HttpResponseMessage UpdateTrainingCategory(TrainingCategoryEntity training)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _TrainingCategory.CheckDuplicateupdate(training.Code, training.CategoryId);
                if (isDuplicate == false)
                {
                    isSucecss = _TrainingCategory.UpdateTrainingCategory(training);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllTrainingCategory.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in UpdateTrainingCategory method of TrainingCategoryController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isDuplicate)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest){ReasonPhrase = "Training Category Code already exists inn system"};
            }
            else if (!isSucecss)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest){ReasonPhrase = "Failed to save Training Category, Please contact system Administrator"};
            }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

        [Route("getactiveTrainingCategory")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetActiveTrainingCategory()
        {
            List<TrainingCategoryEntity> units = new List<TrainingCategoryEntity>();
            TryCatch.Run(() =>
            {
                var list = AllTrainingCategory();
                if (list != null && list.Count() > 0)
                    units = list.Where(m => m.Deactive == false).ToList();

            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetActiveTrainingCategory method of TrainingCategoryController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
        }


  
}
