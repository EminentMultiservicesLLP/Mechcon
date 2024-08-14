using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http;
using BISERP.Areas.Asset.Models;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace BISERP.Areas.Asset.Controllers
{
    public class AssetDeletionRequestController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(AssetDeletionRequestController));

        public AssetDeletionRequestController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

    
        [HttpPost]
        public ActionResult SaveAssetDeletionRequest(DeactivationDetail model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/assetdel/CreateAssetDeletionRequest" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<DeactivationDetail>(result.Content.ReadAsStringAsync().Result);
            }
            if (result.StatusCode.ToString() == "Created")
            {
                _isSuccess = true;
                model = JsonConvert.DeserializeObject<DeactivationDetail>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = "Error In Request Register" });
            else
                return Json(new { success = true, Message = "Asset deletion Request successfully" });
        }

     
    }
}
