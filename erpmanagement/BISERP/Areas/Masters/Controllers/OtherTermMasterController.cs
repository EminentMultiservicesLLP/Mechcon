using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BISERP.Areas.Masters.Models;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Formatting;
using BISERP.Areas.Purchase.Models;
using Newtonsoft.Json;
using BISERP.Filters;

namespace BISERP.Areas.Store.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class OtherTermMasterController : Controller
    {
        HttpClient client;
        string url = Common.Constants.WebAPIAddress;
        static ILogger _logger = Logger.Register(typeof(OtherTermMasterController));

        public OtherTermMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllOtherTerm( string searchOtherTerm, string searchCode)
        {
          JsonResult jResult;
            try
            {
                string _url = url + "/otherterms/getallotherterms" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<OtherTermMasterModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchOtherTerm))
                    {
                        records = records.Where(p => p.OthersTermDesc.ToUpper().StartsWith(searchOtherTerm.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(searchCode))
                    {
                        records = records.Where(p => p.OthersTermCode.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                    }
                    int total = records.Count;
                   
                    jResult = Json(new {success=true, records, total }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success=false,Massage="No other trems detail found"}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllOtherTerm :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Server Error! Please Contact administrator!");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> AllBasisTerm(string searchName, string searchcode)
        {
          JsonResult jResult;
            try
            {
                string _url = url + "/otherterms/getallbasisterms" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<POPriceBasisModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {

                    jResult = Json(new { records,success=true }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, message = "No record found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllOtherTerm :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> AllInspectionTerm(string searchName, string searchcode)
        {
            List<POInspectionModel> oimodel = new List<POInspectionModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/otherterms/getallInspectionterms" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<POInspectionModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchName))
                    {
                        records = records.Where(p => p.InspectionDesc.ToUpper().StartsWith(searchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(searchcode))
                    {
                        records = records.Where(p => p.InspectionCode.ToUpper().StartsWith(searchcode.ToUpper())).ToList();
                    }

                    jResult = Json(new { records,success=true }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { oimodel, message = "No inspection Terms found", success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllOtherTerm :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetActiveOtherTerm(string searchOtherTerm, string searchCode)
        {
            JsonResult jResult;
            try
            {
                string _url = url + "/otherterms/getactive" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<OtherTermMasterModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(searchOtherTerm))
                    {
                        records = records.Where(p => p.OthersTermDesc.ToUpper().StartsWith(searchOtherTerm.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(searchCode))
                    {
                        records = records.Where(p => p.OthersTermCode.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                    }
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetActiveOtherTerm :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveOtherTerm(OtherTermMasterModel otherTerm)
        {
            string _url;
            bool isSuccess = true;
            otherTerm.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            otherTerm.InsertedON = DateTime.Now;
            otherTerm.InsertedIPAddress = Common.Constants.IpAddress;
            otherTerm.InsertedMacID = Common.Constants.MacId;
            otherTerm.InsertedMacName = Common.Constants.MacName;
            if (otherTerm.TermID > 0)
            {
                _url = url + "/otherterms/updateotherterm" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, otherTerm, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    isSuccess = false;
                    otherTerm = JsonConvert.DeserializeObject<OtherTermMasterModel>(result.Content.ReadAsStringAsync().Result);
                }

                if (!isSuccess)
                    return Json(new { success = false, otherTerm.Message });
                else
                    return Json(new { success = true });
            }
            else
            {
                _url = url + "/otherterms/createotherterm" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, otherTerm, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    isSuccess = false;
                    otherTerm = JsonConvert.DeserializeObject<OtherTermMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            //return PartialView();
            if (!isSuccess)
                return Json(new { success = false, otherTerm.Message });
            else
                return Json(new { success = true });
        }


        [HttpPost]
        public ActionResult SaveInspectionmatser(InspectionMasterModel inspection)
        {
            string _url;
            bool isSuccess = true;
            inspection.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            inspection.InsertedON = DateTime.Now;
            inspection.InsertedIPAddress = Common.Constants.IpAddress;
            inspection.InsertedMacID = Common.Constants.MacId;
            inspection.InsertedMacName = Common.Constants.MacName;
            if (inspection.InspectionId > 0)
            {
                _url = url + "/otherterms/updateInspectionmatser" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, inspection, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    isSuccess = false;
                    JsonConvert.DeserializeObject<InspectionMasterModel>(result.Content.ReadAsStringAsync().Result);
                }

                if (!isSuccess)
                    return Json(new { success = false, Message = "Code Is Already Exist" });
                else
                    return Json(new { success = true });
            }
            else
            {
                _url = url + "/otherterms/createInspectionmatser" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, inspection, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    isSuccess = false;
                    JsonConvert.DeserializeObject<InspectionMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            //return PartialView();
            if (!isSuccess)
                return Json(new { success = false, Message = "Code Is Already Exist" });
            else
                return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult SaveBasisMaster(BasisMasterModel basis)
        {
            string _url = "";
            bool isSuccess = true;
            basis.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            basis.InsertedON = DateTime.Now;
            basis.InsertedIPAddress = Common.Constants.IpAddress;
            basis.InsertedMacID = Common.Constants.MacId;
            basis.InsertedMacName = Common.Constants.MacName;
            if (basis.BasisID > 0)
            {
                _url = url + "/otherterms/updateBasisMaster" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, basis, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    isSuccess = false;
                    JsonConvert.DeserializeObject<BasisMasterModel>(result.Content.ReadAsStringAsync().Result);
                }

                if (!isSuccess)
                    return Json(new { success = false, Message = "Code Is Already Exist" });
                else
                    return Json(new { success = true });
            }
            else
            {
                _url = url + "/otherterms/createBasisMaster" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, basis, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    isSuccess = false;
                    JsonConvert.DeserializeObject<BasisMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            //return PartialView();
            if (!isSuccess)
                return Json(new { success = false, Message = "Code Is Already Exist" });
            else
                return Json(new { success = true });
        }
     
    }
}
