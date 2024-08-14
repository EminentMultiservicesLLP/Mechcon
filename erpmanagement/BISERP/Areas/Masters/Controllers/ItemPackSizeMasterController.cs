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
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using BISERP.Filters;

namespace BISERP.Areas.Store.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class ItemPackSizeMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ItemPackSizeMasterController));

        public ItemPackSizeMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        //[HttpGet]
        //public async Task<JsonResult> AllItemPackSizes()
        //{
        //    JsonResult jResult;
        //    try
        //    {
        //        string _url = url + "/ipacksizes/getallitemtypesizes" + Common.Constants.JsonTypeResult;
        //        var records = await Common.AsyncWebCalls.GetAsync<List<ItemPackSizeMasterModel>>(client, _url, CancellationToken.None);
        //        if (records != null && records.Count > 0)
        //        {
        //            jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //            jResult = Json("Error");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error in AllItemPackSizes :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
        //        jResult = Json("Error");
        //    }
        //    return jResult;
        //}
        [HttpGet]
        public async Task<JsonResult> AllItemPackSizes( string SearchName, string Searchcode)
        {
            List<ItemPackSizeMasterModel> pimodel = new List<ItemPackSizeMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/ipacksizes/getallitemtypesizes" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<ItemPackSizeMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.Name.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(Searchcode))
                    {
                        records = records.Where(p => p.Code.ToUpper().StartsWith(Searchcode.ToUpper())).ToList();
                    }
                    int total = records.Count;
                   
                    jResult = Json(new {success=true, records, total }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new {success=false,Massage="No Pack Size Found"}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllItemPackSizes :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Server Error! Please Contact administrator!");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SavePackSize(ItemPackSizeMasterModel itemPackSize)
        {
            string _url = "";
            bool _isSuccess = true;
            itemPackSize.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            itemPackSize.InsertedON = System.DateTime.Now;
            itemPackSize.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            itemPackSize.InsertedMacID = BISERP.Common.Constants.MacId;
            itemPackSize.InsertedMacName = BISERP.Common.Constants.MacName;
            if (itemPackSize.ID > 0)
            {
                _url = url + "/ipacksizes/updateitempacksize" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, itemPackSize, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    itemPackSize = JsonConvert.DeserializeObject<ItemPackSizeMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            else
            {
                _url = url + "/ipacksizes/createitempacksize" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, itemPackSize, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    itemPackSize = JsonConvert.DeserializeObject<ItemPackSizeMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            //return PartialView();
            if (!_isSuccess)
                return Json(new { success = false, Message = itemPackSize.Message });
            else
                return Json(new { success = true });
        }
    }
}
