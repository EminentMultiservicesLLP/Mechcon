using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 using BISERP.Areas.Masters.Models;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using BISERP.Filters;

namespace BISERP.Areas.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class ItemGroupMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ItemGroupMasterController));

        public ItemGroupMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllItemGroups(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/ItemGroups/GetAllItemGroup" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<ItemGroupMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchString))
                    {
                        records = records.Where(p => p.Name.ToUpper().StartsWith(searchString.ToUpper())).ToList();
                    }


                    if (page.HasValue && limit.HasValue)
                    {
                        int start = (page.Value - 1) * limit.Value;
                        records = records.Skip(start).Take(limit.Value).ToList();
                    }
                    jResult = Json(new { records, records.Count }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllItemGroups :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
