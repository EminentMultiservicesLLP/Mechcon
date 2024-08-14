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
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.Asset.Controllers
{
    public class LocationController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(LocationController));

        public LocationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllLocation(string SearchName, string SearchCode)
        {
            JsonResult jResult;
            List<LocationModel> records = new List<LocationModel>();
            try
            {
                string _url = url + "/location/getallLocation" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<LocationModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.LocationName.ToUpper().Contains(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.LocationCode.ToUpper().Contains(SearchCode.ToUpper())).ToList();
                    }
                    int total = records.Count;
                    jResult = Json( records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllLocation :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveLocation(LocationModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = 1;
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            if (model.LocationId > 0)
            {
                _url = url + "/location/updatelocation" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<LocationModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            else
            {
                _url = url + "/location/createlocation" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<LocationModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = model.Message });
            else
                return Json(new { success = true });
        }

        [HttpGet]
        public async Task<JsonResult> GetBranchLocation(int BranchId)
        {
            List<LocationModel> records = new List<LocationModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/location/getbranchLocation/" + BranchId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<LocationModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {

                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetBranchLocation :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    
    }
}
