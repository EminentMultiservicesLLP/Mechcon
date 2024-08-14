using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using BISERP.Models.UserMangement;
namespace BISERP.Areas.Reports.Controllers
{
    public class ReportsController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ReportsController));

        public ReportsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }


        [HttpGet]
        public ActionResult Reports(int MenuId)
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
            }).Where(m => m.ParentMenuId.Equals(215)).ToList();
            if (MenuId > 215)
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
        public ActionResult PurchaseVSgrnItemStatus()
        {
            return PartialView();
        }
        public ActionResult ProjectBudgetStatus()
        {
            return PartialView();
        }
        public ActionResult ProjectBudgetConclusion()
        {
            return PartialView();
        } 
        public ActionResult PurchaseRequestRpt() //newly added forms
        {
            return PartialView();
        }
        public ActionResult PurchaseOrdertRpt()
        {
            return PartialView();
        }
        public ActionResult RFQRpt()
        {
            return PartialView();
        }
        public ActionResult WorkOrderRpt()
        {
            return PartialView();
        }
        public ActionResult GRNRpt()
        {
            return PartialView();
        }
        //public ActionResult ClearanceNoteRpt()
        //{
        //    return PartialView();
        //}
    }
}
