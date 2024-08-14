using BISERP.Areas.Transport.Models;
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
    public class ScheduleTripController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ScheduleTripController));

        public ScheduleTripController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllSchedule()
        {
            List<DriverScheduleModel> records = new List<DriverScheduleModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/driverschedule/getschedule" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<DriverScheduleModel>>(client, _url, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllSchedule :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllSchedulefuel(int ScheduleId)
        {
            List<FuelDetailsModel> records = new List<FuelDetailsModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/fueldetail/getfuelSchedule/" + ScheduleId .ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<FuelDetailsModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllSchedulefuel :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveDriver(DriverScheduleModel Driver)
        {
            string _url = "";
            bool _isSuccess = true;
            Driver.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            Driver.InsertedON = System.DateTime.Now;
            Driver.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            Driver.InsertedMacID = BISERP.Common.Constants.MacId;
            Driver.InsertedMacName = BISERP.Common.Constants.MacName;
            Driver.fuelDetail.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            Driver.fuelDetail.InsertedON = System.DateTime.Now; ;
            Driver.fuelDetail.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            Driver.fuelDetail.InsertedMacID = BISERP.Common.Constants.MacId;
            Driver.fuelDetail.InsertedMacName = BISERP.Common.Constants.MacName;
            if (Driver.ScheduleId > 0)
            {

                _url = url + "/driverschedule/UpdateFuelDetails" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, Driver, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    Driver = JsonConvert.DeserializeObject<DriverScheduleModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            else
            {
                _url = url + "/driverschedule/createdriverschedule" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, Driver, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    Driver = JsonConvert.DeserializeObject<DriverScheduleModel>(result.Content.ReadAsStringAsync().Result);
                }
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = "Error In Schedule Trip" });
            else
                return Json(new { success = true });

        }
        [HttpPost]
        public ActionResult Save(FuelDetailsModel Vehicle)
        {
            string _url = "";
            bool _isSuccess = true;
            Vehicle.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            Vehicle.InsertedON = System.DateTime.Now;
            Vehicle.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            Vehicle.InsertedMacID = BISERP.Common.Constants.MacId;
            Vehicle.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/fueldetail/createfueldetail" + Common.Constants.JsonTypeResult;
            //client.PutAsync(_url, store, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<int>().Result;
            var result = client.PostAsync(_url, Vehicle, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                Vehicle = JsonConvert.DeserializeObject<FuelDetailsModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = Vehicle.Message });
            else
                return Json(new { success = true });
        }
    }
}
