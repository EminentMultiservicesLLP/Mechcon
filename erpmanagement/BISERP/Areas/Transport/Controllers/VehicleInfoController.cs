using BISERP.Areas.Transport.Models;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Training.Models;

namespace BISERP.Areas.Transport.Controllers
{
    public class VehicleInfoController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(VehicleInfoController));

        public VehicleInfoController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllVehicleNO(int branchId)
        {
            JsonResult jResult;
            try
            {
                String url = "/vehicle/getvehiclesNo/" + branchId;
                var records = await Common.AsyncWebCalls.GetAsync<List<VehicleInfoModel>>(url, CancellationToken.None);
                jResult = Json( records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllVehicle :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllVehicle()
        {
            JsonResult jResult;
            try
            {
                String url = "/vehicle/getvehicles/"  +Session["AppUserId"].ToString();
                var records = await Common.AsyncWebCalls.GetAsync<List<VehicleInfoModel>>(url, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllVehicle :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllVehicleSchedule(int BranchId)
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<VehicleInfoModel>>("/vehicle/getvehicleschedule/" + BranchId, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllVehicleSchedule :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveVehicleInfo(VehicleInfoModel model)
        {            
            string _url = "";
            bool _isSuccess = true;

            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            model.file = null;
            if (model.VehicleId > 0)
            {
                _url = url + "/vehicle/vehicleupdate" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                }
                else if (result.StatusCode.ToString() == "OK")
                {
                    _isSuccess = true;
                }

                if (!_isSuccess)
                    return Json(new { success = false });
                else
                    return Json(new { success = true, VehicleId = model.VehicleId });
            }
            else
            {
                _url = url + "/vehicle/vehicleentry" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                }

                if (!_isSuccess)
                    return Json(new { success = false });
                else
                    return Json(new { success = true });
            }
        }
    }
}
