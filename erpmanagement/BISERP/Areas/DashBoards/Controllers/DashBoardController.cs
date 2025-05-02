using BISERP.Areas.DashBoards.Models;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.DashBoard.Controllers;


namespace BISERP.Areas.DashBoard.Controllers
{
    public class DashBoardController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(DashBoardController));

        public DashBoardController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetStockSummary(int byValue)
        {
            JsonResult jResult;
            List<StoreDSBDStockSummaryEntity> result = new List<StoreDSBDStockSummaryEntity>();
            try
            {
                string _url = url + "/dashboard/getstocksummary/" + byValue + Common.Constants.JsonTypeResult;
                result = await Common.AsyncWebCalls.GetAsync<List<StoreDSBDStockSummaryEntity>>(client, _url, CancellationToken.None);
                if (result != null)
                {
                    jResult = Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStockSummary :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        #region DASHBOARD
        [HttpGet]
        public async Task<JsonResult> GetDashBoardCountSummury(int? FinancialYearID =null, int? ProjectID = null)
        {
            string _url = $"{url}/dashboard/GetDashBoardCountSummury/{FinancialYearID}" + (ProjectID != null ? $"/{ProjectID}" : "") + Common.Constants.JsonTypeResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<DashBoardCountSummuryModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetDashBoardCountSummury: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving Details" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> DashboardGetMonthlyPurchase(int? FinancialYearID = null, int? ProjectID = null)
        {
            string _url = $"{url}/dashboard/DashboardGetMonthlyPurchase/{FinancialYearID}" + (ProjectID != null ? $"/{ProjectID}" : "") + Common.Constants.JsonTypeResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<Dashboard_BarTrendModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Dashboard GetMonthlyPurchase: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving Details" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> DashboardGetMonthlySale(int? FinancialYearID = null, int? ProjectID = null)
        {
            string _url = $"{url}/dashboard/DashboardGetMonthlySale/{FinancialYearID}" + (ProjectID != null ? $"/{ProjectID}" : "") + Common.Constants.JsonTypeResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<Dashboard_BarTrendModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Dashboard GetMonthlySale: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving Details" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> DashboardGetMonthlySaleVsTarget(int? FinancialYearID = null,  int? ProjectID = null)
        {
            string _url = $"{url}/dashboard/DashboardGetMonthlySaleVsTarget/{FinancialYearID}" + (ProjectID != null ? $"/{ProjectID}" : "") + Common.Constants.JsonTypeResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<Dashboard_BarTrendModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Dashboard GetMonthlySaleVsTarget: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving Details" }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpGet]
        public async Task<JsonResult> DashboardGetMonthlySaleVsExpense(int? FinancialYearID = null,  int? ProjectID = null)
        {
            string _url = $"{url}/dashboard/DashboardGetMonthlySaleVsExpense/{FinancialYearID}" + (ProjectID != null ? $"/{ProjectID}" : "") + Common.Constants.JsonTypeResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<Dashboard_BarTrendModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Dashboard GetMonthlySaleVsExpense: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving Details" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> DashboardGetMonthlyResourcewiseTarget(int? FinancialYearID = null,  int? ProjectID = null)
        {
            string _url = $"{url}/dashboard/DashboardGetMonthlyResourcewiseTarget/{FinancialYearID}" + (ProjectID != null ? $"/{ProjectID}" : "") + Common.Constants.JsonTypeResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<Dashboard_MultiChartBarTrendModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Dashboard GetMonthlyResourcewiseTarget: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving Details" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> DashboardGetProjectStatusDataYearly(int? FinancialYearID = null, int? ProjectID = null, int? MaxId = null)
        {
            string _url = $"{url}/dashboard/DashboardGetProjectStatusDataYearly/{FinancialYearID}" + (ProjectID != null ? $"/{ProjectID}" : "") +  $"/{MaxId}"+ Common.Constants.JsonTypeResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<Dashboard_ColumnChartModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Dashboard GetProjectStatusDataYearly: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving Details" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion DASHBOARD
    }
}
