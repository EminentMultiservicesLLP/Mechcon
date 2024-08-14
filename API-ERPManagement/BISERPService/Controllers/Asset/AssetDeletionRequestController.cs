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

namespace BISERPService.Controllers.Asset
{
    [RoutePrefix("api/assetdel")]
    public class AssetDeletionRequestController : ApiController
    {
        IAssetDeletionRequestRepository _assetdeletion;
        private static readonly ILogger _loggger = Logger.Register(typeof(AssetDeletionRequestController));

        public AssetDeletionRequestController(IAssetDeletionRequestRepository assetdeletion)
        {
            _assetdeletion = assetdeletion;
        }


        [Route("CreateAssetDeletionRequest")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateAssetDeletionRequest(DeactivationDetailEntity Entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newEntity = _assetdeletion.CreateAssetDeletionRequest(Entity);
                Entity.Id = newEntity.Id;
                    isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateAssetDeletionRequest method of AssetDeletionRequestController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<DeactivationDetailEntity>(Request.RequestUri + Entity.Id.ToString(), Entity);
            else
                return BadRequest();
        }

        [Route("GetAssetdeactivation/{BranchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAssetdeactivation(int BranchId)
        {
            List<DeactivationDetailEntity> asset = new List<DeactivationDetailEntity>();
            TryCatch.Run(() =>
            {
                var list = _assetdeletion.GetAssetdeactivation(BranchId);
                if (list != null && list.Count() > 0)
                    asset = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAssetdeactivation method of AssetDeletionRequestController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (asset.Any())
                return Ok(asset);
            else
                return BadRequest();
        }
      
    }
}
