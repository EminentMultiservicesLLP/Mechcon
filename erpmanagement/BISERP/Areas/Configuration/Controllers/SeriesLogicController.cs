using BISERP.Areas.Configuration.Models;
using BISERP.Filters;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.Configuration.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class SeriesLogicController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(SeriesLogicController));

        public SeriesLogicController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetSeriesLogic()
        {
            JsonResult jResult;
            List<SeriesLogicModel> records = new List<SeriesLogicModel>();
            try
            {
                string _url = url + "/configuration/getSeriesLogic" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<SeriesLogicModel>>(client, _url, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetSeriesLogic :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }

}
