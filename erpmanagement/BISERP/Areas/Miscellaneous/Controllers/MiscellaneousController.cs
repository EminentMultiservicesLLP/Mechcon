using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Miscellaneous.Models;
using BISERP.Models.UserMangement;
using BISERPCommon;
//using BISERP.Areas.DailyData.Models;
using System.Threading;

namespace BISERP.Areas.Miscellaneous.ControllerS
{
    public class MiscellaneousController : Controller
    {
              HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(MiscellaneousController));

        public MiscellaneousController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        public async Task<ActionResult> Miscellaneous(int MenuId)
        {
            if (Session["AppUserId"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            MenuUserRightsModel model = new MenuUserRightsModel();
            List<MenuUserRightsModel> menurights = (List<MenuUserRightsModel>)Session["UserMenu"];

            model.Parent = menurights.Select(row => new ParentMenuRights
            {
                MenuId = row.MenuId,
                UserId = row.UserId,
                Access = row.Access,
                MenuName = row.MenuName,
                PageName = row.PageName,
                ParentMenuId = row.ParentMenuId
            }).Where(m => m.ParentMenuId.Equals(8)).ToList();
            if (MenuId > 8)
            {
                model.Child = menurights.Select(row => new ChildMenuRights
                            {
                                MenuId = row.MenuId,
                                UserId = row.UserId,
                                Access = row.Access,
                                MenuName = row.MenuName,
                                PageName = row.PageName,
                                ParentMenuId = row.ParentMenuId
                            }).Where(m => m.ParentMenuId.Equals(MenuId)).ToList();
            }            
            return View(model);
        }

        public ActionResult StoreConsumption()
        {
            return PartialView();
        }
        public ActionResult OpeningBalance()
        {
            return PartialView();
        }
        public ActionResult StoreQuantityLimits()
        {
            return PartialView();
        }
        public ActionResult CancelPendingMaterialIndent()
        {
            return PartialView();
        }
        public ActionResult MaterialReturn()
        {
            return PartialView();
        }
        public ActionResult MaterialReturnAuthorization()
        {
            return PartialView();
        }
        public ActionResult DiscrepancyReportGeneration()
        {
            return PartialView();
        }
        public ActionResult StockAdjustmentVoucher()
        {
            return PartialView();
        }

        public ActionResult StoreConsumptionCancellation()
        {
            return PartialView();
        }
       
        public ActionResult StockDispose()
        {
            return PartialView();
        }
        public ActionResult StockView()
        {
            return PartialView();
        }
        public ActionResult MasterReport()
        {
            return PartialView();
        }
        public ActionResult EMailTab()
        {
            return PartialView();
        }
     
        public ActionResult SubmitGRNBill()
        {
            return PartialView();
        }
    }
}
