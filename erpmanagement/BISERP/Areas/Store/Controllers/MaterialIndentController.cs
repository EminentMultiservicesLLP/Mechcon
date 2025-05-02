using BISERP.Areas.Store.Models.Store;
using BISERP.Areas.Masters.Models;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERP.Common;
using BISERPCommon.Extensions;
using BISERP.Filters;

namespace BISERP.Areas.Store.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class MaterialIndentController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(MaterialIndentController));
        public MaterialIndentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllMaterialIndent(int StoreId, DateTime fromdate, DateTime todate, string ReportType)
        {
            JsonResult jResult;
            List<MaterialIndentModel> mimodel = new List<MaterialIndentModel>();
            try
            {
                string _url = url + "/materilaindents/getallmaterilaindents/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + "/" + StoreId.ToString() + "/" + ReportType + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<MaterialIndentModel>>(client, _url, CancellationToken.None);
                if (mimodel != null)
                {
                    mimodel = mimodel.OrderByDescending(m => m.Indent_Id).ToList();
                    jResult = Json(mimodel, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(mimodel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllMaterialIndent :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> IndentForVerification(string searchIssueNumber)
        {
            JsonResult jResult;
            List<MaterialIndentModel> mimodel = new List<MaterialIndentModel>();
            try
            {
                string _url = url + "/materilaindents/getallmaterilaindents/0/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<MaterialIndentModel>>(client, _url, CancellationToken.None);
                if (mimodel != null)
                {
                    if (!string.IsNullOrWhiteSpace(searchIssueNumber))
                    {
                        mimodel = mimodel.Where(p => p.IndentNo.ToUpper().Contains(searchIssueNumber.ToUpper())).ToList();
                    }
                    mimodel = mimodel.OrderByDescending(m => m.Indent_Id).ToList();
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in IndentForVerification :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> IndentForAuthorisation(string searchIssueNumber)
        {
            JsonResult jResult;
            List<MaterialIndentModel> mimodel = new List<MaterialIndentModel>();
            try
            {
                string _url = url + "/materilaindents/getallmaterilaindents/1/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<MaterialIndentModel>>(client, _url, CancellationToken.None);
                if (mimodel != null)
                {
                    if (!string.IsNullOrWhiteSpace(searchIssueNumber))
                    {
                        mimodel = mimodel.Where(p => p.IndentNo.ToUpper().Contains(searchIssueNumber.ToUpper())).ToList();
                    }
                    mimodel = mimodel.OrderBy(m => m.Indent_Id).ToList();
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in IndentForAuthorisation :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public async Task<ActionResult> SaveMaterialIndent(MaterialIndentModel items)
        {
            string _url = "";
            bool _isSuccess = true;
            MaterialIndentModel mindent = new MaterialIndentModel();
            List<MaterialIndentDtModel> _indentDetails = new List<MaterialIndentDtModel>();
            MaterialIndentDtModel mdt;
            foreach (var IndentDt in items.Materialindentdt)
            {
                if (IndentDt.state == true)
                {
                    mdt = new MaterialIndentDtModel();
                    mdt = IndentDt;                    
                    _indentDetails.Add(mdt);
                }                
            }
            mindent.Indent_Id = items.Indent_Id;            
            mindent.Priority = items.Priority;
            mindent.Indent_User_Area = items.Indent_User_Area;
            mindent.Indent_User_SubArea = items.Indent_User_SubArea;
            mindent.Indent_FromStore = items.Indent_FromStore;
            mindent.Indent_ToStore = items.Indent_ToStore;
            mindent.Indent_FromStoreID = items.Indent_FromStoreID;
            mindent.Indent_ToStoreID = items.Indent_ToStoreID;
            mindent.ItemCategoryId = items.ItemCategoryId;
            mindent.Indent_Type = items.Indent_Type;
            mindent.Indent_Date = items.Indent_Date;
            mindent.Remarks = items.Remarks;
            mindent.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            mindent.IndentNo = "I-";
            mindent.InsertedON = System.DateTime.Now;
            mindent.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            mindent.InsertedMacID = BISERP.Common.Constants.MacId;
            mindent.InsertedMacName = BISERP.Common.Constants.MacName;

            mindent.Materialindentdt = _indentDetails;
            
            if (mindent.Indent_Id > 0)
            {
                _url = url + "/materilaindents/updatematerialindentitem" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, mindent, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                }
                else if (result.StatusCode.ToString() == "OK")
                {
                    _isSuccess = true;
                }

                if (!_isSuccess)
                    return Json(new { success = false, Message = mindent.Message });
                else
                    return Json(new { success = true, Message = mindent.Message, Data = mindent  });
            }
            else
            {
                _url = url + "/materilaindents/creatematerilaIndent" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, mindent, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIndentModel>().Id;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    mindent = JsonConvert.DeserializeObject<MaterialIndentModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    mindent = JsonConvert.DeserializeObject<MaterialIndentModel>(result.Content.ReadAsStringAsync().Result);
                }

                if (_isSuccess)
                {
                    var mailbody = await Common.AsyncWebCalls.GetAsync<BranchModel>("/stores/BranchbyStore/" + (items.Indent_ToStoreID ?? 0), CancellationToken.None);
                    UtilitiesCls.SendEmailTask(EmailProcessFor.MaterialIndent, this, mindent, false, new string[] { mailbody.Email });
                
                }

                if (!_isSuccess)
                    return Json(new { success = false, Message = mindent.Message });
                else
                    return Json(new { success = true, Message = mindent.IndentNo, Data = mindent });
            }            
        }

        [HttpGet]
        public async Task<JsonResult> MaterialIndentItems(int Indent_Id)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/materilaindents/getmaterilaIndentbyid/" + Indent_Id.ToString() + Common.Constants.JsonTypeResult;
                var indent = await Common.AsyncWebCalls.GetAsync<MaterialIndentModel>(client, _url, CancellationToken.None);
                List<MaterialIndentDtModel> _Materialindentdt = indent.Materialindentdt;
                
                if (_Materialindentdt != null && _Materialindentdt.Count > 0)
                {
                    _Materialindentdt.ForEach(m => m.state = true);
                    jResult = Json(_Materialindentdt, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in MaterialIndentItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
                return jResult;
        }

        [HttpPost]
        public async Task<JsonResult> AuthCancelMaterialIndent(MaterialIndentModel indent)
        {
            try
            {
                string _url = "";
                MaterialIndentModel mindent = new MaterialIndentModel();
                List<MaterialIndentDtModel> _indentDetails = new List<MaterialIndentDtModel>();
                MaterialIndentDtModel mdt;
                foreach (var IndentDt in indent.Materialindentdt)
                {
                    mdt = new MaterialIndentDtModel();
                    mdt = IndentDt;
                    if (IndentDt.state == true)
                    {     
                    }
                    else
                    {
                        mdt.Authorised_Quantity = 0;
                      
                    }
                    if (indent.StatusId == 2)
                    {
                        if (mdt.Authorised_Quantity > 0)
                        {
                            //mdt.Status = "Authorized";
                            mdt.StatusId = 2;
                        }
                        else
                        {
                            //mdt.Status = "Cancelled";
                            mdt.StatusId = 3;
                        }
                    }
                    else
                    {
                        mdt.Status = "Cancelled";
                        mdt.StatusId = 3;
                    }
                    _indentDetails.Add(mdt);
                }
              
                mindent.Indent_Id = indent.Indent_Id;
                mindent.Indent_FromStore = indent.Indent_FromStore;
                mindent.Indent_ToStore = indent.Indent_ToStore;
                mindent.Indent_Date = indent.Indent_Date;
                mindent.StatusId = indent.StatusId;
                //mindent.Authorized = indent.Authorized;
                //mindent.Cancelled = indent.Cancelled;
                mindent.AuthorisedRemarks = indent.AuthorisedRemarks;

                mindent.Materialindentdt = _indentDetails;
                mindent.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                mindent.InsertedON = System.DateTime.Now;
                mindent.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                mindent.InsertedMacID = BISERP.Common.Constants.MacId;
                mindent.InsertedMacName = BISERP.Common.Constants.MacName;
             
                _url = url + "/materilaindents/authcancelmaterialindent/" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, mindent, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<MaterialIndentModel>().Id;
             
                if (result != null)
                {
                    var model = await Common.AsyncWebCalls.GetAsync<BranchModel>("/stores/BranchbyStore/" + (indent.Indent_FromStoreID ?? 0), CancellationToken.None);
                    UtilitiesCls.SendEmailTask(EmailProcessFor.MaterialAuthorizationCancellation, this, mindent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AuthCancelMaterialIndent :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                return Json(ex.Message);
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult VerifyCancelMaterialIndent(MaterialIndentModel indent)
        {
            string _url = "";
            bool _isSuccess = true;
            string message ="";
            JsonResult jResult;

            try
            {
                MaterialIndentModel mindent = new MaterialIndentModel();
                List<MaterialIndentDtModel> _indentDetails = new List<MaterialIndentDtModel>();
                MaterialIndentDtModel mdt;
                foreach (var IndentDt in indent.Materialindentdt)
                {
                    mdt = new MaterialIndentDtModel();
                    mdt = IndentDt;
                    if (IndentDt.state == true)
                    {
                        mdt.User_Quantity = IndentDt.Authorised_Quantity;
                    }
                    else
                    {
                        mdt.User_Quantity = 0;
                    }                    
                    _indentDetails.Add(mdt);
                }
                mindent.Indent_Id = indent.Indent_Id;
                //mindent.Authorized = indent.Authorized;
                //mindent.Cancelled = indent.Cancelled;
                mindent.StatusId = indent.StatusId;
                mindent.AuthorisedRemarks = indent.AuthorisedRemarks;

                mindent.Materialindentdt = _indentDetails;
                mindent.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                mindent.InsertedON = System.DateTime.Now;
                mindent.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                mindent.InsertedMacID = BISERP.Common.Constants.MacId;
                mindent.InsertedMacName = BISERP.Common.Constants.MacName;

                _url = url + "/materilaindents/verifycancelindent/" + Common.Constants.JsonTypeResult;
                _logger.LogInfo("URL:" + _url);
                var result = client.PostAsync(_url, mindent, new JsonMediaTypeFormatter()).Result;
                _logger.LogInfo("result:" + result.ToString());

                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                }
                else if (result.StatusCode.ToString() == "Ok")
                {
                    _isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                _isSuccess = false;
                message = "Error in VerifyCancelMaterialIndent :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString();
                _logger.LogError("Error in VerifyCancelMaterialIndent :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                
            }
            if (!_isSuccess)
                jResult = Json(new { success = false, Message = message.ToString() });
            else
                jResult = Json(new { success = true, Message = message.ToString() });

            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllAuthorizedMaterialIndent(string searchString)
        {
            JsonResult jResult;

            List<MaterialIndentModel> records = new List<MaterialIndentModel>();
            try
            {
                string _url = url + "/materilaindents/getauthmindents/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<MaterialIndentModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchString))
                    {
                        records = records.Where(p => p.IndentNo.ToUpper().StartsWith(searchString.ToUpper())).ToList();
                    }
                    //jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    //jResult = Json("Error");
                    jResult = Json(new { success = false, Messsage = "No Request Number found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllAuthorizedMaterialIndent :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllAuthorizedMaterialIndentForIssue(string searchString)
        {
            JsonResult jResult;

            List<MaterialIndentModel> records = new List<MaterialIndentModel>();
            try
            {
                string _url = url + "/materilaindents/getauthmindentsforIssue/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<MaterialIndentModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchString))
                    {
                        records = records.Where(p => p.IndentNo.ToUpper().StartsWith(searchString.ToUpper())).ToList();
                    }
                    //jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    //jResult = Json("Error");
                    jResult = Json(new { success = false, Messsage = "No Request Number found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllAuthorizedMaterialIndentForIssue :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> AuthorizedUnitMaterialIndent(string searchString)
        {
            JsonResult jResult;
            List<MaterialIndentModel> mimodel;
            try
            {
                string _url = url + "/materilaindents/getauthmaterialindents/0" + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<MaterialIndentModel>>(client, _url, CancellationToken.None);
                if (mimodel != null && mimodel.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchString))
                    {
                        mimodel = mimodel.Where(p => p.IndentNo.ToUpper().StartsWith(searchString.ToUpper())).ToList();
                    }
                    //jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);

                    jResult = Json(new { success = true, mimodel }, JsonRequestBehavior.AllowGet);

                }
                else
                    //jResult = Json("Error");
                    jResult = Json(new { success = false, Messsage = "No Material Indent found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AuthorizedUnitMaterialIndent :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AuthMaterialIndentItems(int Indent_Id)
        {
            JsonResult jResult;
            List<MaterialIndentDtModel> indentDetails = new List<MaterialIndentDtModel>();
            try
            {
                string _url = url + "/materilaindents/getauthmibyid/" + Indent_Id.ToString() + Common.Constants.JsonTypeResult;
                indentDetails = await Common.AsyncWebCalls.GetAsync<List<MaterialIndentDtModel>>(client, _url, CancellationToken.None);

                if (indentDetails != null && indentDetails.Count > 0)
                {
                    jResult = Json(indentDetails, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AuthMaterialIndentItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> VeifiedMaterialIndentItems(int Indent_Id)
        {
            JsonResult jResult;
            List<MaterialIndentDtModel> indentDetails = new List<MaterialIndentDtModel>();
            try
            {
                string _url = url + "/materilaindents/getverifiedmibyid/" + Indent_Id.ToString() + Common.Constants.JsonTypeResult;
                indentDetails = await Common.AsyncWebCalls.GetAsync<List<MaterialIndentDtModel>>(client, _url, CancellationToken.None);

                if (indentDetails != null && indentDetails.Count > 0)
                {
                    indentDetails.ForEach(m => m.state = true);
                    jResult = Json(indentDetails, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in VeifiedMaterialIndentItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AuthMIndentItemDetails(int IndentDetailId)
        {
            JsonResult jResult;
            List<MaterialIndentDtModel> itemDetails = new List<MaterialIndentDtModel>();
            try
            {
                string _url = url + "/materilaindents/getauthmiitemdetail/" + IndentDetailId.ToString() + Common.Constants.JsonTypeResult;
                var indentDetails = await Common.AsyncWebCalls.GetAsync<List<MaterialIndentDtModel>>(client, _url, CancellationToken.None);
                if(indentDetails.Count() == 1)
                indentDetails.ForEach(i => i.state = true);
                float? dblPendIssueQty = 0;
                int intCtr = 0;
                foreach (var indDt in indentDetails)
                {
                    if (dblPendIssueQty == 0)
                    {
                        if (intCtr == 0)
                        {
                            if (indDt.Item_Stock >= indDt.PendingQty)
                            {
                                indDt.IssuedQuantity = indDt.PendingQty;
                                indDt.state = true;
                            }
                            else
                            {
                                indDt.IssuedQuantity = indDt.Item_Stock;
                                indDt.state = true;
                                dblPendIssueQty = indDt.PendingQty - indDt.Item_Stock;
                            }
                        }
                        intCtr++;
                    }
                    else if(dblPendIssueQty > 0)
                    {
                        if (dblPendIssueQty >= indDt.Item_Stock)
                        {
                            indDt.IssuedQuantity = indDt.Item_Stock;
                            indDt.state = true;
                        }
                        else
                        {
                            indDt.IssuedQuantity = dblPendIssueQty;
                            indDt.state = true;
                            dblPendIssueQty = dblPendIssueQty - indDt.Item_Stock;
                        }                        
                    }
                }
                if (indentDetails != null && indentDetails.Count > 0)
                {
                    jResult = Json(indentDetails, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(itemDetails, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AuthMIndentItemDetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> pedMIndentItemDetails(int IndentId)
        {
            JsonResult jResult;
            List<MaterialIndentDtModel> itemDetails = new List<MaterialIndentDtModel>();
            try
            {
                string _url = url + "/materilaindents/getpendmiitemdetail/" + IndentId.ToString() + Common.Constants.JsonTypeResult;
                var indentDetails = await Common.AsyncWebCalls.GetAsync<List<MaterialIndentDtModel>>(client, _url, CancellationToken.None);

                if (indentDetails != null && indentDetails.Count > 0)
                {
                    jResult = Json(indentDetails, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
             
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in pedMIndentItemDetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SavependingmaterialIndent(List<MaterialIndentDtModel> Materialindentdt)
        {

            List<MaterialIndentDtModel> _misdetails = new List<MaterialIndentDtModel>();
            MaterialIndentDtModel misdt;
            string strErrorMsg = "";
            int _rowSelected = 0;
            var IndentDetails = Materialindentdt.OrderBy(i => i.IndentDetails_Id);
            foreach (var IndentDt in IndentDetails)
            {
                if (IndentDt != null)
                {
                    if (IndentDt.state == true)
                    {
                        _rowSelected++;
                        if (IndentDt.MICancelReason == null)
                        {
                            strErrorMsg = "Plz enter Reson";
                            break;
                        }
                        else if (IndentDt.state == true)
                        {
                            var _indentDetailId = IndentDt.IndentDetails_Id;
                            // var _issuedQuantity = IndentDetails.Where(i => i.IndentDetails_Id.Equals(_indentDetailId)).Sum(i => i.IssuedQuantity).GetValueOrDefault();
                            misdt = new MaterialIndentDtModel();
                            misdt.IndentDetails_Id = IndentDt.IndentDetails_Id;
                            misdt.Indent_Id = IndentDt.Indent_Id;
                            misdt.Item_Id = IndentDt.Item_Id;
                            misdt.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                            misdt.InsertedON = System.DateTime.Now;
                            misdt.MICancelReason = IndentDt.MICancelReason;
                            _misdetails.Add(misdt);

                        }
                    }
                }
            }
            if (_rowSelected == 0)
            {
                strErrorMsg = "No Items Selected  ";
            }
            if (strErrorMsg == "")
            {

                string _url = url + "/materilaindents/updatepenmindentitem" + Common.Constants.JsonTypeResult;
                //PrintpdfReport(mis);
                var result = client.PostAsync(_url, _misdetails, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    strErrorMsg = "Error In Material Issue";
                    _misdetails = JsonConvert.DeserializeObject<List<MaterialIndentDtModel>>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    strErrorMsg = "";
                    _misdetails = JsonConvert.DeserializeObject<List<MaterialIndentDtModel>>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (strErrorMsg != "")
                return Json(new { success = false, responseText = strErrorMsg }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = true, responseText = strErrorMsg });
        }
        
        [HttpPost]
        public JsonResult DeleteIndentItem(MaterialIndentDtModel indent)
        {
            if (indent.IndentDetails_Id > 0)
            {
                string _url = url + "/materilaindents/deleteindentitem" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, indent, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIndentDtModel>().Id;
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetIndentToStore(int StoreId)
        {
            JsonResult jResult;
            string _url = url + "stores/getindenttostores/" + StoreId.ToString() + "/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
            List<StoreMasterModel> _itemtype = await Common.AsyncWebCalls.GetAsync<List<StoreMasterModel>>(client, _url, CancellationToken.None);

            return jResult = Json(_itemtype, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> PendingmaterialIndent(int StoreId)
        {
            JsonResult jResult;
            List<MaterialIndentModel> mimodel;
            try
            {
                string _url = url + "/materilaindents/getallpmi/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<MaterialIndentModel>>(client, _url, CancellationToken.None);
                if (mimodel != null && mimodel.Count > 0)
                {
                    //jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    //jResult = Json("Error");
                jResult = Json(new { success = false, Messsage = "Request Not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PendingmaterialIndent :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetItemListForMI(int? StoreId, int? ItemTypeId)
        {
            JsonResult jResult;
            try
            {
                string _url = $"{url}/materilaindents/getItemListForMI" + (StoreId.HasValue ? $"/{StoreId}" : "") + (ItemTypeId.HasValue ? $"/{ItemTypeId}" : "");

                _url += Common.Constants.JsonTypeResult;
                List<ItemMasterModel> items = await Common.AsyncWebCalls.GetAsync<List<ItemMasterModel>>(client, _url, CancellationToken.None);
                if (items != null && items.Count > 0)
                {
                    items.ForEach(m => m.storeId = StoreId);
                    jResult = Json(new { success = true, items }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetItemsByStoreItemType :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            jResult.MaxJsonLength = int.MaxValue;
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetMRSummuryRpt(DateTime fromdate, DateTime todate)
        {
            JsonResult jResult;
            List<MaterialIndentModel> mimodel = new List<MaterialIndentModel>();
            try
            {
                string _url = url + "/materilaindents/GetMRSummuryRpt/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<MaterialIndentModel>>(client, _url, CancellationToken.None);
                if (mimodel != null)
                {
                    jResult = Json(mimodel, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(mimodel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetMRSummuryRpt :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

    }
}
