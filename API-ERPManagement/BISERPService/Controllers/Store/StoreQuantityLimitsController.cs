using BISEPRService.Controllers;
using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon.Extensions;
namespace BISERPService.Controllers
{
    [RoutePrefix("api/sqli")]
    public class StoreQuantityLimitsController : ApiController
    {
        IStoreQuantityLimitsRepository _StoreQuantity;
        private static readonly ILogger _loggger = Logger.Register(typeof(StoreQuantityLimitsController));

        public StoreQuantityLimitsController(IStoreQuantityLimitsRepository StoreQuantity)
        {
            _StoreQuantity = StoreQuantity;
        }

        [Route("getallitemlimits/{storeId}/{ItemTypeId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllItemLimits(int storeId, int ItemTypeId)
        {
            List<StoreQuantityLimitsEntity> entity = null;
            TryCatch.Run(() =>
            {
                var list = _StoreQuantity.GetAllItemLimits( storeId,  ItemTypeId);
                if (list != null && list.Count() > 0)
                    entity = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllItemLimits method of StoreQuantityLimitsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity.IsNotNull())
                return Ok(entity);
            if(entity.IsNull())
                return Ok(entity);
            return BadRequest();
        }
        [Route("Createstoreql")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Createopen(List<StoreQuantityLimitsEntity> entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                foreach (var Limit in entity)
                {
                    var newID = _StoreQuantity.CreateQuantityLimits(Limit);
                }
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Createopen method of StoreQuantityLimitsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("getnotifyQty/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetNotificationQuantity(int UserId)
        {
            List<StoreQuantityLimitsEntity> entity = new List<StoreQuantityLimitsEntity>();
            TryCatch.Run(() =>
            {
                var list = _StoreQuantity.GetNotificationQuantity(UserId);
                if (list != null && list.Count() > 0)
                    entity = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetNotificationQuantity method of StoreQuantityLimitsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity.Any())
                return Ok(entity);
            else
                return BadRequest();
        }

        [Route("GetMasterReport/{StoreId}/{FromDate}/{ToDate}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetMasterReport(int StoreId, DateTime? FromDate, DateTime? ToDate)
        {
            List<MasterReportEntity> entity = null;
            TryCatch.Run(() =>
            {
                var list = _StoreQuantity.GetMasterReport(StoreId, FromDate, ToDate);
                if (list != null && list.Count() > 0)
                    entity = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMasterReport method of StoreQuantityLimitsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity.IsNotNull())
                return Ok(entity);
            if (entity.IsNull())
                return Ok(entity);
            return BadRequest();
        }
        [Route("GetProjectBudgetConclusion/{StoreId}/{FromDate}/{ToDate}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetProjectBudgetConclusion(int StoreId, DateTime? FromDate, DateTime? ToDate)
        {
            List<ProjectBudgetConclusion> entity = null;
            TryCatch.Run(() =>
            {
                var list = _StoreQuantity.GetProjectBudgetConclusion(StoreId, FromDate, ToDate);
                if (list != null && list.Count() > 0)
                    entity = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMasterReport method of StoreQuantityLimitsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity.IsNotNull())
                return Ok(entity);
            if (entity.IsNull())
                return Ok(entity);
            return BadRequest();
        }

    }
}
