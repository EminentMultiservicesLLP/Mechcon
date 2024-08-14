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
    public class InsuranceController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(InsuranceController));

        public InsuranceController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpPost]
        public ActionResult SaveInsurance(InsuranceModel tax)
        {
            string _url = "";
            bool _isSuccess = true;
            tax.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            tax.InsertedON = System.DateTime.Now;
            tax.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            tax.InsertedMacID = BISERP.Common.Constants.MacId;
            tax.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/insurance/createInsurance" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, tax, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                tax = JsonConvert.DeserializeObject<InsuranceModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                _isSuccess = true;
                tax = JsonConvert.DeserializeObject<InsuranceModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false });
            else
                return Json(new { success = true, InsuranceId = tax.InsuranceId });
        }

        [HttpPost]
        public ActionResult UpdateInsurance(InsuranceModel tax)
        {
            string _url = "";
            bool _isSuccess = true;
            tax.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            tax.InsertedON = System.DateTime.Now;
            tax.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            tax.InsertedMacID = BISERP.Common.Constants.MacId;
            tax.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/insurance/UpdateInsurance" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, tax, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                tax = JsonConvert.DeserializeObject<InsuranceModel>(result.Content.ReadAsStringAsync().Result);
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
        public async Task<JsonResult> AllInsuranceCompany()
        {
            List<InsuranceCompanyModel> records = new List<InsuranceCompanyModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/insurancecompany/getall" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<InsuranceCompanyModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllInsuranceCompany :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> VehicleInsurance(int VehicleId)
        {
            List<InsuranceModel> records = new List<InsuranceModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/insurance/vehicleinsurance/" + VehicleId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<InsuranceModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json( records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in VehicleInsurance :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> InsuranceNotification(int DueDays)
        {
            List<InsuranceModel> records = new List<InsuranceModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/insurance/getnotification/" + DueDays + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<InsuranceModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in InsuranceNotification :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
