using BISERP.Areas.Store.Models.Store;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Miscellaneous.Models;
using BISERP.Areas.Accounts.Models;

namespace BISERP.Areas.Store.Controllers
{
    public class BillCreationController : Controller
    {
      HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(BillCreationController));

        public BillCreationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetSupplierBill(DateTime fromdate, DateTime todate,int SupplierID )
        {
            JsonResult jResult;
            List<SupplierBillModel> records;
            try
            {
                string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
                string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
                string _url = url + "/billcreation/getsupplierbill/" + "/" + SupplierID.ToString() + "/" + strfromdate + "/" + strtodate + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<SupplierBillModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {

                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetSupplierBill :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetSupplierBilldt(int BillId)
        {
            JsonResult jResult;
            List<GRNDetailModel> records;
            try
            {

                string _url = url + "/billcreation/getsupplierbilldt/" + BillId.ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<GRNDetailModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {

                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetSupplierBilldt :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

       
        [HttpPost]
        public ActionResult SaveCancelMaterialIndent(List<CancelPendingMaterialIndentModel> entity)
        {
            string _url = "";

            List<CancelPendingMaterialIndentModel> modelList = new List<CancelPendingMaterialIndentModel>();
            foreach (var item in entity)
            {
                CancelPendingMaterialIndentModel model = new CancelPendingMaterialIndentModel();
                model = item;
                model.AuthorizedBy = 1;
                model.AuthorizedOn = System.DateTime.Now;
                //item.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                //item.InsertedMacID = BISERP.Common.Constants.MacId;
                //item.InsertedMacName = BISERP.Common.Constants.MacName;                
                modelList.Add(model);
            }

            _url = url + "/cancelMI/createcancelMI" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, entity, new JsonMediaTypeFormatter()).Result.Content.ReadAsAsync<CancelPendingMaterialIndentModel>().Id;

            return Json(new { success = true });
        }

    }
}
