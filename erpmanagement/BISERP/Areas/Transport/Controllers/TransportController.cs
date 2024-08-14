using BISERP.Areas.Transport.Models.Master;
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

namespace BISERP.Areas.Transport.Controllers
{
    public class TransportController : Controller
    {
        
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(TransportController));

        public TransportController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        public ActionResult Transport(int MenuId)
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

        public ActionResult Division()
        {
            return PartialView();
        }

        public ActionResult SubDivision()
        {
            return PartialView();
        }

        public ActionResult VehicleType()
        {
            return PartialView();
        }

        public ActionResult VehicleModel()
        {
            return PartialView();
        }

        public ActionResult VehicleUsage()
        {
            return PartialView();
        }

        public ActionResult VehicleMaKe()
        {
            return PartialView();
        }
        public ActionResult Labor()
        {
            return PartialView();
        }

        public ActionResult Insurance()
        {
            return PartialView();
        }

        public ActionResult RoadTax()
        {
            return PartialView();
        }

        public ActionResult PermitDetails()
        {
            return PartialView();
        }

        public ActionResult GreenTax()
        {
            return PartialView();
        }

        public ActionResult PUCDetails()
        {
            return PartialView();
        }

        public ActionResult Notification()
        {
            return PartialView();
        }

        public ActionResult VehicleTransfer()
        {
            return PartialView();
        }

        public ActionResult TransferAuthorization()
        {
            return PartialView();
        }

        public ActionResult VehicleInfo()
        {
            return PartialView();
        }

        public ActionResult ScheduleTrip()
        {
            return PartialView();
        }

        public async Task<ActionResult> VehicleInforpt()
        {
            int MenuId = 104;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }

        public async Task<ActionResult> ScheduleTriprpt()
        {
            int MenuId = 113;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }

        public async Task<ActionResult> VehicleTransferrpt()
        {
            int MenuId = 114;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }

        [HttpGet]
        public async Task<JsonResult> AllDrivers(int branchId)
        {
            List<DriverModel> records = new List<DriverModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/driver/Getdriverlist/" + branchId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<DriverModel>>(client, _url, CancellationToken.None);
                jResult = Json( records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllDrivers :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
        [HttpGet]
        public async Task<JsonResult> AllDriverschedule(int branchId)
        {
            //List<DriverModel> records = new List<DriverModel>();
            JsonResult jResult;
            try
            {

                string url = "/driver/getdriverlistSchedule/" + branchId;
                var records = await Common.AsyncWebCalls.GetAsync<List<DriverModel>>(url, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllDriverschedule :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
