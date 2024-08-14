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
    public class DeliveryTermController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(DeliveryTermController));

        public DeliveryTermController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllDeliveryTerms(string SearchName, string Searchcode)
        {
            List<DeliveryTermMasterModel> dimodel = new List<DeliveryTermMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "deliveryterms/getalldeliveryterms" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<DeliveryTermMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.DeliveryTermDesc.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(Searchcode))
                    {
                        records = records.Where(p => p.DeliveryTermCode.ToUpper().StartsWith(Searchcode.ToUpper())).ToList();
                    }
                    int total = records.Count;
                  
                    jResult = Json(new {success = true,  records, total }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Details found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllDeliveryTerms :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Server Error! Please Contact administrator!");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetActiveDeliveryTerms(string searchDesc, string searchCode)
        {
            JsonResult jResult;
            List<DeliveryTermMasterModel> records = new List<DeliveryTermMasterModel>();
            try
            {
                string _url = url + "deliveryterms/getactive" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<DeliveryTermMasterModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(searchDesc))
                    {
                        records = records.Where(p => p.DeliveryTermDesc.ToUpper().StartsWith(searchDesc.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(searchCode))
                    {
                        records = records.Where(p => p.DeliveryTermCode.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                    }
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetActiveDeliveryTerms :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
     
        [HttpPost]
        public ActionResult SaveDeliveryTerm(DeliveryTermMasterModel Delivery)
        {
            string _url = "";
            bool _isSuccess = true;
            Delivery.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            Delivery.InsertedON = System.DateTime.Now;
            Delivery.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            Delivery.InsertedMacID = BISERP.Common.Constants.MacId;
            Delivery.InsertedMacName = BISERP.Common.Constants.MacName;
            if (Delivery.TermID > 0)
            {
                _url = url + "/deliveryterms/updatedeliveryterm" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, Delivery, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    Delivery = JsonConvert.DeserializeObject<DeliveryTermMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
                if (!_isSuccess)
                    return Json(new { success = false, Message = Delivery.Message });
                else
                    return Json(new { success = true });
            }
            else
            {
                _url = url + "/deliveryterms/createdeliveryterm" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, Delivery, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    Delivery = JsonConvert.DeserializeObject<DeliveryTermMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = Delivery.Message });
            else
                return Json(new { success = true });
        }
    }
}
