using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Store.Models.Store;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using BISERP.Areas.VendorProcess.Models;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERP.Areas.VendorProcess.Controllers
{
    public class GRNVendorController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(GRNVendorController));

        public GRNVendorController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpPost]
        public JsonResult SaveGRN(GRNVendorModel grn)
        {
            string _url = "";
            bool _isSuccess = false;
            grn.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            grn.InsertedON = System.DateTime.Now;
            grn.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            grn.InsertedMacID = BISERP.Common.Constants.MacId;
            grn.InsertedMacName = BISERP.Common.Constants.MacName;

            if (grn.ID > 0)
            {
                _url = url + "/grnvendor/creategrnvendor" + Common.Constants.JsonTypeResult;
                client.PostAsync(_url, grn, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<int>().Result;
            }
            else
            {
                _url = url + "/grnvendor/creategrnvendor" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, grn, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    grn = JsonConvert.DeserializeObject<GRNVendorModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    grn = JsonConvert.DeserializeObject<GRNVendorModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = grn.Message });
            else
                return Json(new { success = true, Message = grn.GRNNo });
        }

        [HttpGet]
        public async Task<JsonResult> GRNForAuthorization(int? StoreId, string SearchNumber = null)
        {
            JsonResult jResult;
            List<GRNVendorModel> records = new List<GRNVendorModel>();
            try
            {
                if (StoreId == null)
                    StoreId = 0;
                string _url = url + "/grnvendor/grnvendorforauth/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<GRNVendorModel>>(client, _url, CancellationToken.None);
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
                string _url = url + "/grnvendor/getgrnvendordetail/" + GRNId.ToString() + Common.Constants.JsonTypeResult;
                List<GRNDetailModel> _grndetail = await Common.AsyncWebCalls.GetAsync<List<GRNDetailModel>>(client, _url, CancellationToken.None);
                _grndetail.ForEach(m => m.state = true);
                if (_grndetail != null)
                {
                    jResult = Json(_grndetail, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GRNDetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public JsonResult AuthCancelGRN(GRNVendorModel GRNVendorModel)
        {
            string _url = "";
            GRNVendorModel grn = new GRNVendorModel();
            List<GRNVendorDetailModel> _grnDetails = new List<GRNVendorDetailModel>();
            GRNVendorDetailModel mdt;
            foreach (var grnDt in GRNVendorModel.GRNDetails)
            {
                mdt = new GRNVendorDetailModel();
                mdt = grnDt;
                if (GRNVendorModel.Authorized == true)
                {
                    if (mdt.state == true)
                    {
                        mdt.Authorised = true;
                    }
                    else
                    {
                        mdt.Authorised = false;
                    }
                }
                else
                {
                    mdt.Authorised = false;
                }
                _grnDetails.Add(mdt);
            }
            grn.ID = GRNVendorModel.ID;
            grn.Authorized = GRNVendorModel.Authorized;
            grn.Cancelled = GRNVendorModel.Cancelled;
            grn.StoreId = GRNVendorModel.StoreId;
            grn.AuthorizedDate = GRNVendorModel.AuthorizedDate;
            grn.GrnTypeID = GRNVendorModel.GrnTypeID;
            grn.GRNDetails = _grnDetails;
            grn.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            grn.InsertedON = System.DateTime.Now;
            grn.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            grn.InsertedMacID = BISERP.Common.Constants.MacId;
            grn.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/grnvendor/authcancelgrnvendor" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, grn, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<GRNVendorModel>().Id;
            return Json(new { success = true });
        }
    }
}
