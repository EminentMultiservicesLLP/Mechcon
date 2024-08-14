using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http;
using BISERP.Areas.Training.Models;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace BISERP.Areas.Training.Controllers
{
    public class BatchMasterController : Controller
    {
       HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(BatchMasterController));

        public BatchMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllBatch(string SearchName, string SearchCode)
        {
            List<BatchModel> records = new List<BatchModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/batch/GetAllBatch" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<BatchModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {                    
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllBatch :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetTraniningWiseSlot(int trainingTypeId)
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<BatchModel>>("/batch/GetTraniningWiseSlot/" + trainingTypeId, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetTraniningWiseSlot :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetDateWiseSlot(int trainingCentreId)
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<BatchModel>>("/batch/getdatewiseslot/" + trainingCentreId, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetDateWiseSlot :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveBatchMaster(BatchModel batch)
        {
            string _url = "";
            bool _isSuccess = true;
            batch.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString()); ;
            batch.InsertedON = System.DateTime.Now;
            batch.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            batch.InsertedMacID = BISERP.Common.Constants.MacId;
            batch.InsertedMacName = BISERP.Common.Constants.MacName;
            if (batch.BatchId > 0)
            {
                _url = url + "/batch/UpdateBatch" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, batch, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    batch = JsonConvert.DeserializeObject<BatchModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            else
            {
                _url = url + "/batch/CreateBatch" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, batch, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    batch = JsonConvert.DeserializeObject<BatchModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = batch.Message });
            else
                return Json(new { success = true });
        }
    }
}
