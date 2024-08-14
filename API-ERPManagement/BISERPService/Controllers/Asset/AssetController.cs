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
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPService.Controllers.Asset
{
    [RoutePrefix("api/asset")]
    public class AssetController : ApiController
    {
        IAssetRepository _asset;
        IAssetDetailRepository _assetDetails;
        IAssetCommonRepository _assetCommon;

        private static readonly ILogger _loggger = Logger.Register(typeof(AssetController));
        public AssetController(IAssetRepository asset, IAssetDetailRepository assetDetails,
            IAssetCommonRepository assetCommon)
        {
            _asset = asset;
            _assetDetails = assetDetails;
            _assetCommon = assetCommon;
        }

        [Route("createasset")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateAsset(List<AssetEntity> entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _assetCommon.SaveAsset(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateAsset method of AssetController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<List<AssetEntity>>(Request.RequestUri, entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("getbranchassets/{BranchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetBranchAssets(int BranchId)
        {
            List<AssetEntity> asset = new List<AssetEntity>();
            TryCatch.Run(() =>
            {
                var list = _asset.GetBranchAssets(BranchId);
                if (list != null && list.Count() > 0)
                    asset = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetBranchAssets method of AssetController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (asset.Any())
                return Ok(asset);
            else
                return BadRequest();
        }

        [Route("GetSupplierNameAssetdt/{ItemId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetSupplierNameAssetdt(int ItemId)
        {
            List<SupplierMasterEntities> type = new List<SupplierMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = _assetDetails.GetSupplierNameAssetdt(ItemId);
                if (list != null && list.Count() > 0)
                    type = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetSupplierNameAssetdt method of AssetController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (type.Any())
                return Ok(type);
            else
                return BadRequest();
        }
    }
}
