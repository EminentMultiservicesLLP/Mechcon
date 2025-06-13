
using BISERP.Areas.TimingPlan.Models;
using BISERP.Filters;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.TimingPlan.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class TimingPlanController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(TimingPlanController));

        public TimingPlanController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        #region View
        public ActionResult CreateProjectTaskSchedule()
        {
            return PartialView();
        }
        public ActionResult TaskProgress()
        {
            return PartialView();
        }
        public ActionResult TaskMonitor()
        {
            return PartialView();
        }
        public ActionResult TaskTracking()
        {
            return PartialView();
        }
        #endregion View

        #region Schedule
        [HttpGet]
        public async Task<JsonResult> GetTaskMaster()
        {
            string _url = $"{url}/timingPlan/getTaskMaster{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TP_TaskMaster>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetTaskMaster: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving TaskMaster" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetProjectTaskSchedule(int ProjectID)
        {
            string _url = $"{url}/timingPlan/getProjectTaskSchedule/{ProjectID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TP_ProjectTaskSchedule>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetProjectTaskSchedule: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving ProjectTask" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveProjectTaskSchedule(TP_PTSchedule model)
        {
            string _url = "";
            bool _isSuccess = true;

            try
            {
                int userId = Convert.ToInt32(Session["AppUserId"]);
                model.LoginId = userId;

                _url = url + "/timingPlan/saveProjectTaskSchedule" + Common.Constants.JsonTypeResult;

                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
                var content = result.Content.ReadAsStringAsync().Result;

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<TP_PTSchedule>(content);
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    _isSuccess = true;
                    model = JsonConvert.DeserializeObject<TP_PTSchedule>(content);
                }
                else
                {
                    _isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _isSuccess = false;
                _logger.LogError("Error in SaveProjectTaskSchedule method:" + Environment.NewLine + ex.ToString());
            }

            if (!_isSuccess)
            {
                return Json(new { success = false, Message = "Error while saving ProjectTaskSchedule" });
            }
            else
            {
                return Json(new { success = true, Message = "ProjectTaskSchedule saved successfully", Data = model });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetProjectTaskRole(int ProjectID)
        {
            string _url = $"{url}/timingPlan/getProjectTaskRole/{ProjectID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TP_PTSchedule>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetProjectTaskRole: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving ProjectTask" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetProjectTaskScheduleDetails(int ProjectID)
        {
            string _url = $"{url}/timingPlan/getProjectTaskScheduleDetails/{ProjectID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TP_ProjectTaskScheduleDetails>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetProjectTaskScheduleDetails: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving ProjectTaskScheduleDetails" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Schedule

        #region TaskProgress
        [HttpGet]
        public async Task<JsonResult> GetTaskProgress(int IsCompleted)
        {
            int LoginId = Convert.ToInt32(Session["AppUserId"]);
            string _url = $"{url}/timingPlan/GetTaskProgress/{IsCompleted}/{LoginId}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TP_ProjectTaskSchedule>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetTaskProgress: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving TaskProgress" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveTaskProgress(TP_ProjectTaskSchedule model)
        {
            string _url = "";
            bool _isSuccess = true;

            try
            {
                model.LastActionBy = Convert.ToInt32(Session["AppUserId"].ToString());

                _url = url + "/timingPlan/SaveTaskProgress" + Common.Constants.JsonTypeResult;

                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<TP_ProjectTaskSchedule>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    _isSuccess = true;
                    model = JsonConvert.DeserializeObject<TP_ProjectTaskSchedule>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    _isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _isSuccess = false;
                _logger.LogError("Error in SaveTaskProgress method:" + Environment.NewLine + ex.ToString());
            }
            if (!_isSuccess)
            {
                return Json(new { success = false, Message = "Error while saving TaskProgress" });
            }
            else
            {
                return Json(new { success = true, Message = "TaskProgress saved successfully", Data = model });
            }
        }
        #endregion TaskProgress

        #region TaskMonitor
        [HttpGet]
        public async Task<JsonResult> GetTaskMonitor(int ProjectID)
        {
            string _url = $"{url}/timingPlan/GetTaskMonitor/{ProjectID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TP_ProjectTaskSchedule>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetTaskMonitor: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving TaskMonitor" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion TaskMonitor

        #region TaskTracking
        [HttpGet]
        public async Task<JsonResult> GetTaskTracking(int ProjectID)
        {
            string _url = $"{url}/timingPlan/GetTaskTracking/{ProjectID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TP_ProjectTaskSchedule>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetTaskTracking: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving TaskTracking" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion TaskTracking
    }
}
