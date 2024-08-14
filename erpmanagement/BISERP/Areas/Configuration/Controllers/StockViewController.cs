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
    public class StockViewController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(StockViewController));

        public StockViewController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<ActionResult> GetStockDetails(int StoreID)
        {
            List<StockViewModel> records = new List<StockViewModel>();
            try
            {
                string apiUrl = $"{url}/configuration/getStockDetails/{StoreID}{Common.Constants.JsonTypeResult}";
                records = await Common.AsyncWebCalls.GetAsync<List<StockViewModel>>(client, apiUrl, CancellationToken.None);
                return Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetStockDetails: {ex.Message}\n{ex.StackTrace}");
                return Json("Error");
            }
        }

    }

}
