using BISERP.Areas.Transport.Models;
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
    public class VehicleTransferController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(VehicleTransferController));

        public VehicleTransferController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllTransferVehicle()
        {
            List<VehicleTransferModel> records = new List<VehicleTransferModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/transfer/gettransfervehicle" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<VehicleTransferModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json( records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllTransferVehicle :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveVehicleTransfer(VehicleTransferModel vehicle)
        {
            string _url = "";
            bool _isSuccess = true;
            vehicle.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            vehicle.InsertedON = System.DateTime.Now;
            vehicle.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            vehicle.InsertedMacID = BISERP.Common.Constants.MacId;
            vehicle.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/transfer/createvtransfer" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, vehicle, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                vehicle = JsonConvert.DeserializeObject<VehicleTransferModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                _isSuccess = true;
                vehicle = JsonConvert.DeserializeObject<VehicleTransferModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false });
            else
                return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult AuthorizeCancel(VehicleTransferModel vehicle)
        {
            string _url = "";
            bool _isSuccess = true;
            if (vehicle.Authorised == true)
            {
                vehicle.Authorised = true;
                vehicle.Cancelled = false;
                vehicle.AuthorisedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                vehicle.AuthorisedDate = System.DateTime.Now;
            }
            else
            {
                vehicle.Authorised = false;
                vehicle.Cancelled = true;
                vehicle.AuthorisedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                vehicle.AuthorisedDate = System.DateTime.Now;
            }            
            
            _url = url + "/transfer/authorizecancel" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, vehicle, new JsonMediaTypeFormatter()).Result;
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
                return Json(new { success = true });
        }
    }
}
