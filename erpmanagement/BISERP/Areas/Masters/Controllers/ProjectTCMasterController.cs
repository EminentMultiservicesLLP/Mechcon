using BISERP.Areas.Masters.Models;
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

namespace BISERP.Areas.Masters.Controllers
{
    public class ProjectTCMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ProjectTCMasterController));

        public ProjectTCMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpGet]
        public async Task<JsonResult> AllProjectTC(string searchProjectTC, string searchCode)
        {
            List<ProjectTCMasterModel> Pimodel = new List<ProjectTCMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/projectTC/getAllProjectTC" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<ProjectTCMasterModel>>(client, _url, CancellationToken.None);

                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchProjectTC))
                    {
                        records = records.Where(p => p.ProjectTCDesc.ToUpper().StartsWith(searchProjectTC.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(searchCode))
                    {
                        records = records.Where(p => p.ProjectTCCode.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                    }
                    int total = records.Count;

                    jResult = Json(new { success = true, records, total }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Details found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllProjectTC :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Server Error! Please Contact administrator!");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveProjectTC(ProjectTCMasterModel ProjectTC)
        {
            string _url = "";
            bool _isSuccess = true;
            ProjectTC.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            ProjectTC.InsertedON = System.DateTime.Now;
            ProjectTC.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            ProjectTC.InsertedMacID = BISERP.Common.Constants.MacId;
            ProjectTC.InsertedMacName = BISERP.Common.Constants.MacName;
            if (ProjectTC.ProjectTCID > 0)
            {
                _url = url + "/projectTC/updateProjectTC" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, ProjectTC, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    ProjectTC = JsonConvert.DeserializeObject<ProjectTCMasterModel>(result.Content.ReadAsStringAsync().Result);
                }

                if (!_isSuccess)
                    return Json(new { success = false, Message = ProjectTC.Message });
                else
                    return Json(new { success = true });
            }
            else
            {
                _url = url + "/projectTC/createProjectTC" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, ProjectTC, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    ProjectTC = JsonConvert.DeserializeObject<ProjectTCMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = ProjectTC.Message });
            else
                return Json(new { success = true });
        }
    }
}
