using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using BISERP.Areas.Branch.Models;

namespace BISERP.Areas.Branch.Controllers
{
    public class MomentOrderController : Controller
    {
        readonly HttpClient _client;
        readonly string _url = Common.Constants.WebAPIAddress;
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(MomentOrderController));

        public MomentOrderController()
        {
            _client = new HttpClient { BaseAddress = new Uri(_url) };
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveMomentOrder(MomentOrderModel model)
        {
            //string strErrorMsg = "";
            var isSuccess = false;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = Common.Constants.IpAddress;
            model.InsertedMacID = Common.Constants.MacId;
            model.InsertedMacName = Common.Constants.MacName;

            string url = _url + "/MomentOrder/CreateMomentOrder" + Common.Constants.JsonTypeResult;
            var result = _client.PostAsync(url, model, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                model = JsonConvert.DeserializeObject<MomentOrderModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                isSuccess = true;
                model = JsonConvert.DeserializeObject<MomentOrderModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "OK")
            {
                isSuccess = true;
            }

            if (!isSuccess)
                return Json(new { success = false, Message = "Error In Moment Order" });
            else
                return Json(new { success = true, Message = model.Code });
        }

        [HttpGet]
        public async Task<JsonResult> GetAllMomentOrderList()
        {
            JsonResult jResult;
            try
            {
                string url = _url + "/MomentOrder/GetAllMomentOrderList/" + Session["AppUserId"] + Common.Constants.JsonTypeResult;
                var mimodel = await Common.AsyncWebCalls.GetAsync<List<MomentOrderModel>>(_client, url, CancellationToken.None);
                if (mimodel != null)
                {
                    mimodel = mimodel.OrderByDescending(m => m.OrderId).ToList();
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { MomentOrderModels = (List<MomentOrderModel>)null }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetAllMomentOrderList :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }


        [HttpPost]
        public ActionResult SaveMomentOrderacceptance(MomentOrderModel model)
        {
            //string strErrorMsg = "";
            var isSuccess = false;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = Common.Constants.IpAddress;
            model.InsertedMacID = Common.Constants.MacId;
            model.InsertedMacName = Common.Constants.MacName;

            var url = _url + "/MomentOrder/UpdateMomentOrder" + Common.Constants.JsonTypeResult;
            var result = _client.PostAsync(url, model, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                model=JsonConvert.DeserializeObject<MomentOrderModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                isSuccess = true;
                model=JsonConvert.DeserializeObject<MomentOrderModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "OK")
            {
                isSuccess = true;
            }

            if (!isSuccess)
                return Json(new { success = false, Message = "Error In  OrderAcceptance" });
            else
                return Json(new { success = true, Message = model.StatupCode,model.Status });
        }

        //-------------------------
        [HttpPost]
        public ActionResult ClearBatchFullOrderacceptance(MomentOrderModel model)
        {
            //string strErrorMsg = "";
            var isSuccess = false;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = Common.Constants.IpAddress;
            model.InsertedMacID = Common.Constants.MacId;
            model.InsertedMacName = Common.Constants.MacName;

            var url = _url + "/MomentOrder/ClearBatchFullOrderacceptance" + Common.Constants.JsonTypeResult;
            var result = _client.PostAsync(url, model, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                model = JsonConvert.DeserializeObject<MomentOrderModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                isSuccess = true;
                model = JsonConvert.DeserializeObject<MomentOrderModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "OK")
            {
                isSuccess = true;
            }

            if (!isSuccess)
                return Json(new { success = false, Message = "Error In  OrderAcceptance" });
            else
                return Json(new { success = true, Message = model.StatupCode, model.Status });
        }
    }
}
