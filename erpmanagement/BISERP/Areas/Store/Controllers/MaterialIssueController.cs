using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Formatting;
using BISERP.Areas.Store.Models.Store;
using Newtonsoft.Json;
using System.IO;
using BISERP.Areas.Masters.Models;
using BISERP.Common;
using BISERPCommon.Extensions;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace BISERP.Areas.Store.Controllers
{
    public class MaterialIssueController : Controller
    {
        //
        // GET: /MaterialIssue/
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(MaterialIssueController));

        public MaterialIssueController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllMaterialIssueforAuthorization()
        {
            JsonResult jResult;
            try
            {//Server.MapPath()
                string _url = url + "/materialissue/getAll/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                List<MaterialIssueModel> records = await Common.AsyncWebCalls.GetAsync<List<MaterialIssueModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {                    
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllMaterialIssueforAuthorization :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetMaterialIssue(DateTime fromdate,DateTime todate,int StoreId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/materialissue/getmissue/" + StoreId + "/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + Common.Constants.JsonTypeResult;
                List<MaterialIssueModel> records = await Common.AsyncWebCalls.GetAsync<List<MaterialIssueModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetMaterialIssue :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public async  Task<ActionResult>SaveMaterialIssue(MaterialIndentModel items)
        {
            bool _isSuccess = false;
            MaterialIssueModel mis = new MaterialIssueModel();
            List<MaterialIssueDetailModel> _misdetails = new List<MaterialIssueDetailModel>();
            MaterialIssueDetailModel misdt;
            string strErrorMsg = "";
            int _rowSelected = 0;
            if (items.Materialindentdt.IsNotNull() && items.Materialindentdt.Any())
            {
                var IndentDetails = items.Materialindentdt.OrderBy(i => i.IndentDetails_Id);
                foreach (var IndentDt in IndentDetails)
                {
                    if (IndentDt != null)
                    {
                        if (IndentDt.state == true)
                        {
                            _rowSelected++;
                            var _indentDetailId = IndentDt.IndentDetails_Id;
                            var _issuedQuantity =
                                IndentDetails.Where(i => i.IndentDetails_Id.Equals(_indentDetailId))
                                    .Sum(i => i.IssuedQuantity)
                                    .GetValueOrDefault();
                            if (_issuedQuantity == 0)
                            {
                                strErrorMsg = "Please Enter Issue Quantity for Selected Item.";
                                break;
                            }
                            if (IndentDt.IssuedQuantity > IndentDt.Item_Stock)
                            {
                                strErrorMsg = "Issued Quantity Cannot be More Than Available Quantity";
                                break;
                            }
                            else if (IndentDt.IssuedQuantity > IndentDt.PendingQty)
                            {
                                strErrorMsg = "Issued Quantity Cannot be More Than Balance Quantity For Issue";
                                break;
                            }
                            else if (IndentDt.state == true)
                            {
                                misdt = new MaterialIssueDetailModel();
                                misdt.IndentDetailId = IndentDt.IndentDetails_Id;
                                misdt.ItemId = IndentDt.Item_Id;
                                misdt.ItemName = IndentDt.ItemName;
                                misdt.IssuedQuantity = Math.Round(IndentDt.IssuedQuantity??0,2);
                                misdt.Batchid = IndentDt.BatchId;
                                misdt.AuthorisedQuantity = IndentDt.Authorised_Quantity;
                                misdt.BalIndentQtyForIssue = IndentDt.PendingQty;
                                misdt.MRP = IndentDt.MRP;
                                _misdetails.Add(misdt);
                            }
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
                mis.Indent_Id = items.Indent_Id;
                mis.FromStoreId = items.Indent_FromStoreID;
                mis.IndentNo = items.IndentNo;
                mis.FromStore = items.Indent_FromStore;
                mis.ToStore = items.Indent_ToStore;
                mis.StoreId = items.Indent_ToStoreID;
                mis.IssueDate = items.Indent_Date;
                mis.MaterialIssueDt = _misdetails;
                mis.BranchID = 1;
                mis.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                mis.InsertedON = System.DateTime.Now;
                mis.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                mis.InsertedMacID = BISERP.Common.Constants.MacId;
                mis.InsertedMacName = BISERP.Common.Constants.MacName;

                string _url = url + "/materialissue/createIssue" + Common.Constants.JsonTypeResult;
                //PrintpdfReport(mis);
                var result = client.PostAsync(_url, mis, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    strErrorMsg = "Error In Material Issue";
                    mis = JsonConvert.DeserializeObject<MaterialIssueModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    strErrorMsg = "";
                    mis = JsonConvert.DeserializeObject<MaterialIssueModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (_isSuccess)
            {
                var result = await Common.AsyncWebCalls.GetAsync<BranchModel>("/stores/BranchbyStore/" + (items.Indent_ToStoreID ?? 0), CancellationToken.None);
                UtilitiesCls.SendEmailTask(EmailProcessFor.MaterialIssue, this, mis, false, new string[] { result.Email });
               
            }
            if (strErrorMsg != "")
                return Json(new { success = false, responseText = strErrorMsg, Data=mis }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = true, responseText = mis.IssueNo });
        }

        [HttpPost]
        public ActionResult DeleteIssueItem(MaterialIssueDetailModel items)
        {
            string _url = "";
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedON = System.DateTime.Now;
            items.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            items.InsertedMacID = BISERP.Common.Constants.MacId;
            items.InsertedMacName = BISERP.Common.Constants.MacName;
            if (items.IssueDetailsId > 0)
            {
                _url = url + "/materialissue/deleteissueitem" + Common.Constants.JsonTypeResult;
                //client.PutAsync(_url, items, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<int>().Result;
            }
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<JsonResult> MaterialIssueItems(int IssueId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/materialissue/getmidetailbyid/" + IssueId.ToString() + Common.Constants.JsonTypeResult;
                var issueItems = await Common.AsyncWebCalls.GetAsync<List<MaterialIssueDetailModel>>(client, _url, CancellationToken.None);

                if (issueItems != null)
                {
                    jResult = Json(issueItems, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PurchaseIndentItem :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public async Task<ActionResult>AuthCancelMaterialIssue(MaterialIssueModel issue)
        {
           
            string _url = "";
            MaterialIssueModel _mis = new MaterialIssueModel();
            List<MaterialIssueDetailModel> _misDetails = new List<MaterialIssueDetailModel>();
            string strErrorMsg = "";
            int _rowSelected = 0;
            MaterialIssueDetailModel mdt;
           
                foreach (var IssueDt in issue.MaterialIssueDt)
                {
                    if (IssueDt != null)
                    {
                        mdt = new MaterialIssueDetailModel();
                        mdt = IssueDt;
                        if (issue.Authorised == true)
                        {
                            if (IssueDt.state == true)
                            {
                                mdt.Authorised = true;
                                mdt.Strauthorized = "Authorised";
                                _rowSelected++;

                                if (IssueDt.AuthorisedQuantity == 0)
                                {
                                    strErrorMsg = "Please Enter Accepted Quantity for Selected Item.";
                                    break;
                                }
                                else if (IssueDt.AuthorisedQuantity > IssueDt.IssuedQuantity)
                                {
                                    strErrorMsg = "Accepted Quantity Cannot be More Than Issued Quantity";
                                    break;
                                }
                            }
                            else
                            {
                                mdt.Authorised = false;
                                mdt.Strauthorized = "Cancelled";
                            }
                        }
                        else
                        {
                            if (IssueDt.state == true)
                            {
                                _rowSelected++;
                            }
                            mdt.Authorised = false;
                             mdt.Strauthorized = "Cancelled";
                        }
                        _misDetails.Add(mdt);
                    }
                }
          
            if (_rowSelected == 0)
            {
                strErrorMsg = "No Item Selected For Acceptance";
            }
            if(_misDetails.Count() == 0 && strErrorMsg == "")
            {
                strErrorMsg = "No Item Selected For Acceptance";
            }
            _mis.MaterialIssueDt = _misDetails;
            _mis.IssueId = issue.IssueId;
            _mis.strIssueDate = issue.strIssueDate;
            _mis.IssueNo = issue.IssueNo;
            _mis.FromStore = issue.FromStore;
            _mis.ToStore = issue.ToStore;
            _mis.FromStoreId = issue.FromStoreId;
            _mis.StoreId = issue.StoreId;
            _mis.Authorised = issue.Authorised;
            _mis.Cancelled = issue.Cancelled;
            _mis.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            _mis.InsertedON = System.DateTime.Now;
            _mis.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            _mis.InsertedMacID = BISERP.Common.Constants.MacId;
            _mis.InsertedMacName = BISERP.Common.Constants.MacName;

            if (strErrorMsg == "")
            {
                _url = url + "/materialissue/authcancel" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, _mis, new JsonMediaTypeFormatter()).Result.Content.ReadAsAsync<MaterialIssueModel>().Id;
                if (result != null)
                {
                    var emailBody = await Common.AsyncWebCalls.GetAsync<BranchModel>("/stores/BranchbyStore/" + (issue.StoreId ?? 0), CancellationToken.None);
                    UtilitiesCls.SendEmailTask(EmailProcessFor.MaterialIssueAcceptance, this, _mis, false, new string[] { emailBody.Email });
                    
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, responseText = strErrorMsg }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> UnAcceptedAuthorizedMaterialIssue()
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/materialissue/getunacceptedauthorized" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<MaterialIssueModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {                    
                    jResult = Json( records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in UnAcceptedAuthorizedMaterialIssue :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public JsonResult AcceptMaterialIssue(MaterialIssueModel issue)
        {
            string _url = "";
            issue.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            issue.InsertedON = System.DateTime.Now;
            issue.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            issue.InsertedMacID = BISERP.Common.Constants.MacId;
            issue.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/materialissue/issueacceptance" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, issue, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
            return Json(new { success = true });
        }
    }
}
