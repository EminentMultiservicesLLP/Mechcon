using BISERP.Areas.Marketing.Models;
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

namespace BISERP.Areas.Marketing.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class SM_WorkOrderController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(SM_WorkOrderController));

        public SM_WorkOrderController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetEnqForWorkOrder()
        {
            int UserID = Convert.ToInt32(Session["AppUserId"].ToString());
            string _url = $"{url}/sm_WorkOrder/getEnqForWorkOrder/{UserID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<WorkOrderModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetEnqForWorkOrder: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
