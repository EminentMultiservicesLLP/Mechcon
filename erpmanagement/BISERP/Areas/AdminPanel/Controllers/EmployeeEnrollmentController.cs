using BISERP.Areas.AdminPanel.Models;
using BISERP.Filters;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.AdminPanel.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class EmployeeEnrollmentController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(EmployeeEnrollmentController));

        public EmployeeEnrollmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetUserCode()
        {
            JsonResult jResult;
            List<EmployeeEnrollmentModel> entity = new List<EmployeeEnrollmentModel>();
            try
            {
                string _url = url + "/EmployeeEnrollment/getUserCode/" + Common.Constants.JsonTypeResult;
                entity = await Common.AsyncWebCalls.GetAsync<List<EmployeeEnrollmentModel>>(client, _url, CancellationToken.None);
                if (entity != null && entity.Count > 0)
                {
                    jResult = Json(entity, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(entity, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getUserCode :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }//"/EmployeeEnrollment/AllUsers"
        [HttpGet]
        public async Task<JsonResult> GetUserDetails()
        {
            JsonResult jResult;
            List<EmployeeEnrollmentModel> entity = new List<EmployeeEnrollmentModel>();
            try
            {
                int LoginId = Convert.ToInt32(Session["AppUserId"].ToString());
                string _url = url + "/EmployeeEnrollment/GetUserDetails/" + LoginId + Common.Constants.JsonTypeResult;
                entity = await Common.AsyncWebCalls.GetAsync<List<EmployeeEnrollmentModel>>(client, _url, CancellationToken.None);
                if (entity != null && entity.Count > 0)
                {
                    jResult = Json(entity, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(entity, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetUserDetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public async Task<JsonResult> SaveUser(EmployeeEnrollmentModel Details)
        {
            string _url = "";
            bool _isSuccess = true;
            Details.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            Details.InsertedON = System.DateTime.Now;
            Details.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            Details.InsertedMacID = BISERP.Common.Constants.MacId;
            Details.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/EmployeeEnrollment/SaveUser" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, Details, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                Details = JsonConvert.DeserializeObject<EmployeeEnrollmentModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = Details.Message });
            else
                return Json(new { success = true });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteUser(EmployeeEnrollmentModel Details)
        {
            string _url = "";
            bool _isSuccess = true;
            Details.UpdatedByUserID = Convert.ToInt32(Session["AppUserId"].ToString());
            Details.UpdatedON = System.DateTime.Now;
            Details.UpdatedIPAddress = BISERP.Common.Constants.IpAddress;
            Details.UpdatedMacID = BISERP.Common.Constants.MacId;
            Details.UpdatedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/EmployeeEnrollment/DeleteUser" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, Details, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                Details = JsonConvert.DeserializeObject<EmployeeEnrollmentModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = Details.Message });
            else
                return Json(new { success = true });
        }

        [HttpPost]
        public async Task<JsonResult> ChangePassword(string Password)
        {
            EmployeeEnrollmentModel Details = new EmployeeEnrollmentModel();
            string _url = "";
            bool _isSuccess = true;
            Details.UserID = Session["AppUserId"].ToString();
            Details.UpdatedByUserID = Convert.ToInt32(Session["AppUserId"].ToString());
            Details.Password = Password;
            Details.UpdatedON = System.DateTime.Now;
            Details.UpdatedIPAddress = BISERP.Common.Constants.IpAddress;
            Details.UpdatedMacID = BISERP.Common.Constants.MacId;
            Details.UpdatedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/EmployeeEnrollment/ChangePassword" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, Details, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                Details = JsonConvert.DeserializeObject<EmployeeEnrollmentModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = Details.Message });
            else
                return Json(new { success = true });
        }

        [HttpGet]
        public async Task<JsonResult> GetDepartments()
        {
            try
            {
                string _url = $"{url}/EmployeeEnrollment/GetDepartments{Common.Constants.JsonTypeResult}";
                List<DepartmentModel> departments = await Common.AsyncWebCalls.GetAsync<List<DepartmentModel>>(client, _url, CancellationToken.None);

                if (departments == null || !departments.Any())
                {
                    return Json(new { error = "No departments found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(departments, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetDepartments method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the departments." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetDesignations()
        {
            try
            {
                string _url = $"{url}/EmployeeEnrollment/GetDesignations{Common.Constants.JsonTypeResult}";
                List<DesignationModel> designations = await Common.AsyncWebCalls.GetAsync<List<DesignationModel>>(client, _url, CancellationToken.None);

                if (designations == null || !designations.Any())
                {
                    return Json(new { error = "No designations found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(designations, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetDesignations method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the designations." }, JsonRequestBehavior.AllowGet);
            }
        }

    }

}

