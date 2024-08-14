using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.DashBoard.Interfaces;

namespace BISERPService.Controllers.DashBoard
{
    [RoutePrefix("api/adminDashboard")]
    public class PurchaseDashBoardController : ApiController
    {
        IPurchaseDashBoardRepository _dashBoard;
        private static readonly ILogger _loggger = Logger.Register(typeof(PurchaseDashBoardController));
        public PurchaseDashBoardController(IPurchaseDashBoardRepository dashBoard)
        {
            _dashBoard = dashBoard;
        }

        [Route("SupplierCostLastSixMonth")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SupplierCostLastSixMonth()
        {
            DateTime fromdate = System.DateTime.Now; DateTime todate = DateTime.Now;
            IEnumerable<ChartModelLastFewMonthTotalParent> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.SupplierCostLastSixMonth();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in SupplierCostLastSixMonth method of DashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }
        [Route("GetPurchaseDashBoardRequestCountsQuaterly/{fromDate}/{toDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetPurchaseDashBoardRequestCounts_Quaterly(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<DashboardRequestCounts> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetPurchaseDashBoardRequestCounts_Quaterly(fromDate, toDate);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetPurchaseDashBoardRequestCounts_Quaterly method of DashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }
        [Route("GetDashBoardPurchaseIndentRequestCount/{fromDate}/{toDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDashBoardPurchaseIndentRequestCount(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<DashboardRequestCounts> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetDashBoardPurchaseIndentRequestCount(fromDate, toDate);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetDashBoardPurchaseIndentRequestCount method of DashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }

        [Route("GetDashBoardPurchaseOrderRequestCount/{fromDate}/{toDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDashBoardPurchaseOrderRequestCount(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<DashboardRequestCounts> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetDashBoardPurchaseOrderRequestCount(fromDate, toDate);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetDashBoardPurchaseOrderRequestCount method of DashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }
    }
}
