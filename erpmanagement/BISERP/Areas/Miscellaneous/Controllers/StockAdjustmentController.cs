using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using BISERP.Areas.Store.Models.Store;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using BISERP.Areas.Miscellaneous.Models;

namespace BISERP.Areas.Miscellaneous.Controllers
{
    public class StockAdjustmentController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(StockAdjustmentController));

        public StockAdjustmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveStockAdjustment(AdjustmentVoucherModel entity)
        {
            string strErrorMsg = "";
            bool _isSuccess = false;
           // int intRowCtr = 0;
           
            //foreach (var details in entity.Voucherdetails)
            //{
            //    if (details != null)
            //    {
            //        if (details.state == true)
            //        {
            //            intRowCtr++;
            //        }
            //    }
            //}
            //if(intRowCtr == 0)
            //{
            //    strErrorMsg = "Please Select Items";                
            //}
            entity.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            entity.InsertedON = System.DateTime.Now;
            entity.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            entity.InsertedMacID = BISERP.Common.Constants.MacId;
            entity.InsertedMacName = BISERP.Common.Constants.MacName;
            if (strErrorMsg != "")
            {
                _isSuccess = false;
                return Json(new { success = false, Message = strErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            {
                string _url = url + "/voucher/createadjvoucher" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, entity, new JsonMediaTypeFormatter()).Result;

                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    entity = JsonConvert.DeserializeObject<AdjustmentVoucherModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    entity = JsonConvert.DeserializeObject<AdjustmentVoucherModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)

                return Json(new { success = false, Message = strErrorMsg });
            else

                return Json(new { success = true, Message = entity.VoucherNo });
        }
    }
}
