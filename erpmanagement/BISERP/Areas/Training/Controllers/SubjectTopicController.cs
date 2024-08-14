using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using BISERP.Areas.Training.Models;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading;

namespace BISERP.Areas.Training.Controllers
{
    public class SubjectTopicController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(SubjectTopicController));

        public SubjectTopicController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllSubjectTopic(string SearchName, string SearchCode)
        {
            List<SubjectTopicModel> records = new List<SubjectTopicModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/subjecttopic/allsubjecttopics" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<SubjectTopicModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.TopicName.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.TopicCode.ToUpper().StartsWith(SearchCode.ToUpper())).ToList();
                    }
                    jResult = Json( records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllSubjectTopic :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllActiveSubjectTopic(int SubjectId)
        {
            List<SubjectTopicModel> records = new List<SubjectTopicModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/subjecttopic/getactiveSubjectTopic/" + SubjectId.ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<SubjectTopicModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllActiveSubjectTopic :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveSubjectTopic(SubjectTopicModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/subjecttopic/CreateSubjectTopic" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<SubjectTopicModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = model.Message });
            else
                return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult UpdateSubjectTopic(SubjectTopicModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/subjecttopic/UpdateSubjectTopic" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<SubjectTopicModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = model.Message });
            else
                return Json(new { success = true });
        }
    }
}
