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
    [RoutePrefix("api/assettype")]
    public class AssetTypeController : ApiController
    {
        IAssetTypeRepository _assettype;
        private static readonly ILogger _loggger = Logger.Register(typeof(AssetTypeController));

        public AssetTypeController(IAssetTypeRepository assettype)
        {
            _assettype = assettype;
        }

        private List<AssetTypeEntity> AllAssetType()
        {
            List<AssetTypeEntity> type = new List<AssetTypeEntity>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllAssetType.ToString()))
                {
                    var list = _assettype.GetAllAssetType();
                    if (list != null && list.Count() > 0)
                        type = list.ToList();
                    MemoryCaching.AddCacheValue(CachingKeys.AllAssetType.ToString(), type);
                }
                else
                {
                    type = (List<AssetTypeEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllAssetType.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllAssetType method of AssetTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return type;
        }

        [Route("GetAllassettype")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllAssetType()
        {
            List<AssetTypeEntity> type = new List<AssetTypeEntity>();
            TryCatch.Run(() =>
            {
                type = AllAssetType();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllAssetType method of AssetTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (type.IsNotNull())
                return Ok(type);
            else
                return BadRequest();
        }

        [Route("Createassettype")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateAssetType(AssetTypeEntity type)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _assettype.CheckDuplicateItem(type.AssetTypeCode);
                if (isDuplicate == false)
                {
                    var newID = _assettype.CreateAssetType(type);
                    type.AssetTypeId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllAssetType.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateAssetType method of AssetTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<AssetTypeEntity>(Request.RequestUri + type.AssetTypeId.ToString(), type);
            else
                return BadRequest("Error");
        }
        [Route("Updateassettype")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateAssetType(AssetTypeEntity type)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _assettype.CheckDuplicateupdate(type.AssetTypeCode, type.AssetTypeId);
                if (isDuplicate == false)
                {
                    isSucecss = _assettype.UpdateAssetType(type);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllAssetType.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateAssetType method of AssetTypeController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactiveassettype")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveAssetType()
        {
            List<AssetTypeEntity> units = new List<AssetTypeEntity>();
            TryCatch.Run(() =>
            {
                var list = _assettype.GetActiveAssetType();
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveAssetType method of AssetTypeController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return Ok(units);
        }
    }
}
