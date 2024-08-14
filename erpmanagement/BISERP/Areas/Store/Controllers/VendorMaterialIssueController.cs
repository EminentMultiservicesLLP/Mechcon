using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using Newtonsoft.Json;
using BISERP.Areas.Store.Models.Store;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;

namespace BISERP.Areas.Store.Controllers
{
    public class VendorMaterialIssueController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(VendorMaterialIssueController));

        public VendorMaterialIssueController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpPost]
        public ActionResult SaveMaterialIssue(VendorMaterialIssueModel model)
        {
            string strErrorMsg = "";
            int _rowSelected = 0;
            VendorMaterialIssueModel Vendormimodel = new VendorMaterialIssueModel();
            List<VendorMaterialIssueDtModel> lstdtmodel = new List<VendorMaterialIssueDtModel>();
            VendorMaterialIssueDtModel dtmodel = new VendorMaterialIssueDtModel();
            foreach (var items in model.VendorMaterialIssueDt)
            {
                if (items != null)
                {
                    if (items.state == true)
                    {
                        _rowSelected++;
                        if (items.IssuedQuantity == 0 || items.IssuedQuantity == null)
                        {
                            strErrorMsg = "Please Enter Issue Quantity for Selected Item.";
                            break;
                        }
                        else if (items.Item_Stock < items.IssuedQuantity)
                        {
                            strErrorMsg = "Issued Quantity Cannot be More Than Available Quantity";
                            break;
                        }    
                        else
                        {
                            dtmodel = new VendorMaterialIssueDtModel();
                            dtmodel.IssueDetailsId = items.IssueDetailsId;
                            dtmodel.ItemId = items.ItemId;
                            dtmodel.IssuedQuantity = items.IssuedQuantity;
                            dtmodel.BatchId = items.BatchId;
                            dtmodel.MRP = items.MRP;
                            dtmodel.TotalAmt = items.TotalAmt;
                            lstdtmodel.Add(dtmodel);
                        }
                    }
                }
            }
            if (_rowSelected == 0)
            {
                strErrorMsg = "No Items Selected For Issue";
            }
            if (strErrorMsg == "")
            {
                Vendormimodel.IssueId = model.IssueId;
                Vendormimodel.StoreId = model.StoreId;
                Vendormimodel.ManufactureId = model.ManufactureId;
                Vendormimodel.IssueDate = model.IssueDate;
                Vendormimodel.IssueNo = model.IssueNo;
                Vendormimodel.Nature = model.Nature;
                Vendormimodel.VendorMaterialIssueDt = lstdtmodel;
                
                Vendormimodel.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                Vendormimodel.InsertedON = System.DateTime.Now;
                Vendormimodel.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                Vendormimodel.InsertedMacID = BISERP.Common.Constants.MacId;
                Vendormimodel.InsertedMacName = BISERP.Common.Constants.MacName;

                string _url = url + "/vendormi/createissue" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, Vendormimodel, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    strErrorMsg = "Error In Material Issue";
                    Vendormimodel = JsonConvert.DeserializeObject<VendorMaterialIssueModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    strErrorMsg = "";
                    Vendormimodel = JsonConvert.DeserializeObject<VendorMaterialIssueModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (strErrorMsg != "")
                return Json(new { success = false, responseText = strErrorMsg }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = true, responseText = Vendormimodel.IssueNo });
        }

        [HttpGet]
        public async Task<JsonResult> IssueForAuthorization()
        {
            JsonResult jResult;
            List<VendorMaterialIssueModel> records = new List<VendorMaterialIssueModel>();
            try
            {
                string _url = url + "/vendormi/issueforauth/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<VendorMaterialIssueModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {                    
                    jResult = Json( records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json( records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in IssueForAuthorization :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> VendorMaterialssueItems(int IssueId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/vendormi/getById/" + IssueId.ToString() + Common.Constants.JsonTypeResult;
                var vendorIssue = await Common.AsyncWebCalls.GetAsync<VendorMaterialIssueModel>(client, _url, CancellationToken.None);

                if (vendorIssue.VendorMaterialIssueDt != null)
                {
                    vendorIssue.VendorMaterialIssueDt.ForEach(m => m.state = true);
                    jResult = Json(vendorIssue.VendorMaterialIssueDt, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in VendorMaterialssueItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public JsonResult AuthCancelMaterialIndent(VendorMaterialIssueModel model)
        {
            string _url = "";
            string strErrorMsg = "";
            int _rowSelected = 0;
            VendorMaterialIssueModel mimodel = new VendorMaterialIssueModel();
            List<VendorMaterialIssueDtModel> _lstdtmodel = new List<VendorMaterialIssueDtModel>();
            VendorMaterialIssueDtModel dtmodel;
            foreach (var issueDt in model.VendorMaterialIssueDt)
            {
                dtmodel = new VendorMaterialIssueDtModel();
                dtmodel = issueDt;
                if (dtmodel.state == true)
                {
                    _rowSelected++;
                    if (model.Authorised == true)
                    {
                        if (dtmodel.AuthorisedQuantity == 0 || dtmodel.AuthorisedQuantity == null)
                        {
                            strErrorMsg = "Please Enter Authorized Quantity for Selected Item.";
                            break;
                        }
                    }
                    else if (dtmodel.IssuedQuantity < dtmodel.AuthorisedQuantity)
                    {
                        strErrorMsg = "Authorized Quantity Cannot be More Than Issued Quantity";
                        break;
                    }  
                }
                else
                {
                    dtmodel.AuthorisedQuantity = 0;
                }
                _lstdtmodel.Add(dtmodel);
            }
            if (_rowSelected == 0)
            {
                strErrorMsg = "Please Select Items for Authorization/Cancellation";
            }
            if (strErrorMsg == "")
            {
                mimodel.IssueId = model.IssueId;
                mimodel.StoreId = model.StoreId;
                mimodel.ManufactureId = model.ManufactureId;
                mimodel.Authorised = model.Authorised;
                mimodel.Cancelled = model.Cancelled;

                mimodel.VendorMaterialIssueDt = _lstdtmodel;
                mimodel.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                mimodel.InsertedON = System.DateTime.Now;
                mimodel.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                mimodel.InsertedMacID = BISERP.Common.Constants.MacId;
                mimodel.InsertedMacName = BISERP.Common.Constants.MacName;

                _url = url + "/vendormi/authcancelmaterialissue/" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, mimodel, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<VendorMaterialIssueModel>().Id;
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, responseText = strErrorMsg });
            }
        }

        [HttpGet]
        public async Task<JsonResult> AllVendorMaterialIssueForGRN(int storeId ,int UserId)
        {
            JsonResult jResult;
            List<VendorMaterialIssueModel> records = new List<VendorMaterialIssueModel>();
            try
            {
                string _url = url + "/vendormi/issue/" + UserId.ToString() + "/" + storeId + "/" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<VendorMaterialIssueModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllVendorMaterialIssueForGRN :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllVendorMaterialIssueForGRNDt(int IssueId)
        {
            JsonResult jResult;
            List<VendorMaterialIssueDtModel> records = new List<VendorMaterialIssueDtModel>();
            try
            {
                string _url = url + "/vendormi/getVenById/" + IssueId.ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<VendorMaterialIssueDtModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllVendorMaterialIssueForGRNDt :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }


    }
}
