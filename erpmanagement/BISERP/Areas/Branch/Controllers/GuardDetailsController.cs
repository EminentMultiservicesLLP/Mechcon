using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using BISERP.Areas.Branch.Models;

namespace BISERP.Areas.Branch.Controllers
{
    public class GuardDetailsController : Controller
    {
        readonly HttpClient _client;
        private readonly string _url = Common.Constants.WebAPIAddress;
        private static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(GuardDetailsController));

        public GuardDetailsController()
        {
            _client = new HttpClient {BaseAddress = new Uri(_url)};
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveGuardDetails(GuardInfoModel model)
        {
            //string strErrorMsg = "";
            bool isSuccess = false;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = Common.Constants.IpAddress;
            model.InsertedMacID = Common.Constants.MacId;
            model.InsertedMacName = Common.Constants.MacName;

            var url = _url + "/guardDetails/createguard" + Common.Constants.JsonTypeResult;
            var result = _client.PostAsync(url, model, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                model = JsonConvert.DeserializeObject<GuardInfoModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                isSuccess = true;
                model = JsonConvert.DeserializeObject<GuardInfoModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "OK")
            {
                isSuccess = true;
            }
            return !isSuccess ? Json(new { success = false }) : Json(new { success = true, GuardId = model.GuardDt.Id });
          
        }


        [HttpGet]
        public async Task<JsonResult> GetAllGuardInfo(int branchId)
        {
            JsonResult jResult;
            try
            {
                var url = _url + "/guardDetails/GetAllGuardInfo/" + branchId + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<GuardInfoModel>>(_client, url, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetAllGuardInfo :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveGuardRecruitedDetails(GuardDetailsModel model)
        {
            //string strErrorMsg = "";
            var isSuccess = false;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = Common.Constants.IpAddress;
            model.InsertedMacID = Common.Constants.MacId;
            model.InsertedMacName = Common.Constants.MacName;

            var url = _url + "/guardDetails/createRecruitedguard" + Common.Constants.JsonTypeResult;
            var result = _client.PostAsync(url, model, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
            switch (result.StatusCode.ToString())
            {
                case "BadRequest":
                    model = JsonConvert.DeserializeObject<GuardDetailsModel>(result.Content.ReadAsStringAsync().Result);
                    break;
                case "Created":
                    isSuccess = true;
                    model = JsonConvert.DeserializeObject<GuardDetailsModel>(result.Content.ReadAsStringAsync().Result);
                    break;
                case "OK":
                    isSuccess = true;
                    break;
            }
        
            if (!isSuccess)
                return Json(new { success = false });
            else
                return Json(new { success = true, Id = model.Id });

        }

    }
}
