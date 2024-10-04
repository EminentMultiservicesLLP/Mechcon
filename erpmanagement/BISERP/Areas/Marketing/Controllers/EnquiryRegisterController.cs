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
    public class EnquiryRegisterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(EnquiryRegisterController));

        public EnquiryRegisterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetSource()
        {
            try
            {
                string _url = $"{url}/enquiryRegister/getSource{Common.Constants.JsonTypeResult}";
                List<SourceModel> sources = await Common.AsyncWebCalls.GetAsync<List<SourceModel>>(client, _url, CancellationToken.None);

                if (sources == null || !sources.Any())
                {
                    return Json(new { error = "No sources found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(sources, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetSource method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the data." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetProducts()
        {
            try
            {
                string _url = $"{url}/enquiryRegister/getProducts{Common.Constants.JsonTypeResult}";
                List<ProductModel> products = await Common.AsyncWebCalls.GetAsync<List<ProductModel>>(client, _url, CancellationToken.None);

                if (products == null || !products.Any())
                {
                    return Json(new { error = "No products found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(products, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetProducts method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the products." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetLocations()
        {
            try
            {
                string _url = $"{url}/enquiryRegister/getLocations{Common.Constants.JsonTypeResult}";
                List<LocationModel> locations = await Common.AsyncWebCalls.GetAsync<List<LocationModel>>(client, _url, CancellationToken.None);

                if (locations == null || !locations.Any())
                {
                    return Json(new { error = "No locations found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(locations, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetLocations method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the locations." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetTypes()
        {
            try
            {
                string _url = $"{url}/enquiryRegister/getTypes{Common.Constants.JsonTypeResult}";
                List<TypeModel> types = await Common.AsyncWebCalls.GetAsync<List<TypeModel>>(client, _url, CancellationToken.None);

                if (types == null || !types.Any())
                {
                    return Json(new { error = "No types found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(types, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetTypes method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the types." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetSectors()
        {
            try
            {
                string _url = $"{url}/enquiryRegister/getSectors{Common.Constants.JsonTypeResult}";
                List<SectorModel> sectors = await Common.AsyncWebCalls.GetAsync<List<SectorModel>>(client, _url, CancellationToken.None);

                if (sectors == null || !sectors.Any())
                {
                    return Json(new { error = "No sectors found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(sectors, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetSectors method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the sectors." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetZones()
        {
            try
            {
                string _url = $"{url}/enquiryRegister/getZones{Common.Constants.JsonTypeResult}";
                List<ZoneModel> zones = await Common.AsyncWebCalls.GetAsync<List<ZoneModel>>(client, _url, CancellationToken.None);

                if (zones == null || !zones.Any())
                {
                    return Json(new { error = "No zones found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(zones, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetZones method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the zones." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetStatus()
        {
            try
            {
                string _url = $"{url}/enquiryRegister/getStatus{Common.Constants.JsonTypeResult}";
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
        public ActionResult SaveEnquiry(EnquiryRegisterModel model)
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
                model.UpdatedOn = System.DateTime.Now;
                model.UpdatedMacID = BISERP.Common.Constants.MacId;
                model.UpdatedMacName = BISERP.Common.Constants.MacName;
                model.UpdatedIPAddress = BISERP.Common.Constants.IpAddress;

                _url = url + "/enquiryRegister/saveEnquiry" + Common.Constants.JsonTypeResult;

                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _isSuccess = false;
                    model = JsonConvert.DeserializeObject<EnquiryRegisterModel>(result.Content.ReadAsStringAsync().Result);
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    _isSuccess = true;
                    model = JsonConvert.DeserializeObject<EnquiryRegisterModel>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    _isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _isSuccess = false;
                _logger.LogError("Error in SaveEnquiry method:" + Environment.NewLine + ex.ToString());
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
        public async Task<JsonResult> GetEnquiry(int? statusID)
        {
            string _url = $"{url}/enquiryRegister/getEnquiry/{statusID}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<EnquiryRegisterModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetEnquiry: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetEnquiryDetails(int enquiryId)
        {
            string _url = $"{url}/enquiryRegister/getEnquiryDetails/{enquiryId}{Common.Constants.JsonTypeResult}";

            try
            {
                var record = await Common.AsyncWebCalls.GetAsync<EnquiryRegisterModel>(client, _url, CancellationToken.None);

                if (record == null)
                {
                    return Json(new { success = false, message = "No details found for the specified enquiry" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, record }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetEnquiryDetails: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry details" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetEnqRegFollowUp(int? enquiryId)
        {
            string _url = $"{url}/enquiryRegister/getEnqRegFollowUp/{enquiryId}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<EnqRegFollowUpDetails>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetEnqRegFollowUp: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving enquiry" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
