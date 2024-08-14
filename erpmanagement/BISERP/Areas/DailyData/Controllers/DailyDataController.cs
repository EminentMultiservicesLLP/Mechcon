using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
//using BISERP.Areas.Store.Controllers;
using BISERP.Models.UserMangement;
using BISERPCommon;
//using BISERP.Areas.DailyData.Models;

namespace BISERP.Areas.DailyData.ControllerS
{
    public class DailyDataController : Controller
    {
              HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(DailyDataController));

        public DailyDataController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        public async Task<ActionResult> DailyData(int MenuId)
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
            }).Where(m => m.ParentMenuId.Equals(5)).ToList();
            if (MenuId > 5)
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

        public ActionResult PettyCashRegister()
        {
            return PartialView();
        }
        public ActionResult MonthlyExpenditure()
        {
            return PartialView();
        }
        public ActionResult GeneralBudgetProposal()
        {
            return PartialView();
        }
        public ActionResult GeneralBudgetAuthorization()
        {
            return PartialView();
        }
    }
}
