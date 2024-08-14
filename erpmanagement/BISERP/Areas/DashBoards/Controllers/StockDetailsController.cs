using BISERP.Models;
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
    public class StockDetailsController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(StockDetailsController));


        public StockDetailsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();

        }

        [HttpGet]
        public async Task<JsonResult> GetStoreWisestockData(int storeId, int itemTypeId, int BranchId, string Itemcode, string ItemName, string Batch)
        {
            JsonResult jResult;
            List<StockDetailsEntity> records = new List<StockDetailsEntity>();
            try
            {
                string _url = url + "/stockdetails/getStockDetails/" + storeId.ToString() + "/" + itemTypeId.ToString() + "/" + BranchId.ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<StockDetailsEntity>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(Itemcode))
                    {
                        records = records.Where(p => p.Itemcode.ToUpper().Contains(Itemcode.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(ItemName))
                    {
                        records = records.Where(p => p.ItemName.ToUpper().Contains(ItemName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(Batch))
                    {
                        records = records.Where(p => p.Batch.ToUpper().Contains(Batch.ToUpper())).ToList();
                    }
                   // records = records.OrderByDescending(m => m.ItemID).ToList();
                    jResult = Json(new{records}, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in All GetStoreWisestockData :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
