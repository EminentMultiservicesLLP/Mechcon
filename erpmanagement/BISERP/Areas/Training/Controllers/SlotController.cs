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
    public class SlotController : Controller
    {
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(SlotController));
        [HttpGet]
        public async Task<JsonResult> AllSlot(string searchName, string searchCode) 
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<SlotModel>>("/slot/GetAllSlot", CancellationToken.None);

                if (!string.IsNullOrWhiteSpace(searchName))
                {
                    records = records.Where(p => p.SlotName.ToUpper().StartsWith(searchName.ToUpper())).ToList();
                }
                if (!string.IsNullOrWhiteSpace(searchCode))
                {
                    records = records.Where(p => p.SlotCode.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                }
                jResult = Json(records, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllSlot :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public async Task<JsonResult> SaveSlot(SlotModel model)
        {
            model.UpdatedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.UpdatedOn = DateTime.Now;
            model.UpdatedIPAddress = Common.Constants.IpAddress;
            model.UpdatedMacID = Common.Constants.MacId;
            model.UpdatedMacName = Common.Constants.MacName;
            if (model.SlotId > 0)
            {
                var result = await Common.AsyncWebCalls.PutAsync("/slot/UpdateSlot", model, CancellationToken.None);
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
            else
            {
                var result = await Common.AsyncWebCalls.PostAsync("/slot/CreateSlot", model, CancellationToken.None);
                return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false, result.Message });
            }
        }

        [HttpGet]
        public async Task<JsonResult> AllActiveSlot()
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<SlotModel>>("/slot/getactiveSlot", CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllActiveSlot :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> GetTraniningWiseSlot(int TrainingTypeId)
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<SlotModel>>("/slot/GetTraniningWiseSlot/" + TrainingTypeId, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetTraniningWiseSlot :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetDateWiseSlot()
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<SlotModel>>("/slot/getdatewiseslot", CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetDateWiseSlot :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

    }
}
