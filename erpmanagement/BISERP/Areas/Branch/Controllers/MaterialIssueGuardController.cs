using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using BISERP.Area.Branch.Models;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace BISERP.Areas.Branch.Controllers
{
    public class MaterialIssueGuardController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(MaterialIssueGuardController));

        public MaterialIssueGuardController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveMaterialIssueGuard(MaterialIssueGuardModel mig)
        {
            string strErrorMsg = "";
            mig.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            mig.InsertedON = System.DateTime.Now;
            mig.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            mig.InsertedMacID = BISERP.Common.Constants.MacId;
            mig.InsertedMacName = BISERP.Common.Constants.MacName;

            string _url = url + "/missueguard/createIssueguard" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, mig, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                strErrorMsg = "Error In Material Issue Guard";
                mig = JsonConvert.DeserializeObject<MaterialIssueGuardModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                strErrorMsg = "";
                mig = JsonConvert.DeserializeObject<MaterialIssueGuardModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (strErrorMsg != "")
                return Json(new { success = false, responseText = strErrorMsg }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = true, responseText = mig.IssueNo });
        }

        [HttpGet]
        public async Task<JsonResult> GetMaterialIssueGuard(int StoreId,int EmpId)
        {
            JsonResult jResult;
            List<MaterialIssueGuardModel> mimodel = new List<MaterialIssueGuardModel>();
            try
            {
                string _url = url + "/missueguard/getissueguard/" + StoreId + "/" + EmpId + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<MaterialIssueGuardModel>>(client, _url, CancellationToken.None);
                if (mimodel != null)
                {                    
                    mimodel = mimodel.OrderByDescending(m => m.IssueId).ToList();
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetMaterialIssueGuard :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetAllMaterialIssueGuard()
        {
            JsonResult jResult;
            List<MaterialIssueGuardModel> mimodel = new List<MaterialIssueGuardModel>();
            try
            {
                string _url = url + "/missueguard/getissueguard/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<MaterialIssueGuardModel>>(client, _url, CancellationToken.None);
                if (mimodel != null)
                {
                    mimodel = mimodel.OrderByDescending(m => m.IssueId).ToList();
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllMaterialIssueGuard :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetIssueGuardDetails(int IssueId)
        {
            JsonResult jResult;
            List<MaterialIssueGuardDtModel> dtmodel = new List<MaterialIssueGuardDtModel>();
            try
            {
                string _url = url + "/missueguard/getissueguarddt/" + IssueId + Common.Constants.JsonTypeResult;
                dtmodel = await Common.AsyncWebCalls.GetAsync<List<MaterialIssueGuardDtModel>>(client, _url, CancellationToken.None);
                if (dtmodel != null)
                {
                    dtmodel = dtmodel.OrderByDescending(m => m.IssueId).ToList();
                    jResult = Json(new { dtmodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { dtmodel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetIssueGuardDetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetAllMaterialIssueGuardNO(int branchId)
        {
            JsonResult jResult;
            List<MaterialIssueGuardModel> mimodel = new List<MaterialIssueGuardModel>();
            try
            {
                string _url = url + "/missueguard/getAllissueguard/" + branchId + Common.Constants.JsonTypeResult;
                mimodel = await Common.AsyncWebCalls.GetAsync<List<MaterialIssueGuardModel>>(client, _url, CancellationToken.None);
                if (mimodel != null)
                {
                    mimodel = mimodel.OrderByDescending(m => m.IssueId).ToList();
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { mimodel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllMaterialIssueGuardNO :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

    }
}
