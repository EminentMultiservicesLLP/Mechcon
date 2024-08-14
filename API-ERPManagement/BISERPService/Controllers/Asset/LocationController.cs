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
using BISERPService.Caching;

namespace BISERPService.Controllers.Asset
{
     [RoutePrefix("api/location")]
    public class LocationController : ApiController
    {
          ILocationRepository _loc;
         private static readonly ILogger _loggger = Logger.Register(typeof(LocationController));

         public LocationController(ILocationRepository loc)
         {
             _loc = loc;
         }

         private List<LocationEntity> AllLocation()
         {
             List<LocationEntity> make = new List<LocationEntity>();
             TryCatch.Run(() =>
             {
                 if (!MemoryCaching.CacheKeyExist(CachingKeys.AllLocation.ToString()))
                 {
                     var list = _loc.GetAllLocation();
                     if (list != null && list.Count() > 0)
                         make = list.ToList();
                     MemoryCaching.AddCacheValue(CachingKeys.AllLocation.ToString(), make);
                 }
                 else
                 {
                     make = (List<LocationEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllLocation.ToString()));
                 }
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in AllLocation method of LocationController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             return make;
         }

        [Route("getallLocation")]
        [AcceptVerbs("GET", "POST")]
         public IHttpActionResult GetAllLocation()
        {
            List<LocationEntity> location = new List<LocationEntity>();
            TryCatch.Run(() =>
            {
                var list = AllLocation();
                location = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllLocation method of LocationController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (location.Any())
                return Ok(location);
            else
                return Ok(location);
        }

        [Route("createlocation")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult CreateLocation(LocationEntity entity)
        {
            bool isSucecss = false, isDuplicate = false; ;
            TryCatch.Run(() =>
            {
                isDuplicate = _loc.CheckDuplicateItem(entity.LocationCode);
                if (isDuplicate == false)
                {
                    var newID = _loc.CreateLocation(entity);
                    entity.LocationId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllLocation.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateLocation method of LocationController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<LocationEntity>(Request.RequestUri + entity.LocationId.ToString(), entity);
            else
                return BadRequest("Error");

        }
           
        [Route("updatelocation")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateLocation(LocationEntity entity)
        {
            bool isSucecss = false, isDuplicate = false; ;
            TryCatch.Run(() =>
            {
                isDuplicate = _loc.CheckDuplicateupdate(entity.LocationCode, entity.LocationId);
                if (isDuplicate == false)
                {
                    isSucecss = _loc.UpdateLocation(entity);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllLocation.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateLocation method of LocationController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("getactiveLocation")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveLocation()
        {
            List<LocationEntity> location = new List<LocationEntity>();
            TryCatch.Run(() =>
            {
                var list = AllLocation();
                location = list.Where(m => m.Deactive == false).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in getactiveLocation method of LocationController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (location.Any())
                return Ok(location);
            else
                return Ok(location);
        }

        [Route("getbranchLocation/{BranchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetBranchLocation(int BranchId)
        {
            List<LocationEntity> units = new List<LocationEntity>();
            TryCatch.Run(() =>
            {
                var list = _loc.GetBranchLocation(BranchId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in getbranchLocation method of LocationController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return Ok(units);
        }

    
    }
}
