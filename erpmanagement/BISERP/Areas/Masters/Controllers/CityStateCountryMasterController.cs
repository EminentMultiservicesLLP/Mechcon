using BISERP.Areas.Masters.Models;
using BISERP.Filters;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.Masters.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class CityStateCountryMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(MechconMasterController));

        public CityStateCountryMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveCity(CityMasterModel city)
        {
            string _url = "";
            bool _isSuccess = true;
            city.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            city.InsertedON = System.DateTime.Now;
            city.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            city.InsertedMacID = BISERP.Common.Constants.MacId;
            city.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/cityStateCountryMaster/saveCity" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, city, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                city = JsonConvert.DeserializeObject<CityMasterModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = city.Message });
            else
                return Json(new { success = true });

        }

        [HttpPost]
        public ActionResult SaveState(StateMasterModel state)
        {
            string _url = "";
            bool _isSuccess = true;
            state.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            state.InsertedON = System.DateTime.Now;
            state.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            state.InsertedMacID = BISERP.Common.Constants.MacId;
            state.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/cityStateCountryMaster/saveState" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, state, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                state = JsonConvert.DeserializeObject<StateMasterModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = state.Message });
            else
                return Json(new { success = true });

        }
    }
}
