using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERP.Models.Master;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Formatting;

namespace BISERP.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class BranchMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(BranchMasterController));

        public BranchMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllBranch(string SearchName, string SearchCode)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/branch/getallbranch" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<BranchModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    //if (!string.IsNullOrWhiteSpace(SearchName))
                    //{
                    //    records = records.Where(p => p.Name.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    //}
                    //if (!string.IsNullOrWhiteSpace(SearchCode))
                    //{
                    //    records = records.Where(p => p.Code.ToUpper().StartsWith(SearchCode.ToUpper())).ToList();
                    //}
                    //int total = records.Count;
                    //if (page.HasValue && limit.HasValue)
                    //{
                    //    int start = (page.Value - 1) * limit.Value;
                    //    records = records.Skip(start).Take(limit.Value).ToList();
                    //}
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllSuppliers :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveBranch(BranchModel entity)
        {

            if (entity.BranchID > 0)
            {
                string _url = "";
                entity.UpdatedBy = 1;
                entity.UpdatedOn = System.DateTime.Now;
                entity.UpdatedIPAddress = BISERP.Common.Constants.IpAddress;
                entity.UpdatedMacID = BISERP.Common.Constants.MacId;
                entity.UpdatedMacName = BISERP.Common.Constants.MacName;
                _url = url + "/branch/Updatebranch" + Common.Constants.JsonTypeResult;
                client.PutAsync(_url, entity, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<int>().Result;
            }
            else
            {
                string _url = "";
                entity.InsertedBy = 1;
                entity.InsertedON = System.DateTime.Now;
                entity.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                entity.InsertedMacID = BISERP.Common.Constants.MacId;
                entity.InsertedMacName = BISERP.Common.Constants.MacName;
                _url = url + "/branch/Createbranch" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, entity, new JsonMediaTypeFormatter()).Result.Content.ReadAsAsync<BranchModel>().Id;
            }
            //return PartialView();
            return Json(new { success = true });
        }

        //[HttpGet]
        //public async Task<JsonResult> ActiveSupplier(int? page, int? limit, string sortBy, string direction, string SearchName, string SearchCode)
        //{
        //    JsonResult jResult;
        //    try
        //    {
        //        string _url = url + "/supplier/getactivesupplier" + Common.Constants.JsonTypeResult;
        //        var records = await Common.AsyncWebCalls.GetAsync<List<SupplierMasterModel>>(client, _url, CancellationToken.None);
        //        if (records != null && records.Count > 0)
        //        {
        //            if (!string.IsNullOrWhiteSpace(SearchName))
        //            {
        //                records = records.Where(p => p.Name.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
        //            }
        //            if (!string.IsNullOrWhiteSpace(SearchCode))
        //            {
        //                records = records.Where(p => p.Code.ToUpper().StartsWith(SearchCode.ToUpper())).ToList();
        //            }
        //            if (page.HasValue && limit.HasValue)
        //            {
        //                int start = (page.Value - 1) * limit.Value;
        //                records = records.Skip(start).Take(limit.Value).ToList();
        //            }
        //            jResult = Json(new { records, records.Count }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //            jResult = Json("Error");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error in AllSuppliers :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
        //        jResult = Json("Error");
        //    }
        //    return jResult;
        //}
    }
}
