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
    public class ClearanceNoteController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ClearanceNoteController));

        public ClearanceNoteController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }


        [HttpPost]
        public ActionResult SaveClearanceNote(ClearanceNoteModel items)
        {
            string _url = "";
            bool _isSuccess = true;
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedOn = System.DateTime.Now;

            _url = url + "/clearanceNote/saveClearanceNote" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, items, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                items = JsonConvert.DeserializeObject<ClearanceNoteModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                _isSuccess = true;
                items =JsonConvert.DeserializeObject<ClearanceNoteModel>(result.Content.ReadAsStringAsync().Result);
            }
            if (!_isSuccess)
            {
                return Json(new { success = false, Message = "Error while save ClearanceNote", Data = items });
            }
            else
            {
                return Json(new { success = true, Message = items.ClearanceNoteId, Data = items });
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetClearanceNote(int StoreId)
        {
            List<ClearanceNoteModel> records = new List<ClearanceNoteModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/clearanceNote/getClearanceNote/" + StoreId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<ClearanceNoteModel>>(client, _url, CancellationToken.None);
                if (records != null && records.Count > 0)
                {
                    jResult = Json(new { success = true, records }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Clearance Note Found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetClearanceNote:" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
