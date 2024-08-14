using BISERP.Areas.Masters.Models;
using BISERP.Filters;
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

namespace BISERP.Areas.Store.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class UnitMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(UnitMasterController));

        public UnitMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllUnits(string SearchName, string Searchcode)
        {
            List<UnitMasterModel> records = new List<UnitMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/Units/GetAllUnits" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<UnitMasterModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.UnitName.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(Searchcode))
                    {
                        records = records.Where(p => p.UnitCode.ToUpper().StartsWith(Searchcode.ToUpper())).ToList();
                    }
                    //jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Massage = "No Record Available" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllUnits :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Server Error! Please Contact administrator!");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveUnit(UnitMasterModel unit)
        {
            string _url = "";
            bool _isSuccess = true;
            unit.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            unit.InsertedON = System.DateTime.Now;
            unit.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            unit.InsertedMacID = BISERP.Common.Constants.MacId;
            unit.InsertedMacName = BISERP.Common.Constants.MacName;        
            _url = url + "/Units/CreateUnit" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, unit, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                unit = JsonConvert.DeserializeObject<UnitMasterModel>(result.Content.ReadAsStringAsync().Result);
            }
           
          if (!_isSuccess)
              return Json(new { success = false, Message = unit.Message });
          else
              return Json(new { success = true });

        }
        [HttpPost]
        public ActionResult updateunit(UnitMasterModel unit)
        {
            string _url = "";
            bool _isSuccess = true;
            unit.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            unit.InsertedON = System.DateTime.Now;
            unit.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            unit.InsertedMacID = BISERP.Common.Constants.MacId;
            unit.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/Units/UpdateUnit" + Common.Constants.JsonTypeResult;
            //client.PutAsync(_url, store, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<int>().Result;
            var result = client.PostAsync(_url, unit, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                unit = JsonConvert.DeserializeObject<UnitMasterModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = unit.Message });
            else
                return Json(new { success = true });
        }
    }
}
