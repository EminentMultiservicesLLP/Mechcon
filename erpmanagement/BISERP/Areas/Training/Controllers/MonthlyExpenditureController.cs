using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using BISERP.Areas.Training.Models;
using BISERPCommon;

namespace BISERP.Areas.Training.Controllers
{
    public class MonthlyExpenditureController : Controller
    {
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(MonthlyExpenditureController));

        [HttpPost]
        public async Task<JsonResult> SaveMonthlyExpenditure(MonthlyExpenditureModel items)
        {
            MonthlyExpenditureDtModel dt = new MonthlyExpenditureDtModel();

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

            if (items.Id > 0)
            {
                var result =
                    await
                        Common.AsyncWebCalls.PutAsync("", items, CancellationToken.None);
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
                var result = await Common.AsyncWebCalls.PostAsync("/MonthlyExpenditure/CreateMonthlyExpenditure", items, CancellationToken.None);
                return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false, result.Message });
            }
        }



        public async Task<JsonResult> GetExpenditureByMonth(int ExpMonth, int ExpYear)
        {
            JsonResult jResult;
            List<MonthlyExpenditureDtModel> obj = new List<MonthlyExpenditureDtModel>();
            try
            {
                //string strexpMonth = Convert.ToDateTime(ExpMonth).;
                var records = await Common.AsyncWebCalls.GetAsync<List<MonthlyExpenditureDtModel>>(
                            "/MonthlyExpenditure/getExpenditureByMonth" + "/" + ExpMonth + "/" + ExpYear, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in getExpenditureByMonth :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        public async Task<JsonResult> GetBuExpenditureByMonth(int BudgetMonth, int BudgetYear)
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

        public async Task<JsonResult> GetActualExpence(int ExpMonth, int ExpYear, string BudgetHead)
        {
            JsonResult jResult;
            List<MonthlyExpenditureDtModel> obj = new List<MonthlyExpenditureDtModel>();
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<MonthlyExpenditureDtModel>>(
                            "/MonthlyExpenditure/GetActualExpence" + "/" + ExpMonth + "/" + ExpYear + "/" + BudgetHead, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetActualExpence :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }


    }


}
