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
    public class ItemTypeMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ItemTypeMasterController));

        public ItemTypeMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpGet]
        public async Task<JsonResult> AllItemTypes(string ItemTypeCode,string ItemTypeName)
        {
            List<ItemTypeMasterModel> records = new List<ItemTypeMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/itemtype/getallitemtypes" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ItemTypeMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(ItemTypeCode))
                    {
                        records = records.Where(p => p.Code.ToUpper().Contains(ItemTypeCode.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(ItemTypeName))
                    {
                        records = records.Where(p => p.Name.ToUpper().Contains(ItemTypeName.ToUpper())).ToList();
                    }
                    //jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Record found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllItemTypes :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Server Error! Please Contact administrator!");
            }
            return jResult;
        }
        //[HttpGet]
        //public async Task<JsonResult> AllItemTypes(int? page, int? limit, string sortBy, string direction, string searchName, string searchCode)
        //{
        //    JsonResult jResult;
        //    try
        //    {
        //        string _url = url + "/itemtype/getallitemtypes" + Common.Constants.JsonTypeResult;
        //        var records = await Common.AsyncWebCalls.GetAsync<List<ItemTypeMasterModel>>(client, _url, CancellationToken.None);
        //        if (records != null && records.Count > 0)
        //        {
        //            if (!string.IsNullOrWhiteSpace(searchName))
        //            {
        //                records = records.Where(p => p.Name.ToUpper().StartsWith(searchName.ToUpper())).ToList();
        //            }
        //            if (!string.IsNullOrWhiteSpace(searchCode))
        //            {
        //                records = records.Where(p => p.Code.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
        //            }
        //            int total = records.Count;
        //            if (page.HasValue && limit.HasValue)
        //            {
        //                int start = (page.Value - 1) * limit.Value;
        //                records = records.Skip(start).Take(limit.Value).ToList();
        //            }
        //            jResult = Json(new { records, total }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //            jResult = Json("Error");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error in AllItemTypes :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
        //        jResult = Json("Error");
        //    }
        //    return jResult;
        //}
       

        [HttpPost]
        public ActionResult SaveItemType(ItemTypeMasterModel itype)
        {
            string _url = "";
            bool _isSuccess = true;
            itype.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            itype.InsertedON = System.DateTime.Now;
            itype.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            itype.InsertedMacID = BISERP.Common.Constants.MacId;
            itype.InsertedMacName = BISERP.Common.Constants.MacName;
            if (itype.ID > 0)
            {
                _url = url + "/itemtype/updateitype" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, itype, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    itype = JsonConvert.DeserializeObject<ItemTypeMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
           
            }
            else
            {
                _url = url + "/itemtype/createitype" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, itype, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    itype = JsonConvert.DeserializeObject<ItemTypeMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = itype.Message });
            else
                return Json(new { success = true });

        }
    }
}
