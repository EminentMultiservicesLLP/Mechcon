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
using BISERPBusinessLayer.Entities.Purchase;

namespace BISERPService.Controllers.Asset
{
    [RoutePrefix("api/assetlocation")]
    public class AssetLocationController : ApiController
    {
        IAssetLocationRepository _assetlocation;

        private static readonly ILogger _loggger = Logger.Register(typeof(AssetLocationController));
        public AssetLocationController(IAssetLocationRepository assetlocation)
        {
            _assetlocation = assetlocation;
        }

        [Route("createassetlocation")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateAssetLocation(AssetLocationEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var NewId = _assetlocation.CreateAssetLocation(entity);
                entity.AssetLocationId = NewId;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateAssetLocation method of AssetLocationController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<AssetLocationEntity>(Request.RequestUri, entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("updateassetlocation")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateAssetLocation(AssetLocationEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _assetlocation.UpdateAssetLocation(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateAssetLocation method of AssetLocationController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Ok();
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("getassetlocation/{AssetId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAssetLocation(int AssetId)
        {
            AssetLocationEntity location = new AssetLocationEntity();
            TryCatch.Run(() =>
            {
                location = _assetlocation.GetAssetLocation(AssetId);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAssetLocation method of AssetLocationController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (location.IsNotNull())
                return Ok(location);
            else
                return BadRequest();
        }

        [Route("GetPoNoAssetLo/{ItemId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetPoNoAssetLo(int ItemId)
        {
            List<PurchaseOrderEntities> type = new List<PurchaseOrderEntities>();
            TryCatch.Run(() =>
            {
                var list = _assetlocation.GetPoNoAssetLo( ItemId);
                if (list != null && list.Count() > 0)
                    type = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetSupplierNameAssetdt method of AssetLocationController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (type.Any())
                return Ok(type);
            else
                return Ok(type);
        }

        [Route("assetdetailreport/{BranchId}/{ItemId}/{FloorId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AssetDetailReport(int BranchId, int ItemId, int FloorId)
        {
            List<AssetLocationEntity> location = new List<AssetLocationEntity>();
            TryCatch.Run(() =>
            {
                var list = _assetlocation.AssetDetailReport(BranchId, ItemId,FloorId);
                if (list != null && list.Count() > 0)
                    location = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AssetDetailReport method of AssetLocationController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (location.IsNotNull())
                return Ok(location);
            else
                return BadRequest();
        }
        [Route("assetdetailreport/{BranchId}/{AssetId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AssetDetailReport(int BranchId, int AssetId)
        {
            List<AssetLocationEntity> location = new List<AssetLocationEntity>();
            TryCatch.Run(() =>
            {
                var list = _assetlocation.AssetDetailReport(BranchId, AssetId);
                if (list != null && list.Count() > 0)
                    location = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AssetDetailReport method of AssetLocationController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (location.IsNotNull())
                return Ok(location);
            else
                return BadRequest();
        }

        [Route("assetlocationreport/{locationId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AssetLocationReport(int locationId)
        {
            List<AssetLocationEntity> location = new List<AssetLocationEntity>();
            TryCatch.Run(() =>
            {
                var list = _assetlocation.AssetLocationReport(locationId);
                if (list != null && list.Count() > 0)
                    location = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AssetLocationReport method of AssetLocationController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (location.IsNotNull())
                return Ok(location);
            else
                return BadRequest();
        }
    }
}
