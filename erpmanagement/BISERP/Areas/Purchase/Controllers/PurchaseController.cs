using BISERP.Filters;
using BISERP.Models.UserMangement;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.Purchase.Controllers
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

        [HttpGet]
        public ActionResult Purchase(int MenuId)
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
            }).Where(m => m.ParentMenuId.Equals(3)).ToList();
            if (MenuId > 3)
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

        public ActionResult     PurchaseOrder()
        {
            return PartialView();
        }

        //public ActionResult AuthorizedPurchaseOrder()
        //{
        //    return PartialView();
        //}

        public ActionResult PurchaseIndent()
        {
            return PartialView();
        }
        public ActionResult PRVerification()
        {
            return PartialView();
        }
        public ActionResult PIAuthorization()
        {
            return View();
        }
        public ActionResult POAmendment()
        {
            return PartialView();
        }
        public ActionResult PurchaseAuthorization()
        {
            return View();
        }
        public ActionResult POVerification()
        {
            return PartialView();
        }
        public ActionResult POAuthorization()
        {
            return View();
        }
        public ActionResult PurchaseReturn()
        {
            return View();
        }
        public ActionResult PurchaseReturnAuthorization()
        {
            return View();
        }
        public ActionResult PurchaseOrderReport()
        {
            return PartialView();
        }
        public ActionResult PendingPurchaseIndent()
        {
            return PartialView();
        }
        public ActionResult POClose()
        {
            return PartialView();
        }
        public ActionResult RequestForQuote()
        {
            return PartialView();
        }
        public ActionResult RFQAuthorization()
        {
            return PartialView();
        }
        public ActionResult WorkOrder()
        {
            return PartialView();
        }
        public ActionResult WOAuthorization()
        {
            return PartialView();
        }
        public ActionResult PRStatus()
        {
            return PartialView();
        }
        public ActionResult POStatus()
        {
            return PartialView();
        }
        public async Task<ActionResult> RptPurchase()
        {
            int MenuId = 170;
            List<ReportListModel> model = new List<ReportListModel>();
            string url = this.url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, url, CancellationToken.None);
            return View(model);
        }

     
    }
}
