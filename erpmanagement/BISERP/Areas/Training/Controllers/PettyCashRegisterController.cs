using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using BISERP.Areas.Branch.Models;
using BISERP.Areas.DailyData.Models;
using iTextSharp.text;

namespace BISERP.Areas.Training.Controllers
{
    public class PettyCashRegisterController : Controller
    {
        readonly HttpClient _client;
        readonly string _url = Common.Constants.WebAPIAddress;
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(PettyCashRegisterController));

        public PettyCashRegisterController()
        {
            _client = new HttpClient { BaseAddress = new Uri(_url) };
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveCashDetails(List<CashDepositeModel> items)
        {
            //string strErrorMsg = "";
            var isSuccess = false;
            List<CashDepositeModel> modelList = new List<CashDepositeModel>();
            foreach (var item in items)
            {
                CashDepositeModel model = new CashDepositeModel();
                item.CreatedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                item.CreatedDate = DateTime.Now;
                model = item;
                modelList.Add(model);
            }
      
            string url = _url + "/PettyCash/CreateCashDeposite" + Common.Constants.JsonTypeResult;
            var result = _client.PostAsync(url, modelList, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
            if (result.StatusCode.ToString() == "BadRequest")
            {
               JsonConvert.DeserializeObject<List<CashDepositeModel>>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                isSuccess = true;
                 JsonConvert.DeserializeObject<List<CashDepositeModel>>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "OK")
            {
                isSuccess = true;
            }

            if (!isSuccess)
                return Json(new { success = false, Message = "Error In  PettyCash Register" });
            else
                return Json(new { success = true,  Message = "PettyCash Register saved successfully." });
        }

     

        [HttpPost]
        public ActionResult SaveCashWithdrawal(List<CashWithdrawalModel> items)
        {
            //string strErrorMsg = "";
            var isSuccess = false;
            List<CashWithdrawalModel>  modelList = new List<CashWithdrawalModel>();
            foreach (var item in items)
            {
                CashWithdrawalModel model = new CashWithdrawalModel();
                item.CreatedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                item.CreatedDate = DateTime.Now;
                model = item;
                modelList.Add(model);
            }
            var url = _url + "/PettyCash/CreateCashWithdrawal" + Common.Constants.JsonTypeResult;
            var result = _client.PostAsync(url, modelList, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                JsonConvert.DeserializeObject<List<CashWithdrawalModel>>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                isSuccess = true;
                JsonConvert.DeserializeObject<List<CashWithdrawalModel>>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "OK")
            {
                isSuccess = true;
            }

            if (!isSuccess)
                return Json(new { success = false, Message = "Error In  PettyCash Register" });
            else
                return Json(new { success = true, Message = "PettyCash Register saved successfully." });
        }
    }
}
