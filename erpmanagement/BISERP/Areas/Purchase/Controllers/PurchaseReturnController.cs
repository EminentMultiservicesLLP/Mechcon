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
using BISERP.Areas.Store.Models.Master;
using BISERP.Common;
using BISERP.Filters;

namespace BISERP.Area.Purchase.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class PurchaseReturnController : Controller
    {
        //
        // GET: /PurchaseReturn/

        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(PurchaseReturnController));

        public PurchaseReturnController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpGet]
        public async Task<JsonResult> PurchaseGrn(int? storeID, string searchGRNNumber)
        {
            JsonResult jResult;
            List<PurchaseReturnModel> mimodel = new List<PurchaseReturnModel>();
            try
            {
                string _url = $"{url}/purchasereturn/getpurchasegrn" + (storeID.HasValue ? $"/{storeID.Value}" : "") + Common.Constants.JsonTypeResult;

                mimodel = await Common.AsyncWebCalls.GetAsync<List<PurchaseReturnModel>>(client, _url, CancellationToken.None);
                if (mimodel != null & mimodel.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchGRNNumber))
                    {
                        mimodel = mimodel.Where(p => p.Grnno.ToUpper().Contains(searchGRNNumber.ToUpper())).ToList();
                    }
                    mimodel = mimodel.OrderByDescending(m => m.ID).ToList();
                    jResult = Json(new { success = true, mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Grn found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PurchaseGrn :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> PurchaseGrndt(int GrnId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/purchasereturn/getpgrndt/" + GrnId.ToString() + Common.Constants.JsonTypeResult;
                var Items = await Common.AsyncWebCalls.GetAsync<List<PurchaseReturnDtModel>>(client, _url, CancellationToken.None);

                if (Items != null)
                {
                    jResult = Json(Items, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PurchaseGrndt :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> AllPurchaseReturn(int? storeID, string searchPurchaseNumber)
        {
            JsonResult jResult;
            List<PurchaseReturnModel> mimodel = new List<PurchaseReturnModel>();
            try
            {
                string _url = $"{url}/PurchaseReturn/getAllPr" + (storeID.HasValue ? $"/{storeID.Value}" : "") + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<PurchaseReturnModel>>(client, _url, CancellationToken.None);
                if (mimodel != null & mimodel.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchPurchaseNumber))
                    {
                        mimodel = mimodel.Where(p => p.PRNo.ToUpper().Contains(searchPurchaseNumber.ToUpper())).ToList();
                    }
                    mimodel = mimodel.OrderByDescending(m => m.ID).ToList();
                    jResult = Json(new { success = true, mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "Purchase Return No. not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllPurchaseReturn :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> PurchaseReturnItems(int PrID)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/purchasereturn/getpurchasedtbyid/" + PrID.ToString() + Common.Constants.JsonTypeResult;
                var Items = await Common.AsyncWebCalls.GetAsync<List<PurchaseReturnDtModel>>(client, _url, CancellationToken.None);

                if (Items != null)
                {
                    jResult = Json(Items, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PurchaseReturnItems :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        public async Task<ActionResult> SavePurchaseReturn(PurchaseReturnModel items)
        {
            string _url = "";
            string strErrorMsg = "";
            bool _isSuccess = false;
            //int _rowSelected = 0;
            PurchaseReturnModel pindent = new PurchaseReturnModel();
            List<PurchaseReturnDtModel> _indentDetails = new List<PurchaseReturnDtModel>();
            PurchaseReturnDtModel mdt;
            foreach (var IndentDt in items.PurchaseReturnDt)
            {
                if (IndentDt != null)
                {
                    //if (IndentDt.state == true)
                    //{
                    if (IndentDt.Qty == 0)
                    {
                        strErrorMsg = "Please Enter Return Quantity for  Item.";
                        break;
                    }
                    if (IndentDt.Qty == null)
                    {
                        strErrorMsg = "Please Enter Return Quantity for Item.";
                        break;
                    }
                    if (IndentDt.Reason == null)
                    {
                        strErrorMsg = "Please Enter Reason  for  Item.";
                        break;
                    }
                    if ((IndentDt.Qty > IndentDt.GrnQty))
                    {
                        strErrorMsg = "Return Quantity CanNot be Greater Than GRN Quantity.";
                        break;
                    }
                    if ((IndentDt.FreeQty > IndentDt.GrnFreeQty))
                    {
                        strErrorMsg = "ReturnFree Quantity CanNot be Greater Than GRNFree Quantity.";
                        break;
                    }

                    if (IndentDt.Qty > IndentDt.StockQty)
                    {
                        strErrorMsg = "Return Quantity CanNot be Greater Than Stock Quantity ";
                    }
                    else
                    {
                        mdt = new PurchaseReturnDtModel();
                        mdt = IndentDt;
                        _indentDetails.Add(mdt);
                    }
                    //    _rowSelected++;
                    //}
                }
            }
            pindent.ID = items.ID;
            pindent.GrnID = items.GrnID;
            pindent.PRDate = items.PRDate;
            pindent.PRNo = items.PRNo;
            pindent.supplierid = items.supplierid;
            pindent.StoreId = items.StoreId;
            pindent.Remark = items.Remark;
            pindent.InsertedBy = 1;
            pindent.InsertedON = System.DateTime.Now;
            pindent.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            pindent.InsertedMacID = BISERP.Common.Constants.MacId;
            pindent.InsertedMacName = BISERP.Common.Constants.MacName;
            pindent.PurchaseReturnDt = _indentDetails;
            pindent.Supplier = items.Supplier;
            pindent.Grnno = items.Grnno;


            if (strErrorMsg != "")
            {
                _isSuccess = false;
                return Json(new { success = false, Message = strErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _isSuccess = true;
                _url = url + "/purchasereturn/createPurchaseReturn" + Common.Constants.JsonTypeResult;
                // var result = client.PostAsync(_url, pindent, new JsonMediaTypeFormatter()).Result.Content.ReadAsAsync<PurchaseReturnModel>().Id;
                var result = client.PostAsync(_url, pindent, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    pindent = JsonConvert.DeserializeObject<PurchaseReturnModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    pindent = JsonConvert.DeserializeObject<PurchaseReturnModel>(result.Content.ReadAsStringAsync().Result);
                }
                //return Json(new { success = true, responseText = "" });
            }
            if (_isSuccess)
            {
                UtilitiesCls.SendEmailTask(EmailProcessFor.PurchaseReturn, this, pindent);

            }
            if (!_isSuccess)
                return Json(new { success = false, Message = items.Message });
            else
                return Json(new { success = true, Message = pindent.PRNo });
        }
        [HttpPost]
        public JsonResult AuthCancelPurchaseAccept(PurchaseReturnModel PR)
        {
            string _url = "";
            PurchaseReturnModel _mis = new PurchaseReturnModel();
            _mis.ID = PR.ID;
            _mis.StoreId = PR.StoreId;
            //_mis.ReturnTo = MR.ReturnTo;
            _mis.Authorized = PR.Authorized;
            _mis.cancelled = PR.cancelled;
            _mis.PurchaseReturnDt = PR.PurchaseReturnDt;
            _mis.InsertedBy = 1;
            _mis.InsertedON = System.DateTime.Now;
            _mis.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            _mis.InsertedMacID = BISERP.Common.Constants.MacId;
            _mis.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/purchasereturn/authcancel" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, _mis, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<PurchaseReturnModel>().Id;
            return Json(new { success = true });

        }

        [HttpGet]
        public async Task<JsonResult> PurchaseReturnForRpt()
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/purchasereturn/PurchaseReturnForRpt" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<PurchaseReturnModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PurchaseReturnForRpt :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

    }
}
