using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Entities.Asset;
using BISERPService.Caching;

namespace BISERPService.Controllers.Asset
{
    [RoutePrefix("api/maintainancetype")]
    public class MaintainanceTypeController : ApiController
    {
        IMaintainanceTypeRepository _MaintainanceType;
        private static readonly ILogger _loggger = Logger.Register(typeof(MaintainanceTypeController));

         public MaintainanceTypeController(IMaintainanceTypeRepository MaintainanceType)
        {
            _MaintainanceType = MaintainanceType;
        }

         private List<MaintainanceTypeEntity> AllMaintainanceType()
         {
             List<MaintainanceTypeEntity> type = new List<MaintainanceTypeEntity>();
             TryCatch.Run(() =>
             {
                 if (!MemoryCaching.CacheKeyExist(CachingKeys.AllMaintainanceType.ToString()))
                 {
                     var list = _MaintainanceType.GetAllMaintainanceType();
                     if (list != null && list.Count() > 0)
                         type = list.ToList();
                     MemoryCaching.AddCacheValue(CachingKeys.AllMaintainanceType.ToString(), type);
                 }
                 else
                 {
                     type = (List<MaintainanceTypeEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllMaintainanceType.ToString()));
                 }
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in AllMaintainanceType method of MaintainanceTypeController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             return type;
         }

        [Route("GetAllMaintainanceType")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMaintainanceType()
        {
            List<MaintainanceTypeEntity> mtype = new List<MaintainanceTypeEntity>();
            TryCatch.Run(() =>
            {
                mtype = AllMaintainanceType();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaintainanceType method of MaintainanceTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (mtype.IsNotNull())
                return Ok(mtype);
            else
                return BadRequest();
        }

        [Route("CreateMaintainanceType")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateMaintainanceType(MaintainanceTypeEntity MaintainanceType)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _MaintainanceType.CheckDuplicateItem(MaintainanceType.MaintainanceTypeCode);
                if (isDuplicate == false)
                {
                    var newID = _MaintainanceType.CreateMaintainanceType(MaintainanceType);
                    MaintainanceType.MaintainanceTypeId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllMaintainanceType.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateMaintainanceType method of MaintainanceTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<MaintainanceTypeEntity>(Request.RequestUri + MaintainanceType.MaintainanceTypeId.ToString(), MaintainanceType);
            else
                return BadRequest();
        }
        [Route("UpdateMaintainanceType")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateMaintainanceType(MaintainanceTypeEntity MaintainanceType)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _MaintainanceType.CheckDuplicateupdate(MaintainanceType.MaintainanceTypeCode, MaintainanceType.MaintainanceTypeId);
                if (isDuplicate == false)
                {
                    isSucecss = _MaintainanceType.UpdateMaintainanceType(MaintainanceType);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllMaintainanceType.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateMaintainanceType method of MaintainanceTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactiveMaintainanceType")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveMaintainanceType()
        {
            List<MaintainanceTypeEntity> units = new List<MaintainanceTypeEntity>();
            TryCatch.Run(() =>
            {
                var list = _MaintainanceType.GetActiveMaintainanceType();
                if (list != null && list.Count() > 0)
                    units = list.ToList();


            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveMaintainanceType method of MaintainanceTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
