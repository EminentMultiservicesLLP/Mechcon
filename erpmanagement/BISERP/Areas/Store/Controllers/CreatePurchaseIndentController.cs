using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Store.Models.Store;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Formatting;
using BISERP.Area.Purchase.Models;
using BISERP.Areas.Store.Models.Master;
using BISERP.Common;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using Newtonsoft.Json;


namespace BISERP.Areas.Store.Controllers
{
    public class CreatePurchaseIndentController : Controller
    {        
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(CreatePurchaseIndentController));

        public CreatePurchaseIndentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllAuthStoreMaterialIndents(int Storeid)//(int? page, int? limit, string sortBy, string direction, string SearchName, string SearchCode)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/materilaindents/getallauthmaterilaindents/" + Storeid.ToString() + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                var indent = await Common.AsyncWebCalls.GetAsync<MaterialIndentModel>(client, _url, CancellationToken.None);

                jResult = Json(new { indent }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllAuthStoreMaterialIndents :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllAuthStoreMaterialIndentsDt(int Indent_id)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/materilaindents/getmaterilaIndentbyid/" + Indent_id.ToString() + Common.Constants.JsonTypeResult;
                var indent = await Common.AsyncWebCalls.GetAsync<MaterialIndentModel>(client, _url, CancellationToken.None);
                List<MaterialIndentDtModel> _Materialindentdt = indent.Materialindentdt;

                if (_Materialindentdt != null && _Materialindentdt.Count > 0)
                {
                    jResult = Json(_Materialindentdt, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllAuthStoreMaterialIndentsDt :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult CreatePurchaseIndent(List<MaterialIndentDtModel> Materialindentdt)
        {
            PurchaseIndentModel pindent = new PurchaseIndentModel();
            List<PurchaseIndentDetailModel> _indentDetails = new List<PurchaseIndentDetailModel>();
            PurchaseIndentDetailModel pidt;
            List<PurchaseMaterialIndentModel> lstpmimodel = new List<PurchaseMaterialIndentModel>();
            PurchaseMaterialIndentModel pmimodel;
           
            bool _isSuccess = true;
            int _itemId = 0;
            float Quantity = 0;
            var IndentDetails = Materialindentdt.OrderBy(i => i.Item_Id);
            foreach (var IndentDt in IndentDetails)
            {
                if (IndentDt != null)
                {
                    if (_itemId == 0)
                    {
                        _itemId = IndentDt.Item_Id;
                        Quantity = Materialindentdt.Where(i => i.Item_Id == _itemId).Sum(i => i.PendingQty).GetValueOrDefault();
                        
                        pidt = new PurchaseIndentDetailModel();
                        pidt.ItemId = _itemId;
                        pidt.ItemQty = Quantity;
                        pidt.ItemName = IndentDt.ItemName;
                        pidt.CurrentQty = IndentDt.PendingQty;
                        _indentDetails.Add(pidt);
                    }
                    else if(_itemId != IndentDt.Item_Id)
                    {
                        _itemId = IndentDt.Item_Id;
                        Quantity = Materialindentdt.Where(i => i.Item_Id == _itemId).Sum(i => i.PendingQty).GetValueOrDefault();
                        pidt = new PurchaseIndentDetailModel();
                        pidt.ItemId = _itemId;
                        pidt.ItemQty = Quantity;
                        pidt.ItemName = IndentDt.ItemName;
                        pidt.CurrentQty = IndentDt.PendingQty;
                        _indentDetails.Add(pidt);
                    }                    
                }
            }
            pindent.IndentNature = 3;
            pindent.Storeid = 209;
            pindent.Remarks = "Direct Purchase Indent";
            pindent.IndentDate = System.DateTime.UtcNow;
            pindent.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            pindent.InsertedON = System.DateTime.Now;
            pindent.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            pindent.InsertedMacID = BISERP.Common.Constants.MacId;
            pindent.InsertedMacName = BISERP.Common.Constants.MacName;
            pindent.IndentDetails = _indentDetails;

            string _url = url + "/pindent/createpurchaseindent" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, pindent, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIndentModel>().Id;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                pindent = JsonConvert.DeserializeObject<PurchaseIndentModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                _isSuccess = true;
                pindent = JsonConvert.DeserializeObject<PurchaseIndentModel>(result.Content.ReadAsStringAsync().Result);

                List<int> lstPIndentId = Materialindentdt.Select(m => m.Indent_Id).Distinct().ToList();
                foreach (int pIndentId in lstPIndentId)
                {
                    pmimodel = new PurchaseMaterialIndentModel();
                    pmimodel.IndentId = pIndentId;
                    lstpmimodel.Add(pmimodel);
                }
                lstpmimodel.ForEach(m => m.PIndentId = pindent.IndentId);

                _url = "";
                _url = url + "/pmindent/createpmindent" + Common.Constants.JsonTypeResult;                
                var newresult = client.PostAsync(_url, lstpmimodel, new JsonMediaTypeFormatter()).Result;
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = pindent.Message });
            else
                
                UtilitiesCls.SendEmailTask(EmailProcessFor.PurchaseIndentRequest, this, pindent);
            
                return Json(new { success = true, Message = pindent.IndentNumber });
        }

    }
}
