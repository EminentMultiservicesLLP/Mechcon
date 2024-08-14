using BISERP.Areas.Masters.Models;
using BISERP.Filters;
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

namespace BISERP.Areas.Masters.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class PackingListController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(PackingListController));

        public PackingListController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SavePackingList(PackingListModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedOn = System.DateTime.Now;

            _url = url + "/packingList/savePackingList" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<PackingListModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                _isSuccess = true;
                model = JsonConvert.DeserializeObject<PackingListModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
            {
                return Json(new { success = false, Message = "Error while save PackingList", Data = model });
            }
            else
            {
                return Json(new { success = true, Message = model.PackingListId, Data = model });
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetPackingList(int StoreId)
        {
            List<PackingListModel> records = new List<PackingListModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/packingList/getPackingList/" + StoreId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<PackingListModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No PackingList Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPackingList:" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        } 
        
        [HttpGet]
        public async Task<JsonResult> GetPackingListforRpt(int StoreId, DateTime fromdate, DateTime todate)
        {
            List<PackingListModel> records = new List<PackingListModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/packingList/getPackingListforRpt/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + "/" + StoreId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<PackingListModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No PackingList Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPackingList:" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetPackingListDetail(int PackingListId)
        {
            List<PackingListDetailModel> records = new List<PackingListDetailModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/packingList/getPackingListDetail/" + PackingListId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<PackingListDetailModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No PackingListDetail Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPackingListDetail:" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
