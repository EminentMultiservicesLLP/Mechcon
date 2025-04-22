using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.Entities.Store;

namespace BISERPService.Controllers.Store
{
    [RoutePrefix("api/dashboard")]
    public class DashBoardController : ApiController
    {
        IDashBoardRepository _dashBoard;
        private static readonly ILogger _loggger = Logger.Register(typeof(DashBoardController));
        public DashBoardController(IDashBoardRepository dashBoard)
        {
            _dashBoard = dashBoard;
        }

        [Route("getitemsummary")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetItemSummary()
        {
            StoreDashBoardEntity entity = new StoreDashBoardEntity();
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetItemSummary();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetItemSummary method of DashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
                return Ok(entity);
            else
                return BadRequest();

        }
		
		[Route("getstocksummary/{byValue}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetStockSummary(int byValue = 1)
        {
            IEnumerable<StoreDSBDStockSummaryEntity> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetStoreDSBDStockSummary(byValue);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetStockSummary method of DashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
            {
                return Ok(entity);
            }
            else
                return BadRequest();
        }

        [Route("getguardissuesummary")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetGuardIssueSummary()
        {
            IEnumerable<StoreDSBDGuardIssueSummaryEntity> entity = null;
            TryCatch.Run(() =>
            {
                entity = _dashBoard.GetStoreDSBDGuardIssueSummary();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetGuardIssueSummary method of DashBoardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (entity != null)
                return Ok(entity);
            else
                return BadRequest();
        }


        [Route("GetDashBoardCountSummury/{FromDate}/{ToDate}")]
        [Route("GetDashBoardCountSummury/{FromDate}/{ToDate}/{ProjectID}")]
        [HttpGet]
        public IHttpActionResult GetDashBoardCountSummury(string FromDate = null, string ToDate = null, int ? ProjectID = null)
        {
            try
            {
                var data = _dashBoard.GetDashBoardCountSummury(FromDate, ToDate, ProjectID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetDashBoardCountSummury of DashBoardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

    }
}
