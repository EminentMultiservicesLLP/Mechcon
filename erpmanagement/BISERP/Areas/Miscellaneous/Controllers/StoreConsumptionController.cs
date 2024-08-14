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
using BISERP.Areas.Store.Models.Store;
using Newtonsoft.Json;
using BISERP.Areas.Miscellaneous.Models;

namespace BISERP.Areas.Miscellaneous.Controllers
{
    public class StoreConsumptionController : Controller
    {   
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(StoreConsumptionController));

        public StoreConsumptionController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        //
        // GET: /StoreConsumption/
        [HttpGet]
        public async Task<JsonResult> GetentityConsumption(int StoreId, string strSearchItemName = null)
          {
            JsonResult jResult;
            List<StoreConumptiondtModel> entity = new List<StoreConumptiondtModel>();

            try
            {
                if (strSearchItemName == null)
                    strSearchItemName = "";

                string _url = url + "/StockConsumption/getItemconsumption/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                entity = await Common.AsyncWebCalls.GetAsync<List<StoreConumptiondtModel>>(client, _url, CancellationToken.None);
                if (entity != null && entity.Count > 0)
                {
                    if (strSearchItemName != "")
                        entity = entity.Where(p => p.ItemName.ToUpper().StartsWith(strSearchItemName.ToUpper().Trim())).ToList();

                    jResult = Json(entity, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(entity, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetentityConsumption :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetConsumptionNO( string strSearchItemName = null)
        {
            JsonResult jResult;
            List<StoreConumptionModel> entity = new List<StoreConumptionModel>();

            try
            {
                if (strSearchItemName == null)
                    strSearchItemName = "";

                string _url = url + "/StockConsumption/getConsumptionNO/" + Common.Constants.JsonTypeResult;
                entity = await Common.AsyncWebCalls.GetAsync<List<StoreConumptionModel>>(client, _url, CancellationToken.None);
                if (entity != null && entity.Count > 0)
                {
                    if (strSearchItemName != "")
                        entity = entity.Where(p => p.ConsumptionCode.ToUpper().StartsWith(strSearchItemName.ToUpper().Trim())).ToList();

                    jResult = Json(entity, JsonRequestBehavior.AllowGet);
                }
                else
                   jResult = Json(entity, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetConsumptionNO :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpPost]
        public ActionResult SaveConsumption(StoreConumptionModel entity)
        {
            StoreConumptionModel sc = new StoreConumptionModel();
            List<StoreConumptiondtModel> scd = new List<StoreConumptiondtModel>();
            StoreConumptiondtModel scdt;
            string strErrorMsg = "";
            bool _isSuccess = false;
            var details = entity.StockConsumptiondt.OrderBy(i =>i.ConsumptionDtlId);
           foreach (var scdts in details)
            {
                if (scdts!=null)
                 {
                     if (scdts.ConsumeQty == 0 || scdts.ConsumeQty ==null)
                     {
                         strErrorMsg = "Please Enter ConsumeQty Quantity for Item.";
                         break;
                     }
                     if (scdts.Remark == null)
                     {
                         strErrorMsg = "Please Enter Remark for Item.";
                         break;
                     }
                     if (scdts.ConsumeQty > scdts.StockQty)
                     {
                         strErrorMsg = "Cannot be grater then Batch Quantity";
                         break;
                     }
                     else
                     {
                            scdt = new StoreConumptiondtModel();
                            scdt = scdts;
                            scd.Add(scdt);
                     }
                 }
            }
           sc.ConsumptionId = entity.ConsumptionId;
           sc.StoreId = entity.StoreId;
           sc.ConsumptionCode = entity.ConsumptionCode;
           sc.ConsumptionDate = entity.ConsumptionDate;
           sc.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
           sc.InsertedON = System.DateTime.Now;
           sc.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
           sc.InsertedMacID = BISERP.Common.Constants.MacId;
           sc.InsertedMacName = BISERP.Common.Constants.MacName;
           sc.StockConsumptiondt = scd;
           if (strErrorMsg != "")
            {
                _isSuccess = false;
                return Json(new { success = false, Message = strErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            {
               string _url = "";
        
               _url = url + "/StockConsumption/createStockConsumption" + Common.Constants.JsonTypeResult;
               var result = client.PostAsync(_url, sc, new JsonMediaTypeFormatter()).Result;
             
               if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    sc = JsonConvert.DeserializeObject<StoreConumptionModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    sc = JsonConvert.DeserializeObject<StoreConumptionModel>(result.Content.ReadAsStringAsync().Result);
                }
            }            
            if (!_isSuccess)
                
                return Json(new { success = false, Message = entity.Message });
            else
                return Json(new { success = true, Message = sc.ConsumptionCode });
        }
        public async Task<JsonResult> GetConsumptioncodedt(int Id, string strSearchItemName = null)
        {
            JsonResult jResult;
            List<StoreConsumptionCancellationModel> entity = new List<StoreConsumptionCancellationModel>();

            try
            {
                if (strSearchItemName == null)
                    strSearchItemName = "";

                string _url = url + "/StockConsumption/getconsumptiondt/" + Id.ToString() + Common.Constants.JsonTypeResult;
                entity = await Common.AsyncWebCalls.GetAsync<List<StoreConsumptionCancellationModel>>(client, _url, CancellationToken.None);
                if (entity != null && entity.Count > 0)
                {
                    if (strSearchItemName != "")
                        entity = entity.Where(p => p.ItemName.ToUpper().StartsWith(strSearchItemName.ToUpper().Trim())).ToList();

                    jResult = Json(entity, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(entity, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetConsumptioncodedt :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpPost]
        public ActionResult SaveConsumptioncel(List<StoreConsumptionCancellationModel> items)
        {
            string _url = "";
            string strErrorMsg = "";
            List<StoreConsumptionCancellationModel> modelList = new List<StoreConsumptionCancellationModel>();
            StoreConsumptionCancellationModel model = new StoreConsumptionCancellationModel();
            
            foreach (var item in items)
            {
                item.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                item.InsertedON = DateTime.Now;
                item.InsertedIPAddress = Common.Constants.IpAddress;
                item.InsertedMacID = Common.Constants.MacId;
                item.InsertedMacName = Common.Constants.MacName;
                if (item != null)
                {
                   
                    if (item.Qty == null ||item.Qty == 0 )
                    {
                        strErrorMsg = "Please Enter CancelQty for Item";
                        break;
                    }

                    if (item.Remark == null)
                    {
                        strErrorMsg = "Please Enter Remark for Item";
                        break;
                    }
                    if (item.Qty > item.ConsumeQty)
                    {
                        strErrorMsg = "Cannot be grater then ConsumeQty ";
                        break;
                    }
                    else
                    {
                        item.Cancelledby = Convert.ToInt32(Session["AppUserId"].ToString());
                        model = item;
                        modelList.Add(model);
                    }
                }
               
            }
            if (strErrorMsg!= "")
            {
                return Json(new { success = false ,Message = strErrorMsg});
            }
            else
            {
                _url = url + "/StockConsumption/createStockcancel" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, modelList, new JsonMediaTypeFormatter()).Result.Content.ReadAsAsync<StoreConsumptionCancellationModel>().Id;

                return Json(new { success = true , Message = strErrorMsg});
            }
        }

      
    }
}
