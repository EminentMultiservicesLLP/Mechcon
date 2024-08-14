using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon.Extensions;
using BISERPService.Caching;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/storetypes")]
    public class StoreTypeController : ApiController
    {
        IStoreTypeMasterRepository _storetypeMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(StoreTypeController));

        public StoreTypeController(IStoreTypeMasterRepository storetypeMaster)
          {
              _storetypeMaster = storetypeMaster;          
          }

        private List<StoreTypeMasterEntities> AllStoreType()
        {
            List<StoreTypeMasterEntities> storetypes = null;
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllStoreTypes.ToString()))
                {
                    var list = _storetypeMaster.GetAllStoreTypes();
                    if (list != null && list.Count() > 0)
                        storetypes = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllStoreTypes.ToString(), storetypes);
                }
                else
                {
                    storetypes = (List<StoreTypeMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.AllStoreTypes.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllStoreType method of StoreTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return storetypes;
        }

        [Route("getallstoretypemasters")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllStoreTypes()
        {
            List<StoreTypeMasterEntities> storetypes= null;
            TryCatch.Run(() =>
            {
                storetypes = AllStoreType();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllStoreTypes method of StoreTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (storetypes.Any())
                return Ok(storetypes);
            else
                return BadRequest();
        }
        [Route("getstoretypebyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetStoreTypeById(int id)
        {
            StoreTypeMasterEntities storetypes = null;
            TryCatch.Run(() =>
            {
                var temp = AllStoreType();
                storetypes = temp.Where(w => w.Deactive == false && w.ID == id).FirstOrDefault();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStoreTypeById method of StoreTypeController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (storetypes.IsNotNull())
                return Ok(storetypes);
            else
                return NotFound();
        }
    

        [Route("Createstoretype")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult CreateStoreType(StoreTypeMasterEntities storetypeMaster)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _storetypeMaster.CreateStoreType(storetypeMaster);
                storetypeMaster.ID = newID;
                isSucecss = true;
                MemoryCaching.RemoveCacheValue(CachingKeys.AllStoreTypes.ToString());
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateStoreType method of StoreTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<StoreTypeMasterEntities>(Request.RequestUri + storetypeMaster.ID.ToString(), storetypeMaster);
            else
                return BadRequest();
        }
        [Route("Updatestoretype")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateStoreType(int StoreTypeID, StoreTypeMasterEntities storetypeMaster)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _storetypeMaster.UpdateStoreType(storetypeMaster);
                MemoryCaching.RemoveCacheValue(CachingKeys.AllStoreTypes.ToString());
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateStoreType of StoreTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok(Created<StoreTypeMasterEntities>(Request.RequestUri + storetypeMaster.ID.ToString(), storetypeMaster));
            else
                return BadRequest();
        }

        [Route("Deletestoretype")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteStoreType(int StoreTypeID, StoreTypeMasterEntities storetypeMaster)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _storetypeMaster.DeleteStoreType(storetypeMaster);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in DeleteStoreType method of StoreTypeController : parameter :" + StoreTypeID.ToString() + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }
    }
}
