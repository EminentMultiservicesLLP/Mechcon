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

namespace BISERP.Areas.Masters.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class StoreMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(StoreMasterController));

        public StoreMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllStoreMasters(string SearchName, string SearchCode)
        {
            JsonResult jResult;
            List<StoreMasterModel> records = new List<StoreMasterModel>();
            try
            {
                string _url = url + "/Stores/GetAllStoreMasters" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.Name.ToUpper().Contains(SearchName.ToUpper())).ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.Code.ToUpper().Contains(SearchCode.ToUpper())).ToList();
                    }
                    //jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    // jResult = Json("Error");
                    jResult = Json(new { success = false, Massage = "No Data Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllStoreMasters :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Server Error! Please Contact administrator!");
            }
            return jResult;
        }

        [HttpPost]
        public JsonResult SaveStoreMaster(StoreMasterModel store)
        {
            string _url = "";
            bool _isSuccess = true;
            store.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            store.InsertedON = System.DateTime.Now;
            store.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            store.InsertedMacID = BISERP.Common.Constants.MacId;
            store.InsertedMacName = BISERP.Common.Constants.MacName;
            if (store.ID > 0)
            {
                _url = url + "/stores/updatestore" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, store, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    store = JsonConvert.DeserializeObject<StoreMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
                if (!_isSuccess)
                    return Json(new { success = false, Message = store.Message });
                else
                    return Json(new { success = true });
            }
            else
            {
                _url = url + "/stores/createstore" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, store, new JsonMediaTypeFormatter()).Result;
                store = JsonConvert.DeserializeObject<StoreMasterModel>(result.Content.ReadAsStringAsync().Result);
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = store.Message, StoreID = store.ID });
            else
                return Json(new { success = true, StoreID = store.ID });

        }
        [HttpGet]
        public async Task<JsonResult> GetStoredtl(int storeId)
        {
            List<StoreMasterDtlModel> records = new List<StoreMasterDtlModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/stores/GetStoredtl/" + storeId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<StoreMasterDtlModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No record  Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllStoreMasters :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetStoreBudgetdtl(int storeId)
        {
            List<ProjectBudgetDtlModel> records = new List<ProjectBudgetDtlModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/stores/GetStoreBudgetdtl/" + storeId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ProjectBudgetDtlModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No record  Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStoreBudgetdtl :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetProjectBudget(int storeId, int ID)
        {
            List<ProjectBudgetDtlModel> records = new List<ProjectBudgetDtlModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/stores/GetProjectBudget/" + storeId + "/" + ID + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ProjectBudgetDtlModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No record  Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetProjectBudget :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetProjectBudgetStatus(int storeId)
        {
            List<StoreMasterModel> records = new List<StoreMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/stores/GetProjectBudgetStatus/" + storeId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No record  Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetProjectBudgetStatus :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        public async Task<JsonResult> GetMasterCode(int masterId)
        {
            List<StoreMasterModel> records = new List<StoreMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/stores/GetMasterCode/" + masterId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {

                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetMasterCode :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> ProjectPoNo(int projectId)
        {
            JsonResult jResult;
            string _url = url + "/stores/getallmainstore" + Common.Constants.JsonTypeResult;
            List<StoreMasterModel> _store = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);
            var pono = _store.Where(m => m.ID == projectId).Select(m => m.ClientPoNo);
            return jResult = Json(pono, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetClientProject(int clientId)
        {
            JsonResult jResult;
            string _url = url + "/stores/getallmainstore" + Common.Constants.JsonTypeResult;
            List<StoreMasterModel> _store = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);
            var project = _store.Where(m => m.ClientId == clientId).ToList();
            return jResult = Json(project, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetBudgetUtilzedDetails(int ProjectID, int ItemTypeId)
        {
            List<StoreMasterModel> records = new List<StoreMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/stores/GetBudgetUtilzedDetails/" + ProjectID + "/" + ItemTypeId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetBudgetUtilzedDetails no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetStorewiseProjectTC(int storeId)
        {

            JsonResult jResult;
            List<ProjectTCMasterModel> records = new List<ProjectTCMasterModel>();
            try
            {
                string _url = url + "/stores/getStorewiseProjectTC/" + storeId.ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ProjectTCMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStorewiseProjectTC  :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveStoreMasterApproval(StoreMasterModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            _url = url + "/stores/saveStoreMasterApproval" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<StoreMasterModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = model.Message });
            else
                return Json(new { success = true });
        }

        [HttpGet]
        public async Task<JsonResult> GetRevisionDetails(int StoreID, string Department)
        {
            JsonResult jResult;
            List<StoreMasterModel> records = new List<StoreMasterModel>();
            try
            {
                string _url = url + "/stores/getRevisionDetails/" + StoreID +"/"+ Department + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { data = records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { data = records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getRevisionDetails  :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetProjectTransactionRecord(int StoreId)
        {
            List<ProjectTransactionRecordModel> records = new List<ProjectTransactionRecordModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/stores/getProjectTransactionRecord/" + StoreId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ProjectTransactionRecordModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No PackingList Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetProjectTransactionRecord:" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetDeliverablesDetail(int StoreId)
        {
            List<DeliverablesDetailModel> records = new List<DeliverablesDetailModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/stores/getDeliverablesDetail/" + StoreId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<DeliverablesDetailModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No DeliverablesDetail Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetDeliverablesDetail:" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetBudgetStatus()
        {
            List<BudgetStatusModel> records = new List<BudgetStatusModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/stores/getBudgetStatus/" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<BudgetStatusModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No BudgetStatus Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetBudgetStatus:" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
