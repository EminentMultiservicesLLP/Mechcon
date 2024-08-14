using BISERP.Areas.DashBoards.Models;
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
using BISERP.Areas.DashBoard.Controllers;


namespace BISERP.Areas.DashBoard.Controllers
{
    public class DashBoardController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(DashBoardController));

        public DashBoardController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetStockSummary(int byValue)
        {
            JsonResult jResult;
            List<StoreDSBDStockSummaryEntity> result = new List<StoreDSBDStockSummaryEntity>();
            try
            {
                string _url = url + "/dashboard/getstocksummary/" + byValue + Common.Constants.JsonTypeResult;
                result = await Common.AsyncWebCalls.GetAsync<List<StoreDSBDStockSummaryEntity>>(client, _url, CancellationToken.None);
                if (result != null)
                {
                    jResult = Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStockSummary :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
