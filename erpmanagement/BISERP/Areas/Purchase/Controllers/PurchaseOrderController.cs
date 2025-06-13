using BISERP.Area.Purchase.Models;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using BISERP.Filters;
using System.Net;

namespace BISERP.Area.Purchase.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class PurchaseOrderController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(PurchaseIndentController));

        public PurchaseOrderController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public JsonResult SavePurchaseOrder(PurchaseOrderModel po)
        {
            string errorMessage = "";
            string _url = "";
            bool isSuccess = true;
            string poNumber = "";

            try
            {
                po.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                po.InsertedON = DateTime.Now;
                po.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                po.InsertedMacID = BISERP.Common.Constants.MacId;
                po.InsertedMacName = BISERP.Common.Constants.MacName;

                if (po.ID > 0)
                {
                    _url = url + "/purchaseorder/Updatepo" + Common.Constants.JsonTypeResult;
                }
                else
                {
                    _url = url + "/purchaseorder/createpo" + Common.Constants.JsonTypeResult;
                }
                var result = client.PostAsync(_url, po, new JsonMediaTypeFormatter()).Result;

                if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    isSuccess = false;
                    if (po.ID == 0)
                    {
                        po = JsonConvert.DeserializeObject<PurchaseOrderModel>(result.Content.ReadAsStringAsync().Result);
                        errorMessage = po.Message;
                    }
                }
                else if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
                {
                    isSuccess = true;
                    if (po.ID == 0)
                    {
                        po = JsonConvert.DeserializeObject<PurchaseOrderModel>(result.Content.ReadAsStringAsync().Result);
                        poNumber = po.PONo;
                    }
                }

                return Json(new { success = isSuccess, Message = isSuccess ? poNumber : errorMessage });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<JsonResult> AllPurchaseOrder(string SearchNumber, DateTime? SearchDate)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/purchaseorder/getall" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<PurchaseOrderModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(SearchNumber))
                    {
                        records = records.Where(p => p.PONo.ToUpper().StartsWith(SearchNumber.ToUpper())).ToList();
                    }
                    if (SearchDate != null)
                    {
                        records = records.Where(p => p.PODate == SearchDate).ToList();
                    }
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllPurchaseOrder :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AuthorizedPurchaseOrder(int StoreId, string SearchNumber, DateTime? SearchDate)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/purchaseorder/getauthorized/" + StoreId.ToString() + Common.Constants.JsonTypeResult;
                List<PurchaseOrderModel> records = await Common.AsyncWebCalls.GetAsync<List<PurchaseOrderModel>>(client, _url, CancellationToken.None);

                if (records != null & records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(SearchNumber))
                    {
                        records = records.Where(p => p.PONo.ToUpper().StartsWith(SearchNumber.ToUpper())).ToList();
                    }
                    if (SearchDate != null)
                    {
                        records = records.Where(p => p.PODate == SearchDate).ToList();
                    }
                    //jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    //jResult = Json("Error");
                    jResult = Json(new { success = false, Messsage = "Purchase Order Not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AuthorizedPurchaseOrder :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> PurchaseOrderdetails(int POId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/purchaseorder/getbypoid/" + POId.ToString() + Common.Constants.JsonTypeResult;
                PurchaseOrderModel _porder = await Common.AsyncWebCalls.GetAsync<PurchaseOrderModel>(client, _url, CancellationToken.None);

                if (_porder != null)
                {
                    jResult = Json(_porder, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PurchaseOrderdetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> POdetails(int POId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/purchaseorder/getdetailsbypoid/" + POId.ToString() + Common.Constants.JsonTypeResult;
                List<PurchaseOrderDetailModel> _porder = await Common.AsyncWebCalls.GetAsync<List<PurchaseOrderDetailModel>>(client, _url, CancellationToken.None);

                if (_porder != null)
                {
                    jResult = Json(_porder, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in POdetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetPOForAuthorization(int? page, int? limit, string sortBy, string direction, int? StoreId, int? AgainstId)
        {
            JsonResult jResult;
            try
            {
                if (StoreId == null)
                    StoreId = 0;
                if (AgainstId == null)
                {

                    List<PurchaseOrderModel> records = await Common.AsyncWebCalls.GetAsync<List<PurchaseOrderModel>>("/purchaseorder/getpoforauthorization/" + StoreId.ToString(), CancellationToken.None);
                    if (records != null & records.Count > 0)
                    {
                        //jResult = Json(records, JsonRequestBehavior.AllowGet);
                        jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    List<PurchaseOrderModel> records = await Common.AsyncWebCalls.GetAsync<List<PurchaseOrderModel>>("/purchaseorder/getpoforauthorization/" + StoreId.ToString() + "/" + AgainstId, CancellationToken.None);
                    //jResult = Json(records, JsonRequestBehavior.AllowGet);
                    if (records != null & records.Count > 0)
                    {
                        //jResult = Json(records, JsonRequestBehavior.AllowGet);
                        jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jResult = Json(new { success = false, Messsage = "No Purchase Order found" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPOForAuthorization :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetUnAuthorizationPo(int? page, int? limit, string sortBy, string direction, int? StoreId, int? AgainstId)
        {
            JsonResult jResult = null;
            try
            {
                if (StoreId == null)
                    StoreId = 0;
                List<PurchaseOrderModel> records = await Common.AsyncWebCalls.GetAsync<List<PurchaseOrderModel>>("/purchaseorder/GetUnAuthorizationPo/" + StoreId.ToString(), CancellationToken.None);
                if (records != null & records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPOForAuthorization :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllAuthorizedPurchaseOrders()
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/purchaseorder/getauthorized/" + Common.Constants.JsonTypeResult;
                List<PurchaseOrderModel> records = await Common.AsyncWebCalls.GetAsync<List<PurchaseOrderModel>>(client, _url, CancellationToken.None);

                if (records != null)
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AuthorizedPurchaseOrder :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public JsonResult AuthCancelPurchaseOrder(PurchaseOrderModel pomodel)
        {
            if (pomodel == null)
            {
                return Json(new { success = false, ResponseText = "No record selected." });
            }

            try
            {
                pomodel.InsertedBy = Convert.ToInt32(Session["AppUserId"]);
                pomodel.InsertedON = DateTime.Now;
                pomodel.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                pomodel.InsertedMacID = BISERP.Common.Constants.MacId;
                pomodel.InsertedMacName = BISERP.Common.Constants.MacName;

                string _url = $"{url}/purchaseorder/authcancelpo{Common.Constants.JsonTypeResult}";

                var result = client.PostAsync(_url, pomodel, new JsonMediaTypeFormatter()).Result;

                if (result.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, ResponseText = "An error occurred while processing your request." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ResponseText = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult SavePoAmendment(PurchaseOrderModel po)
        {
            string _url = "";

            po.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            po.InsertedON = System.DateTime.Now;
            po.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            po.InsertedMacID = BISERP.Common.Constants.MacId;
            po.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/purchaseorder/SavePoAmendment/" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, po, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<PurchaseOrderModel>().Id;
            return Json(new { success = true });
        }

        private void prepareReport(LocalReport report, string reportFilePath, ReportDataSource data)
        {
            report.ReportPath = reportFilePath;
            report.DataSources.Add(data);
        }

        private void GenerateFile(string fileType, LocalReport report, string savePath)
        {
            //byte[] bytes = report.Render(fileType);
            //FileStream fs = new FileStream(savePath, FileMode.Create);
            //fs.Write(bytes, 0, bytes.Length);
            //fs.Close();
            Byte[] bytesPdf = report.Render(fileType);
            if (bytesPdf != null)
            {
                switch (fileType)
                {
                    case "PDF":
                        Response.AddHeader("content-disposition", "attachment;filename= PO.pdf");
                        Response.ContentType = "application/octectstream";
                        break;
                    case "Excel":
                        Response.AddHeader("content-disposition", "attachment;filename= PO.xlsx");
                        Response.ContentType = "application/vnd.ms-excel";
                        break;
                    default:
                        Response.AddHeader("content-disposition", "attachment;filename= PO.doc");
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        break;
                }
            }

            Response.BinaryWrite(bytesPdf);
            Response.End();
        }

        public async Task<JsonResult> ExportPO(int poid, string poType)
        {
            BISERP.Views.Shared.ReportViewer rptViewr = new BISERP.Views.Shared.ReportViewer();
            rptViewr.PurchaseRpt(poid, poType, "PDF");
            return Json(new { success = true });
        }
        [HttpGet]
        public async Task<JsonResult> PurchaseOrderForGrn(int storeId)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/purchaseorder/getbypoidforgrn/" + storeId.ToString() + Common.Constants.JsonTypeResult;
                List<PurchaseOrderModel> records = await Common.AsyncWebCalls.GetAsync<List<PurchaseOrderModel>>(client, _url, CancellationToken.None);

                if (records != null & records.Count > 0)
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                else
                    jResult = Json(new { success = false, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PurchaseOrderForGrn :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public JsonResult PurchaseOrderAmendment(List<PurchaseOrderDetailModel> pomodel)
        {
            string _url = "";
            if (pomodel == null)
            {
                return Json(new { success = false, ResponseText = "No record selected." });
            }
            else
            {
                foreach (var model in pomodel)
                {
                    if (model.ID > 0)
                    {

                        model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                        model.InsertedON = System.DateTime.Now;
                        model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                        model.InsertedMacID = BISERP.Common.Constants.MacId;
                        model.InsertedMacName = BISERP.Common.Constants.MacName;

                    }
                }
                _url = url + "/purchaseorder/PurchaseOrderAmendment" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, pomodel, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<PurchaseIndentModel>().Id;
                return Json(new { success = true });
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetPoForAmmendmet(int PoId)
        {
            JsonResult jResult = null;
            try
            {

                List<PurchaseOrderModel> records = await Common.AsyncWebCalls.GetAsync<List<PurchaseOrderModel>>("/purchaseorder/GetPoForAmmendmet/" + PoId, CancellationToken.None);
                if (records != null & records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jResult = Json(new { success = false, Messsage = "No Items found" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPOForAuthorization :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public JsonResult PurchaseOrderClose(int PoId)
        {
            string _url = "";
            PurchaseOrderModel model = new PurchaseOrderModel();
            model.ID = PoId;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString()); ;
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            try
            {
                _url = url + "/purchaseorder/purchaseorderClose" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
                return Json(new { success = true });
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<JsonResult> PurchaseOrderForRpt(string SearchNumber, DateTime? SearchDate)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/purchaseorder/PurchaseOrderForRpt" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<PurchaseOrderModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                    jResult.MaxJsonLength = int.MaxValue;  // Increase max JSON length here
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PurchaseOrderForRpt :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }


        [HttpGet]
        public async Task<JsonResult> GetPOStateDetails(int? POID)
        {
            try
            {
                string _url = $"{url}/purchaseorder/getPOStateDetails/{POID}{Common.Constants.JsonTypeResult}";
                List<POStateDetailsModel> data = await Common.AsyncWebCalls.GetAsync<List<POStateDetailsModel>>(client, _url, CancellationToken.None);

                if (data == null || !data.Any())
                {
                    return Json(new { error = "No data found." }, JsonRequestBehavior.AllowGet);
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPOStateDetails method:" + Environment.NewLine + ex.ToString());
                return Json(new { error = "An error occurred while retrieving the data." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetSupplierDeliveryReport(int? supplierID, string FromDate = null, string ToDate = null)
        {
            string _url = $"{url}/purchaseorder/getSupplierDeliveryReport/{supplierID}/{FromDate}/{ToDate}{Common.Constants.JsonTypeResult}";

            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<SupplierDeliveryReportModel>>(client, _url, CancellationToken.None);

                if (records == null)
                {
                    return Json(new { success = false, message = "No records found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetSupplierDeliveryReport: {ex.Message} {Environment.NewLine} {ex.StackTrace}");
                return Json(new { success = false, message = "An error occurred while retrieving Details" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
