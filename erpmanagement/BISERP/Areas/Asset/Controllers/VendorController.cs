using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Store.Models.Master;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using BISERP.Areas.Asset.Models;

namespace BISERP.Areas.Asset.Controllers
{
    public class VendorController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(VendorController));

        public VendorController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllVendor( string SearchName, string SearchCode)
        {
            JsonResult jResult;
            List<VendorModel> records = new List<VendorModel>();
            try
            {
                string _url = url + "/vendor/getallvendor" + Common.Constants.JsonTypeResult;
                 records = await Common.AsyncWebCalls.GetAsync<List<VendorModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.VendorName.ToUpper().Contains(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.VendorCode.ToUpper().Contains(SearchCode.ToUpper())).ToList();
                    }
                    int total = records.Count;
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllVendor :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Vendor Master Error! Please Contact administrator!");
            }
            return jResult;
        }
       
        [HttpPost]
        public ActionResult SaveVendor(VendorModel Vendor)
        {
            string _url = "";
            bool _isSuccess = true;
            Vendor.InsertedBy = 1;
            Vendor.InsertedON = DateTime.Now;
            Vendor.InsertedIPAddress = Common.Constants.IpAddress;
            Vendor.InsertedMacID = Common.Constants.MacId;
            Vendor.InsertedMacName = Common.Constants.MacName;
            if (Vendor.VendorId > 0)
            {

                _url = url + "/vendor/Updatevendor" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, Vendor, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    Vendor = JsonConvert.DeserializeObject<VendorModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            else
            {
                _url = url + "/vendor/Createvendor" + Common.Constants.JsonTypeResult;
               var result = client.PostAsync(_url, Vendor, new JsonMediaTypeFormatter()).Result;
               if (result.StatusCode.ToString() == "BadRequest")
               {
                   _isSuccess = false;
                   Vendor = JsonConvert.DeserializeObject<VendorModel>(result.Content.ReadAsStringAsync().Result);
               }
            }
               //return PartialView();
               if (!_isSuccess)
                   return Json(new { success = false, Message = Vendor.Message });
               else
                   return Json(new { success = true });
           
        
          
        }

        [HttpGet]
        public async Task<JsonResult> ActiveVendor( string SearchName, string SearchCode)
        {
            List<VendorModel> records = new List<VendorModel>();
              JsonResult jResult;
              try
              {
                  string _url = url + "/vendor/getactivevendor" + Common.Constants.JsonTypeResult;
                  records = await Common.AsyncWebCalls.GetAsync<List<VendorModel>>(client, _url, CancellationToken.None);
                  if (records != null)
                  {
                      jResult = Json(records, JsonRequestBehavior.AllowGet);
                  }
                  else
                      jResult = Json("Error");
              }
              catch (Exception ex)
              {
                  _logger.LogError("Error in AllActiveVendor :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                  jResult = Json("Error");
              }
              return jResult;
        }


     
    }
}
