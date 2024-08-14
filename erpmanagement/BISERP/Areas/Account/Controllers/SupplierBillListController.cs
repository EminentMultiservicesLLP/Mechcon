using BISERP.Areas.Masters.Models;
using BISERP.Areas.Accounts.Models;
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

namespace BISERP.Areas.Account.Controllers
{
    public class SupplierBillListController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(SupplierBillListController));

        public SupplierBillListController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
    
        [HttpGet]
        public async Task<JsonResult> GetSupplierBilldt(int SupplierId)
        {
            JsonResult jResult;
            List<SupplierBillModel> records;
            try
            {

                string _url = url + "/supplierbillList/getsupplierbillListdt/" + SupplierId.ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<SupplierBillModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    records.ForEach(m => m.state = false);
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("No Record Found");

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetSupplierBilldt :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetAllSupplierBillList()
        {
            JsonResult jResult;
            List<SupplierMasterModel> records;
            try
            {
                string _url = url + "/supplierbillList/getsupplierbillList" + Common.Constants.JsonTypeResult;
                 records = await Common.AsyncWebCalls.GetAsync<List<SupplierMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllSupplierBillList :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
      


    }
}
