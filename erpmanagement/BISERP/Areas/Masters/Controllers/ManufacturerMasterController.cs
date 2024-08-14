using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Masters.Models;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using BISERP.Filters;

namespace BISERP.Areas.Store.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class ManufacturerMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ManufacturerMasterController));

        public ManufacturerMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        
        [HttpGet]
        public async Task<JsonResult> AllManufacturers( string SearchName, string SearchCode)
        {
            List<ManufacturerMasterModel> records = new List<ManufacturerMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/Manufacturer/GetAllManufacturer" + Common.Constants.JsonTypeResult;
                 records = await Common.AsyncWebCalls.GetAsync<List<ManufacturerMasterModel>>(client, _url, CancellationToken.None);
                if (records != null )
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.Name.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.Code.ToUpper().StartsWith(SearchCode.ToUpper())).ToList();
                    }
                   
                    jResult = Json(new { records}, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllManufacturers :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveManufacturer(ManufacturerMasterModel manufacturer)
        {
            string _url = "";
            bool _isSuccess = true;
            manufacturer.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            manufacturer.InsertedON = System.DateTime.Now;
            manufacturer.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            manufacturer.InsertedMacID = BISERP.Common.Constants.MacId;
            manufacturer.InsertedMacName = BISERP.Common.Constants.MacName;
            if (manufacturer.ID > 0)
            {
                _url = url + "/manufacturer/updatemanufacturer" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, manufacturer, new JsonMediaTypeFormatter()).Result; //.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    manufacturer = JsonConvert.DeserializeObject<ManufacturerMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            else
            {
                _url = url + "/manufacturer/createmanufacture" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, manufacturer, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    manufacturer = JsonConvert.DeserializeObject<ManufacturerMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            //return PartialView();
            if (!_isSuccess)
                return Json(new { success = false, Message = manufacturer.Message });
            else
                return Json(new { success = true });
           
        }
    }
}
