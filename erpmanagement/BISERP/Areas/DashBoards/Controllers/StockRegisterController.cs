using BISERP.Models;
using BISERP.Areas.Store.Models.Master;
using BISERP.Areas.DashBoards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;

namespace BISERP.Areas.DashBoard.Controllers
{
    public class StockRegisterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(StockRegisterController));


        public StockRegisterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetStoreWisestockData(int storeId, int itemTypeId, DateTime? FromDate, DateTime? Todate, string ItemName, int IssueTo)
        {
            JsonResult jResult;
            List<StockRegisterModel> records = new List<StockRegisterModel>();
            try
            {
                string strfromdate = Convert.ToDateTime(FromDate).ToString("MM-dd-yy");
                string strtodate = Convert.ToDateTime(Todate).ToString("MM-dd-yy");
                string _url = url + "/stockregister/getStorewiseStock/" + storeId.ToString() + "/" + itemTypeId.ToString() + "/" + strfromdate + "/" + strtodate + "/" + IssueTo + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<StockRegisterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(ItemName))
                    {
                        records = records.Where(p => p.ItemName.ToUpper().Contains(ItemName.ToUpper())).ToList();
                    }
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStoreWisestockData :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetStoreWiseItemstockData(int storeId, int itemTypeId, DateTime? FromDate, DateTime? Todate, string ItemName)
        {
            JsonResult jResult;
            List<StockRegisterModel> records = new List<StockRegisterModel>();

            try
            {
                string strfromdate = Convert.ToDateTime(FromDate).ToString("MM-dd-yy");
                string strtodate = Convert.ToDateTime(Todate).ToString("MM-dd-yy");
                string _url = url + "/stockregister/getStoreItemwiseStock/" + storeId.ToString() + "/" + itemTypeId.ToString() + "/" + strfromdate + "/" + strtodate  + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<StockRegisterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(ItemName))
                    {
                        records = records.Where(p => p.ItemName.ToUpper().Contains(ItemName.ToUpper())).ToList();
                    }
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetStoreWiseItemstockData :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }

            return jResult;

        }



    }
}
