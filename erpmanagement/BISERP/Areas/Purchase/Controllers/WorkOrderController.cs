using BISERP.Areas.Purchase.Models;
using BISERP.Filters;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.Purchase.Controllers
{

    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class WorkOrderController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(WorkOrderController));

        public WorkOrderController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveWorkOrder(WorkOrderModel items)
        {
            string errorMessage = "";
            string _url = "";
            bool isSuccess = true;
            string indentNumber = "";

            try
            {
                var mindent = new WorkOrderModel
                {
                    IndentId = items.IndentId,
                    IndentNumber = items.IndentNumber,
                    IndentDate = items.IndentDate,
                    IndentNature = items.IndentNature,
                    QuotationDeadLine = items.QuotationDeadLine,
                    PurchaseIndentId = items.PurchaseIndentId,
                    Storeid = items.Storeid,
                    ItemCategoryId = items.ItemCategoryId,
                    Remarks = items.Remarks,
                    StoreName = items.StoreName,
                    RequiredDate = items.RequiredDate,
                    InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString()),
                    InsertedON = DateTime.Now,
                    InsertedIPAddress = BISERP.Common.Constants.IpAddress,
                    InsertedMacID = BISERP.Common.Constants.MacId,
                    InsertedMacName = BISERP.Common.Constants.MacName,
                    WODeliveryTerms = items.WODeliveryTerms,
                    WOPaymenterms = items.WOPaymenterms,
                    WOOtherTerms = items.WOOtherTerms,
                    IndentDetails = items.IndentDetails.Where(d => d.state == true).ToList()
                };

                if (mindent.IndentId > 0)
                {
                    _url = url + "/WorkOrder/UpdateWorkOrder" + Common.Constants.JsonTypeResult;
                }
                else
                {
                    _url = url + "/WorkOrder/CreateWorkOrder" + Common.Constants.JsonTypeResult;
                }

                var result = client.PostAsync(_url, mindent, new JsonMediaTypeFormatter()).Result;

                if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    isSuccess = false;
                    if (mindent.IndentId == 0)
                    {
                        mindent = JsonConvert.DeserializeObject<WorkOrderModel>(result.Content.ReadAsStringAsync().Result);
                        errorMessage = mindent.Message;
                    }
                }
                else if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
                {
                    isSuccess = true;
                    if (mindent.IndentId == 0)
                    {
                        mindent = JsonConvert.DeserializeObject<WorkOrderModel>(result.Content.ReadAsStringAsync().Result);
                        indentNumber = mindent.IndentNumber;
                    }
                }

                return Json(new { success = isSuccess, Message = isSuccess ? indentNumber : errorMessage });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetWorkOrder(int AuthorizationStatusId)
        {
            try
            {
                string _url = $"{url}/WorkOrder/GetWorkOrder/{AuthorizationStatusId}{Common.Constants.JsonTypeResult}";
                List<WorkOrderModel> mimodel = await Common.AsyncWebCalls.GetAsync<List<WorkOrderModel>>(client, _url, CancellationToken.None);

                return Json(new { mimodel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetWorkOrder :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                return Json(new { error = "An error occurred while retrieving work orders." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetWorkOrderById(int IndentId)
        {
            JsonResult jResult;
            try
            {
                string _url = $"{url}/WorkOrder/GetWorkOrderById/{IndentId}{Common.Constants.JsonTypeResult}";
                WorkOrderModel _porder = await Common.AsyncWebCalls.GetAsync<WorkOrderModel>(client, _url, CancellationToken.None);

                if (_porder != null)
                {
                    _porder.IndentDetails.ForEach(m => m.state = true);
                    jResult = Json(_porder, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jResult = Json("Error", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetWorkOrderById :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                return Json(new { error = "An error occurred while retrieving work orders by id." }, JsonRequestBehavior.AllowGet);
            }
            return jResult;
        }


        [HttpPost]
        public async Task<JsonResult> AuthCancelWorkOrder(WorkOrderModel indent)
        {
            try
            {
                indent.InsertedBy = Convert.ToInt32(Session["AppUserId"]);
                indent.InsertedON = DateTime.Now;
                indent.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                indent.InsertedMacID = BISERP.Common.Constants.MacId;
                indent.InsertedMacName = BISERP.Common.Constants.MacName;

                string _url = $"{url}/WorkOrder/AuthCancelWorkOrder{Common.Constants.JsonTypeResult}";

                HttpResponseMessage response = await client.PostAsync(_url, indent, new JsonMediaTypeFormatter());

                bool isSuccess = response.IsSuccessStatusCode;
                WorkOrderModel workOrder = isSuccess ? JsonConvert.DeserializeObject<WorkOrderModel>(await response.Content.ReadAsStringAsync()): null;

                string message = isSuccess ? null : workOrder.Message;

                return Json(new { success = isSuccess, Message = message });
            }
            catch (Exception ex)
            {
                // Log exception (using your preferred logging framework)
                return Json(new { success = false, Message = "An error occurred while processing your request." });
            }
        }

        [HttpGet]
        public async Task<JsonResult> WOforReport()
        {
            JsonResult jResult;
            List<WorkOrderModel> mimodel = new List<WorkOrderModel>();
            try
            {
                string _url = url + "/WorkOrder/WOforReport" + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<WorkOrderModel>>(client, _url, CancellationToken.None);
                if (mimodel != null)
                {
                    //if (!string.IsNullOrWhiteSpace(searchIssueNumber))
                    //{
                    //    mimodel = mimodel.Where(p => p.IndentNumber.ToUpper().Contains(searchIssueNumber.ToUpper())).ToList();
                    //}
                    mimodel = mimodel.OrderByDescending(m => m.IndentId).ToList();
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in WO for Report :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
