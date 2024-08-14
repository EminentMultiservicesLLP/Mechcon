using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPService.Caching;
using System.Net.Http;
using System.Net;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/rating")]
    public class RatingController : ApiController
    {
        readonly IRatingRepository _rating; 
        private static readonly ILogger Loggger = Logger.Register(typeof(RatingController));
        public RatingController(IRatingRepository rating)
        {
            _rating = rating;
        }
        private List<RatingEntity> AllRating()
        {
            List<RatingEntity> type = new List<RatingEntity>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllRating.ToString()))
                {
                    var list = _rating.GetAllRating();
                    if (list != null && list.Count() > 0)
                        type = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllRating.ToString(), type);
                }
                else
                {
                    type = (List<RatingEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllRating.ToString()));
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in AllRating method of RatingController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return type;
        }

        [Route("GetAllRating")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllRating()
        {
            IEnumerable<RatingEntity> rating = null; 
            TryCatch.Run(() =>
            {
                rating = AllRating();
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetAllRating method of RatingController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (rating == null)
                return BadRequest("Unknown EError! Failed to save Slot, Please contact system Administrator");
               
            else
                return Ok(rating);
        }

        [Route("CreateRating")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateRating(RatingEntity rating)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _rating.CheckDuplicateItem(rating.RatingCode);
                if (isDuplicate == false)
                {
                    var newId = _rating.CreateRating(rating); 
                    rating.RatingId = newId;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllRating.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CreateRating method of RatingController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Rating Already exist in system, please use another code");
            else if (isSucecss)
                return Created<RatingEntity>(Request.RequestUri + rating.RatingId.ToString(), rating);
            else
                return BadRequest("Unknown Error! Failed to save Rating, Please contact system Administrator");
        }

        [Route("UpdateRating")]
        [AcceptVerbs("PUT")]
        public HttpResponseMessage UpdateRating(RatingEntity rating) 
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _rating.CheckDuplicateupdate(rating.RatingCode, rating.RatingId);
                if (isDuplicate == false)
                {
                    isSucecss = _rating.UpdateRating(rating);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllRating.ToString());
                }
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in UpdateRating method of RatingController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Rating Code already exists inn system" };
            }
            else if (!isSucecss)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Failed to save Rating, Please contact system Administrator" };
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        [Route("getactiveRating")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetActiveRating()
        {
            List<RatingEntity> units = new List<RatingEntity>();
            TryCatch.Run(() =>
            {
                var list = AllRating();
                if (list != null && list.Count() > 0)
                    units = list.Where(m => m.Deactive == false).ToList();

            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetActiveRating method of RatingController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
