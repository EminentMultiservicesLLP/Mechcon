using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERP.Filters;
using BISERP.Models.UserMangement;
using BISERPCommon;

namespace BISERP.Areas.Accounts.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class AccountsController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _url = Common.Constants.WebAPIAddress;
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(AccountsController));

        public AccountsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        public ActionResult Accounts(int MenuId)
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
            }).Where(m => m.ParentMenuId.Equals(163)).ToList();
            if (MenuId > 163)
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

        public PartialViewResult ClientBilling()
        {
            return PartialView();
        }
        public PartialViewResult ClientBillReciept()
        {
            return PartialView();
        }
        public PartialViewResult ClientBillCancel()
        {
            return PartialView();
        }
        public PartialViewResult ClientBillStatus()
        {
            return PartialView();
        }

        public PartialViewResult ClientBillingDetails()
        {
            return PartialView();
        }
        public PartialViewResult SupplierBilling()
        {
            return PartialView();
        }
        public PartialViewResult SupplierSummarizedBill()
        {
            return PartialView();
        }
        public PartialViewResult VendorBilling()
        {
            return PartialView();
        }
        public PartialViewResult VendorSummarizedBill()
        {
            return PartialView();
        }

        public PartialViewResult Summary()
        {
            return PartialView();
        }
        public PartialViewResult TaxCredited()
        {
            return PartialView();
        }
        public PartialViewResult TaxPaid()
        {
            return PartialView();
        }
        public async Task<PartialViewResult> Reports()
        {
            int MenuId = 188;
            List<ReportListModel> model = new List<ReportListModel>();
            string url = this._url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(_client, url, CancellationToken.None);
            return PartialView(model);
        }
    }
}
