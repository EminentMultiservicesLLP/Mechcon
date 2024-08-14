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
using BISERP.Areas.Masters.Models;
using BISERP.Common;

namespace BISERP.Areas.Store.Controllers
{
    public class MaterialTransferController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(MaterialTransferController));

        public MaterialTransferController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public async Task<ActionResult> SaveMaterialIssue(MaterialIndentModel items)
        {
            MaterialIssueModel mis = new MaterialIssueModel();
            List<MaterialIssueDetailModel> _misdetails = new List<MaterialIssueDetailModel>();
            MaterialIssueDetailModel misdt;
            string strErrorMsg = "";
            int _rowSelected = 0;
            var IndentDetails = items.Materialindentdt.OrderBy(i => i.IndentDetails_Id);
            foreach (var IndentDt in IndentDetails)
            {
                if (IndentDt != null)
                {
                    if (IndentDt.state == true)
                    {
                        _rowSelected++;
                        var _indentDetailId = IndentDt.IndentDetails_Id;
                        var _issuedQuantity = IndentDetails.Where(i => i.IndentDetails_Id.Equals(_indentDetailId)).Sum(i => i.IssuedQuantity).GetValueOrDefault();
                        if (_issuedQuantity == 0)
                        {
                            strErrorMsg = "Please Enter Issue Quantity for Selected Item.";
                            break;
                        }
                        if (_issuedQuantity > IndentDt.Item_Stock)
                        {
                            strErrorMsg = "Issued Quantity Cannot be More Than Available Quantity";
                            break;
                        }
                        else if (_issuedQuantity > IndentDt.PendingQty)
                        {
                            strErrorMsg = "Issued Quantity Cannot be More Than Balance Quantity For Issue";
                            break;
                        }
                        else if (IndentDt.state == true)
                        {
                            misdt = new MaterialIssueDetailModel();
                            misdt.IndentDetailId = IndentDt.IndentDetails_Id;
                            misdt.ItemId = IndentDt.Item_Id;
                            misdt.IssuedQuantity = IndentDt.IssuedQuantity;
                            misdt.Batchid = IndentDt.BatchId;
                            misdt.ItemName = IndentDt.ItemName;
                           
                            _misdetails.Add(misdt);
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
                //string _url = "";
                mis.Indent_Id = items.Indent_Id;
                mis.FromStoreId = items.Indent_FromStoreID;
                mis.StoreId = items.Indent_ToStoreID;
                mis.IssueDate = items.Indent_Date;
                mis.MaterialIssueDt = _misdetails;

                mis.Authorised = true;
                mis.BranchID = 1;
                mis.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                mis.InsertedON = System.DateTime.Now;
                mis.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                mis.InsertedMacID = BISERP.Common.Constants.MacId;
                mis.InsertedMacName = BISERP.Common.Constants.MacName;

                string _url = url + "/transfermaterial/materialtransfer" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, mis, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    strErrorMsg = "Error In Material Transfer";
                    mis = JsonConvert.DeserializeObject<MaterialIssueModel>(result.Content.ReadAsStringAsync().Result);
                   
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    strErrorMsg = "";
                    var emailBody = await Common.AsyncWebCalls.GetAsync<BranchModel>("/stores/BranchbyStore/" + (items.BranchID ?? 0), CancellationToken.None);
                    UtilitiesCls.SendEmailTask(EmailProcessFor.MaterialTransfer, this, mis, false, new string[] { emailBody.Email });
                   mis = JsonConvert.DeserializeObject<MaterialIssueModel>(result.Content.ReadAsStringAsync().Result);
                }

            }
            if (strErrorMsg != "")
                return Json(new { success = false, responseText = strErrorMsg }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = true, responseText = mis.IssueNo });
        }
    }
}
