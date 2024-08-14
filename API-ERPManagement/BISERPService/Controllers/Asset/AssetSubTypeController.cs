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
    [RoutePrefix("api/assetsubtype")]
    public class AssetSubTypeController : ApiController
    {
        IAssetSubTypeRepository _assettype;
        private static readonly ILogger _loggger = Logger.Register(typeof(AssetSubTypeController));

        public AssetSubTypeController(IAssetSubTypeRepository assettype)
        {
            _assettype = assettype;
        }

        private List<AssetSubTypeEntity> AllAssetSubType()
        {
            List<AssetSubTypeEntity> type = new List<AssetSubTypeEntity>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllAssetSubType.ToString()))
                {
                    var list = _assettype.GetAllAssetSubType();
                    if (list != null && list.Count() > 0)
                        type = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllAssetSubType.ToString(), type);
                }
                else
                {
                    type = (List<AssetSubTypeEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllAssetSubType.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllAssetSubType method of AssetSubTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return type;
        }

        [Route("GetAllassetsubtype")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllAssetSubType()
        {
            List<AssetSubTypeEntity> type = new List<AssetSubTypeEntity>();
            TryCatch.Run(() =>
            {
                type = AllAssetSubType();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllAssetSubType method of AssetSubTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (type.IsNotNull())
                return Ok(type);
            else
                return BadRequest();
        }

        [Route("createassetsubtype")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateAssetSubType(AssetSubTypeEntity type)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _assettype.CheckDuplicateItem(type.SubTypeCode);
                if (isDuplicate == false)
                {
                    var newID = _assettype.CreateAssetSubType(type);
                    type.AssetTypeId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllAssetSubType.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateAssetSubType method of AssetSubTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<AssetSubTypeEntity>(Request.RequestUri + type.AssetTypeId.ToString(), type);
            else
                return BadRequest("Error");
        }
        [Route("Updateassetsubtype")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateAssetSubType(AssetSubTypeEntity type)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _assettype.CheckDuplicateupdate(type.SubTypeCode, type.SubTypeId);
                if (isDuplicate == false)
                {
                    isSucecss = _assettype.UpdateAssetSubType(type);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllAssetSubType.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateAssetSubType method of AssetSubTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactiveassetsubtype")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveAssetSubType()
        {
            List<AssetSubTypeEntity> units = new List<AssetSubTypeEntity>();
            TryCatch.Run(() =>
            {
                var list = _assettype.GetActiveAssetSubType();
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveAssetSubType method of AssetSubTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }

        [Route("getactiveassetsubtype/{AssetTypeId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveAssetSubType(int AssetTypeId)
        {
            List<AssetSubTypeEntity> units = new List<AssetSubTypeEntity>();
            TryCatch.Run(() =>
            {
                var list = _assettype.GetActiveAssetSubType(AssetTypeId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveAssetSubType method of AssetSubTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
