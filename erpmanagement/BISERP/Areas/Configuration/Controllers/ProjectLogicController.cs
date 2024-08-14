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
    public class ProjectLogicController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ProjectLogicController));

        public ProjectLogicController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> GetProjectLogic()
        {
            JsonResult jResult;
            List<ProjectLogicModel> records = new List<ProjectLogicModel>();
            try
            {
                string _url = url + "/configuration/getProjectLogic" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ProjectLogicModel>>(client, _url, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetProjectLogic :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
