using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using BISERPCommon;
using System.Web.Mvc;
using BISERP.Areas.Accounts.Models;
using System.Threading.Tasks;
using System.Threading;
using BISERP.Areas.Store.Models.Store;
using BISERP.Filters;

namespace BISERP.Areas.Accounts.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class ClientBillCancelController : Controller
    {
       HttpClient client;
       string url = BISERP.Common.Constants.WebAPIAddress;
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(ClientBillCancelController));
        public ClientBillCancelController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpPost]
        public async Task<JsonResult> CancelGeneratedBill(int billingId)
        {
            string _url = "";
            ClientBillingModel _model = new ClientBillingModel();
            _model.ClientBillId = billingId;
            _model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            _model.InsertedOn = DateTime.Now;
            _model.InsertedIpAddress = Common.Constants.IpAddress;
            _model.InsertedMacId = Common.Constants.MacId;
            _model.InsertedMacName = BISERP.Common.Constants.MacName;
            var result = await Common.AsyncWebCalls.PutAsync("/clientBillCancel/cancelGeneratedBill", _model, CancellationToken.None);
            if (result.IsSuccessStatusCode == false)
            {
                return Json(new { success = false, Message = "Bill cannot be cancelled as Reciepts on this Bill exist" });
            }
            return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }
        [HttpPost]
        public async Task<JsonResult> CancelRecieptBill(int recieptId)
        {
            string _url = "";
            ClientRecieptModel _model = new ClientRecieptModel();
            _model.RecieptId = recieptId;
            _model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            _model.InsertedOn = DateTime.Now;
            _model.InsertedIpAddress = Common.Constants.IpAddress;
            _model.InsertedMacId = Common.Constants.MacId;
            _model.InsertedMacName = BISERP.Common.Constants.MacName;
            var result = await Common.AsyncWebCalls.PutAsync("/clientBillCancel/cancelRecieptBill", _model, CancellationToken.None);
            return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false });
        }
        [HttpGet]
        public async Task<JsonResult> GetTaxCredited(int projectId)
        {
            List<GRNModel> records = new List<GRNModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/clientBillCancel/GetTaxCredited/" + projectId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<GRNModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Detail Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetTaxCredited no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetTaxPaid(int projectId)
        {
            List<ClientBillingModel> records = new List<ClientBillingModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/clientBillCancel/GetTaxPaid/" + projectId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ClientBillingModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Detail Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in ClientBillingModel no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
