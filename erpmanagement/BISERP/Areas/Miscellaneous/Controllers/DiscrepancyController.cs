using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using BISERP.Areas.Miscellaneous.Models;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace BISERP.Areas.Miscellaneous.Controllers
{
    public class DiscrepancyController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(DiscrepancyController));

        public DiscrepancyController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveDiscrepancy(DiscrepancyModel entity)
        {
            string strErrorMsg = "";
            bool _isSuccess = false;            
            foreach (var details in entity.Discrepancydetails)
            {
                if (details != null)
                {
                    if (details.PhysicalQty == null)
                    {
                        strErrorMsg = "Please Enter Physical Stock for Item.";
                        break;
                    }
                    if (details.Reason == null)
                    {
                        strErrorMsg = "Please Enter Reason for Item.";
                        break;
                    }
                    
                }
            }
            entity.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            entity.InsertedON = System.DateTime.Now;
            entity.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            entity.InsertedMacID = BISERP.Common.Constants.MacId;
            entity.InsertedMacName = BISERP.Common.Constants.MacName;            
            if (strErrorMsg != "")
            {
                _isSuccess = false;
                return Json(new { success = false, Message = strErrorMsg }, JsonRequestBehavior.AllowGet);
            }
            {
                string _url = url + "/dcr/createdcr" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, entity, new JsonMediaTypeFormatter()).Result;

                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    entity = JsonConvert.DeserializeObject<DiscrepancyModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    _isSuccess = true;
                    entity = JsonConvert.DeserializeObject<DiscrepancyModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)

                return Json(new { success = false, Message = strErrorMsg });
            else
                return Json(new { success = true, Message = entity.DiscrepancyNo });
        }

        [HttpGet]
        public async Task<JsonResult> DiscrepancyList()
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/dcr/getalldiscrepancy/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                List<DiscrepancyModel> records = await Common.AsyncWebCalls.GetAsync<List<DiscrepancyModel>>(client, _url, CancellationToken.None);

                if (records != null && records.Count > 0)
                {
                    jResult = Json( records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in DiscrepancyList :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> DiscrepancyDetails(int DiscrepancyId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/dcr/discrepancydt/" + DiscrepancyId.ToString() + Common.Constants.JsonTypeResult;
                List<DiscrepancyDtModel> records = await Common.AsyncWebCalls.GetAsync<List<DiscrepancyDtModel>>(client, _url, CancellationToken.None);

                if (records != null && records.Count > 0)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in DiscrepancyDetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
