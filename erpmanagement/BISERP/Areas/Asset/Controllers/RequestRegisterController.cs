using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http;
using BISERP.Areas.Asset.Models;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace BISERP.Areas.Asset.Controllers
{
    public class RequestRegisterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(RequestRegisterController));

        public RequestRegisterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetRequestno(int BranchId)
        {
            List<RequestRegisterModel> records = new List<RequestRegisterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/requestreg/GetRequestno/" + BranchId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<RequestRegisterModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetRequestno :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetRequestDetail(int RequisitionId)
        {
            RequestRegisterModel records = new RequestRegisterModel();
            JsonResult jResult;
            try
            {
                string _url = url + "/requestreg/getrequestdt/" + RequisitionId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<RequestRegisterModel>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetRequestDetail :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetRequestNoDeletion(int BranchId)
        {
            List<RequestRegisterModel> records = new List<RequestRegisterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/requestreg/GetRequestNoDeletion/" + BranchId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<RequestRegisterModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetRequestNoDeletion :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveRequestRegister(RequestRegisterModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            if(model.RequestId > 0)
            {
                _url = url + "/requestreg/UpdateRequestRegister" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<RequestRegisterModel>(result.Content.ReadAsStringAsync().Result);
                }
                if (result.StatusCode.ToString() == "OK")
                {
                    _isSuccess = true;
                }
                if (!_isSuccess)
                    return Json(new { success = false, Message = "Error In Request Register" });
                else
                    return Json(new { success = true });
            }
            else
            {
                _url = url + "/requestreg/CreateRequestRegister" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<RequestRegisterModel>(result.Content.ReadAsStringAsync().Result);
                }
                if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    model = JsonConvert.DeserializeObject<RequestRegisterModel>(result.Content.ReadAsStringAsync().Result);
                }
                if (!_isSuccess)
                    return Json(new { success = false, Message = "Error In Request Register" });
                else
                    return Json(new { success = true, Message = model.RequestCode });
            }            
        }

        [HttpPost]
        public ActionResult UpdRequestAcceptance(RequestRegisterModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/requestreg/UpdRequestAcceptance" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<RequestRegisterModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false,  Message = "Error In  Accept Request "  });
            else
                return Json(new { success = true, Message = "Accept Request successfully" });
        }

        [HttpGet]
        public async Task<JsonResult> GetRequestRegister(DateTime? fromdate, DateTime? todate, int BranchId, int Status)
        {
            JsonResult jResult;
            List<RequestRegisterModel> model = new List<RequestRegisterModel>();
            try
            {
                string strfromdate = ""; string strtodate = "";
                if (fromdate == null)
                    strfromdate = System.DateTime.UtcNow.ToString("MM-dd-yy");
                else
                    strfromdate = Convert.ToDateTime(fromdate).ToString("MM-dd-yy");

                if (fromdate == null)
                    strtodate = System.DateTime.UtcNow.ToString("MM-dd-yy");
                else
                    strtodate = Convert.ToDateTime(todate).ToString("MM-dd-yy");

                string _url = url + "/requestreg/getregrequest/" + strfromdate + "/" + strtodate + "/" + BranchId + "/" + Status + Common.Constants.JsonTypeResult;
                model = await Common.AsyncWebCalls.GetAsync<List<RequestRegisterModel>>(client, _url, CancellationToken.None);
                if (model != null)
                {
                    jResult = Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetRequestRegister :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetAllRequestRegister()
        {
            JsonResult jResult;
            List<RequestRegisterModel> model = new List<RequestRegisterModel>();
            try
            {
                string _url = url + "/requestreg/getallregrequest" + Common.Constants.JsonTypeResult;
                model = await Common.AsyncWebCalls.GetAsync<List<RequestRegisterModel>>(client, _url, CancellationToken.None);
                if (model != null)
                {
                    jResult = Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllRequestRegister :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetRequestNotification(int DueDays)
        {
            List<RequestRegisterModel> records = new List<RequestRegisterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/requestreg/getRequestNotification/" + DueDays + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<RequestRegisterModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    // jResult = Json(records, JsonRequestBehavior.AllowGet);
                    jResult = Json(records, JsonRequestBehavior.AllowGet); 
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetRequestNotification :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
