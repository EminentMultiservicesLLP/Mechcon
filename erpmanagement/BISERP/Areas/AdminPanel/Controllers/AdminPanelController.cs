
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERP.Filters;
using BISERP.Models.UserMangement;

namespace BISERP.Areas.AdminPanel.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class AdminPanelController : Controller
    {
        public ActionResult AdminPanel(int MenuId)
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
            }).Where(m => m.ParentMenuId.Equals(181)).ToList();
            if (MenuId > 181)
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
        public ActionResult EmployeeEnrollment()
        {
            return PartialView();
        }
        public ActionResult RoleAccess()
        {
            return PartialView();
        }
        public ActionResult GroupMaster()
        {
            return PartialView();
        }
        public ActionResult ChangePassword()
        {
            return PartialView();
        }
    }
}
