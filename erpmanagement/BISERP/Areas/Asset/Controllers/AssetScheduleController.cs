using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using BISERP.Areas.Asset.Models;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace BISERP.Areas.Asset.Controllers
{
    public class AssetScheduleController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(AssetScheduleController));

        public AssetScheduleController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveAssetSchedule(AssetScheduleModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/assetschedule/createassetschedule" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<AssetScheduleModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false,Message = "Schedule Already Exist Or Error in Save." });
            else
                return Json(new { success = true });
        }

        [HttpGet]
        public async Task<JsonResult> GetAssetSchedule(DateTime fromdate, DateTime todate, int BranchId)
        {
            List<AssetScheduleDetailsModel> records = new List<AssetScheduleDetailsModel>();
            JsonResult jResult;
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            try
            {
                string _url = url + "/assetschedule/GetAssetSchedule/" + strfromdate + "/" + strtodate + "/" + BranchId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<AssetScheduleDetailsModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAssetcodeSchedule :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AssetScheduleReport(DateTime fromdate, DateTime todate, int MaintenanceTypeId)
        {
            List<AssetScheduleDetailsModel> records = new List<AssetScheduleDetailsModel>();
            JsonResult jResult;
            string strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");
            string strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");
            try
            {
                string _url = url + "/assetschedule/GetAssetSchedule/" + strfromdate + "/" + strtodate + "/" + MaintenanceTypeId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<AssetScheduleDetailsModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AssetScheduleReport :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        //[HttpGet]
        //public async Task<JsonResult> GetAssetcodeScheduledt( int ScheduleId)
        //{
        //    List<AssetScheduleDetailsModel> records = new List<AssetScheduleDetailsModel>();
        //    JsonResult jResult;
        //    try
        //    {
        //        string _url = url + "/assetschedule/GetAssetcodeScheduledt/" + ScheduleId + Common.Constants.JsonTypeResult;
        //        records = await Common.AsyncWebCalls.GetAsync<List<AssetScheduleDetailsModel>>(client, _url, CancellationToken.None);
        //        if (records != null)
        //        {
        //            jResult = Json(records, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //            jResult = Json("Error");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error in GetAssetcodeSchedule :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
        //        jResult = Json("Error");
        //    }
        //    return jResult;
        //}

        [HttpPost]
        public ActionResult SaveAssetScheduleCompletion(List<AssetScheduleCompletionModel> items)
        {
            string _url = "";
            bool _isSuccess = true;
            string strErrorMsg = "";
            List<AssetScheduleCompletionModel> modelList = new List<AssetScheduleCompletionModel>();
            foreach (var item in items)
            {
                if (item != null)
                {  
                   // DateTime str = Convert.ToDateTime( 1/1/0001 12:00:00 AM);
                    string str =Convert.ToDateTime(item.CompletedDate).ToString("MM-dd-yy");
                    string str1 = "01-01-01";
                    if (str != str1 )
                    {
                        if (item.DoneBy == null)
                        {
                            strErrorMsg = "Please Enter DoneBy.";
                            break;
                        }
                        if (item.CompletionRemark == null)
                        {
                            strErrorMsg = "Please Enter Reason  for  CompletionRemark.";
                            break;
                        }
                       
                        else
                        {
                            AssetScheduleCompletionModel model = new AssetScheduleCompletionModel();
                            item.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                            item.InsertedON = System.DateTime.Now;
                            item.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                            item.InsertedMacID = BISERP.Common.Constants.MacId;
                            item.InsertedMacName = BISERP.Common.Constants.MacName;
                            model = item;
                            modelList.Add(model);
                        }
                    }
                }
            }
            if (strErrorMsg != "")
            {
                return Json(new { success = false, Message = strErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _url = url + "/assetschedule/Createschedulecompletion" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, modelList, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    items = JsonConvert.DeserializeObject<List<AssetScheduleCompletionModel>>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = "Error In  Asset Schedule Completion  " });
            else
                return Json(new { success = true, Message = "Asset Schedule Completion successfully" });
        }

        [HttpGet]
        public async Task<JsonResult> GetBranchAssetsforschedule(int BranchId)
        {
            List<AssetModel> records = new List<AssetModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/assetschedule/GetBranchAssetsforschedule/" + BranchId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<AssetModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetBranchAssetsforschedule :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }


        [HttpGet]
        public async Task<JsonResult> AMCNotification(int DueDays)
        {
            List<AssetScheduleDetailsModel> records = new List<AssetScheduleDetailsModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/assetschedule/getAMCnotification/" + DueDays + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<AssetScheduleDetailsModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AMCNotification :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> CMCNotification(int DueDays)
        {
            List<AssetScheduleDetailsModel> records = new List<AssetScheduleDetailsModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/assetschedule/getCMCnotification/" + DueDays + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<AssetScheduleDetailsModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in CMCNotification :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> OtherDetailNotification(int DueDays)
        {
            List<AssetScheduleDetailsModel> records = new List<AssetScheduleDetailsModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/assetschedule/getOtherNotification/" + DueDays + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<AssetScheduleDetailsModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    // jResult = Json(records, JsonRequestBehavior.AllowGet);
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in OtherDetailNotification :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}

