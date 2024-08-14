using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using BISERP.Areas.Accounts.Models;
using BISERPCommon;
using Newtonsoft.Json;
using System.Linq;
using System.Web.UI;
using BISERP.Areas.Masters.Models;
using BISERP.Filters;

namespace BISERP.Areas.Accounts.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class ClientBillingController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(ClientBillingController));

        public ClientBillingController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpPost]
        public async Task<ActionResult> CreateClientBill(ClientBillingModel items)
        {
            string _url = "";
            bool _isSuccess = true;

            var records = Session["PaymentTermSession"] as List<PaymentTermMasterModel>;
            if(records!=null)
            { 
            var Temp = records.Where(m => m.State == true).ToList();
            if(Temp.Count!=0)
            { items.PaymentTerm = Temp; }
            }
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedOn = DateTime.Now;
            items.InsertedIpAddress = Common.Constants.IpAddress;
            items.InsertedMacId = Common.Constants.MacId;
            items.InsertedMacName = Common.Constants.MacName;

            Logger.LogError(
                   "Error in CreateClientBillmST method of ClientBilling request Controler : parameter :" + items.BillDate + " BillDate/DufeDate " + items.DueDate +
                   Environment.NewLine);

            _url = url + "/clientbilling/createClientbill" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, items, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    Session["PaymentTermSession"] = null;
                }
                if (result.StatusCode.ToString() == "OK")
                {
                    _isSuccess = true;
                    items = JsonConvert.DeserializeObject<ClientBillingModel>(result.Content.ReadAsStringAsync().Result);
                    Session["PaymentTermSession"] = null;
                }
                if (!_isSuccess)
                    return Json(new { success = false, Message = "Bill Not Processed" });
                else
                    return Json(new { success = true, billNo = items.BillNo });
            }
         [HttpPost]
        public async Task<ActionResult> RecieptClientBill(ClientRecieptModel items)
        {
            string _url = "";
            bool _isSuccess = true;
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedOn = DateTime.Now;
            items.InsertedIpAddress = Common.Constants.IpAddress;
            items.InsertedMacId = Common.Constants.MacId;
            items.InsertedMacName = Common.Constants.MacName;
            _url = url + "/clientbilling/recieptClientbill" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, items, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
            }
            if (result.StatusCode.ToString() == "OK")
            {
                _isSuccess = true;
                items = JsonConvert.DeserializeObject<ClientRecieptModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = "Bill Not Processed" });
            else
                return Json(new { success = true, responseText = items.RecieptNo });
        }
        [HttpGet]
        public async Task<JsonResult> GetClienBillNo(int branchId)
        {
            List<ClientBillingModel> records = new List<ClientBillingModel>();
            JsonResult jResult;
            try
            {
               string _url = url + "/clientbilling/getClienBillNo/" + branchId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ClientBillingModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count>0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "Bill Number Not  Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetClientbill no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
          [HttpGet]
        public async Task<JsonResult> GetClienBilldeatailById(int clientBillId)
        {
            List<ClientBillingDtModel> records = new List<ClientBillingDtModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/clientbilling/getClienBilldeatailById/" + clientBillId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ClientBillingDtModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetClienBilldeatailById no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        /*************************Client Bill Reciep Area********************************************/
          [HttpGet]
          public async Task<JsonResult> GetClienBillRecieptByBillingId(int clientBillId)
          {
              List<ClientRecieptModel> records = new List<ClientRecieptModel>();
              JsonResult jResult;
              try
              {
                  string _url = url + "/clientbilling/getClienBillRecieptByBillingId/" + clientBillId + Common.Constants.JsonTypeResult;
                  records = await Common.AsyncWebCalls.GetAsync<List<ClientRecieptModel>>(client, _url, CancellationToken.None);
                  if (records != null)
                  {
                      jResult = Json(records, JsonRequestBehavior.AllowGet);
                  }
                  else
                      jResult = Json("Error");
              }
              catch (Exception ex)
              {
                  Logger.LogError("Error in GetClienBillRecieptByBillingId no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                  jResult = Json("Error");
              }
              return jResult;
          }

        [HttpGet]
        public async Task<JsonResult> GetAllPaymentModes()
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<PayModeModel>>("/clientbilling/GetAllPaymentModes", CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllActiveBudgetHeads :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetClienRecieptdeatailById(int recieptId)
        {
            List<ClientRecieptDtModel> records = new List<ClientRecieptDtModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/clientbilling/getClienRecieptdeatailById/" + recieptId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ClientRecieptDtModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetClienRecieptdeatailById :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetSummary(int clientId,int projectId)
        {

            JsonResult jResult;
            try
            {
                string _url = url + "/clientbilling/GetSummary" + "/" + clientId + "/" + projectId +Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<ClientBillingModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Consignee Entry found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetSummary :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json(new { success = false, Messsage = "please contact Administrator." }, JsonRequestBehavior.AllowGet);
            }
            return jResult;
        }

        }
    }

