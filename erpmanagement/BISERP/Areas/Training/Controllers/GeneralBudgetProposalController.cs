using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;   
using BISERP.Areas.Training.Models;
using BISERPCommon;

namespace BISERP.Areas.Training.Controllers
{
    public class GeneralBudgetProposalController : Controller
    {
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(GeneralBudgetProposalController));

        [HttpPost]
        public async Task<JsonResult> SaveGeneralBudgetProposal(GeneralBudgetProposalModel items)
        {

            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedON = DateTime.Now;
            items.InsertedIPAddress = Common.Constants.IpAddress;
            items.InsertedMacID = Common.Constants.MacId;
            items.InsertedMacName = Common.Constants.MacName;

            items.UpdatedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.UpdatedOn = DateTime.Now;
            items.UpdatedIPAddress = Common.Constants.IpAddress;
            items.UpdatedMacID = Common.Constants.MacId;
            items.UpdatedMacName = Common.Constants.MacName;

            if (items.BudgetId > 0)
            {
                var result =await Common.AsyncWebCalls.PutAsync("", items, CancellationToken.None);
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
                var result = await Common.AsyncWebCalls.PostAsync("/GeneralBudgetProposal/CreateGeneralBudgetProposal", items, CancellationToken.None);
                return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false, result.Message });
            }
        }

        public async Task<JsonResult> GetExpenditureByMonth(int BudgetMonth, int BudgetYear)
        {
            JsonResult jResult;
            List<GeneralBudgetProposalDtModel> obj = new List<GeneralBudgetProposalDtModel>();
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<GeneralBudgetProposalDtModel>>("/GeneralBudgetProposal/GetbudgetProposalByMonth" + "/" + BudgetMonth + "/" + BudgetYear, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in Expenditure :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

         [HttpGet]
        public async Task<JsonResult> GetGeneralBudgetProposal()
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<GeneralBudgetProposalModel>>("/GeneralBudgetProposal/getgeneralbudgetproposal", CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetGeneralBudgetProposal :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }
         [HttpGet]
         public async Task<JsonResult> GetAllGeneralBudgetProposalDtl(int budgetId)
         {
             JsonResult jResult;
             try
             {
                 var records = await Common.AsyncWebCalls.GetAsync<List<GeneralBudgetProposalDtModel>>("/GeneralBudgetProposal/getgeneralbudgetproposal/" + budgetId, CancellationToken.None);
                 jResult = Json(records, JsonRequestBehavior.AllowGet);
             }
             catch (Exception ex)
             {
                 Logger.LogError("Error in GetAllGeneralBudgetProposalDtl :" + ex.Message + Environment.NewLine + ex.StackTrace);
                 jResult = Json("Error");
             }
             return jResult;
         }
        [HttpPut]
         public async Task<JsonResult> AuthCancelBudgetProposal(GeneralBudgetProposalModel gb)
         {
             string _url = "";
             GeneralBudgetProposalModel _gb = new GeneralBudgetProposalModel();
             _gb.BudgetId = gb.BudgetId;
             _gb.Authorized = gb.Authorized;
             _gb.cancelled = gb.cancelled;
             _gb.BudgetDtModel = gb.BudgetDtModel;
             _gb.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
             _gb.InsertedON = DateTime.Now;
             _gb.InsertedIPAddress = Common.Constants.IpAddress;
             _gb.InsertedMacID = Common.Constants.MacId;
             _gb.InsertedMacName = BISERP.Common.Constants.MacName;
             var result = await Common.AsyncWebCalls.PutAsync("/GeneralBudgetProposal/authcancel", _gb, CancellationToken.None);
             return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false});
         }
    }
}
