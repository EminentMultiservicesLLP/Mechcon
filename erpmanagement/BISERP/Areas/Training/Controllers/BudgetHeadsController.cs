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
    public class BudgetHeadsController : Controller
    {

        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(BudgetHeadsController));
        [HttpGet]
        public async Task<JsonResult> AllBudgetHeads(string searchName, string searchCode)
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<BudgetHeadsModel>>("/BudgetHeads/GetAllBudgetHeads", CancellationToken.None);
                if (!string.IsNullOrWhiteSpace(searchName))
                {
                    records = records.Where(p => p.BudgetHeads.ToUpper().StartsWith(searchName.ToUpper())).ToList();
                }
                if (!string.IsNullOrWhiteSpace(searchCode))
                {
                    records = records.Where(p => p.BudgetCode.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                }
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllBudgetCode :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public async Task<JsonResult> SaveBudgetHeads(BudgetHeadsModel model)
        {
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = Common.Constants.IpAddress;
            model.InsertedMacID = Common.Constants.MacId;
            model.InsertedMacName = Common.Constants.MacName;

            var result = await Common.AsyncWebCalls.PostAsync("/BudgetHeads/CreateBudgetHeads", model, CancellationToken.None);
            return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false, result.Message });
        }

        [HttpPut]
        public async Task<JsonResult> UpdateBudgetHeads(BudgetHeadsModel model)
        {
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = DateTime.Now;
            model.InsertedIPAddress = Common.Constants.IpAddress;
            model.InsertedMacID = Common.Constants.MacId;
            model.InsertedMacName = Common.Constants.MacName;

            var result =
                await
                    Common.AsyncWebCalls.PutAsync("/BudgetHeads/UpdateBudgetHeads", model,
                        CancellationToken.None);

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
        public async Task<JsonResult> AllActiveBudgetHeads()
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<BudgetHeadsModel>>("/BudgetHeads/getactiveBudgetHeads", CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in AllActiveBudgetHeads :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
