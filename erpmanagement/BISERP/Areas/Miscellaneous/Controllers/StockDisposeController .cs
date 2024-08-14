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
    public class StockDispseController : Controller
    {   
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(StockDispseController));

        public StockDispseController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        //
        // GET: /StoreConsumption/
   
    
        [HttpPost]
        public ActionResult Savedispose(StockDisposeModel entity)
        {
            StockDisposeModel sc = new StockDisposeModel();
            List<StockDisposedtModel> scd = new List<StockDisposedtModel>();
            StockDisposedtModel scdt;
            string strErrorMsg = "";
            bool _isSuccess = false;
            var details = entity.StockDisposedt.OrderBy(i => i.DisposeDetailId);
           foreach (var scdts in details)
            {
                if (scdts!=null)
                 {
                     if (scdts.DisposedQuantity == 0 || scdts.DisposedQuantity == null)
                     {
                         strErrorMsg = "Please Enter disposeQty Quantity for Item.";
                         break;
                     }
                     if (scdts.Reason == null)
                     {
                         strErrorMsg = "Please Enter Reason for Item.";
                         break;
                     }
                     if (scdts.DisposedQuantity > scdts.CurrentQty)
                     {
                         strErrorMsg = "Cannot be grater then Batch Quantity";
                         break;
                     }
                     else
                     {
                         scdt = new StockDisposedtModel();
                            scdt = scdts;
                            scd.Add(scdt);
                     }
                 }
            }
           sc.DisposeId = entity.DisposeId;
           sc.StoreId = entity.StoreId;
           sc.DisposeNo = entity.DisposeNo;
           sc.DisposeDate = entity.DisposeDate;
           sc.InsertedBy = BISERP.Common.Constants.AppUserId;
           sc.InsertedON = System.DateTime.Now;
           sc.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
           sc.InsertedMacID = BISERP.Common.Constants.MacId;
           sc.InsertedMacName = BISERP.Common.Constants.MacName;
           sc.StockDisposedt = scd;
           if (strErrorMsg != "")
            {
                _isSuccess = false;
                return Json(new { success = false, Message = strErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            {
               string _url = "";

               _url = url + "/Stockdispose/createStockdispose" + Common.Constants.JsonTypeResult;
               var result = client.PostAsync(_url, sc, new JsonMediaTypeFormatter()).Result;
             
               if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    sc = JsonConvert.DeserializeObject<StockDisposeModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    sc = JsonConvert.DeserializeObject<StockDisposeModel>(result.Content.ReadAsStringAsync().Result);
                }
            }            
            if (!_isSuccess)

                return Json(new { success = false, Message = entity.Message });
            else
                return Json(new { success = true, Message = sc.DisposeNo });
        }
    }
}
