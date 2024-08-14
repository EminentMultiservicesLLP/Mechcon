using BISERP.Models;
using BISERP.Areas.Masters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
//using log4net.Core;

namespace BISERP.Areas.Store.Controllers
{
    public class StoreUnitLinkingController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(StoreUnitLinkingController));

        public StoreUnitLinkingController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }


        [HttpGet]
        public async Task<JsonResult> AllStoreunit(string SubStore, string UnitStore)
        {
            JsonResult jResult;
            List<StoreUnitLinkingModel> unitStore;
            try
            {
                string _url = url + "/storeunit/getallunitstore" + Common.Constants.JsonTypeResult;
                unitStore = await Common.AsyncWebCalls.GetAsync<List<StoreUnitLinkingModel>>(client, _url, CancellationToken.None);
                if (unitStore != null)
                {
                    if (!string.IsNullOrWhiteSpace(SubStore))
                    {
                        unitStore = unitStore.Where(p => p.StoreName.ToUpper().Contains(SubStore.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(UnitStore))
                    {
                        unitStore = unitStore.Where(p => p.UnitStore.ToUpper().Contains(UnitStore.ToUpper())).ToList();
                    }
                    jResult = Json(new { unitStore }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { unitStore }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllStoreunit :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveStoreunit(StoreUnitLinkingModel storeUnit)
        {
            string _url = "";
            bool _isSuccess = true;
            storeUnit.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            storeUnit.InsertedON = System.DateTime.Now;
            storeUnit.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            storeUnit.InsertedMacID = BISERP.Common.Constants.MacId;
            storeUnit.InsertedMacName = BISERP.Common.Constants.MacName;
            if (storeUnit.ID > 0)
            {
                storeUnit.Deactive = true;
                _url = url + "/storeunit/updatestoreunit" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, storeUnit, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    storeUnit = JsonConvert.DeserializeObject<StoreUnitLinkingModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            else
            {
                _url = url + "/storeunit/createstoreunit" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, storeUnit, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    storeUnit = JsonConvert.DeserializeObject<StoreUnitLinkingModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = storeUnit.Message });
            else
                return Json(new { success = true });
        }

        //[HttpPost]
        //public ActionResult updateStoreunit(StoreUnitLinkingModel store)
        //{
        //    string _url = "";
        //    bool _isSuccess = true;
        //    store.InsertedBy = 1;
        //    store.InsertedON = System.DateTime.Now;
        //    store.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
        //    store.InsertedMacID = BISERP.Common.Constants.MacId;
        //    store.InsertedMacName = BISERP.Common.Constants.MacName;

        //    _url = url + "/storeunit/updatestoreunit" + Common.Constants.JsonTypeResult;
        //    var result = client.PutAsync(_url, store, new JsonMediaTypeFormatter()).Result;
        //    if (result.StatusCode.ToString() == "BadRequest")
        //    {
        //        _isSuccess = false;
        //        store = JsonConvert.DeserializeObject<StoreUnitLinkingModel>(result.Content.ReadAsStringAsync().Result);
        //    }
        //    if (!_isSuccess)
        //        return Json(new { success = false, Message = store.Message });
        //    else
        //        return Json(new { success = true });
        //}
    }
}
