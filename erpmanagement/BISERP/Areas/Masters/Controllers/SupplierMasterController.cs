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
    public class SupplierMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(SupplierMasterController));

        public SupplierMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllSupplier(int? page, int? limit, string sortBy, string direction, string SearchName, string SearchCode)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/Supplier/GetAllSupplier" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<SupplierMasterModel>>(client, _url, CancellationToken.None);
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
                    jResult = Json(new {success=true, records, total }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Supplier Entry found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllSuppliers :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Server Error! Please Contact administrator!");
            }
            return jResult;
        }
       
        [HttpPost]
        public ActionResult SaveSupplier(SupplierMasterModel Supplier)
        {
            string _url = "";
            bool _isSuccess = true;
            Supplier.UpdatedBy = 1;
            Supplier.UpdatedOn = System.DateTime.Now;
            Supplier.UpdatedIPAddress = BISERP.Common.Constants.IpAddress;
            Supplier.UpdatedMacID = BISERP.Common.Constants.MacId;
            Supplier.UpdatedMacName = BISERP.Common.Constants.MacName;   
            if (Supplier.ID > 0)
            {
                  
                _url = url + "/Supplier/UpdateSupplier" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, Supplier, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                }
                    Supplier = JsonConvert.DeserializeObject<SupplierMasterModel>(result.Content.ReadAsStringAsync().Result);
            }
            else
            {
               _url = url + "/Supplier/CreateSupplier" + Common.Constants.JsonTypeResult;
               var result = client.PostAsync(_url, Supplier, new JsonMediaTypeFormatter()).Result;
               if (result.StatusCode.ToString() == "BadRequest")
               {
                   _isSuccess = false;
               }
                   Supplier = JsonConvert.DeserializeObject<SupplierMasterModel>(result.Content.ReadAsStringAsync().Result);
            }
               //return PartialView();
               if (!_isSuccess)
                   return Json(new { success = false, Message = Supplier.Message });
               else
                   return Json(new { success = true, Data = Supplier });
        }

       
        [HttpPost]
        public ActionResult AuthCanSupplier(SupplierMasterModel Supplier)
        {
            string _url = "";
            bool _isSuccess = true;
            
            _url = url + "/Supplier/AuthCanSupplier" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, Supplier, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                Supplier = JsonConvert.DeserializeObject<SupplierMasterModel>(result.Content.ReadAsStringAsync().Result);
            }
                   
            //return PartialView();
            if (!_isSuccess)
                return Json(new { success = false, Message = Supplier.Message });
            else
                return Json(new { success = true, Data = Supplier });
        }


        [HttpGet]
        public async Task<JsonResult> ActiveSupplier(int? page, int? limit, string sortBy, string direction, string SearchName, string SearchCode)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/supplier/getactivesupplier" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<SupplierMasterModel>>(client, _url, CancellationToken.None);
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
                _logger.LogError("Error in ActiveSupplier :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

      
        [HttpGet]
        public async Task<JsonResult> GetSupplierById(int id)
        {
            JsonResult jResult;
            SupplierMasterModel items = new SupplierMasterModel();
            try
            {
                string _url = url + "/supplier/getSupplierById/" + id.ToString() + Common.Constants.JsonTypeResult;
                items = await Common.AsyncWebCalls.GetAsync<SupplierMasterModel>(client, _url, CancellationToken.None);
                if (items != null)
                {
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetMechconData :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

    }
}
