using BISERP.Areas.Store.Models.Store;
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
using BISERP.Areas.Miscellaneous.Models;
using BISERP.Common;
using BISERP.Areas.Masters.Models;

namespace BISERP.Areas.Miscellaneous.Controllers
{
    public class MaterialReturnController : Controller
    {
        //
        // GET: /MaterialReturn/

       HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(MaterialReturnController));

        public MaterialReturnController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpGet]
        public async Task<JsonResult> AllMaterialReturn( string searchIssueNumber)
        {
            JsonResult jResult;
            List<MaterialReturnModel> records = new List<MaterialReturnModel>();
            try
            {
                string _url = url + "/materialreturn/getAllmrslist" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<MaterialReturnModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchIssueNumber))
                    {
                        records = records.Where(p => p.IssueNo.ToUpper().StartsWith(searchIssueNumber.ToUpper())).ToList();
                    }
                    //int total = records.Count();
                    //if (page.HasValue && limit.HasValue)
                    //{
                    //    int start = (page.Value - 1) * limit.Value;
                    //    records = records.Skip(start).Take(limit.Value).ToList();
                    //}
                    records = records.OrderByDescending(m => m.ReturnID).ToList();
                   // jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                    
                }
                else
                    //jResult = Json("Error");
                    jResult = Json(new { success = false, Messsage = "Issue Number Not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllMaterialReturn :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> MaterialReturnItems(int ReturnID)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/materialreturn/getmaterialreturntbyid/" + ReturnID.ToString() + Common.Constants.JsonTypeResult;
                var issueItems = await Common.AsyncWebCalls.GetAsync<List<MaterialReturnDtModel>>(client, _url, CancellationToken.None);

                if (issueItems != null)
                {
                    jResult = Json(issueItems, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in MaterialReturnItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }


        [HttpGet]
        public async Task<JsonResult> AllMaterialReturnAuth()
        {
            JsonResult jResult;
            List<MaterialReturnModel> records = new List<MaterialReturnModel>();
            try
            {
                string _url = url + "/materialreturn/getAllmr/0" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<MaterialReturnModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    records = records.OrderByDescending(m => m.ReturnID).ToList();
                    jResult = Json( records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllMaterialReturnAuth :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AuthMaterialReturn()
        {
            JsonResult jResult;
            List<MaterialReturnModel> records = new List<MaterialReturnModel>();
            try
            {
                string _url = url + "/materialreturn/getAllmr/1" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<MaterialReturnModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    records = records.OrderByDescending(m => m.ReturnID).ToList();
                    jResult = Json( records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AuthMaterialReturn :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        } 

        [HttpGet]
        public async Task<JsonResult> AllMaterialIssue(int? page, int? limit, string sortBy, string direction, string searchString, int? userId, int? LOCId, string Type)
        {
            JsonResult jResult;
            try
            {
                if (Type == null)
                    Type = "ISSUE";
                userId = 1;
                LOCId = 1;
               
                string _url = url + "/materialissue/getallmlist/" + userId.ToString() + "/" + LOCId.ToString() + "/" + Type + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<MaterialIssueModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchString))
                    {
                        records = records.Where(p => p.IssueNo.ToUpper().StartsWith(searchString.ToUpper())).ToList();
                    }
                    int total = records.Count;
                    if (page.HasValue && limit.HasValue)
                    {
                        int start = (page.Value - 1) * limit.Value;
                        records = records.Skip(start).Take(limit.Value).ToList();
                    }
                    jResult = Json(new { records, total }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllMaterialIssue :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public async Task<ActionResult> SaveMaterialReturn(MaterialReturnModel items)
        {
            string strErrorMsg = "";
            bool isSuccess = false;
            List<MaterialReturnDtModel> indentDetails = new List<MaterialReturnDtModel>();

            // Validate and collect valid material return details
            foreach (var indentDt in items.MaterialReturnDt)
            {
                if (indentDt != null)
                {
                    if (indentDt.Quantity == null || indentDt.Quantity == 0)
                    {
                        strErrorMsg = "Please Enter Return Quantity for Item.";
                        break;
                    }
                    if (string.IsNullOrEmpty(indentDt.Reason))
                    {
                        strErrorMsg = "Please Enter Reason for Item.";
                        break;
                    }
                    if (indentDt.Quantity > indentDt.IssueQty)
                    {
                        strErrorMsg = "Return Quantity Cannot be Greater Than Issue Quantity";
                        break;
                    }
                    if (indentDt.Quantity > indentDt.StockQty)
                    {
                        strErrorMsg = "Return Quantity Cannot be Greater Than Stock Quantity";
                        break;
                    }
                    indentDetails.Add(indentDt);
                }
            }

            // Check for errors
            if (!string.IsNullOrEmpty(strErrorMsg))
            {
                return Json(new { success = false, Message = strErrorMsg }, JsonRequestBehavior.AllowGet);
            }

            // Prepare main material return model
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedON = DateTime.Now;
            items.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            items.InsertedMacID = BISERP.Common.Constants.MacId;
            items.InsertedMacName = BISERP.Common.Constants.MacName;

            MaterialReturnModel mindent = new MaterialReturnModel
            {
                IssueId = items.IssueId,
                ReturnID = items.ReturnID,
                ReturnType = items.ReturnType,
                ReturnDate = items.ReturnDate,
                ReturnFrom = items.ReturnFrom,
                ReturnTo = items.ReturnTo,
                ReturnNo = items.ReturnNo,
                StrReturnFrom = items.StrReturnFrom,
                StrReturnTo = items.StrReturnTo,
                IssueNo = "I-",
                InsertedBy = 1,
                InsertedON = DateTime.Now,
                InsertedIPAddress = BISERP.Common.Constants.IpAddress,
                InsertedMacID = BISERP.Common.Constants.MacId,
                InsertedMacName = BISERP.Common.Constants.MacName,
                MaterialReturnDt = indentDetails
            };

            // Send the data to the server
            string _url = url + "/materialreturn/creatematerialReturn" + Common.Constants.JsonTypeResult;
            var result = await client.PostAsync(_url, mindent, new JsonMediaTypeFormatter());

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                isSuccess = false;
                mindent = JsonConvert.DeserializeObject<MaterialReturnModel>(await result.Content.ReadAsStringAsync());
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                isSuccess = true;
                mindent = JsonConvert.DeserializeObject<MaterialReturnModel>(await result.Content.ReadAsStringAsync());
            }

            // Return the result
            if (!isSuccess)
            {
                return Json(new { success = false, Message = items.Message });
            }
            else
            {
                var branchResult = await Common.AsyncWebCalls.GetAsync<BranchModel>("/stores/BranchbyStore/" + items.FromStoreID, CancellationToken.None);
                UtilitiesCls.SendEmailTask(EmailProcessFor.MaterialReturn, this, mindent, false, new string[] { branchResult.Email });

                return Json(new { success = true, Message = mindent.ReturnNo });
            }
        }


        [HttpGet]
        public async Task<JsonResult> MaterialIssueItems(int IssueId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/materialreturn/getmiredetailbyid/" + IssueId.ToString() + Common.Constants.JsonTypeResult;
                var issueItems = await Common.AsyncWebCalls.GetAsync<List<MaterialReturnDtModel>>(client, _url, CancellationToken.None);

                if (issueItems != null)
                {
                    jResult = Json(issueItems, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in MaterialIssueItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpPost]
        public async Task<JsonResult> AuthCancelMaterialAccept(MaterialReturnModel MR)
        {
            if (MR == null)
            {
                return Json(new { success = false, message = "Invalid input data" });
            }

            MR.InsertedBy = 1; // Consider injecting this value
            MR.InsertedON = DateTime.Now;
            MR.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            MR.InsertedMacID = BISERP.Common.Constants.MacId;
            MR.InsertedMacName = BISERP.Common.Constants.MacName;

            string _url = $"{url}/materialreturn/authcancel{Common.Constants.JsonTypeResult}";

            try
            {
                var response = await client.PostAsync(_url, MR, new JsonMediaTypeFormatter());
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Error in processing request" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return Json(new { success = false, message = ex.Message });
            }
        }



    }
}
