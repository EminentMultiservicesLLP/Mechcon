using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Accounts.Models;
using System.Threading.Tasks;
using System.Threading;
using BISERPCommon;
using Newtonsoft.Json;
using System.Net.Http;
using BISERP.Areas.Store.Models.Store;
using System.Net.Http.Formatting;
using BISERP.Filters;

namespace BISERP.Areas.Accounts.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class SupplierBillingController : Controller
    {

        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(SupplierBillingController));


        public SupplierBillingController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpPost]
        public async Task<ActionResult> CreateSupplierBill(SupplierBillingModel items)
        {
            string _url = "";
            bool _isSuccess = true;
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedOn = DateTime.Now;
            items.InsertedIpAddress = Common.Constants.GetIPAddress();
            items.InsertedMacId = Common.Constants.MacId;
            items.InsertedMacName = Common.Constants.MacName;
            //items.GRNId = items..ID;
            _url = url + "/SupplierBilling/createSupplierBill" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, items, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
            }
            if (result.StatusCode.ToString() == "OK")
            {
                _isSuccess = true;
                items = JsonConvert.DeserializeObject<SupplierBillingModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = "Bill Not Processed" });
            else
                return Json(new { success = true, billNo = items.SupplierbillNo });
        }
        [HttpGet]
        public async Task<JsonResult> GetPoBySupplierId(int supplierId,int vendorid)
        {
            List<SupplierBillingModel> records = new List<SupplierBillingModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/SupplierBilling/GetPoBySupplierId/" + supplierId+"/" + vendorid + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<SupplierBillingModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "PO Number Not  Found" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetPoBySupplierId no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetGRNbyPOId(int POId, int supplier, int vendor)
        {
            List<SupplierBillingdtModel> records = new List<SupplierBillingdtModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/SupplierBilling/GetGRNbyPOId/" + POId + "/" + supplier + "/" + vendor+Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<SupplierBillingdtModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "PO Number Not  Found" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetGRNbyPOId no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }



        [HttpGet]
        public async Task<JsonResult> GetAllGRNForSupplier()
        {
            List<SupplierBillingdtModel> records = new List<SupplierBillingdtModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/SupplierBilling/GetAllGRNForSupplier/" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<SupplierBillingdtModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "GRN  Not  Found" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetAllGRNForSupplier no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> getGRNbySupplierId( int Supid=0)
        {
            List<SupplierBillingdtModel> records = new List<SupplierBillingdtModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/SupplierBilling/getGRNbySupplierId/"+Supid + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<SupplierBillingdtModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "GRN  Not  Found" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Logger.LogError("Error in getGRNbySupplierId no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetSummarizedBill(int GRNId)
        {
            List<SupplierBillingdtModel> records = new List<SupplierBillingdtModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/SupplierBilling/GetSummarizedBill/" + GRNId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<SupplierBillingdtModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "Bill Not  Found" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetSummarizedBill no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public async Task<JsonResult> UpdateFileNamegrnBill(int billingId)
        {
            string _url = "";
            SupplierBillingdtModel items = new SupplierBillingdtModel();
            items.GRNId = billingId;
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedOn = DateTime.Now;
            items.InsertedIpAddress = Common.Constants.GetIPAddress();
            items.InsertedMacId = Common.Constants.MacId;
            items.InsertedMacName = Common.Constants.MacName;
            var result = await Common.AsyncWebCalls.PutAsync("/SupplierBilling/UpdateFileNamegrnBill", items, CancellationToken.None);
            return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }


        [HttpPost]
        public async Task<JsonResult> CancelSupplierBill(int billingId)
        {
            string _url = "";
            SupplierBillingdtModel items = new SupplierBillingdtModel();
            items.SupplierbillId = billingId;
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedOn = DateTime.Now;
            items.InsertedIpAddress = Common.Constants.GetIPAddress();
            items.InsertedMacId = Common.Constants.MacId;
            items.InsertedMacName = Common.Constants.MacName;
            var result = await Common.AsyncWebCalls.PutAsync("/SupplierBilling/CancelSupplierBill", items, CancellationToken.None);
            return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }


        /*************************vendor billing Area***********************/
        [HttpGet]
        public async Task<JsonResult> GetGRNbyVendorId(int vendorId)
        {
            List<VendorBillingDtlModel> records = new List<VendorBillingDtlModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/SupplierBilling/GetGRNbyVendorId/" + vendorId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<VendorBillingDtlModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "GRN Number Not  Found" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetGRNbyVendorId no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpPost]
        public async Task<ActionResult> CreateVendorBill(VendorBillingModel items)
        {
            string _url = "";
            bool _isSuccess = true;
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedOn = DateTime.Now;
            items.InsertedIpAddress = Common.Constants.GetIPAddress();
            items.InsertedMacId = Common.Constants.MacId;
            items.InsertedMacName = Common.Constants.MacName;
            _url = url + "/SupplierBilling/createvendorBill" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, items, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
            }
            if (result.StatusCode.ToString() == "OK")
            {
                _isSuccess = true;
                items = JsonConvert.DeserializeObject<VendorBillingModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = "Bill Not Processed" });
            else
                return Json(new { success = true, billNo = items.VendorbillNo });
        }

        [HttpGet]
        public async Task<JsonResult> GetVendorSummarizedBill(int GRNId)
        {
            List<VendorBillingDtlModel> records = new List<VendorBillingDtlModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/SupplierBilling/GetVendorSummarizedBill/" + GRNId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<VendorBillingDtlModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "Bill Not  Found" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetVendorSummarizedBill no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public async Task<JsonResult> CancelVendorBill(int billingId)
        {
            string _url = "";
            VendorBillingModel items = new VendorBillingModel();
            items.VendorbillId = billingId;
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedOn = DateTime.Now;
            items.InsertedIpAddress = Common.Constants.GetIPAddress();
            items.InsertedMacId = Common.Constants.MacId;
            items.InsertedMacName = Common.Constants.MacName;
            var result = await Common.AsyncWebCalls.PutAsync("/SupplierBilling/CancelVendorBill", items, CancellationToken.None);
            return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }
    }
}
