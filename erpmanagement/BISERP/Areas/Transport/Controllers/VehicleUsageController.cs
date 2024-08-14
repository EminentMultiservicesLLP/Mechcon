using BISERP.Areas.Transport.Models.Master;
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

namespace BISERP.Areas.Transport.Controllers
{
    public class VehicleUsageController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(VehicleUsageController));

        public VehicleUsageController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllVehicleUsage(string SearchName, string Searchcode)
        {
            List<VehicleUsageModel> records = new List<VehicleUsageModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/vehicleUsage/getvehicleusage" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<VehicleUsageModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.UsedFor.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(Searchcode))
                    {
                        records = records.Where(p => p.Code.ToUpper().StartsWith(Searchcode.ToUpper())).ToList();
                    }
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                   
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllVehicleUsage :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllActiveVehicleUsage()
        {
            List<VehicleUsageModel> records = new List<VehicleUsageModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/vehicleUsage/GetActiveVehicleUsage" + "/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<VehicleUsageModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {

                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllActiveVehicleUsage :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpPost]
        public ActionResult SaveVehicleUsage(VehicleUsageModel Vehicle)
        {
            string _url = "";
            bool _isSuccess = true;
            Vehicle.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            Vehicle.InsertedON = System.DateTime.Now;
            Vehicle.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            Vehicle.InsertedMacID = BISERP.Common.Constants.MacId;
            Vehicle.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/vehicleUsage/CreateVehicleUsage" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, Vehicle, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                Vehicle = JsonConvert.DeserializeObject<VehicleUsageModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = Vehicle.Message });
            else
                return Json(new { success = true });

        }
        [HttpPost]
        public ActionResult updateVehicleUsage(VehicleUsageModel Vehicle)
        {
            string _url = "";
            bool _isSuccess = true;
            Vehicle.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            Vehicle.InsertedON = System.DateTime.Now;
            Vehicle.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            Vehicle.InsertedMacID = BISERP.Common.Constants.MacId;
            Vehicle.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/vehicleUsage/UpdatevehicleUsage" + Common.Constants.JsonTypeResult;
            //client.PutAsync(_url, store, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<int>().Result;
            var result = client.PostAsync(_url, Vehicle, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                Vehicle = JsonConvert.DeserializeObject<VehicleUsageModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = Vehicle.Message });
            else
                return Json(new { success = true });
        }
    }
}
