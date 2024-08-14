using BISERP.Areas.Miscellaneous.Models;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.Miscellaneous.Controllers
{
    public class CancelPendingMaterialIndentController : Controller
    {
      HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(CancelPendingMaterialIndentController));

        public CancelPendingMaterialIndentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetCancelMaterialIndent(int storeId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/cancelMI/getcancelMI/" + storeId.ToString() + Common.Constants.JsonTypeResult;
                List<CancelPendingMaterialIndentModel> records = await Common.AsyncWebCalls.GetAsync<List<CancelPendingMaterialIndentModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {

                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetCancelMaterialIndent :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        //[HttpPost]
        //public ActionResult SaveCancelMaterialIndent(List<CancelPendingMaterialIndentModel> items)
        //{
        //    string _url = "";
        //    List<CancelPendingMaterialIndentModel> modelList = new List<CancelPendingMaterialIndentModel>();
        //    foreach (var item in items)
        //    {
        //        OpeningBalanceModel model = new OpeningBalanceModel();
        //        //item.InsertedBy = 1;
        //        //item.InsertedON = System.DateTime.Now;
        //        //item.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
        //        //item.InsertedMacID = BISERP.Common.Constants.MacId;
        //        //item.InsertedMacName = BISERP.Common.Constants.MacName;
        //        model = item;
        //        modelList.Add(model);
        //    }

        //    _url = url + "/cancelMI/createcancelMI" + Common.Constants.JsonTypeResult;
        //    var result = client.PostAsync(_url, modelList, new JsonMediaTypeFormatter()).Result.Content.ReadAsAsync<CancelPendingMaterialIndentModel>().Id;
        //    //}
        //    return Json(new { success = true });
        //}
        [HttpPost]
        public ActionResult SaveCancelMaterialIndent(List<CancelPendingMaterialIndentModel> entity)
        {
            string _url = "";

            List<CancelPendingMaterialIndentModel> modelList = new List<CancelPendingMaterialIndentModel>();
            foreach (var item in entity)
            {
                CancelPendingMaterialIndentModel model = new CancelPendingMaterialIndentModel();
                model = item;
                model.AuthorizedBy = 1;
                model.AuthorizedOn = System.DateTime.Now;
                //item.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                //item.InsertedMacID = BISERP.Common.Constants.MacId;
                //item.InsertedMacName = BISERP.Common.Constants.MacName;                
                modelList.Add(model);
            }

            _url = url + "/cancelMI/createcancelMI" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, entity, new JsonMediaTypeFormatter()).Result.Content.ReadAsAsync<CancelPendingMaterialIndentModel>().Id;

            return Json(new { success = true });
        }

    }
}
