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
using BISERP.Areas.Asset.Models;
using BISERP.Areas.Miscellaneous.Models;

namespace BISERP.Areas.Miscellaneous.Controllers
{
    public class StoreQuantityLimitsController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(StoreQuantityLimitsController));

        public StoreQuantityLimitsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> Getallitemlimits(int StoreId, int? ItemTypeId)
        {
            JsonResult jResult;

            try
            {
                if (ItemTypeId == null)
                    ItemTypeId = 0;

                string _url = url + "sqli/getallitemlimits/" + StoreId.ToString() + "/" + ItemTypeId.ToString() + Common.Constants.JsonTypeResult;
                List<StoreQuantityLimits> records = await Common.AsyncWebCalls.GetAsync<List<StoreQuantityLimits>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    //jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    //jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Getallitemlimits :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveStoreQuantityLimits(List<StoreQuantityLimits> griditems)
        {
            string _url = "";
            List<StoreQuantityLimits> modelList = new List<StoreQuantityLimits>();
            foreach (var item in griditems)
            {
                StoreQuantityLimits model = new StoreQuantityLimits();

                //item.ID = model.ID;
                //item.MaxLevel = model.MaxLevel;
                //item.MinLevel = model.MinLevel;
                //item.OPBalance = model.OPBalance;
                //item.storeId = model.storeId;
                //item.Packsize = model.Packsize;
                //item.ReOrderLevel = model.ReOrderLevel;
                model = item;
                modelList.Add(model);
            }

            _url = url + "/sqli/Createstoreql" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, modelList, new JsonMediaTypeFormatter()).Result.Content.ReadAsAsync<StoreQuantityLimits>().Id;

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<JsonResult> GetNotificationQuantity()
        {
            JsonResult jResult;

            try
            {
                string _url = url + "sqli/getnotifyQty/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                List<StoreQuantityLimits> records = await Common.AsyncWebCalls.GetAsync<List<StoreQuantityLimits>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetNotificationQuantity :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }



        [HttpGet]
        public async Task<JsonResult> GetMasterReport(int StoreId, DateTime? FromDate, DateTime? ToDate)
        {
            JsonResult jResult;
            List<MasterReportModel> model = new List<MasterReportModel>();
            try
            {
                string strfromdate = ""; string strtodate = "";
                if (FromDate == null)
                    strfromdate = System.DateTime.UtcNow.ToString("MM-dd-yy");
                else
                    strfromdate = Convert.ToDateTime(FromDate).ToString("MM-dd-yy");

                if (ToDate == null)
                    strtodate = System.DateTime.UtcNow.ToString("MM-dd-yy");
                else
                    strtodate = Convert.ToDateTime(ToDate).ToString("MM-dd-yy");
                string _url = url + "/sqli/GetMasterReport/" + StoreId + "/" + strfromdate + "/" + strtodate + Common.Constants.JsonTypeResult;
                model = await Common.AsyncWebCalls.GetAsync<List<MasterReportModel>>(client, _url, CancellationToken.None);
                if (model != null)
                {
                    jResult = Json(new { success = true, model }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetMasterReport :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }


        [HttpGet]
        public async Task<JsonResult> GetProjectBudgetConclusion(int StoreId, DateTime? FromDate, DateTime? ToDate)
        {
            JsonResult jResult;
            List<ProjectBudgetConclusion> model = new List<ProjectBudgetConclusion>();
            try
            {
                string strfromdate = ""; string strtodate = "";
                if (FromDate == null)
                    strfromdate = System.DateTime.UtcNow.ToString("MM-dd-yy");
                else
                    strfromdate = Convert.ToDateTime(FromDate).ToString("MM-dd-yy");

                if (ToDate == null)
                    strtodate = System.DateTime.UtcNow.ToString("MM-dd-yy");
                else
                    strtodate = Convert.ToDateTime(ToDate).ToString("MM-dd-yy");
                string _url = url + "/sqli/GetProjectBudgetConclusion/" + StoreId + "/" + strfromdate + "/" + strtodate + Common.Constants.JsonTypeResult;
                model = await Common.AsyncWebCalls.GetAsync<List<ProjectBudgetConclusion>>(client, _url, CancellationToken.None);
                if (model != null)
                {
                    jResult = Json(new { success = true, model }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetMasterReport :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }
    }
}
