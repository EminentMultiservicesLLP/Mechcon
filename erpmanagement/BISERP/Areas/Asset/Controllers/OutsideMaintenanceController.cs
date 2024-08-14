using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http;
using BISERP.Areas.Asset.Models;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace BISERP.Areas.Asset.Controllers
{
    public class OutsideMaintenanceController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(OutsideMaintenanceController));

        public OutsideMaintenanceController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

     
        [HttpPost]
        public ActionResult SaveOutsideMaintenance(OutsideMaintenanceModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/outsidemaint/CreateOutsideMaintenance" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<OutsideMaintenanceModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (result.StatusCode.ToString() == "Created")
            {
                _isSuccess = true;
                model = JsonConvert.DeserializeObject<OutsideMaintenanceModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = "Error In  OutsideMaintenance" });
            else
                return Json(new { success = true, Message = model.Code });
        }

        [HttpGet]
        public async Task<JsonResult> GetOutsideMaintenanceNodt(int MaintenanceId)
        {
            OutsideMaintenanceModel records = new OutsideMaintenanceModel();
            JsonResult jResult;
            try
            {
                string _url = url + "/outsidemaint/GetOutsideMaintenanceNo/" + MaintenanceId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<OutsideMaintenanceModel>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetOutsideMaintenanceNodt :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }


        [HttpGet]
        public async Task<JsonResult> GetOutsideMaintenanceNodtItem(int OUTHOUSEID)
        {
            List<SparePartsOuthouseUtilModel> records = new List<SparePartsOuthouseUtilModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/outsidemaint/GetOutsideMaintenanceNoit/" + OUTHOUSEID + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync < List<SparePartsOuthouseUtilModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetOutsideMaintenanceNodtItem :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
