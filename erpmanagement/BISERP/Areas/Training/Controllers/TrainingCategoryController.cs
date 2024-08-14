using BISERP.Areas.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BISERPCommon;
using System.Threading.Tasks;
using System.Threading;

namespace BISERP.Areas.Training.Controllers
{
    public class TrainingCategoryController : Controller
    {
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(TrainingCategoryController));

        [HttpGet]
        public async Task<JsonResult> AllTrainingCategory(string searchName, string searchCode)
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TrainingCategoryModel>>("/TrainingCategory/GetAllTrainingCategory", CancellationToken.None);
                    if (!string.IsNullOrWhiteSpace(searchName))
                    {
                        records = records.Where(p => p.Category.ToUpper().StartsWith(searchName.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(searchCode))
                    {
                        records = records.Where(p => p.Code.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                    }
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllTrainingCategory :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public async Task<JsonResult> SaveTrainingCategory(TrainingCategoryModel model)
        {
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = Common.Constants.IpAddress;
            model.InsertedMacID = Common.Constants.MacId;
            model.InsertedMacName = Common.Constants.MacName;

            var result = await Common.AsyncWebCalls.PostAsync("/TrainingCategory/CreateTrainingCategory", model, CancellationToken.None);
            return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false, result.Message });
        }

        [HttpPut]
        public async Task<JsonResult> UpdateTrainingCategory(TrainingCategoryModel model)
        {
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = Common.Constants.IpAddress;
            model.InsertedMacID = Common.Constants.MacId;
            model.InsertedMacName = Common.Constants.MacName;

            var result =
                await
                    Common.AsyncWebCalls.PutAsync("/TrainingCategory/UpdateTrainingCategory", model,
                        CancellationToken.None);

            if (result != null)
            {
                if (result.IsSuccessStatusCode)
                    return Json(new {success = true});
                else
                    return Json(new {success = false, Message = result.ReasonPhrase});
            }
            else
            {
                return Json(new { success = false, Message = "Unknown error, please contact server Administrator"});
            }
        }

        [HttpGet]
        public async Task<JsonResult> AllActiveTrainingCategory()
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TrainingCategoryModel>>("/TrainingCategory/getactiveTrainingCategory", CancellationToken.None);
               
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllActiveTrainingCategory :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

    }
}
