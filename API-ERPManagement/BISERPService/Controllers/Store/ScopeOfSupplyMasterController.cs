using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPService.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Store
{
    [RoutePrefix("api/scopeOfSupply")]
    public class ScopeOfSupplyController : ApiController
    {
        IScopeOfSupplyMasterRepository _scopeOfSupplyMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(ScopeOfSupplyController));

        public ScopeOfSupplyController(IScopeOfSupplyMasterRepository scopeOfSupplyMaster)
        {
            _scopeOfSupplyMaster = scopeOfSupplyMaster;
        }

        private List<ScopeOfSupplyMasterEntities> AllScopeOfSupply()
        {
            List<ScopeOfSupplyMasterEntities> scopeOfSupply = new List<ScopeOfSupplyMasterEntities>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.ScopeOfSupply.ToString()))
                {
                    var list = _scopeOfSupplyMaster.GetAllScopeOfSupply();
                    if (list != null && list.Count() > 0)
                        scopeOfSupply = list.ToList();

                    MemoryCaching.AddCacheValue(CachingKeys.ScopeOfSupply.ToString(), scopeOfSupply);
                }
                else
                {
                    scopeOfSupply = (List<ScopeOfSupplyMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.ScopeOfSupply.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllScopeOfSupply method of ScopeOfSupplyController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return scopeOfSupply;
        }

        [Route("getallscopeOfSupply")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllScopeOfSupply()
        {
            List<ScopeOfSupplyMasterEntities> scopeOfSupply = new List<ScopeOfSupplyMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = AllScopeOfSupply();
                if (list != null && list.Count() > 0)
                    scopeOfSupply = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllScopeOfSupply method of ScopeOfSupplyController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (scopeOfSupply.Any())
                return Ok(scopeOfSupply);
            else
                return Ok(scopeOfSupply);
        }

        [Route("createScopeOfSupply")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult CreateScopeOfSupply(ScopeOfSupplyMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _scopeOfSupplyMaster.CheckDuplicateItem(entity.ScopeOfSupplyName);
                if (isDuplicate == false)
                {
                    var newID = _scopeOfSupplyMaster.CreateScopeOfSupply(entity);
                    entity.ScopeOfSupplyID = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.ScopeOfSupply.ToString());
                }

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateUnit method of  ScopeOfSupplyController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<ScopeOfSupplyMasterEntities>(Request.RequestUri + entity.ScopeOfSupplyID.ToString(), entity);
            else
                return BadRequest();
        }

        [Route("updateScopeOfSupply")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateScopeOfSupply(ScopeOfSupplyMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _scopeOfSupplyMaster.CheckDuplicateupdate(entity.ScopeOfSupplyName, entity.ScopeOfSupplyID);
                if (isDuplicate == false)
                {
                    isSucecss = _scopeOfSupplyMaster.UpdateScopeOfSupply(entity);
                    MemoryCaching.RemoveCacheValue(CachingKeys.ScopeOfSupply.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateScopeOfSupply method of ScopeOfSupplyController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok(Created<ScopeOfSupplyMasterEntities>(Request.RequestUri + entity.ScopeOfSupplyID.ToString(), entity));
            else
                return BadRequest();
        }

    }
}
