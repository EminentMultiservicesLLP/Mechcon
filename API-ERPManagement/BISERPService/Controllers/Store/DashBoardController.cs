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

        #region DASHBOARD
        [Route("GetDashBoardCountSummury/{FinancialYearID}")]
        [Route("GetDashBoardCountSummury/{FinancialYearID}/{ProjectID}")]
        [HttpGet]
        public IHttpActionResult GetDashBoardCountSummury(int? FinancialYearID = null, int ? ProjectID = null)
        {
            try
            {
                var data = _dashBoard.GetDashBoardCountSummury(FinancialYearID, ProjectID);

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

        [Route("DashboardGetMonthlySale/{FinancialYearID}")]
        [Route("DashboardGetMonthlySale/{FinancialYearID}/{ProjectID}")]
        [HttpGet]
        public IHttpActionResult DashboardGetMonthlySale(int? FinancialYearID = null, int? ProjectID = null)
        {
            try
            {
                var data = _dashBoard.DashboardGetMonthlySale(FinancialYearID, ProjectID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in DashboardGetMonthlySale of DashBoardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("DashboardGetMonthlyPurchase/{FinancialYearID}")]
        [Route("DashboardGetMonthlyPurchase/{FinancialYearID}/{ProjectID}")]
        [HttpGet]
        public IHttpActionResult DashboardGetMonthlyPurchase(int? FinancialYearID = null, int? ProjectID = null)
        {
            try
            {
                var data = _dashBoard.DashboardGetMonthlyPurchase(FinancialYearID, ProjectID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in DashboardGetMonthlySale of DashBoardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }


        [Route("DashboardGetMonthlySaleVsTarget/{FinancialYearID}")]
        [Route("DashboardGetMonthlySaleVsTarget/{FinancialYearID}/{ProjectID}")]
        [HttpGet]
        public IHttpActionResult DashboardGetMonthlySaleVsTarget(int? FinancialYearID = null, int? ProjectID = null)
        {
            try
            {
                var data = _dashBoard.DashboardGetMonthlySaleVsTarget(FinancialYearID, ProjectID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in Dashboard GetMonthlySaleVsTarget of DashBoardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }


        [Route("DashboardGetMonthlySaleVsExpense/{FinancialYearID}")]
        [Route("DashboardGetMonthlySaleVsExpense/{FinancialYearID}/{ProjectID}")]
        [HttpGet]
        public IHttpActionResult DashboardGetMonthlySaleVsExpense(int? FinancialYearID = null, int? ProjectID = null)
        {
            try
            {
                var data = _dashBoard.DashboardGetMonthlySaleVsExpense(FinancialYearID, ProjectID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in Dashboard GetMonthlySaleVsExpense of DashBoardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("DashboardGetMonthlyResourcewiseTarget/{FinancialYearID}")]
        [Route("DashboardGetMonthlyResourcewiseTarget/{FinancialYearID}/{ProjectID}")]
        [HttpGet]
        public IHttpActionResult DashboardGetMonthlyResourcewiseTarget(int? FinancialYearID= null, int? ProjectID = null)
        {
            try
            {
                var data = _dashBoard.DashboardGetMonthlyResourcewiseTarget(FinancialYearID, ProjectID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in Dashboard GetMonthlyResourcewiseTarget of DashBoardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("DashboardGetProjectStatusDataYearly/{FinancialYearID}/{MaxId}")]
        [Route("DashboardGetProjectStatusDataYearly/{FinancialYearID}/{ProjectID}/{MaxId}")]
        [HttpGet]
        public IHttpActionResult DashboardGetProjectStatusDataYearly(int? FinancialYearID = null, int? ProjectID = null, int? MaxId = null)
        {
            try
            {
                var data = _dashBoard.DashboardGetProjectStatusDataYearly(FinancialYearID, ProjectID, MaxId);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified enquiry ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in Dashboard GetProjectStatusDataYearly of DashBoardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }
        #endregion DASHBOARD
    }
}
