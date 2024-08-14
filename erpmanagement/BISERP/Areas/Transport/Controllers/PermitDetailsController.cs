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
    public class PermitDetailsController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(PermitDetailsController));

        public PermitDetailsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpPost]
        public ActionResult SavePermitDetails(PermitDetailsModel tax)
        {
            string _url = "";
            bool _isSuccess = true;
            tax.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            tax.InsertedON = System.DateTime.Now;
            tax.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            tax.InsertedMacID = BISERP.Common.Constants.MacId;
            tax.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/permit/createPermitDetails" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, tax, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                tax = JsonConvert.DeserializeObject<PermitDetailsModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                _isSuccess = true;
                tax = JsonConvert.DeserializeObject<PermitDetailsModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false });
            else
                return Json(new { success = true, PermitId = tax.PermitId });
        }

        [HttpPost]
        public ActionResult UpdatePermitDetails(PermitDetailsModel tax)
        {
            string _url = "";
            bool _isSuccess = true;
            tax.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            tax.InsertedON = System.DateTime.Now;
            tax.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            tax.InsertedMacID = BISERP.Common.Constants.MacId;
            tax.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/permit/UpdatePermitDetails" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, tax, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                tax = JsonConvert.DeserializeObject<PermitDetailsModel>(result.Content.ReadAsStringAsync().Result);
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
        public async Task<JsonResult> VehiclePermitDetails(int VehicleId)
        {
            List<PermitDetailsModel> records = new List<PermitDetailsModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/permit/vehiclepermit/" + VehicleId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<PermitDetailsModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json( records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in VehiclePermitDetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> PermitDetailsNotification(int DueDays)
        {
            List<PermitDetailsModel> records = new List<PermitDetailsModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/permit/getnotification/" + DueDays + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<PermitDetailsModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PermitDetailsNotification :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
