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
     [RoutePrefix("api/storeDashboard")]
    public class StoreDashboardController : ApiController
    {
         IStoreDashboardRepository _dashBoard;
        private static readonly ILogger _loggger = Logger.Register(typeof(StoreDashboardController));
        public StoreDashboardController(IStoreDashboardRepository dashBoard)
        {
            _dashBoard = dashBoard;
        }
        [Route("GetGrnCount/{fromDate}/{toDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetGrnCount(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<DashboardRequestCounts> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetGRNCounts(fromDate, toDate);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetGRNCounts method of StoreDashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }
        [Route("GetMIndentCount/{fromDate}/{toDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetMIndentCount(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<DashboardRequestCounts> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetMIndentCount(fromDate, toDate);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMIndentCount method of StoreDashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }
        [Route("GetMIssueCount/{fromDate}/{toDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetMIssueCount(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<DashboardRequestCounts> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetMIssueCount(fromDate, toDate);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMIssueCount method of StoreDashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }
        [Route("GetVIssueCount/{fromDate}/{toDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetVIssueCount(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<DashboardRequestCounts> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetVIssueCount(fromDate, toDate);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetVIssueCount method of StoreDashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }

        [Route("GetMReturnCount/{fromDate}/{toDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetMReturnCount(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<DashboardRequestCounts> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetMReturnCount(fromDate, toDate,4);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMReturnCount method of StoreDashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }
        [Route("GetStockDisposeCount/{fromDate}/{toDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetStockDisposeCount(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<DashboardRequestCounts> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetMReturnCount(fromDate, toDate, 5);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStockDisposeCount method of StoreDashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }

        [Route("GetStockAdjustmentCount/{fromDate}/{toDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetStockAdjustmentCount(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<DashboardRequestCounts> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetStockAdjustmentCount(fromDate, toDate, 6);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStockAdjustmentCount method of StoreDashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }
        [Route("GetOpeningBalanceCount/{fromDate}/{toDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetOpeningBalanceCount(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<DashboardRequestCounts> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetStockAdjustmentCount(fromDate, toDate, 7);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetOpeningBalanceCount method of StoreDashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }
        [Route("SendMailStatusOfDashBoard/{fromDate}/{toDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SendMailStatusOfDashBoard(DateTime? fromDate = null, DateTime? toDate = null)
        {
            IEnumerable<DashboardRequestCounts> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.SendMailStatusOfDashBoard(fromDate, toDate);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetGRNCounts method of StoreDashBoardController :" + Environment.NewLine + ex.StackTrace);
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
