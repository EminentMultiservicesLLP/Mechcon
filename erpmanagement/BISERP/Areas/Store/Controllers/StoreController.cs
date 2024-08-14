using BISERP.Models.Common;
//using BISERP.Areas.Store.Models.Master;
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
using BISERP.Areas.Masters.Models;
//using BISERP.Models.Report;

namespace BISERP.Areas.Store.Controllers
{
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
        //public async Task<ActionResult> Store(int MenuId)
        //{
        //    if (Session["AppUserId"] == null)
        //    {
        //        return RedirectToAction("Login", "Account", new { area = "" });
        //    }
        //    MenuUserRightsModel model = new MenuUserRightsModel();
        //    List<MenuUserRightsModel> menurights = (List<MenuUserRightsModel>)Session["UserMenu"];

        //    model.Parent = menurights.Select(row => new ParentMenuRights
        //    {
        //        MenuId = row.MenuId,
        //        UserId = row.UserId,
        //        Access = row.Access,
        //        MenuName = row.MenuName,
        //        PageName = row.PageName,
        //        ParentMenuId = row.ParentMenuId
        //    }).Where(m => m.ParentMenuId.Equals(4)).ToList();
        //    if (MenuId > 4)
        //    {
        //        model.Child = menurights.Select(row => new ChildMenuRights
        //                    {
        //                        MenuId = row.MenuId,
        //                        UserId = row.UserId,
        //                        Access = row.Access,
        //                        MenuName = row.MenuName,
        //                        PageName = row.PageName,
        //                        ParentMenuId = row.ParentMenuId
        //                    }).Where(m => m.ParentMenuId.Equals(MenuId)).ToList();
        //    }            
        //    return View(model);
        //}

        public ActionResult Reports()
        {
            return PartialView();
        }

        public ActionResult Indent()
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

        public ActionResult MaterialReturnList()
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
        public ActionResult SupplierBillList()
        {
            return PartialView();
        }

        public ActionResult DashBoard()
        {
            return PartialView();
        }

        public ActionResult RequestStatus()
        {
            return PartialView();
        }
        public ActionResult MaterialReturn()
        {
            return RedirectToAction("MaterialReturn", "Miscellaneous", new { area = "Miscellaneous" });
        }

        public ActionResult MaterialReturnAuthorization()
        {
            return RedirectToAction("MaterialReturnAuthorization", "Miscellaneous", new { area = "Miscellaneous" });
        }

        public ActionResult Masters(int MenuId)
        {
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
      

        public ActionResult IndentTemplateMaster()
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
        public ActionResult GRNVendor()
        {
            return PartialView();
        }
        public ActionResult GRNVendorAuthorization()
        {
            return PartialView();
        }
        public ActionResult BillCreation()
        {
            return PartialView();
        }
        [HttpGet]
        public async Task<JsonResult> GetUserReports(int MenuId)
        {
            JsonResult jResult;
         
            try
            {
            List<ReportListModel> model = new List<ReportListModel>();
                string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
                //model1.ReportList = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);
                model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);
                if(model != null)
                {
                    jResult = Json(new { model }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jResult = Json(new { model }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Others Reports :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        public async Task<ActionResult> MIndentRpt()
        {
            int MenuId = 76;
            //ReportInfo model1 = new ReportInfo();
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            //model1.ReportList = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);
            model= await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);
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
            int MenuId = 78;
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
            int MenuId = 81;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }
        public async Task<ActionResult> DisposeRpt()
        {
            int MenuId = 82;
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

        public async Task<ActionResult> SupplierBillRpt()
        {
            int MenuId = 108;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }

        public async Task<ActionResult> InvoiceRpt()
        {
            int MenuId = 166;
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
                    jResult = Json(new { data }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetFlashDetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        public ActionResult BillPayment()
        {
            return PartialView();
        }
        //public ActionResult ReportTemplate(string ReportName, string ReportDescription, int Width, int Height)
        //{
        //    Dictionary<string, string> Dicparameteres1 = new Dictionary<string, string>();
        //    Dicparameteres1.Add("ID", "1");
        //    Session["Dicparameteres"] = Dicparameteres1;

        //    var rptInfo = new ReportInfo
        //    {

        //        ReportName = ReportName,
        //        ReportDescription = ReportDescription,
        //        ReportURL = String.Format("../../Reports/ReportTemplate.aspx?ReportName={0}&Height={1}", ReportName, Height),
        //        Width = Width,
        //        Height = Height
        //    };

        //    return PartialView("MIndentRpt", rptInfo);
        //}

        public ActionResult StockItemWiseDetails()
        {

            return View();
        }
    }
}
