using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using BISERP.Areas.Training.Models;
using System.Threading;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace BISERP.Areas.Training.Controllers
{
    public class TrainingTypeController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(TrainingTypeController));

        public TrainingTypeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllTrainingType(string SearchName, string SearchCode)
        {
            List<TrainingTypeModel> records = new List<TrainingTypeModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/trainingtype/GetAllTrainingType" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<TrainingTypeModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.TrainingType.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.TrainingTypeCode.ToUpper().StartsWith(SearchCode.ToUpper())).ToList();
                    }
                    jResult = Json( records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllTrainingType :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }


        [HttpGet]
        public async Task<JsonResult> GetdailyupdateTrainingType(string SearchName, string SearchCode)
        {
            List<TrainingTypeModel> records = new List<TrainingTypeModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/trainingtype/GetdailyupdateTrainingType" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<TrainingTypeModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.TrainingType.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.TrainingTypeCode.ToUpper().StartsWith(SearchCode.ToUpper())).ToList();
                    }
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetdailyupdateTrainingType :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllActiveTrainingType()
        {
            List<TrainingTypeModel> records = new List<TrainingTypeModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/trainingtype/getactiveTrainingType" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<TrainingTypeModel>>(client, _url, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
             
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllActiveTrainingType :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveTrainingType(TrainingTypeModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/trainingtype/CreateTrainingType" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<TrainingTypeModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = model.Message });
            else
                return Json(new { success = true });

        }
        [HttpPost]
        public ActionResult UpdateTrainingType(TrainingTypeModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/trainingtype/UpdateTrainingType" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<TrainingTypeModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = model.Message });
            else
                return Json(new { success = true });
        }
    }
}
