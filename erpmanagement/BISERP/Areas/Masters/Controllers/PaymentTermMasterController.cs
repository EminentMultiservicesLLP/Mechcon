using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Masters.Models;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using BISERP.Filters;

namespace BISERP.Areas.Store.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class PaymentTermMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(PaymentTermMasterController));

        public PaymentTermMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        //[HttpGet]
        //public async Task<JsonResult> AllPaymentTerm()
        //{
        //    JsonResult jResult;
        //    try
        //    {
        //        string _url = url + "/payterms/getallpayterms" + Common.Constants.JsonTypeResult;
        //        var records = await Common.AsyncWebCalls.GetAsync<List<PaymentTermMasterModel>>(client, _url, CancellationToken.None);
        //        if (records != null && records.Count > 0)
        //        {
                  
        //            jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //            jResult = Json("Error");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error in PaymentTerms :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
        //        jResult = Json("Error");
        //    }
        //    return jResult;
        //}
        [HttpGet]
        public async Task<JsonResult> AllPaymentTerm( string searchPaymentTerm, string searchCode)
        {
            List<PaymentTermMasterModel> Pimodel = new List<PaymentTermMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/payterms/getallpayterms" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<PaymentTermMasterModel>>(client, _url, CancellationToken.None);
               
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchPaymentTerm))
                    {
                        records = records.Where(p => p.PaymentTermDesc.ToUpper().StartsWith(searchPaymentTerm.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(searchCode))
                    {
                        records = records.Where(p => p.PaymentTermCode.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                    }
                    int total = records.Count;
                   
                    jResult = Json(new {success=true, records, total }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Details found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllPaymentTerm :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Server Error! Please Contact administrator!");
            }
            return jResult;
        }
        [HttpPost]
        public ActionResult SavePaymentTerm(PaymentTermMasterModel PaymentTerm)
        {
            string _url = "";
            bool _isSuccess = true;
            PaymentTerm.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            PaymentTerm.InsertedON = System.DateTime.Now;
            PaymentTerm.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            PaymentTerm.InsertedMacID = BISERP.Common.Constants.MacId;
            PaymentTerm.InsertedMacName = BISERP.Common.Constants.MacName;
            if (PaymentTerm.TermID > 0)
            {
                _url = url + "/payterms/updatepayterm" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, PaymentTerm, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    PaymentTerm = JsonConvert.DeserializeObject<PaymentTermMasterModel>(result.Content.ReadAsStringAsync().Result);
                }

                if (!_isSuccess)
                    return Json(new { success = false, Message = PaymentTerm.Message });
                else
                    return Json(new { success = true });
            }
            else
            {
                _url = url + "/payterms/createpayterm" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, PaymentTerm, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    PaymentTerm = JsonConvert.DeserializeObject<PaymentTermMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            //return PartialView();
            if (!_isSuccess)
                return Json(new { success = false, Message = PaymentTerm.Message });
            else
                return Json(new { success = true });
        }

        [HttpGet]
        public async Task<JsonResult> GetActivePaymentTerm(string searchPaymentTerm, string searchCode)
        {
            JsonResult jResult;
            List<PaymentTermMasterModel> records = new List<PaymentTermMasterModel>();
            try
            {
                string _url = url + "/payterms/getactive" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<PaymentTermMasterModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(searchPaymentTerm))
                    {
                        records = records.Where(p => p.PaymentTermDesc.ToUpper().StartsWith(searchPaymentTerm.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(searchCode))
                    {
                        records = records.Where(p => p.PaymentTermCode.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                    }                    
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetActivePaymentTerm :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        public async Task<JsonResult> GetPaymentTermSession(int paymenttemId, bool state)
        {
            JsonResult jlResult;
            List<PaymentTermMasterModel> records = new List<PaymentTermMasterModel>();
            if (Session["PaymentTermSession"] == null)
            {
                string _url = url + "/payterms/getactive" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<PaymentTermMasterModel>>(client, _url, CancellationToken.None);
                Session["PaymentTermSession"] = records.ToList();
                jlResult = Json( records , JsonRequestBehavior.AllowGet);
            }
            else
            {
                var jResult = Session["PaymentTermSession"];
                var sessionRecords = jResult as List<PaymentTermMasterModel>;
                if (paymenttemId!=0)
                {
                    sessionRecords.Where(m => m.TermID == paymenttemId).ToList()[0].State = state;
                }
                var Result = Json( sessionRecords , JsonRequestBehavior.AllowGet);
                return Result;
            }
            return jlResult;
        }
    }
}
