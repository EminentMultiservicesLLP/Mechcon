using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using BISERP.Areas.Asset.Models;

namespace BISERP.Areas.Asset.Controllers
{
    public class WarrantyMaintenanceController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(WarrantyMaintenanceController));

        public WarrantyMaintenanceController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveWarrantyMaintenance(WarrantyMaintenanceModel warranty)
        {
            string _url = "";
            string _strError = "";
            bool _isSuccess = false;
            warranty.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            warranty.InsertedON = System.DateTime.Now;
            warranty.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            warranty.InsertedMacID = BISERP.Common.Constants.MacId;
            warranty.InsertedMacName = BISERP.Common.Constants.MacName;
            if (_strError == "")
            {
                _url = url + "/warranty/createwarrantymaintenance" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, warranty, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                    _isSuccess = false;
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    warranty = JsonConvert.DeserializeObject<WarrantyMaintenanceModel>(result.Content.ReadAsStringAsync().Result);
                }                
            }
            if (!_isSuccess)
                return Json(new { success = false });
            else
                return Json(new { success = true, Message = warranty.Code });
        }
    }
}
