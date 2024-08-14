using BISERP.Areas.Purchase.Models;
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

namespace BISERP.Areas.Purchase.Controllers
{

    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class RequestForQuoteController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(RequestForQuoteController));

        public RequestForQuoteController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveRequestForQuote(RequestForQuoteModel items)
        {
            string _url = "";
            bool _isSuccess = true;
            RequestForQuoteModel mindent = new RequestForQuoteModel();
            List<RequestForQuoteDetailModel> _indentDetails = new List<RequestForQuoteDetailModel>();
            RequestForQuoteDetailModel mdt;
            foreach (var IndentDt in items.IndentDetails)
            {
                if (IndentDt.state == true)
                {
                    mdt = new RequestForQuoteDetailModel();
                    mdt = IndentDt;
                    _indentDetails.Add(mdt);
                }
            }
            mindent.IndentId = items.IndentId;
            mindent.IndentNumber = items.IndentNumber;
            mindent.IndentDate = items.IndentDate;
            mindent.IndentNature = items.IndentNature;
            mindent.QuotationDeadLine = items.QuotationDeadLine;
            mindent.PurchaseIndentId = items.PurchaseIndentId;
            mindent.Storeid = items.Storeid;
            mindent.ItemCategoryId = items.ItemCategoryId;
            //mindent.SupplierID = items.SupplierID;
            //mindent.SupplierName = items.SupplierName;
            mindent.Remarks = items.Remarks;
            mindent.StoreName = items.StoreName;
            mindent.RequiredDate = items.RequiredDate;
            mindent.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            mindent.InsertedON = System.DateTime.Now;
            mindent.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            mindent.InsertedMacID = BISERP.Common.Constants.MacId;
            mindent.InsertedMacName = BISERP.Common.Constants.MacName;

            mindent.IndentDetails = _indentDetails;
            mindent.RFQDeliveryTerms = items.RFQDeliveryTerms;
            mindent.RFQPaymenterms = items.RFQPaymenterms;

            if (mindent.IndentId > 0)
            {
                _url = url + "/RequestForQuote/UpdateRequestForQuote" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, mindent, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                }
                else if (result.StatusCode.ToString() == "OK")
                {
                    _isSuccess = true;
                }
                if (!_isSuccess)
                    return Json(new { success = false });
                else
                    return Json(new { success = true });
            }
            else
            {
                _url = url + "/RequestForQuote/CreateRequestForQuote" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, mindent, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    mindent =
                        JsonConvert.DeserializeObject<RequestForQuoteModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    mindent =
                        JsonConvert.DeserializeObject<RequestForQuoteModel>(result.Content.ReadAsStringAsync().Result);
                }
                if (!_isSuccess)
                {
                    return Json(new { success = false, Message = mindent.Message, Data = mindent });
                }
                else
                {
                    return Json(new { success = true, Message = mindent.IndentNumber, Data = mindent });
                }
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetAllRequestForQuote(string searchIssueNumber)
        {
            JsonResult jResult;
            List<RequestForQuoteModel> mimodel = new List<RequestForQuoteModel>();
            try
            {
                string _url = url + "/RequestForQuote/GetAllRequestForQuote" + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<RequestForQuoteModel>>(client, _url, CancellationToken.None);
                if (mimodel != null)
                {
                    if (!string.IsNullOrWhiteSpace(searchIssueNumber))
                    {
                        mimodel = mimodel.Where(p => p.IndentNumber.ToUpper().Contains(searchIssueNumber.ToUpper())).ToList();
                    }
                    mimodel = mimodel.OrderByDescending(m => m.IndentId).ToList();
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllRequestForQuote :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetRequestForQuoteById(int IndentId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/RequestForQuote/GetRequestForQuoteById/" + IndentId.ToString() + Common.Constants.JsonTypeResult;
                //var indent = await Common.AsyncWebCalls.GetAsync<RequestForQuoteModel>(client, _url, CancellationToken.None);
                RequestForQuoteModel _porder = await Common.AsyncWebCalls.GetAsync<RequestForQuoteModel>(client, _url, CancellationToken.None);
                //List<RequestForQuoteDetailModel> _indentDetails = indent.IndentDetails;

                if (_porder != null)
                {
                    _porder.IndentDetails.ForEach(m => m.state = true);
                    jResult = Json(_porder, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetRequestForQuoteById :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public JsonResult AuthCancelRequestForQuote(RequestForQuoteModel indent)
        {
            string _url = "";
            bool _isSuccess = false;
            RequestForQuoteModel RFQ = new RequestForQuoteModel();
            List<RequestForQuoteDetailModel> _indentDetails = new List<RequestForQuoteDetailModel>();
            RequestForQuoteDetailModel RFQdt;
            foreach (var IndentDt in indent.IndentDetails)
            {
                RFQdt = new RequestForQuoteDetailModel();
                RFQdt = IndentDt;
                if (IndentDt.state == true)
                {
                }
                else
                {
                    RFQdt.AuthorisedQty = 0;
                }
                _indentDetails.Add(RFQdt);
            }
            RFQ.IndentId = indent.IndentId;
            RFQ.AuthorizationStatusId = indent.AuthorizationStatusId;
            RFQ.AuthorisedRemarks = indent.AuthorisedRemarks;
            RFQ.SupplierID = indent.SupplierID;
            RFQ.SupplierName = indent.SupplierName;
            RFQ.IndentDetails = _indentDetails;          
            RFQ.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            RFQ.InsertedON = System.DateTime.Now;
            RFQ.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            RFQ.InsertedMacID = BISERP.Common.Constants.MacId;
            RFQ.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/RequestForQuote/AuthCancelRequestForQuote" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, RFQ, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                RFQ = JsonConvert.DeserializeObject<RequestForQuoteModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "OK")
            {
                _isSuccess = true;
                RFQ = JsonConvert.DeserializeObject<RequestForQuoteModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = RFQ.Message });
            else
                return Json(new { success = true });
        }

        [HttpGet]
        public async Task<JsonResult> GetAuthorizedRequestForQuote(int StoreId, string IndentNo)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/RequestForQuote/GetAuthorizedRequestForQuote/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                List<RequestForQuoteModel> records = await Common.AsyncWebCalls.GetAsync<List<RequestForQuoteModel>>(client, _url, CancellationToken.None);
                if (!string.IsNullOrWhiteSpace(IndentNo))
                {
                    records = records.Where(s => s.IndentNumber.ToUpper().StartsWith(IndentNo.ToUpper())).ToList();
                }
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "Indent Number Not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAuthorizedRequestForQuote :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> RFQforReport()
        {
            JsonResult jResult;
            List<RequestForQuoteModel> mimodel = new List<RequestForQuoteModel>();
            try
            {
                string _url = url + "/RequestForQuote/RFQforReport" + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<RequestForQuoteModel>>(client, _url, CancellationToken.None);
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
                _logger.LogError("Error in RFQ for Report :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
