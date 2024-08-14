using BISERP.Areas.Masters.Models;
using BISERP.Areas.Store.Controllers;
using BISERP.Common;
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
    public class MechconMasterController : Controller
    {
        
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(MechconMasterController));

        public MechconMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }


        [HttpGet]
        public async Task<JsonResult> GetMechconData()
        {
            JsonResult jResult;
            MechconMasterModel items = new MechconMasterModel();
            try
            {
                string _url = url + "/mechconMaster/getmechcondata" + Common.Constants.JsonTypeResult;
                items = await Common.AsyncWebCalls.GetAsync<MechconMasterModel>(client, _url, CancellationToken.None);
                if (items != null)
                {
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(items, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetMechconData :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }


        [HttpPost]
        public JsonResult SaveMechconDetails(MechconMasterModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;

                _url = url + "/mechconMaster/savemechcondata" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<MechconMasterModel>(result.Content.ReadAsStringAsync().Result);
            }
          
            if (!_isSuccess)
                return Json(new { success = false, Message = model.Message });
            else
                return Json(new { success = true });
        }

        public List<MechconMasterModel> GetReportData()
        {
            List<MechconMasterModel> _result = null;
            
            

            string _url = url + "/mechconMaster/getmechcondata" + Common.Constants.JsonTypeResult;
            var output = Common.AsyncWebCalls.GetAsync<List<MechconMasterModel>>(client, _url, CancellationToken.None);

            //_result = output;

            
            return _result;
            //try
            //{
            //    List<MechconMasterModel> company;
            //    company = GetMechconData();
            //    CompanyMasterModel com = new CompanyMasterModel();
            //    if (company.Count > 0)
            //    {
            //        com.Id = company[0].Id;
            //        com.CompanyName = company[0].CompanyName;
            //        com.LogoAttachment = company[0].LogoAttachment;
            //        com.StampAttachment = company[0].StampAttachment;
            //        com.GSTNo = company[0].GSTNo;
            //        com.PANNo = company[0].PANNo;

            //    }
            //    return com;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError("Error in GetCompanyNameLogo :" + ex.Message + Environment.NewLine + ex.StackTrace);
            //    throw ex;
            //}

        }
    }
}
