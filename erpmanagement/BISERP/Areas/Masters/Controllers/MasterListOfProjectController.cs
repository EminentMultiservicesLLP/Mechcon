using BISERP.Areas.Masters.Models;
using BISERP.Filters;
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

namespace BISERP.Areas.Masters.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class MasterListOfProjectController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(MasterListOfProjectController));

        public MasterListOfProjectController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetMasterListOfProject(int financialYearID, int pending)
        {
            List<MasterListOfProjectModel> records = new List<MasterListOfProjectModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/MasterListOfProject/getMasterListOfProject/" + financialYearID + '/' + pending + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<MasterListOfProjectModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Clearance Note Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetMasterListOfProject:" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }


        [HttpGet]
        public async Task<JsonResult> GetProjectCostingSummary(int financialYearID)
        {
            List<ProjectCostingSummaryModel> records = new List<ProjectCostingSummaryModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/MasterListOfProject/getProjectCostingSummary/" + financialYearID + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ProjectCostingSummaryModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Project Costing Summary Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetProjectCostingSummary:" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
