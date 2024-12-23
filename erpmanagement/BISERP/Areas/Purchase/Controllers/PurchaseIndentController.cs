using BISERP.Areas.Store.Models.Store;
using BISERP.Area.Purchase.Models;
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
using BISERP.Filters;

namespace BISERP.Area.Purchase.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class PurchaseIndentController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(PurchaseIndentController));

        public PurchaseIndentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllPurchaseIndents(string searchIssueNumber)
        {
            JsonResult jResult;
            List<PurchaseIndentModel> mimodel = new List<PurchaseIndentModel>();
            try
            {
                string _url = url + "/pindent/getpurchaseindent" + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<PurchaseIndentModel>>(client, _url, CancellationToken.None);
                if (mimodel != null)
                {
                    if (!string.IsNullOrWhiteSpace(searchIssueNumber))
                    {
                        mimodel = mimodel.Where(p => p.IndentNumber.ToUpper().Contains(searchIssueNumber.ToUpper())).ToList();
                    }
                    mimodel = mimodel.OrderByDescending(m => m.IndentId).ToList();
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllPurchaseIndents :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> PurchaseIndentItems(int IndentId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/pindent/getpurchaseindentbyid/" + IndentId.ToString() + Common.Constants.JsonTypeResult;
                var indent = await Common.AsyncWebCalls.GetAsync<PurchaseIndentModel>(client, _url, CancellationToken.None);
                List<PurchaseIndentDetailModel> _indentDetails = indent.IndentDetails;

                if (_indentDetails != null)
                {
                    _indentDetails.ForEach(m => m.state = true);
                    jResult = Json(_indentDetails, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PurchaseIndentItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AuthPurchaseIndentItems(int IndentId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/pindent/getauthpurchaseindentbyid/" + IndentId.ToString() + Common.Constants.JsonTypeResult;
                var _indentDetails = await Common.AsyncWebCalls.GetAsync<List<PurchaseIndentDetailModel>>(client, _url, CancellationToken.None);

                if (_indentDetails != null)
                {
                    jResult = Json(_indentDetails, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AuthPurchaseIndentItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SavePurchaseIndent(PurchaseIndentModel items)
        {
            string _url = "";
            bool _isSuccess = true;
            PurchaseIndentModel mindent = new PurchaseIndentModel();
            List<PurchaseIndentDetailModel> _indentDetails = new List<PurchaseIndentDetailModel>();
            PurchaseIndentDetailModel mdt;
            foreach (var IndentDt in items.IndentDetails)
            {
                if (IndentDt.state == true)
                {
                    mdt = new PurchaseIndentDetailModel();
                    mdt = IndentDt;
                    _indentDetails.Add(mdt);
                }
            }
            mindent.IndentId = items.IndentId;
            mindent.IndentNature = items.IndentNature;
            mindent.Storeid = items.Storeid;
            mindent.ProductID = items.ProductID;
            mindent.ItemCategoryId = items.ItemCategoryId;
            mindent.Remarks = items.Remarks;
            mindent.IndentDate = items.IndentDate;
            mindent.Remarks = items.Remarks;
            mindent.IndentNumber = items.IndentNumber;
            mindent.StoreName = items.StoreName;
            mindent.RequiredDate = items.RequiredDate;
            mindent.MaterialIndentId = items.MaterialIndentId;
            mindent.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            mindent.InsertedON = System.DateTime.Now;
            mindent.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            mindent.InsertedMacID = BISERP.Common.Constants.MacId;
            mindent.InsertedMacName = BISERP.Common.Constants.MacName;

            mindent.IndentDetails = _indentDetails;

            if (mindent.IndentId > 0)
            {
                _url = url + "/pindent/updatepurchaseindentqty" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, mindent, new JsonMediaTypeFormatter()).Result;
                //.Content.ReadAsAsync<int>().Result;
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
                _url = url + "/pindent/createpurchaseindent" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, mindent, new JsonMediaTypeFormatter()).Result;
                //.Content.ReadAsAsync<MaterialIndentModel>().Id;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    mindent =
                        JsonConvert.DeserializeObject<PurchaseIndentModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    mindent =
                        JsonConvert.DeserializeObject<PurchaseIndentModel>(result.Content.ReadAsStringAsync().Result);
                }
                if (!_isSuccess)
                {
                    return Json(new { success = false, Message = mindent.Message, Data = mindent });
                }
                else
                {
                    //var emailBody = new UtilitiesCls().RenderRazorViewToString(this, Constants.PIndentCreatedTemplate,
                    //mindent, false);
                    //MailHelper.SendEmail(new string[] { Constants.PurchaseEmailAddress }, new string[] { },
                    //Constants.NewPICreatedSubject, emailBody);
                    return Json(new { success = true, Message = mindent.IndentNumber, Data = mindent });
                }
            }

        }

        [HttpGet]
        public async Task<JsonResult> PurchaseIndentForVerification(int storeId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/pindent/getforverification/" + storeId.ToString() + Common.Constants.JsonTypeResult;
                List<PurchaseIndentModel> records = await Common.AsyncWebCalls.GetAsync<List<PurchaseIndentModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PurchaseIndentForAuthorization :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> PurchaseIndentForAuthorization(int storeId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/pindent/getforauthorization/" + storeId.ToString() + Common.Constants.JsonTypeResult;
                List<PurchaseIndentModel> records = await Common.AsyncWebCalls.GetAsync<List<PurchaseIndentModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PurchaseIndentForAuthorization :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AuthorizedPurchaseIndent(int StoreId, string IndentNo)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/pindent/getauthorized/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                List<PurchaseIndentModel> records = await Common.AsyncWebCalls.GetAsync<List<PurchaseIndentModel>>(client, _url, CancellationToken.None);
                if (!string.IsNullOrWhiteSpace(IndentNo))
                {
                    records = records.Where(s => s.IndentNumber.ToUpper().StartsWith(IndentNo.ToUpper())).ToList();
                }
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "Indent Number Not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AuthorizedPurchaseIndent :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public JsonResult AuthCancelPurchaseIndent(PurchaseIndentModel indent)
        {
            string _url = "";
            bool _isSuccess = false;
            PurchaseIndentModel Pindent = new PurchaseIndentModel();
            List<PurchaseIndentDetailModel> _indentDetails = new List<PurchaseIndentDetailModel>();

            PurchaseIndentDetailModel pdt;
            foreach (var IndentDt in indent.IndentDetails)
            {
                pdt = new PurchaseIndentDetailModel();
                pdt = IndentDt;
                if (IndentDt.state == true)
                {
                }
                else
                {
                    pdt.AuthorisedQty = 0;
                }
                _indentDetails.Add(pdt);
            }
            Pindent.IndentId = indent.IndentId;
            Pindent.AuthorizationStatusId = indent.AuthorizationStatusId;
            Pindent.AuthorisedRemarks = indent.AuthorisedRemarks;

            Pindent.IndentDetails = _indentDetails;
            Pindent.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            Pindent.InsertedON = System.DateTime.Now;
            Pindent.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            Pindent.InsertedMacID = BISERP.Common.Constants.MacId;
            Pindent.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/pindent/authcancelpurchaseindent" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, Pindent, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIndentModel>().Id;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                Pindent = JsonConvert.DeserializeObject<PurchaseIndentModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "OK")
            {
                _isSuccess = true;
                Pindent = JsonConvert.DeserializeObject<PurchaseIndentModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = Pindent.Message });
            else
                return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult DeleteIndentItem(int IndentDetailId)
        {
            if (IndentDetailId > 0)
            {
                PurchaseIndentModel indent = new PurchaseIndentModel();
                string _url = url + "/pindent/deleteindentitem/" + IndentDetailId.ToString() + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, indent, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<PurchaseIndentModel>().Id;
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetPendintMaterialRequest(int clientId)
        {
            List<PurchaseIndentModel> records = new List<PurchaseIndentModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/pindent/getPendintMaterialRequest/" + clientId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<PurchaseIndentModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Number  Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPendintMaterialRequest no :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        /*********************************Indent Template Grid ********************************/
        [HttpPost]
        public async Task<ActionResult> SaveIndentTemplate(PurchaseIndentModel items)
        {
            string _url = "";
            bool _isSuccess = true;
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedON = System.DateTime.Now;
            items.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            items.InsertedMacID = BISERP.Common.Constants.MacId;
            items.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/pindent/saveIndentTemplate" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, items, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
            }
            if (result.StatusCode.ToString() == "Created")
            {
                _isSuccess = true;
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = "Something went Wrong" });
            else
                return Json(new { success = true, Message = "Template saved successfully" });
        }
        [HttpGet]
        public async Task<JsonResult> GetAllIndentTemplate(int StoreId, int ItemCategoryId)
        {
            List<IndentTepmlateClass> records = new List<IndentTepmlateClass>();
            JsonResult jResult;
            try
            {
                string _url = url + "/pindent/GetAllIndentTemplate/" + StoreId + "/" + ItemCategoryId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<IndentTepmlateClass>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jResult = Json(new { success = false, Messsage = "No Record  Found" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllIndentTemplate :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetIndentTemplateforId(int templateId)
        {
            List<PurchaseIndentDetailModel> records = new List<PurchaseIndentDetailModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/pindent/GetIndentTemplateforId/" + templateId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<PurchaseIndentDetailModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Number  Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetIndentTemplateforId:" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> PurchaseIndentforRpt()
        {
            JsonResult jResult;
            List<PurchaseIndentModel> mimodel = new List<PurchaseIndentModel>();
            try
            {
                string _url = url + "/pindent/getpurchaseindentforRpt" + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<PurchaseIndentModel>>(client, _url, CancellationToken.None);
                if (mimodel != null)
                {
                    //if (!string.IsNullOrWhiteSpace(searchIssueNumber))
                    //{
                    //    mimodel = mimodel.Where(p => p.IndentNumber.ToUpper().Contains(searchIssueNumber.ToUpper())).ToList();
                    //}
                    mimodel = mimodel.OrderByDescending(m => m.IndentId).ToList();
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PurchaseIndentforRpt :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetPIRemarkLibrary(int StoreId, int ItemId)
        {
            List<PIRemarkLibrary> records = new List<PIRemarkLibrary>();
            JsonResult jResult;
            try
            {
                string _url = url + "/pindent/GetPIRemarkLibrary/" + StoreId + "/" + ItemId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<PIRemarkLibrary>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Remark Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPIRemarkLibrary:" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetProduct()
        {
            try
            {
                string _url = $"{url}/pindent/getProduct{Common.Constants.JsonTypeResult}";
                List<ProductModel> data = await Common.AsyncWebCalls.GetAsync<List<ProductModel>>(client, _url, CancellationToken.None);

                if (data == null || !data.Any())
                {
                    return Json(new { error = "No data found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetProduct method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the data." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetProject(int ProductID)
        {
            try
            {
                string _url = $"{url}/pindent/getProject/{ProductID}{Common.Constants.JsonTypeResult}";
                List<ProjectModel> data = await Common.AsyncWebCalls.GetAsync<List<ProjectModel>>(client, _url, CancellationToken.None);

                if (data == null || !data.Any())
                {
                    return Json(new { error = "No data found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetProject method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the data." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetProductItem(int ProductID, int? ProjectID)
        {
            string _url = $"{url}/pindent/getProductItem/{ProductID}/{ProjectID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<ProductItemModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetProductItem: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetPRStateDetails(int? IndentId)
        {
            try
            {
                string _url = $"{url}/pindent/getPRStateDetails/{IndentId}{Common.Constants.JsonTypeResult}";
                List<PRStateDetailsModel> data = await Common.AsyncWebCalls.GetAsync<List<PRStateDetailsModel>>(client, _url, CancellationToken.None);

                if (data == null || !data.Any())
                {
                    return Json(new { error = "No data found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPRStateDetails method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the data." }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
