using BISERP.Areas.Store.Models.Store;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using BISERP.Common;

namespace BISERP.Areas.Store.Controllers
{
    public class GRNController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(GRNController));

        public GRNController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public JsonResult SaveGRN(GRNModel grn)
        {
            string _url = "";
            bool _isSuccess = false;
            grn.PONo = "GRN-";
            grn.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            grn.InsertedON = System.DateTime.Now;
            grn.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            grn.InsertedMacID = BISERP.Common.Constants.MacId;
            grn.InsertedMacName = BISERP.Common.Constants.MacName;

            if (grn.ID > 0)
            {
                _url = url + "/grn/updategrn" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, grn, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                }
                else if (result.StatusCode.ToString() == "OK")
                {
                    _isSuccess = true;
                }
                if (!_isSuccess)
                    return Json(new { success = false });
                else
                    return Json(new { success = true });
            }
            else
            {
                _url = url + "/grn/creategrn" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, grn, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    grn = JsonConvert.DeserializeObject<GRNModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    grn = JsonConvert.DeserializeObject<GRNModel>(result.Content.ReadAsStringAsync().Result);
                }
                if (_isSuccess)
                {
                    UtilitiesCls.SendEmailTask(EmailProcessFor.GrnRequest, this, grn);
                }
                if (!_isSuccess)
                    return Json(new { success = false, Message = grn.Message, Data = grn });
                else
                    return Json(new { success = true, Message = grn.GRNNo, Data = grn });
            }            
        }

        [HttpGet]
        public async Task<JsonResult> GRNForAuthorization(int? StoreId, string SearchNumber = null)
        {
            JsonResult jResult;
            List<GRNModel> records = new List<GRNModel>();
            try
            {
                if (StoreId == null)
                    StoreId = 0;
                string _url = url + "/grn/grnforauth/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<GRNModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(SearchNumber))
                    {
                        records = records.Where(p => p.GRNNo.ToUpper().StartsWith(SearchNumber.ToUpper())).ToList();
                    }
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GRNForAuthorization :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GRNDetails(int GRNId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/grn/getgrndetail/" + GRNId.ToString() + Common.Constants.JsonTypeResult;
                List<GRNDetailModel> _grndetail = await Common.AsyncWebCalls.GetAsync<List<GRNDetailModel>>(client, _url, CancellationToken.None);

                if (_grndetail != null)
                {
                    _grndetail.ForEach(m => m.state = true);
                    jResult = Json(_grndetail, JsonRequestBehavior.AllowGet);
                }
                else
                    //jResult = Json("Error");
                    jResult = Json(_grndetail, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GRNDetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public JsonResult AuthCancelGRN(GRNModel grnmodel)
        {
            string _url = "";
            bool _isSuccess = true;
            GRNModel grn = new GRNModel();
            List<GRNDetailModel> _grnDetails = new List<GRNDetailModel>();
            GRNDetailModel mdt;
            foreach (var grnDt in grnmodel.GRNDetails)
            {
                mdt = new GRNDetailModel();
                mdt = grnDt;
                if(grnmodel.Authorized == true)
                {
                    if (mdt.state == true)
                    {
                        mdt.Authorised = true;
                        mdt.strauthorized = "Authorised";
                    }
                    else
                    {
                        mdt.Authorised = false;
                        mdt.strauthorized = "Cancelled";
                    }
                }    
                else
                {
                    mdt.Authorised = false;
                    mdt.strauthorized = "Cancelled";
                }
                _grnDetails.Add(mdt);
            }
            grn.ID = grnmodel.ID;
            grn.Authorized = grnmodel.Authorized;
            grn.Cancelled = grnmodel.Cancelled;
            grn.StoreId = grnmodel.StoreId;
            grn.AuthorizedDate = grnmodel.AuthorizedDate;
            grn.GrnTypeID = grnmodel.GrnTypeID;
            grn.StoreName = grnmodel.StoreName;
            grn.SupplierName = grnmodel.SupplierName;
            grn.strGRNDate = grnmodel.strGRNDate;
            grn.GRNNo = grnmodel.GRNNo;
            grn.DCNo = grnmodel.DCNo;
            grn.GRNDetails = _grnDetails;
            grn.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            grn.InsertedON = System.DateTime.Now;
            grn.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            grn.InsertedMacID = BISERP.Common.Constants.MacId;
            grn.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/grn/authcancelgrn" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, grn, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<GRNModel>().Id;
            if (_isSuccess)
            {
                UtilitiesCls.SendEmailTask(EmailProcessFor.GrnAuthorization, this, grn);
               
            }
            if (!_isSuccess)
                return Json(new { success = false});
            else
                return Json(new { success = true });
        
        }
        [HttpGet]
        public async Task<JsonResult> AllGRNNO(int StoreId, DateTime fromdate, DateTime todate, int SuppId)
        {
            JsonResult jResult;
            List<GRNModel> records = new List<GRNModel>();
            try
            {
                string _url = url + "/grn/getallgrnno/" + StoreId + "/" + SuppId + "/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<GRNModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {                    
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllGRNNO :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        public async Task<JsonResult> ExportGrn(int grnId, DateTime grnDate,string storename)
        {
            BISERP.Views.Shared.ReportViewer rptViewr = new BISERP.Views.Shared.ReportViewer();
            rptViewr.GRNDetailsReport(grnDate, grnDate, 0, 0, grnId, storename, "PDF");
            return Json(new { success = true });
        }


        [HttpGet]
        public async Task<JsonResult> GRNForReport(int? StoreId, string SearchNumber = null)
        {
            JsonResult jResult;
            List<GRNModel> records = new List<GRNModel>();
            try
            {
                if (StoreId == null)
                    StoreId = 0;
                string _url = url + "/grn/grnforreport/" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<GRNModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    //if (!string.IsNullOrWhiteSpace(SearchNumber))
                    //{
                    //    records = records.Where(p => p.GRNNo.ToUpper().StartsWith(SearchNumber.ToUpper())).ToList();
                    //}
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GRNForAuthorization :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
