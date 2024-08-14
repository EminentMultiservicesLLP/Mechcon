using System;
using System.Web.Mvc;
using BISERPCommon;
using System.Threading.Tasks;
using BISERP.Areas.Training.Models;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace BISERP.Areas.Training.Controllers
{
    public class RatingController : Controller
    {
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(RatingController));
        [HttpGet]
        public async Task<JsonResult> AllRating(string searchName, string searchCode) 
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<RatingModel>>("/rating/GetAllRating", CancellationToken.None);

                if (!string.IsNullOrWhiteSpace(searchName))
                {
                    records = records.Where(p => p.Rating.ToUpper().StartsWith(searchName.ToUpper())).ToList();
                }
                if (!string.IsNullOrWhiteSpace(searchCode))
                {
                    records = records.Where(p => p.RatingCode.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                }
                jResult = Json(records, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllRating :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public async Task<JsonResult> SaveRating(RatingModel model)
        {
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = Common.Constants.IpAddress;
            model.InsertedMacID = Common.Constants.MacId;
            model.InsertedMacName = Common.Constants.MacName;
            var result = await Common.AsyncWebCalls.PostAsync("/rating/CreateRating", model, CancellationToken.None);
            return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false, result.Message });
        }

        [HttpPut]
        public async Task<JsonResult> UpdateRating(RatingModel model)
        {
            model.UpdatedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.UpdatedOn = DateTime.Now;
            model.UpdatedIPAddress = Common.Constants.IpAddress;
            model.UpdatedMacID = Common.Constants.MacId;
            model.UpdatedMacName = Common.Constants.MacName;
            var result = await Common.AsyncWebCalls.PutAsync("/rating/UpdateRating", model, CancellationToken.None);
            if (result != null)
            {
                if (result.IsSuccessStatusCode)
                    return Json(new { success = true });
                else
                    return Json(new { success = false, Message = result.ReasonPhrase });
            }
            else
            {
                return Json(new { success = false, Message = "Unknown error, please contact server Administrator" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> AllActiveRating()
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<RatingModel>>("/rating/getactiveRating", CancellationToken.None);
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
              
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllActiveRating :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
