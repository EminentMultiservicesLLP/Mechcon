using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using BISERP.Areas.Training.Models;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace BISERP.Areas.Training.Controllers
{
    public class TrainingScheduleController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _url = Common.Constants.WebAPIAddress;
        private static readonly BISERPCommon.ILogger Logger = BISERPCommon.Logger.Register(typeof(TrainingScheduleController));

        public TrainingScheduleController()
        {
            _client = new HttpClient {BaseAddress = new Uri(_url)};
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllSchedules()
        {
            JsonResult jResult;
            try
            {
                string apiurl = _url + "/schedule/GetAllSchedules" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<ScheduleModel>>(_client, apiurl, CancellationToken.None);
                jResult = records != null ? Json(records, JsonRequestBehavior.AllowGet) : Json("Error");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetAllSchedules :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveTrainingSchedule(ScheduleModel model)
        {
            bool isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            string apiurl;
            if (model.ScheduleId > 0)
            {
                apiurl = _url + "/schedule/UpdateSchedule" + Common.Constants.JsonTypeResult;
            }
            else
            {
                apiurl = _url + "/schedule/CreateSchedule" + Common.Constants.JsonTypeResult;    
            }
            var result = _client.PostAsync(apiurl, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                isSuccess = false;
                model = JsonConvert.DeserializeObject<ScheduleModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!isSuccess)
                return Json(new { success = false, Message = model.Message });
            else
                return Json(new { success = true });
        }
    }
}
