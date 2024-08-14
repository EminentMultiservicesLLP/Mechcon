using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using BISERP.Areas.DashBoards.Models;
using System.Threading;

namespace BISERP.Areas.DashBoard.Controllers
{
    public class RequestStatusController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(RequestStatusController));

        public RequestStatusController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetRequestStatus(DateTime fromdate,DateTime todate,int StoreId, string IndentNo)
        {
            JsonResult jResult;
            List<RequestStatusModel> records = new List<RequestStatusModel>();
            try
            {
                if (IndentNo == "") IndentNo = "''";
                string _url = url + "/materilaindents/requeststatus/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + "/" + StoreId + "/" + IndentNo.Replace("/","~") + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<RequestStatusModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {                    
                    jResult = Json( records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetRequestStatus :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
