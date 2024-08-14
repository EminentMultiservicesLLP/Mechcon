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
    public class InsideMaintenanceController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(InsideMaintenanceController));

        public InsideMaintenanceController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveInsideMaintenance(InsideMaintenanceModel model)
        {
            string _url = "";
            string _strError = "";
            bool _isSuccess = false;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            if (_strError == "")
            {                
                if (model.InHouseId > 0)
                {
                    _url = url + "/inside/updateinsidemaintenance" + Common.Constants.JsonTypeResult;
                    var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
                    if (result.StatusCode.ToString() == "BadRequest")
                        _isSuccess = false;
                    else if (result.StatusCode.ToString() == "OK")
                    {
                        _isSuccess = true;
                    }
                    if (!_isSuccess)
                        return Json(new { success = false });
                    else
                        return Json(new { success = true });
                }
                else
                {
                    _url = url + "/inside/createinsidemaintenance" + Common.Constants.JsonTypeResult;
                    var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
                    if (result.StatusCode.ToString() == "BadRequest")
                        _isSuccess = false;
                    else if (result.StatusCode.ToString() == "Created")
                    {
                        _isSuccess = true;
                        model = JsonConvert.DeserializeObject<InsideMaintenanceModel>(result.Content.ReadAsStringAsync().Result);
                    }
                    if (!_isSuccess)
                        return Json(new { success = false });
                    else
                        return Json(new { success = true, Message = model.Code });
                }                
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetInHouseMaintenanceDetail(int InHouseId)
        {
            JsonResult jResult;
            InsideMaintenanceModel model = new InsideMaintenanceModel();
            try
            {
                string _url = url + "/inside/getimdetail/" + InHouseId + Common.Constants.JsonTypeResult;
                model = await Common.AsyncWebCalls.GetAsync<InsideMaintenanceModel>(client, _url, CancellationToken.None);
                if (model != null)
                {
                    jResult = Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetInHouseMaintenanceDetail :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
