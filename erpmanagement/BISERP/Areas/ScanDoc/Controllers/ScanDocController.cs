using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using BISERP.Areas.ScanDoc.Models;
using System.Threading;
using BISERP.Filters;

namespace BISERP.Areas.ScanDoc.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class ScanDocController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        string ScandocUrl = BISERP.Common.Constants.ScandocUrl;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ScanDocController));

        public ScanDocController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetScanDocUrl(int FileId, int ScanDocSubTypeId)
        {
            List<ScanDocModel> records = new List<ScanDocModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/scandoc/getscandoc/" + FileId + "/" + ScanDocSubTypeId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ScanDocModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetScanDocUrl :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpPost]
        public async Task<JsonResult> DeleteScanDocfile(int fileId, int scanDocId)
        {
            List<ScanDocModel> records = new List<ScanDocModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/scandoc/deleteScanDocfile/" + fileId + "/" + scanDocId + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, records, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in DeleteScanDocfile :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
