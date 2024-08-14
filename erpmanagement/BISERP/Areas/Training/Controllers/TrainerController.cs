using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BISERPCommon;
using BISERP.Areas.Training.Models;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BISERP.Areas.Training.Controllers
{
    public class TrainerController : Controller
    {
        //HttpClient client;
        //string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(TrainerController));

        public TrainerController()
        {
            //client = new HttpClient();
            //client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<JsonResult> AllTrainer(string SearchName)
        {
            List<TrainerModel> records = new List<TrainerModel>();
            JsonResult jResult;
            try
            {
                records = await Common.AsyncWebCalls.GetAsync<List<TrainerModel>>("/trainer/GetAllTrainer", CancellationToken.None);
                if (records != null)
                {
                    if (!string.IsNullOrWhiteSpace(SearchName))
                    {
                        records = records.Where(p => p.TrainerName.ToUpper().StartsWith(SearchName.ToUpper())).ToList();
                    }
                    jResult = Json( records , JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllTrainer :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> AllActiveTrainer()
        {
            List<TrainerModel> records = new List<TrainerModel>();
            JsonResult jResult;
            try
            {

                records = await Common.AsyncWebCalls.GetAsync<List<TrainerModel>>("/trainer/getactiveTrainer", CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllActiveTrainer :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public async Task<JsonResult>  SaveTrainer(TrainerModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;

            var result = await Common.AsyncWebCalls.PostAsync("/trainer/CreateTrainer", model, CancellationToken.None);
            return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false, result.Message });
            //if (result.StatusCode.ToString() == "BadRequest")
            //{
            //    _isSuccess = false;
            //    model = JsonConvert.DeserializeObject<TrainerModel>(result.Content.ReadAsStringAsync().Result);
            //}

            //if (!_isSuccess)
            //    return Json(new { success = false, Message = model.Message });
            //else
            //    return Json(new { success = true });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTrainer(TrainerModel model)
        {
            string _url = "";
            bool _isSuccess = true;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;

            var result = await Common.AsyncWebCalls.PutAsync("/trainer/UpdateTrainer", model, CancellationToken.None);
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                model = JsonConvert.DeserializeObject<TrainerModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, model.Message });
            else
                return Json(new { success = true });
        }
    }
}
