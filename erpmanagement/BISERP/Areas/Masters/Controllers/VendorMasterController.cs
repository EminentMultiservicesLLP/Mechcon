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

namespace BISERP.Areas.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class VendorMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(VendorMasterController));

        public VendorMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllVendor(int? page, int? limit, string sortBy, string direction, string SearchName, string SearchCode)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/Vendor/GetAllVendor" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<VendorMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.Name.ToUpper().Contains(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.Code.ToUpper().Contains(SearchCode.ToUpper())).ToList();
                    }
                    int total = records.Count;
                    if (page.HasValue && limit.HasValue)
                    {
                        int start = (page.Value - 1) * limit.Value;
                        records = records.Skip(start).Take(limit.Value).ToList();
                    }
                    //jResult = Json(new { records, total }, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, records, total }, JsonRequestBehavior.AllowGet);
                }
                else
                    //jResult = Json("Error");
                    jResult = Json(new { success = false, Messsage = "No Vendor found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllVendor :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
       
        [HttpPost]
        public ActionResult SaveVendor(VendorMasterModel Vendor)
        {
            string _url = "";
            bool _isSuccess = true;
            Vendor.UpdatedBy = 1;
            Vendor.UpdatedOn = DateTime.Now;
            Vendor.UpdatedIPAddress = Common.Constants.IpAddress;
            Vendor.UpdatedMacID = Common.Constants.MacId;
            Vendor.UpdatedMacName = Common.Constants.MacName;
            Vendor.InsertedBy = 1;
            Vendor.InsertedON=DateTime.Now;
            Vendor.InsertedIPAddress = Common.Constants.IpAddress;
            Vendor.InsertedMacID = Common.Constants.MacId;
            Vendor.InsertedMacName = Common.Constants.MacName;
            if (Vendor.VendorId > 0)
            {
                  
                _url = url + "/Vendor/UpdateVendor" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, Vendor, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    Vendor = JsonConvert.DeserializeObject<VendorMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            else
            {
               _url = url + "/Vendor/CreateVendor" + Common.Constants.JsonTypeResult;
               var result = client.PostAsync(_url, Vendor, new JsonMediaTypeFormatter()).Result;
               if (result.StatusCode.ToString() == "BadRequest")
               {
                   _isSuccess = false;
                   Vendor = JsonConvert.DeserializeObject<VendorMasterModel>(result.Content.ReadAsStringAsync().Result);
               }
            }
               //return PartialView();
               if (!_isSuccess)
                   return Json(new { success = false, Message = Vendor.Message });
               else
                   return Json(new { success = true });
           }

        [HttpGet]
        public async Task<JsonResult> ActiveVendor(int? page, int? limit, string sortBy, string direction, string SearchName, string SearchCode)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/Vendor/getactiveVendor" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<VendorMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.Name.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.Code.ToUpper().StartsWith(SearchCode.ToUpper())).ToList();
                    }
                    if (page.HasValue && limit.HasValue)
                    {
                        int start = (page.Value - 1) * limit.Value;
                        records = records.Skip(start).Take(limit.Value).ToList();
                    }
                    jResult = Json(new { records, records.Count }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in ActiveVendor :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

    }
}
