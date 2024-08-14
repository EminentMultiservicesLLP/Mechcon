using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using BISERP.Areas.Asset.Models;
using System.Threading;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace BISERP.Areas.Asset.Controllers
{
    public class MaintainanceTypeController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(MaintainanceTypeController));

        public MaintainanceTypeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllMaintainanceType(string SearchName, string SearchCode)
        {
            List<MaintainanceTypeModel> records = new List<MaintainanceTypeModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/maintainancetype/GetAllMaintainanceType" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<MaintainanceTypeModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.MaintainanceType.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.MaintainanceTypeCode.ToUpper().StartsWith(SearchCode.ToUpper())).ToList();
                    }
                    jResult = Json( records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllMaintainanceType :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllActiveMaintainanceType()
        {
            List<MaintainanceTypeModel> records = new List<MaintainanceTypeModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/maintainancetype/getactiveMaintainanceType" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<MaintainanceTypeModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllActiveMaintainanceType :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveMaintainanceType(MaintainanceTypeModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/maintainancetype/CreateMaintainanceType" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<MaintainanceTypeModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = model.Message });
            else
                return Json(new { success = true });

        }
        [HttpPost]
        public ActionResult UpdateMaintainanceType(MaintainanceTypeModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/maintainancetype/UpdateMaintainanceType" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<MaintainanceTypeModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = model.Message });
            else
                return Json(new { success = true });
        }
    }
}
