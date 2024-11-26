using BISERP.Areas.Ticket.Models;
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
using static BISERP.Areas.Ticket.Models.TicketActivityModel;

namespace BISERP.Areas.Ticket.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class TicketActivityController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(TicketActivityController));

        public TicketActivityController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetTicketForActivity(int? TicketType)
        {
            int UserID = Convert.ToInt32(Session["AppUserId"].ToString());
            string _url = $"{url}/ticketActivity/getTicketForActivity/{UserID}/{TicketType}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TicketRegisterModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetTicketForActivity: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving ticket" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetActivityID(int TicketID)
        {
            int InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());

            string _url = $"{url}/ticketActivity/getActivityID/{TicketID}/{InsertedBy}{Common.Constants.JsonTypeResult}";

            try
            {
                var record = await Common.AsyncWebCalls.GetAsync<TicketActivityModel>(client, _url, CancellationToken.None);

                if (record == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, record }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetActivityID: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving ActivityID" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveActivity(ActivityListModel model)
        {
            string _url = "";
            bool _isSuccess = true;

            try
            {
                model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                model.InsertedOn = System.DateTime.Now;
                model.InsertedMacID = BISERP.Common.Constants.MacId;
                model.InsertedMacName = BISERP.Common.Constants.MacName;
                model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                model.UpdatedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                model.UpdatedOn = System.DateTime.Now;
                model.UpdatedMacID = BISERP.Common.Constants.MacId;
                model.UpdatedMacName = BISERP.Common.Constants.MacName;
                model.UpdatedIPAddress = BISERP.Common.Constants.IpAddress;

                _url = url + "/ticketActivity/saveActivity" + Common.Constants.JsonTypeResult;

                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<ActivityListModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    _isSuccess = true;
                    model = JsonConvert.DeserializeObject<ActivityListModel>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    _isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _isSuccess = false;
                _logger.LogError("Error in SaveActivity method:" + Environment.NewLine + ex.ToString());
            }
            if (!_isSuccess)
            {
                return Json(new { success = false, Message = "Error while saving Activity" });
            }
            else
            {
                return Json(new { success = true, Message = "Activity saved successfully", Data = model });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetActivity(int? ticketID)
        {
            string _url = $"{url}/ticketActivity/getActivity/{ticketID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TicketActivityModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetActivity: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving ticket" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
