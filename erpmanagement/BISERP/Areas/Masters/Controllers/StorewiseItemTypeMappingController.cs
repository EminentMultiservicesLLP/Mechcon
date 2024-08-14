using BISERP.Areas.Store.Models.Master;
using BISERP.Areas.Masters.Models;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERP.Filters;

namespace BISERP.Areas.Masters.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class StorewiseItemTypeMappingController : Controller
    {
        //
        // GET: /StorewiseItemTypeMapping/

        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(StorewiseItemTypeMappingController));
        public StorewiseItemTypeMappingController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

     
        [HttpGet]
        public async Task<JsonResult> StorewiseItemTypeItems(int storeId)
        {
            
            JsonResult jResult;
            List<StorewiseItemTypeMappingMasterModel> records = new List<StorewiseItemTypeMappingMasterModel>();
            try
            {
                string _url = url + "/storeitemtype/getallstoreitemtypeid/" + storeId.ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<StorewiseItemTypeMappingMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in StorewiseItemTypeItems  :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        //[HttpPost]
        //public ActionResult SaveStorewiseItemType(StorewiseItemTypeMappingMasterModel item)
        //{
        //        string _url = "";
        //        item.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
        //        item.InsertedON = System.DateTime.Now;
        //        item.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
        //        item.InsertedMacID = BISERP.Common.Constants.MacId;
        //        item.InsertedMacName = BISERP.Common.Constants.MacName;
        //        if (item.StoreID > 0)
        //        {
        //            _url = url + "/storeitemtype/createstoreitemtype" + Common.Constants.JsonTypeResult;
        //            var result = client.PostAsync(_url, item, new JsonMediaTypeFormatter()).Result.Content.ReadAsAsync<StorewiseItemTypeMappingMasterModel>().Id;
        //        }
        //    return Json(new { success = true });
        //}
        

        [HttpPost]
        public ActionResult UpdateStorewiseItemType(StorewiseItemTypeMappingMasterModel unit)
        {
            string _url = "";
            unit.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            unit.InsertedON = System.DateTime.Now;
            unit.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            unit.InsertedMacID = BISERP.Common.Constants.MacId;
            unit.InsertedMacName = BISERP.Common.Constants.MacName;
            if (unit.StoreID > 0)
            {
                _url = url + "/storeitemtype/Updatestoreitemtype" + Common.Constants.JsonTypeResult;
                client.PostAsync(_url, unit, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<int>().Result;
            }
         
            //return PartialView();
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult SaveStorewiseItemType(List<StorewiseItemTypeMappingMasterModel> items)
        {
            string _url = "";
            List<StorewiseItemTypeMappingMasterModel> modelList = new List<StorewiseItemTypeMappingMasterModel>();
            foreach (var item in items)
            {
                StorewiseItemTypeMappingMasterModel model = new StorewiseItemTypeMappingMasterModel();
                item.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                item.InsertedON = System.DateTime.Now;
                item.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                item.InsertedMacID = BISERP.Common.Constants.MacId;
                item.InsertedMacName = BISERP.Common.Constants.MacName;
                model = item;
                modelList.Add(model);
            }

            _url = url + "/storeitemtype/Updatestoreitemtype" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, modelList, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<StorewiseItemTypeMappingMasterModel>().Id;
          
            return Json(new { success = true });
        }
     
    }
}
