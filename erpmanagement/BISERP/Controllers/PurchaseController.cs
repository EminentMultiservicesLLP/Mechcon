using BISERP.Area.Purchase.Models;
using BISERP.Models.UserMangement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http;
using BISERP.Filters;

namespace BISERP.Area.Purchase.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class PurchaseController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(PurchaseController));

        public PurchaseController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Purchase(int MenuId)
        {
            if (Session["AppUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            MenuUserRightsModel model = new MenuUserRightsModel();
            string _url = url + "/user/getusermenu/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            var records = await Common.AsyncWebCalls.GetAsync<List<MenuUserRightsModel>>(client, _url, CancellationToken.None);
            var parentMenuCount = records.Select(m => m.ParentMenuId).Distinct().Count();
            if (parentMenuCount == 1)
            {
                model.Parent = records.Select(row => new ParentMenuRights
                {
                    MenuId = row.MenuId,
                    UserId = row.UserId,
                    Access = row.Access,
                    MenuName = row.MenuName,
                    PageName = row.PageName,
                    ParentMenuId = row.ParentMenuId
                }).Where(m => m.Access.Equals(true)).ToList();
            }
            else
            {
                model.Parent = records.Select(row => new ParentMenuRights
                {
                    MenuId = row.MenuId,
                    UserId = row.UserId,
                    Access = row.Access,
                    MenuName = row.MenuName,
                    PageName = row.PageName,
                    ParentMenuId = row.ParentMenuId
                }).Where(m => (m.ParentMenuId < MenuId)).Where(m => m.Access.Equals(true)).ToList();

                model.Child = records.Select(row => new ChildMenuRights
                {
                    MenuId = row.MenuId,
                    UserId = row.UserId,
                    Access = row.Access,
                    Add = row.Add,
                    Edit = row.Edit,
                    DeletePerm = row.DeletePerm,
                    SuperPerm = row.SuperPerm,
                    MenuName = row.MenuName,
                    PageName = row.PageName,
                    ParentMenuId = row.ParentMenuId
                }).Where(m => m.ParentMenuId.Equals(MenuId)).Where(m => m.Access.Equals(true)).ToList();
            }
            return View(model);
        }
      

        public ActionResult PurchaseOrder()
        {
            return PartialView();
        }

        public ActionResult PurchaseIndent()
        {
            return PartialView();
        }
        public ActionResult POAmendment()
        {
            return PartialView();
        }

        public ActionResult PurchaseAuthorization()
        {
            return View();
        }
        public ActionResult PIAuthorization()
        {
            return View();
        }

        public ActionResult POAuthorization()
        {
            return View();
        }
        public async Task<ActionResult> PReports()
        {
            int MenuId = 69;
            MenuUserRightsModel model = new MenuUserRightsModel();
            string _url = url + "/user/getusermenu/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            var records = await Common.AsyncWebCalls.GetAsync<List<MenuUserRightsModel>>(client, _url, CancellationToken.None);
            var parentMenuCount = records.Select(m => m.ParentMenuId).Distinct().Count();
            if (parentMenuCount == 1)
            {
                model.Parent = records.Select(row => new ParentMenuRights
                {
                    MenuId = row.MenuId,
                    UserId = row.UserId,
                    Access = row.Access,
                    MenuName = row.MenuName,
                    PageName = row.PageName,
                    ParentMenuId = row.ParentMenuId
                }).Where(m => m.Access.Equals(true)).ToList();
            }
            else
            {
                model.Parent = records.Select(row => new ParentMenuRights
                {
                    MenuId = row.MenuId,
                    UserId = row.UserId,
                    Access = row.Access,
                    MenuName = row.MenuName,
                    PageName = row.PageName,
                    ParentMenuId = row.ParentMenuId
                }).Where(m => (m.ParentMenuId < MenuId)).Where(m => m.Access.Equals(true)).ToList();

                model.Child = records.Select(row => new ChildMenuRights
                {
                    MenuId = row.MenuId,
                    UserId = row.UserId,
                    Access = row.Access,
                    Add = row.Add,
                    Edit = row.Edit,
                    DeletePerm = row.DeletePerm,
                    SuperPerm = row.SuperPerm,
                    MenuName = row.MenuName,
                    PageName = row.PageName,
                    ParentMenuId = row.ParentMenuId
                }).Where(m => m.ParentMenuId.Equals(MenuId)).Where(m => m.Access.Equals(true)).ToList();
            }
            return PartialView(model);
        }
        public async Task<ActionResult> PIReports()
        {
            int MenuId = 70;
            MenuUserRightsModel model = new MenuUserRightsModel();
            string _url = url + "/user/getusermenu/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            var records = await Common.AsyncWebCalls.GetAsync<List<MenuUserRightsModel>>(client, _url, CancellationToken.None);
            var parentMenuCount = records.Select(m => m.ParentMenuId).Distinct().Count();
            if (parentMenuCount == 1)
            {
                model.Parent = records.Select(row => new ParentMenuRights
                {
                    MenuId = row.MenuId,
                    UserId = row.UserId,
                    Access = row.Access,
                    MenuName = row.MenuName,
                    PageName = row.PageName,
                    ParentMenuId = row.ParentMenuId
                }).Where(m => m.Access.Equals(true)).ToList();
            }
            else
            {
                model.Parent = records.Select(row => new ParentMenuRights
                {
                    MenuId = row.MenuId,
                    UserId = row.UserId,
                    Access = row.Access,
                    MenuName = row.MenuName,
                    PageName = row.PageName,
                    ParentMenuId = row.ParentMenuId
                }).Where(m => (m.ParentMenuId < MenuId)).Where(m => m.Access.Equals(true)).ToList();

                model.Child = records.Select(row => new ChildMenuRights
                {
                    MenuId = row.MenuId,
                    UserId = row.UserId,
                    Access = row.Access,
                    Add = row.Add,
                    Edit = row.Edit,
                    DeletePerm = row.DeletePerm,
                    SuperPerm = row.SuperPerm,
                    MenuName = row.MenuName,
                    PageName = row.PageName,
                    ParentMenuId = row.ParentMenuId
                }).Where(m => m.ParentMenuId.Equals(MenuId)).Where(m => m.Access.Equals(true)).ToList();
            }
            return PartialView(model);
        }
    }
}
