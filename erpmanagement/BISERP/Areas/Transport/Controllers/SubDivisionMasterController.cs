using BISERP.Areas.Store.Models.Master;
using BISERP.Areas.Transport.Models.Master;
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

namespace BISERP.Areas.Store.Controllers
{
    public class SubDivisionMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(SubDivisionMasterController));

        public SubDivisionMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllSubDivision(string SearchName)
        {
            List<SubDivisionMasterModel> records = new List<SubDivisionMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/subDivision/GetAllSubDivision" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<SubDivisionMasterModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.SubDivisionName.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                   
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllSubDivision :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }



        [HttpPost]
        public ActionResult SaveSubDivision(SubDivisionMasterModel div)
        {
            string _url = "";
            bool _isSuccess = true;
            div.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            div.InsertedON = System.DateTime.Now;
            div.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            div.InsertedMacID = BISERP.Common.Constants.MacId;
            div.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/subDivision/CreateSubdivision" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, div, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                div = JsonConvert.DeserializeObject<SubDivisionMasterModel>(result.Content.ReadAsStringAsync().Result);
            }
           
          if (!_isSuccess)
              return Json(new { success = false, Message = div.Message });
          else
              return Json(new { success = true });

        }
        [HttpPost]
        public ActionResult updateDivision(SubDivisionMasterModel unit)
        {
            string _url = "";
            bool _isSuccess = true;
            unit.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            unit.InsertedON = System.DateTime.Now;
            unit.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            unit.InsertedMacID = BISERP.Common.Constants.MacId;
            unit.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/subDivision/Updatedivision" + Common.Constants.JsonTypeResult;
            //client.PutAsync(_url, store, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<int>().Result;
            var result = client.PostAsync(_url, unit, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                unit = JsonConvert.DeserializeObject<SubDivisionMasterModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = unit.Message });
            else
                return Json(new { success = true });
        }
      
    }
}
