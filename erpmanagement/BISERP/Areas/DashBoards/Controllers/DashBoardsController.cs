using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//using BISERP.Areas.Store.Controllers;
using BISERP.Models.UserMangement;
using BISERPCommon;
using BISERP.Areas.DashBoards.Models;
using System.Threading;

namespace BISERP.Areas.DashBoards.ControllerS
{
    public class DashBoardsController : Controller
    {
              HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(DashBoardsController));

        public DashBoardsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        public async Task<ActionResult> DashBoards(int MenuId)
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
            }).Where(m => m.ParentMenuId.Equals(1)).ToList();
            if (MenuId > 1)
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
        public async Task<ActionResult> Summary()
        {
            string _url = url + "/dashboard/getitemsummary" + Common.Constants.JsonTypeResult;
            DashBoardModel records = await Common.AsyncWebCalls.GetAsync<DashBoardModel>(client, _url, CancellationToken.None);
            return PartialView(records);
        }
        public ActionResult DashBoard()
        {
            return PartialView();
        }

        public ActionResult RequestStatus()
        {
            return PartialView();
        }
       
        public ActionResult StockRegisterView()
        {
            return PartialView();
        }
        public ActionResult PurchaseDashBoard()
        {
            return PartialView();
        }
        public ActionResult StoreDashboard()
        {
            return PartialView();
        }

    }
}
