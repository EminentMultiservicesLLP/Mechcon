using BISERP.Areas.SM_Reports.Models;
using BISERP.Filters;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.SM_Reports.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class SM_ReportsController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(SM_ReportsController));

        public SM_ReportsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        #region Views

        public ActionResult EnquiryRegisterReport()
        {
            return PartialView();
        }

        public ActionResult OrderBookRpt()
        {
            return PartialView();
        }

        public ActionResult ResourceWiseMonthlyStatusRpt()
        {
            return PartialView();
        }

        public ActionResult MasterListProjectsRpt()
        {
            return PartialView();
        }


        public ActionResult SectorWiseSalesRpt()
        {
            return PartialView();
        }


        public ActionResult LocationWiseSalesRpt()
        {
            return PartialView();
        }


        public ActionResult ProductWiseSalesRpt()
        {
            return PartialView();
        } 
        public ActionResult WorkOrderReport()
        {
            return PartialView();
        }

        #endregion Views

        #region GetReportsCall

        [HttpGet]
        public async Task<JsonResult> GetEnquiryRegisterRpt(int? financialYearID)
        {
            string _url = $"{url}/sm_Reports/getEnquiryRegisterRpt/{financialYearID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<EnquiryRegisterReportModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetEnquiryRegisterRpt: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOrderBookRpt(int? financialYearID)
        {
            string _url = $"{url}/sm_Reports/getOrderBookRpt/{financialYearID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<OrderBookReportModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetOrderBookRpt: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetResourceWiseMonthlyStatusRpt(int? financialYearID)
        {
            string _url = $"{url}/sm_Reports/getResourceWiseMonthlyStatusRpt/{financialYearID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<MonthlyTargetReportModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetResourceWiseMonthlyStatusRpt: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetSectorWiseSalesRpt(int? financialYearID)
        {
            string _url = $"{url}/sm_Reports/getSectorWiseSalesRpt/{financialYearID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<SectorWiseSalesReportModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetSectorWiseSalesRpt: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetLocationWiseSalesRpt(int? financialYearID)
        {
            string _url = $"{url}/sm_Reports/getLocationWiseSalesRpt/{financialYearID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<LocationWiseSalesReportModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetLocationWiseSalesRpt: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetProductWiseSalesRpt(int? financialYearID)
        {
            string _url = $"{url}/sm_Reports/getProductWiseSalesRpt/{financialYearID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<ProductWiseSalesReportModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetProductWiseSalesRpt: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion GetReportsCall

    }
}
