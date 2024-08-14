using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using BISERP.Models.UserMangement;
using BISERPCommon;

namespace BISERP.Areas.VendorProcess.Controllers
{
    public class VendorProcessController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(VendorProcessController));
        public VendorProcessController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        public async Task<ActionResult> VendorProcess(int MenuId)
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
            }).Where(m => m.ParentMenuId.Equals(143)).ToList();
            if (MenuId > 143)
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

           public ActionResult VendorIssue()
        {
            return PartialView();
        }
        public ActionResult VendorIssueAuthorization()
        {
            return PartialView();
        }
        public ActionResult GRNVendor()
        {
            return PartialView();
        }
        public ActionResult GRNVendorAuthorization()
        {
            return PartialView();
        }

    }
}
