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
    public class GreenTaxController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(GreenTaxController));

        public GreenTaxController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllRoadTax()
        {
            List<GreenTaxModel> records = new List<GreenTaxModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/greentax/getgreentax" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<GreenTaxModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllRoadTax :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveGreenTax(GreenTaxModel tax)
        {
            string _url = "";
            bool _isSuccess = true;
            tax.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            tax.InsertedON = System.DateTime.Now;
            tax.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            tax.InsertedMacID = BISERP.Common.Constants.MacId;
            tax.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/greentax/creategreenTax" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, tax, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                tax = JsonConvert.DeserializeObject<GreenTaxModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                _isSuccess = true;
                tax = JsonConvert.DeserializeObject<GreenTaxModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false });
            else
                return Json(new { success = true, GreenTaxId = tax.GreenTaxId });
        }

        [HttpPost]
        public ActionResult UpdateGreenTax(GreenTaxModel tax)
        {
            string _url = "";
            bool _isSuccess = true;
            tax.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            tax.InsertedON = System.DateTime.Now;
            tax.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            tax.InsertedMacID = BISERP.Common.Constants.MacId;
            tax.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/greentax/UpdateGreentax" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, tax, new JsonMediaTypeFormatter()).Result;
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

        [HttpGet]
        public async Task<JsonResult> VehicleGreenTax(int VehicleId)
        {
            List<GreenTaxModel> records = new List<GreenTaxModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/greentax/vehiclegreentax/" + VehicleId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<GreenTaxModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json( records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in VehicleGreenTax :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GreenTaxNotification(int DueDays)
        {
            List<GreenTaxModel> records = new List<GreenTaxModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/greentax/getnotification/" + DueDays + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<GreenTaxModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GreenTaxNotification :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
