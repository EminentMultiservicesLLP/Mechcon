using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using BISERP.Areas.Accounts.Models;

namespace BISERP.Areas.Account.Controllers
{
    public class BillPaymentController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(BillPaymentController));
        public BillPaymentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveBillPayment(List<SupplierBillModel> bills)
        {
            string _url = "";
            bool _isSuccess = true;
            string strError = "";
            SupplierBillPaymentModel _bill = new SupplierBillPaymentModel();
            List<SupplierBillPaymentModel> _lstbill = new List<SupplierBillPaymentModel>();
            foreach (var b in bills)
            {
                if (b.PartyPayable != null)
                {
                    if(b.PartyPayable > 0)
                    {
                        if (b.BillAmount < b.PartyPayable)
                        {
                            _isSuccess = false;
                            strError = "Paid Amount Cannot Be More Than Bill Amount";
                        }
                        else
                        {
                            _bill = new SupplierBillPaymentModel();
                            _bill.BillId = b.BillId;
                            _bill.SupplierId = b.SuppilerId;
                            _bill.PaidAmount = b.PartyPayable;
                            _bill.PaymentDate = System.DateTime.Now;
                            _bill.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                            _bill.InsertedON = System.DateTime.Now;
                            _bill.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                            _bill.InsertedMacID = BISERP.Common.Constants.MacId;
                            _bill.InsertedMacName = BISERP.Common.Constants.MacName;
                            _lstbill.Add(_bill);
                        }
                    }                    
                }
            }
            if (_isSuccess == true && strError == "")
            {
                _url = url + "/billpayment/createbillpayment" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, _lstbill, new JsonMediaTypeFormatter()).Result;
                
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    _lstbill = JsonConvert.DeserializeObject<List<SupplierBillPaymentModel>>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = strError });
            else
                return Json(new { success = true });
        }
    }
}
