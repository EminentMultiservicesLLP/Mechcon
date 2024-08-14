using BISERP.Areas.Asset.Models;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BISERP.Areas.Asset.Controllers
{
    public class TechnicianMasterController : Controller
    {
        readonly HttpClient _client;
        private readonly string _url = BISERP.Common.Constants.WebAPIAddress;
        private static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(TechnicianMasterController));

        public TechnicianMasterController()
        {
            _client = new HttpClient {BaseAddress = new Uri(_url)};
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllTechnician(string searchName, string searchCode)
        {
            List<TechnicianModel> records;
            JsonResult jResult;
            try
            {
                string url = this._url + "/technician/GetAlltechnician" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<TechnicianModel>>(_client, url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(searchName))
                    {
                        records = records.Where(p => p.TechnicianName.ToUpper().StartsWith(searchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(searchCode))
                    {
                        records = records.Where(p => p.TechnicianCode.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                    }
                    jResult = Json( records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllTechnician :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllActiveTechnician()
        {
            JsonResult jResult;
            try
            {
                string url = this._url + "/technician/getactivetechnician" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<TechnicianModel>>(_client, url, CancellationToken.None);
                if (records != null)
                {

                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllActiveTechnician :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveTechnician(TechnicianModel technician)
        {
            string url = "";
            bool isSuccess = true;
            technician.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            technician.InsertedON = DateTime.Now;
            technician.InsertedIPAddress = Common.Constants.IpAddress;
            technician.InsertedMacID = Common.Constants.MacId;
            technician.InsertedMacName = Common.Constants.MacName;
            url = this._url + "/technician/CreateTechnician" + Common.Constants.JsonTypeResult;
            var result = _client.PostAsync(url, technician, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                isSuccess = false;
                technician = JsonConvert.DeserializeObject<TechnicianModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!isSuccess)
                return Json(new { success = false, Message = technician.Message });
            else
                return Json(new { success = true });

        }
        [HttpPost]
        public ActionResult UpdateTechnician(TechnicianModel technician)
        {
            string url = "";
            bool isSuccess = true;
            technician.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            technician.InsertedON = DateTime.Now;
            technician.InsertedIPAddress = Common.Constants.IpAddress;
            technician.InsertedMacID = Common.Constants.MacId;
            technician.InsertedMacName = Common.Constants.MacName;

            url = _url + "/technician/UpdateTechnician" + Common.Constants.JsonTypeResult;
            var result = _client.PostAsync(url, technician, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                isSuccess = false;
                technician = JsonConvert.DeserializeObject<TechnicianModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!isSuccess)
                return Json(new { success = false, Message = technician.Message });
            else
                return Json(new { success = true });
        }
    }
}
