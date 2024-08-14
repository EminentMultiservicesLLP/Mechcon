using BISERP.Areas.Asset.Models;
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

namespace BISERP.Areas.Asset.Controllers
{
    public class AMCProviderController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(AMCProviderController));

        public AMCProviderController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllAMCProvider(string SearchName, string SearchCode)
        {
            JsonResult jResult;
            List<AMCProviderModel> records = new List<AMCProviderModel>();
            try
            {
                string _url = url + "/amcprovider/getallprovider" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<AMCProviderModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.Name.ToUpper().Contains(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.Code.ToUpper().Contains(SearchCode.ToUpper())).ToList();
                    }
                    int total = records.Count;
                    jResult = Json( records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllAMCProvider :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveAMCProvider(AMCProviderModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = 1;
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            if (model.Id > 0)
            {
                _url = url + "/amcprovider/updateamcprovider" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<AMCProviderModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            else
            {
                _url = url + "/amcprovider/createamcprovider" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<AMCProviderModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false });
            else
                return Json(new { success = true });
        }
    }
}
