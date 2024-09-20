using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Masters.Models;
using BISERP.Filters;
using BISERPCommon;
using Newtonsoft.Json;

namespace BISERP.Areas.Masters.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class ClientMasterController : Controller
    {

        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ClientMasterController));
        public ClientMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpPost]
        public ActionResult SaveClient(ClientMasterModel Client)
        {
            string _url = "";
            bool _isSuccess = true;
            Client.UpdatedBy = 1;
            Client.UpdatedOn = DateTime.Now;
            Client.UpdatedIPAddress = Common.Constants.IpAddress;
            Client.UpdatedMacID = Common.Constants.MacId;
            Client.UpdatedMacName = Common.Constants.MacName;
            Client.InsertedBy = 1;
            Client.InsertedON = DateTime.Now;
            Client.InsertedIPAddress = Common.Constants.IpAddress;
            Client.InsertedMacID = Common.Constants.MacId;
            Client.InsertedMacName = Common.Constants.MacName;
            if (Client.ClientId > 0)
            {

                _url = url + "/client/UpdateClient" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, Client, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                }
                Client = JsonConvert.DeserializeObject<ClientMasterModel>(result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                _url = url + "/client/CreateClient" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, Client, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                }
                Client = JsonConvert.DeserializeObject<ClientMasterModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = Client.Message });
            else
                return Json(new { success = true, data = Client });

        }

        [HttpGet]
        public async Task<JsonResult> AllClient(int? page, int? limit, string sortBy, string direction, string SearchName, string SearchCode)
        {

            JsonResult jResult;
            try
            {
                string _url = url + "/client/GetAllClient" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<ClientMasterModel>>(client, _url, CancellationToken.None);
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
                    if (page.HasValue && limit.HasValue)
                    {
                        int start = (page.Value - 1) * limit.Value;
                        records = records.Skip(start).Take(limit.Value).ToList();
                    }
                    jResult = Json(new { success = true, records, total }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Client Entry found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllClient :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json(new { success = false, Messsage = "please contact Administrator." }, JsonRequestBehavior.AllowGet);
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetClientConsignee(int clientId)
        {

            JsonResult jResult;
            try
            {
                string _url = url + "/client/GetClientConsignee" + "/" + clientId + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<ConsigneeModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Consignee Entry found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetClientConsignee :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json(new { success = false, Messsage = "please contact Administrator." }, JsonRequestBehavior.AllowGet);
            }
            return jResult;
        }

        public PartialViewResult ClientLink()
        {
            try
            {
                return PartialView("~/Areas/Masters/Views/Masters/ClientMaster.cshtml");
            }
            catch (Exception ex)
            {

                throw;
            }


        }
    }
}
