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
    public class ResourceTargetController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ResourceTargetController));

        public ResourceTargetController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetFinancialYear()
        {
            try
            {
                string _url = $"{url}/resourceTarget/getFinancialYear{Common.Constants.JsonTypeResult}";
                List<FinancialYearModel> data = await Common.AsyncWebCalls.GetAsync<List<FinancialYearModel>>(client, _url, CancellationToken.None);

                if (data == null || !data.Any())
                {
                    return Json(new { error = "No data found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetFinancialYear method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the data." }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult SaveResourceTargetDetail(ResourceTargetModel model)
        {
            string _url = "";
            bool _isSuccess = true;

            try
            {
                model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                model.InsertedON = System.DateTime.Now;
                model.InsertedMacID = BISERP.Common.Constants.MacId;
                model.InsertedMacName = BISERP.Common.Constants.MacName;
                model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                model.UpdatedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                model.UpdatedON = System.DateTime.Now;
                model.UpdatedMacID = BISERP.Common.Constants.MacId;
                model.UpdatedMacName = BISERP.Common.Constants.MacName;
                model.UpdatedIPAddress = BISERP.Common.Constants.IpAddress;

                _url = url + "/orderBook/saveResourceTargetDetail" + Common.Constants.JsonTypeResult;

                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<ResourceTargetModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    _isSuccess = true;
                    model = JsonConvert.DeserializeObject<ResourceTargetModel>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    _isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _isSuccess = false;
                _logger.LogError("Error in SaveResourceTargetDetail method:" + Environment.NewLine + ex.ToString());
            }
            if (!_isSuccess)
            {
                return Json(new { success = false, Message = "Error while saving Enquiry" });
            }
            else
            {
                return Json(new { success = true, Message = "Enquiry saved successfully", Data = model });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetResourceTargetDetail(int financialYearID)
        {
            string _url = $"{url}/resourceTarget/getResourceTargetDetail/{financialYearID}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<ResourceTargetDetailsModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No ResourceTarget details found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetResourceTargetDetail: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving GetResourceTargetDetail" }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
