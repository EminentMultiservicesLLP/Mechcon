using BISERP.Areas.DashBoards.Models;
using BISERP.Areas.Store.Models.Master;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BISERP.Areas.DashBoards.Controllers
{
    public class PurchaseDashBoardController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(PurchaseDashBoardController));

        public PurchaseDashBoardController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpGet]
        public async Task<JsonResult> SupplierCostLastSixMonth()
        {
            JsonResult jResult;
            try
            {
                var _url = url + "/adminDashboard/SupplierCostLastSixMonth" + Common.Constants.JsonTypeResult;

                var output = await Common.AsyncWebCalls.GetAsync<List<ChartModelLastFewMonthTotalParent>>(client, _url, CancellationToken.None);
                if (output != null)
                {
                    BarChartPopulateParent result = new BarChartPopulateParent();
                    result.Labels = new List<string>();
                    result.Legends = new List<string>();
                    result.LabelValues = new List<double[]>();
                    foreach (var parentItem in output)
                    {
                        result.Legends.Add(parentItem.EntityName);
                        if (result.Labels == null) result.Labels = new List<string>();
                        if (result.Labels != null & result.Labels.Count == 0) result.Labels.AddRange(parentItem.MonthNameValues.Select(s => s.Name).ToList());

                        if (result.LabelValues == null) result.LabelValues = new List<double[]>();
                        result.LabelValues.Add(parentItem.MonthNameValues.Select(s => s.Value).ToArray());
                    }
                    jResult = Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in SupplierCostLastSixMonth :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetPurchaseRequestCounts(DateTime fromdate, DateTime todate, bool isTodaysRequest = false, bool isPiRequest = false)
        {
            JsonResult jResult;
            try
            {
                var _url = url + (isTodaysRequest ? (isPiRequest ? "/adminDashboard/GetDashBoardPurchaseIndentRequestCount/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy")
                                                                    : "/adminDashboard/GetDashBoardPurchaseOrderRequestCount/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy")
                                                    )
                                    : "/adminDashboard/GetPurchaseDashBoardRequestCountsQuaterly/null/null") + Common.Constants.JsonTypeResult;

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
                _logger.LogError("Error in GetPurchaseRequestCounts :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }


    }
}
