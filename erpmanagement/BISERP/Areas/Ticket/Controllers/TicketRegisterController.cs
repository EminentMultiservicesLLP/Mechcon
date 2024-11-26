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

namespace BISERP.Areas.Ticket.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class TicketRegisterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(TicketRegisterController));

        public TicketRegisterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetProject(int? ClientID)
        {
            string _url = $"{url}/ticketRegister/getProject/{ClientID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<ProjectModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetProject: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving Project" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetPriority()
        {
            try
            {
                string _url = $"{url}/ticketRegister/getPriority{Common.Constants.JsonTypeResult}";
                List<PriorityModel> status = await Common.AsyncWebCalls.GetAsync<List<PriorityModel>>(client, _url, CancellationToken.None);

                if (status == null || !status.Any())
                {
                    return Json(new { error = "No statuses found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPriority method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the Priorities." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetStatus()
        {
            try
            {
                string _url = $"{url}/ticketRegister/getStatus{Common.Constants.JsonTypeResult}";
                List<StatusModel> status = await Common.AsyncWebCalls.GetAsync<List<StatusModel>>(client, _url, CancellationToken.None);

                if (status == null || !status.Any())
                {
                    return Json(new { error = "No statuses found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStatus method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the statuses." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveTicket(TicketRegisterModel model)
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

                _url = url + "/ticketRegister/saveTicket" + Common.Constants.JsonTypeResult;

                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<TicketRegisterModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    _isSuccess = true;
                    model = JsonConvert.DeserializeObject<TicketRegisterModel>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    _isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _isSuccess = false;
                _logger.LogError("Error in SaveTicket method:" + Environment.NewLine + ex.ToString());
            }
            if (!_isSuccess)
            {
                return Json(new { success = false, Message = "Error while saving Ticket" });
            }
            else
            {
                return Json(new { success = true, Message = "Ticket saved successfully", Data = model });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetTicket(int? statusID)
        {
            string _url = $"{url}/ticketRegister/getTicket/{statusID}{Common.Constants.JsonTypeResult}";

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
                _logger.LogError($"Error in GetTicket: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving ticket" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
