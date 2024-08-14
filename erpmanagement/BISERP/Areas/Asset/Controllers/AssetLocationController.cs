using BISERP.Area.Purchase.Models;
using BISERP.Areas.Asset.Models;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BISERP.Areas.Asset.Controllers
{
    public class AssetLocationController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _url = Common.Constants.WebAPIAddress;
        private static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(AssetLocationController));

        public AssetLocationController()
        {
            _client = new HttpClient {BaseAddress = new Uri(_url)};
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetAssetLocations(int assetId)
        {
            JsonResult jResult;
            try
            {
                var url = _url + "/assetlocation/getassetlocation/" + assetId + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<AssetLocationModel>(_client, url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetAssetLocations :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveAssetLocation(AssetLocationModel model)
        {
            string url = "";
            bool isSuccess = false;

            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = Common.Constants.IpAddress;
            model.InsertedMacID = Common.Constants.MacId;
            model.InsertedMacName = Common.Constants.MacName;
            if (model.AssetLocationId > 0)
            {
                url = _url + "/assetlocation/updateassetlocation" + Common.Constants.JsonTypeResult;
            }
            else
            {
                url = _url + "/assetlocation/createassetlocation" + Common.Constants.JsonTypeResult;
            }
            var result = _client.PostAsync(url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                isSuccess = false;
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                isSuccess = true;
                model = JsonConvert.DeserializeObject<AssetLocationModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "OK")
            {
                isSuccess = true;
            }
            if (!isSuccess)
                return Json(new { success = false });
            else
                return Json(new { success = true, AssetLocationId = model.AssetLocationId });
        }

        [HttpGet]
        public async Task<JsonResult> GetPoNoAssetLo(int itemId)
        {
            JsonResult jResult;
            try
            {
                var url = _url + "/assetlocation/GetPoNoAssetLo/" + itemId + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<PurchaseOrderModel>>(_client, url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetPoNoAssetLo :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
