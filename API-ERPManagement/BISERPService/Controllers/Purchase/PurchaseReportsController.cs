using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Purchase
{
    [RoutePrefix("api/PurchaseReports")]
    public class PurchaseReportsController : ApiController
    {
        IPurchaseReportsRepository _PurchaseReports;
        private static readonly ILogger _loggger = Logger.Register(typeof(StoreQuantityLimitsController));

        public PurchaseReportsController(IPurchaseReportsRepository PurchaseReports)
        {
            _PurchaseReports = PurchaseReports;
        }

        [Route("grnVSpoitemcomparison/{StoreId}/{SupplierId}/{FromDate}/{ToDate}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult grnVSpoitemcomparison(int StoreId, int SupplierId, DateTime? FromDate, DateTime? ToDate)
        {
            List<ProjectBudgetConclusion> entity = null;
            TryCatch.Run(() =>
            {
                var list = _PurchaseReports.grnVSpoitemcomparison(StoreId, SupplierId, FromDate, ToDate);
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
