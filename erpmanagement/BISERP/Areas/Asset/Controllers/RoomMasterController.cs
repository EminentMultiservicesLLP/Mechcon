using BISERP.Areas.Asset.Models;
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
    public class RoomMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(RoomMasterController));

        public RoomMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllRoom(string SearchName, string SearchCode)
        {
            List<RoomModel> records = new List<RoomModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/Room/GetAllRoom" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<RoomModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.RoomName.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(SearchCode))
                    {
                        records = records.Where(p => p.RoomCode.ToUpper().StartsWith(SearchCode.ToUpper())).ToList();
                    }
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllRoom :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllActiveAllRoom()
        {
            List<RoomModel> records = new List<RoomModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/Room/getactiveRoom/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<RoomModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    
                    jResult = Json(new { records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllActiveAllRoom :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> FloorwiseRoom(int FloorId)
        {
            List<RoomModel> records = new List<RoomModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/Room/getfloorRoom/" + FloorId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<RoomModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in FloorwiseRoom :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveRoom(RoomModel Floor)
        {
            string _url = "";
            bool _isSuccess = true;
            Floor.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            Floor.InsertedON = System.DateTime.Now;
            Floor.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            Floor.InsertedMacID = BISERP.Common.Constants.MacId;
            Floor.InsertedMacName = BISERP.Common.Constants.MacName;
            _url = url + "/Room/CreateRoom" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, Floor, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                Floor = JsonConvert.DeserializeObject<RoomModel>(result.Content.ReadAsStringAsync().Result);
            }
           
          if (!_isSuccess)
              return Json(new { success = false, Message = Floor.Message });
          else
              return Json(new { success = true });

        }
        [HttpPost]
        public ActionResult UpdateRoom(RoomModel Floor)
        {
            string _url = "";
            bool _isSuccess = true;
            Floor.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            Floor.InsertedON = System.DateTime.Now;
            Floor.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            Floor.InsertedMacID = BISERP.Common.Constants.MacId;
            Floor.InsertedMacName = BISERP.Common.Constants.MacName;

            _url = url + "/Room/UpdateRoom" + Common.Constants.JsonTypeResult;
            //client.PutAsync(_url, store, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<int>().Result;
            var result = client.PostAsync(_url, Floor, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                Floor = JsonConvert.DeserializeObject<RoomModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, Message = Floor.Message });
            else
                return Json(new { success = true });
        }
      
    }
}
