using BISERP.Areas.Masters.Models;
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
    public class ScopeOfSupplyMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ScopeOfSupplyMasterController));

        public ScopeOfSupplyMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpGet]
        public async Task<JsonResult> AllScopeOfSupply(string searchScopeOfSupply, string searchCode)
        {
            List<ScopeOfSupplyMasterModel> Pimodel = new List<ScopeOfSupplyMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/ScopeOfSupply/getAllScopeOfSupply" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<ScopeOfSupplyMasterModel>>(client, _url, CancellationToken.None);

                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchScopeOfSupply))
                    {
                        records = records.Where(p => p.ScopeOfSupplyDesc.ToUpper().StartsWith(searchScopeOfSupply.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(searchCode))
                    {
                        records = records.Where(p => p.ScopeOfSupplyName.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                    }
                    int total = records.Count;

                    jResult = Json(new { success = true, records, total }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Details found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllScopeOfSupply :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Server Error! Please Contact administrator!");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveScopeOfSupply(ScopeOfSupplyMasterModel ScopeOfSupply)
        {
            string _url = "";
            bool _isSuccess = true;
            if (ScopeOfSupply.ScopeOfSupplyID > 0)
            {
                _url = url + "/ScopeOfSupply/updateScopeOfSupply" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, ScopeOfSupply, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    ScopeOfSupply = JsonConvert.DeserializeObject<ScopeOfSupplyMasterModel>(result.Content.ReadAsStringAsync().Result);
                }

                if (!_isSuccess)
                    return Json(new { success = false, Message = ScopeOfSupply.Message });
                else
                    return Json(new { success = true });
            }
            else
            {
                _url = url + "/ScopeOfSupply/createScopeOfSupply" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, ScopeOfSupply, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    ScopeOfSupply = JsonConvert.DeserializeObject<ScopeOfSupplyMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = ScopeOfSupply.Message });
            else
                return Json(new { success = true });
        }
    }
}
