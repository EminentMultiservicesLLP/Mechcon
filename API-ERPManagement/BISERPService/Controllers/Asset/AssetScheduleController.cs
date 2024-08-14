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
    [RoutePrefix("api/assetschedule")]
    public class AssetScheduleController : ApiController
    {
        IAssetScheduleRepository _assetSchedule;
        IAssetScheduleDetailRepository _assetScheduleDt;
        IAssetScheduleCommonRepository _assetschCommon;

        private static readonly ILogger _loggger = Logger.Register(typeof(AssetScheduleController));
        public AssetScheduleController(IAssetScheduleRepository assetSchedule, IAssetScheduleDetailRepository assetScheduleDt,
                                    IAssetScheduleCommonRepository assetschCommon)
        {
            _assetSchedule = assetSchedule;
            _assetScheduleDt = assetScheduleDt;
            _assetschCommon = assetschCommon;
        }

        [Route("createassetschedule")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SaveAssetSchedule(AssetScheduleEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var NewId = _assetschCommon.SaveAssetSchedule(entity);
                if (NewId.ScheduleId > 0)
                    isSucecss = true;
                else
                    isSucecss = false;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateAssetLocation method of AssetScheduleController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<AssetScheduleEntity>(Request.RequestUri, entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("GetAssetSchedule/{fromdate}/{todate}/{branchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAssetSchedule(DateTime fromdate, DateTime todate, int branchId)
        {
            List<AssetScheduleDetailsEntity> units = new List<AssetScheduleDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _assetSchedule.GetAssetSchedule(fromdate, todate, branchId);
                if (list != null && list.Count() > 0)
                    units = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAssetSchedule method of AssetScheduleController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }

        //[Route("GetAssetcodeScheduledt/{ScheduleId}")]
        //[AcceptVerbs("GET", "POST")]
        //public IHttpActionResult GetAssetcodeScheduledt( int ScheduleId)
        //{
        //    List<AssetScheduleDetailsEntity> units = new List<AssetScheduleDetailsEntity>();
        //    TryCatch.Run(() =>
        //    {
        //        var list = _assetScheduleDt.GetAssetcodeScheduledt( ScheduleId);
        //        if (list != null && list.Count() > 0)
        //            units = list.ToList();
        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in GetAssetcodeScheduledt method of AssetScheduleController :" + Environment.NewLine + ex.StackTrace);
        //        return InternalServerError();
        //    });
        //    if (units.Any())
        //        return Ok(units);
        //    else
        //        return BadRequest();
        //}

        [Route("Createschedulecompletion")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Createschedulecompletion(List<AssetScheduleCompletionEntity> entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                foreach (var Sch in entity)
                {
                    var newID = _assetSchedule.CreateAssetScheduleCompletion(Sch);
                }
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Createschedulecompletion method of AssetScheduleController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("GetBranchAssetsforschedule/{BranchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetBranchAssetsforschedule(int BranchId)
        {
            List<AssetEntity> asset = new List<AssetEntity>();
            TryCatch.Run(() =>
            {
                var list = _assetSchedule.GetBranchAssetsforschedule(BranchId);
                if (list != null && list.Count() > 0)
                    asset = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetBranchAssetsforschedule method of AssetScheduleController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (asset.Any())
                return Ok(asset);
            else
                return BadRequest();
        }

        [Route("getAMCnotification/{DueDays}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AMCNotification(int DueDays)
        {
            List<AssetScheduleDetailsEntity> TYPE = new List<AssetScheduleDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _assetScheduleDt.AMCNotification(DueDays);
                if (list != null && list.Count() > 0)
                    TYPE = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AMCNotification method of AssetScheduleController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (TYPE.Any())
                return Ok(TYPE);
            else
                return Ok(TYPE);
        }

        [Route("getCMCnotification/{DueDays}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult CMCNotification(int DueDays)
        {
            List<AssetScheduleDetailsEntity> TYPE = new List<AssetScheduleDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _assetScheduleDt.CMCNotification(DueDays);
                if (list != null && list.Count() > 0)
                    TYPE = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetBranchAssetsforschedule method of AssetScheduleController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (TYPE.Any())
                return Ok(TYPE);
            else
                return Ok(TYPE);
        }
        [Route("getOtherNotification/{DueDays}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult OtherNotification(int DueDays)
        {
            List<AssetScheduleDetailsEntity> TYPE = new List<AssetScheduleDetailsEntity>();
            TryCatch.Run(() =>
            {
                var list = _assetScheduleDt.OtherNotification(DueDays);
                if (list != null && list.Count() > 0)
                    TYPE = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in getOtherNotification method of AssetScheduleController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (TYPE.Any())
                return Ok(TYPE);
            else
                return Ok(TYPE);
        }


        [Route("AssetScheduleReport/{fromdate}/{todate}/{MaintenanceTypeId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AssetScheduleReport(DateTime fromdate, DateTime todate, int MaintenanceTypeId)
        {
            List<AssetScheduleEntity> grn = new List<AssetScheduleEntity>();
            TryCatch.Run(() =>
            {
                var list = _assetschCommon.AssetScheduleReport(fromdate, todate, MaintenanceTypeId);
                if (list != null && list.Count() > 0)
                    grn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AssetScheduleReport method of GRNController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (grn.IsNull())
                return Ok();
            else if (grn.Any())
                return Ok(grn);
            else
                return Ok(grn);
        }

     
    }
}
