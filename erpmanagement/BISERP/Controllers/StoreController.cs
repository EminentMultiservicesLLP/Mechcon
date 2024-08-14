using BISERP.Models;
using BISERP.Areas.Store.Models.Store;
using BISERP.Areas.Store.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using BISERP.Models.Common;
using BISERP.Models.UserMangement;
//using log4net.Core;
using BISERP.Areas.Masters.Models;
using BISERP.Filters;

namespace BISERP.Controllers.Store
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class StoreController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(StoreController));

        public StoreController()
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
        public async Task<ActionResult> Store(int MenuId)
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

        public ActionResult StoreMaster()
        {
            return PartialView();
        }

        public ActionResult Indent()
        {
            return View();
        }

        //public ActionResult SupplierMaster()
        //{
        //    return PartialView();
        //}

        public ActionResult StoreMasters()
        {
            return View();
        }

        public ActionResult CommonGeneralInfo()
        {
            return PartialView();
        }

        public ActionResult MaterialIndent()
        {
            return PartialView();
        }
        public ActionResult MaterialTransfer()
        {
            return PartialView();
        }
        public ActionResult IndentAuthorization()
        {
            return PartialView();
        }
        public ActionResult IndentVerification()
        {
            return PartialView();
        }
        public ActionResult MaterialIssue()
        {
            return PartialView();
        }
        public ActionResult MaterialIssueAuthorization()
        {
            return PartialView();

        }
        public ActionResult MaterialAcceptance()
        {
            return PartialView();
        }
        public ActionResult VendorIssue()
        {
            return PartialView();
        }
        public ActionResult VendorIssueAuthorization()
        {
            return PartialView();
        }
        public ActionResult GRN()
        {
            return PartialView();
        }
        public ActionResult GRNAuthorization()
        {
            return PartialView();
        }
        public ActionResult PurchaseReturn()
        {
            return PartialView();
        }
        public ActionResult PurchaseReturnAuthorization()
        {
            return PartialView();
        }
        public ActionResult MaterialReturn()
        {
            return PartialView();
        }
        public ActionResult CreatePurchaseIndent()
        {
            return PartialView();
        }
        public ActionResult CreatePurchaseIndentAuthorization()
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
        public ActionResult StockDispose()
        {
            return PartialView();
        }
        public ActionResult StoreConsumption()
        {
            return PartialView();
        }
        public ActionResult StoreConsumptionCancellation()
        {
            return PartialView();
        }
        public ActionResult StoreQuantityLimits()
        {
            return PartialView();
        }
        public ActionResult AutoIndent()
        {
            return PartialView();
        }
        public ActionResult OpeningBalance()
        {
            return PartialView();
        }
        public ActionResult OutWardGatepass()
        {

            return PartialView();
        }
        public ActionResult CancelPendingMaterialIndent()
        {
            return PartialView();
        }
        public ActionResult GRNSummary()
        {
            return PartialView();
        }
        public ActionResult StockView()
        {
            return PartialView();
        }
        public ActionResult StockRegisterView()
        {
            return PartialView();
        }

        public async Task<ActionResult> Masters(int MenuId)
        {
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

        public ActionResult UnitMaster()
        {
            return PartialView();
        }

        public ActionResult ItemTypeMaster()
        {
            return PartialView();
        }

        public ActionResult ItemPackSizeMaster()
        {
            return PartialView();
        }

        public async Task<ActionResult> ManufacturerMaster()
        {
            ManufacturerMasterModel m = new ManufacturerMasterModel();

            string _url = url + "/generic/getallcitymasters" + Common.Constants.JsonTypeResult;
            List<CityMasterModel> _cities = await Common.AsyncWebCalls.GetAsync<List<CityMasterModel>>(client, _url, CancellationToken.None);

            _url = url + "/generic/getallvillagemasters" + Common.Constants.JsonTypeResult;
            List<VillageMasterModel> _villages = await Common.AsyncWebCalls.GetAsync<List<VillageMasterModel>>(client, _url, CancellationToken.None);

            _url = url + "/generic/getallstatemasters" + Common.Constants.JsonTypeResult;
            List<StateMasterModel> _states = await Common.AsyncWebCalls.GetAsync<List<StateMasterModel>>(client, _url, CancellationToken.None);

            _url = url + "/generic/getallcountrymasters" + Common.Constants.JsonTypeResult;
            List<CountryMasterModel> _countries = await Common.AsyncWebCalls.GetAsync<List<CountryMasterModel>>(client, _url, CancellationToken.None);

            if (_cities != null)
                m.Cities = _cities;

            if (_villages != null)
                m.Villages = _villages;

            if (_states != null)
                m.States = _states;

            if (_countries != null)
                m.Countries = _countries;

            return PartialView(m);
        }
        public ActionResult SupplierMaster()
        {
            return PartialView();
        }
        public ActionResult DeliveryTermMaster()
        {
            return PartialView();
        }

        public ActionResult PaymentTermMaster()
        {
            return PartialView();
        }

        public ActionResult OtherTermMaster()
        {
            return PartialView();
        }

        public ActionResult ItemMaster()
        {
            return PartialView();
        }

        public ActionResult IndentTemplateMaster()
        {
            return PartialView();
        }

        public ActionResult StorewiseItemTypeMappingMaster()
        {
            return PartialView();
        }
        public ActionResult PrefixMaster()
        {
            return PartialView();
        }
        public ActionResult BranchMaster()
        {
            return PartialView();
        }
        public ActionResult StoreUnitLinking()
        {
            return PartialView();
        }
        public ActionResult MaterialReturnAuthorization()
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
        public async Task<ActionResult> MIndentRpt()
        {
            int MenuId = 76;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }
        public async Task<ActionResult> MIssueRpt()
        {
            int MenuId = 77;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }
        public async Task<ActionResult> MReturnRpt()
        {
            int MenuId = 81;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }
        public async Task<ActionResult> PReturnRpt()
        {
            int MenuId = 82;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }
        public async Task<ActionResult> GRNRpt()
        {
            int MenuId = 80;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }
        public async Task<ActionResult> DiscrepancyRpt()
        {
            int MenuId = 86;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }
        public async Task<ActionResult> DisposeRpt()
        {
            int MenuId = 85;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }
        public async Task<ActionResult> StockRpt()
        {
            int MenuId = 83;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }
        public async Task<JsonResult> GetFlashDetails(string menuId)
        {
            JsonResult jResult;
            List<FlashEntity> data = new List<FlashEntity>();
            try
            {
                string _url = url + "flash/getFlash/1/1";
                data = await Common.AsyncWebCalls.GetAsync<List<FlashEntity>>(client, _url, CancellationToken.None);
                if (data != null)
                {
                    //if (!string.IsNullOrWhiteSpace(searchIssueNumber))
                    //{
                    //    mimodel = mimodel.Where(p => p.IndentNo.ToUpper().Contains(searchIssueNumber.ToUpper())).ToList();
                    //}
                    //mimodel = mimodel.OrderByDescending(m => m.Indent_Id).ToList();
                    jResult = Json(new { data }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in IndentForVerification :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
