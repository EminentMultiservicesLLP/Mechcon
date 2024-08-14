using BISERP.Areas.Asset.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using BISERPCommon;

namespace BISERP.Areas.Asset.Controllers
{
    public class CallTypeController : Controller
    {
        readonly HttpClient _client;
        private readonly string _url = Common.Constants.WebAPIAddress;
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(CallTypeController));

        public CallTypeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllCallType(string SearchName, string SearchCode)
        {
            JsonResult jResult;
            try
            {
                var url = this._url + "/Calltype/GetAllCallType" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<CallTypeModel>>(_client, url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.CallType.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.CallTypeCode.ToUpper().StartsWith(SearchCode.ToUpper())).ToList();
                    }
                    jResult = Json( records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllCallType :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllActiveCallType()
        {
            JsonResult jResult;
            try
            {
                var url = this._url + "/Calltype/getactiveCallType" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<CallTypeModel>>(_client, url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllActiveCallType :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveCallType(CallTypeModel model)
        {
            string url = "";
            bool isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = Common.Constants.IpAddress;
            model.InsertedMacID = Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            url = _url + "/Calltype/CreateCallType" + Common.Constants.JsonTypeResult;
            var result = _client.PostAsync(url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                isSuccess = false;
                model = JsonConvert.DeserializeObject<CallTypeModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!isSuccess)
                return Json(new { success = false, Message = model.Message });
            else
                return Json(new { success = true });

        }
        [HttpPost]
        public ActionResult UpdateCallType(CallTypeModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = this._url + "/Calltype/UpdateCallType" + Common.Constants.JsonTypeResult;
            var result = _client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<CallTypeModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = model.Message });
            else
                return Json(new { success = true });
        }
    }
}
