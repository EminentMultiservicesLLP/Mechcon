using System.Web.Mvc;
using BISERP.Areas.Store.Models.Master;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BISERP.Areas.DashBoards.Models;

namespace BISERP.Areas.DashBoards.Controllers
{
    public class StoreDashboardController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(StoreDashboardController));
        public StoreDashboardController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpGet]
        public async Task<JsonResult> GetGrnCount(DateTime fromdate, DateTime todate)
        {
            JsonResult jResult;
            try
            {
                var _url = url + "/storeDashboard/GetGrnCount/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + Common.Constants.JsonTypeResult;

                var result = await Common.AsyncWebCalls.GetAsync<List<DashboardRequestCounts>>(client, _url, CancellationToken.None);
                if (result != null)
                {
                    jResult = Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetGrnCount :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetMIndentCount(DateTime fromdate, DateTime todate)
        {
            JsonResult jResult;
            try
            {
                var _url = url + "/storeDashboard/GetMIndentCount/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + Common.Constants.JsonTypeResult;

                var result = await Common.AsyncWebCalls.GetAsync<List<DashboardRequestCounts>>(client, _url, CancellationToken.None);
                if (result != null)
                {
                    jResult = Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetMIndentCount :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetMIssueCount(DateTime fromdate, DateTime todate)
        {
            JsonResult jResult;
            try
            {
                var _url = url + "/storeDashboard/GetMIssueCount/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + Common.Constants.JsonTypeResult;

                var result = await Common.AsyncWebCalls.GetAsync<List<DashboardRequestCounts>>(client, _url, CancellationToken.None);
                if (result != null)
                {
                    jResult = Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetMIssueCount :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetVIssueCount(DateTime fromdate, DateTime todate)
        {
            JsonResult jResult;
            try
            {
                var _url = url + "/storeDashboard/GetVIssueCount/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + Common.Constants.JsonTypeResult;

                var result = await Common.AsyncWebCalls.GetAsync<List<DashboardRequestCounts>>(client, _url, CancellationToken.None);
                if (result != null)
                {
                    jResult = Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetVIssueCount :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetMReturnCount(DateTime fromdate, DateTime todate)
        {
            JsonResult jResult;
            try
            {
                var _url = url + "/storeDashboard/GetMReturnCount/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + Common.Constants.JsonTypeResult;

                var result = await Common.AsyncWebCalls.GetAsync<List<DashboardRequestCounts>>(client, _url, CancellationToken.None);
                if (result != null)
                {
                    jResult = Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetMReturnCount :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetStockDisposeCount(DateTime fromdate, DateTime todate)
        {
            JsonResult jResult;
            try
            {
                var _url = url + "/storeDashboard/GetStockDisposeCount/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + Common.Constants.JsonTypeResult;

                var result = await Common.AsyncWebCalls.GetAsync<List<DashboardRequestCounts>>(client, _url, CancellationToken.None);
                if (result != null)
                {
                    jResult = Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStockDisposeCount :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetStockAdjustmentCount(DateTime fromdate, DateTime todate)
        {
            JsonResult jResult;
            try
            {
                var _url = url + "/storeDashboard/GetStockAdjustmentCount/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + Common.Constants.JsonTypeResult;

                var result = await Common.AsyncWebCalls.GetAsync<List<DashboardRequestCounts>>(client, _url, CancellationToken.None);
                if (result != null)
                {
                    jResult = Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStockAdjustmentCount :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetOpeningBalanceCount(DateTime fromdate, DateTime todate)
        {
            JsonResult jResult;
            try
            {
                var _url = url + "/storeDashboard/GetOpeningBalanceCount/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + Common.Constants.JsonTypeResult;

                var result = await Common.AsyncWebCalls.GetAsync<List<DashboardRequestCounts>>(client, _url, CancellationToken.None);
                if (result != null)
                {
                    jResult = Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in OpeningBalance :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
            //return PartialView("~/Areas/AdminDashBoard/Views/AdminDashBoard/StoreTableDashBoard.cshtml", jResult);
        }

    }
}
