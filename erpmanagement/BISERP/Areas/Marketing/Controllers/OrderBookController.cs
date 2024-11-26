using BISERP.Areas.Marketing.Models;
using BISERP.Filters;
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

namespace BISERP.Areas.Marketing.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class OrderBookController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(OrderBookController));

        public OrderBookController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetEnqForOrderBook()
        {
            int UserID = Convert.ToInt32(Session["AppUserId"].ToString());
            string _url = $"{url}/orderBook/getEnqForOrderBook/{UserID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<EnquiryRegisterModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetEnqForOrderBook: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetConsignee(int? enquiryId)
        {
            string _url = $"{url}/orderBook/getConsignee/{enquiryId}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<ConsigneeModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetConsignee: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving Consignee" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public async Task<JsonResult> GetFinalOffer(int EnquiryID)
        {
            string _url = $"{url}/orderBook/getFinalOffer/{EnquiryID}{Common.Constants.JsonTypeResult}";

            try
            {
                var record = await Common.AsyncWebCalls.GetAsync<OfferDetailModel>(client, _url, CancellationToken.None);

                if (record == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, record }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetEnqForOrderBook: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveOrderBook(OrderBookModel model)
        {
            string _url = "";
            bool _isSuccess = true;

            try
            {
                model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                model.InsertedON = System.DateTime.Now;
                model.InsertedMacID = BISERP.Common.Constants.MacId;
                model.InsertedMacName = BISERP.Common.Constants.MacName;
                model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                model.UpdatedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                model.UpdatedOn = System.DateTime.Now;
                model.UpdatedMacID = BISERP.Common.Constants.MacId;
                model.UpdatedMacName = BISERP.Common.Constants.MacName;
                model.UpdatedIPAddress = BISERP.Common.Constants.IpAddress;

                _url = url + "/orderBook/saveOrderBook" + Common.Constants.JsonTypeResult;

                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<OrderBookModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    _isSuccess = true;
                    model = JsonConvert.DeserializeObject<OrderBookModel>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    _isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _isSuccess = false;
                _logger.LogError("Error in SaveOrderBook method:" + Environment.NewLine + ex.ToString());
            }
            if (!_isSuccess)
            {
                return Json(new { success = false, Message = "Error while saving Enquiry" });
            }
            else
            {
                return Json(new { success = true, Message = "Enquiry saved successfully", Data = model });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOrderBook()
        {
            int UserID = Convert.ToInt32(Session["AppUserId"].ToString());
            string _url = $"{url}/orderBook/getOrderBook/{UserID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<OrderBookModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetEnquiry: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOBOtherDetails(int OrderBookID)
        {
            string _url = $"{url}/orderBook/getOBOtherDetails/{OrderBookID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<OrderBookOtherDetail>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetOBOtherDetails: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving GetOBOtherDetails" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOrderBookProjectTC(int OrderBookID)
        {
            string _url = $"{url}/orderBook/getOrderBookProjectTC/{OrderBookID}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<ProjectTCDetails>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No project TC details found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetOrderBookProjectTC: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving project TC details" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOrderBookPaymentTerms(int OrderBookID)
        {
            string _url = $"{url}/orderBook/getOrderBookPaymentTerms/{OrderBookID}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<PaymentTermDetails>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No payment terms found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetOrderBookPaymentTerms: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving payment terms" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOrderBookDeliveryTerms(int OrderBookID)
        {
            string _url = $"{url}/orderBook/getOrderBookDeliveryTerms/{OrderBookID}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<DeliveryTermDetails>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No delivery terms found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetOrderBookDeliveryTerms: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving delivery terms" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public async Task<JsonResult> GetIncoTerm()
        {
            try
            {
                string _url = $"{url}/orderBook/getIncoTerm{Common.Constants.JsonTypeResult}";
                List<IncoTermModel> incoTerm = await Common.AsyncWebCalls.GetAsync<List<IncoTermModel>>(client, _url, CancellationToken.None);

                if (incoTerm == null || !incoTerm.Any())
                {
                    return Json(new { error = "No incoTermes found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(incoTerm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetIncoTerm method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the incoTermes." }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
