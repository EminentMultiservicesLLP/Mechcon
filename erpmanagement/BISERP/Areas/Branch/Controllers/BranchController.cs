using BISERP.Area.Branch.Models;
using BISERP.Areas.Branch.Models;
using BISERP.Areas.Store.Models.Store;
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

namespace BISERP.Areas.Branch.Controllers
{
    public class BranchController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(BranchController));
        public BranchController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        public PartialViewResult GuardList()
        {
            return PartialView();
        }

        public PartialViewResult GuardIssueList()
        {
            return PartialView();
        }
        public PartialViewResult GuardDetails()
        {
            return PartialView();
        }
        public PartialViewResult PettyCashRegister()
        {
            return PartialView();
        }
        public async Task<PartialViewResult> IssueReceipt()
        {
            int MenuId = 87;
            List<ReportListModel> model = new List<ReportListModel>();
            string _url = url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(client, _url, CancellationToken.None);

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Branch(int MenuId)
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

        public ViewResult GuardIssueReturn(int EmpId)
        {
            List<StoreMenuModel> smenu = new List<StoreMenuModel>();
            string strViewname = "GuardIssueReturn";
            smenu = new List<StoreMenuModel>() { new StoreMenuModel { MenuId = 101, MenuName = "Material Issue Guard", MenuAction = "MaterialIssueGuard" },
                                                        new StoreMenuModel { MenuId = 102, MenuName = "Material Return Guard", MenuAction = "MaterialReturnGuard" }};


            return View(strViewname, smenu);
        }
        public ViewResult MaterialIssueGuard(int EmpId, int brId)
        {
            List<EmployeeModel> employee = new List<EmployeeModel>();
            if (Session["EmpList"] != null)
            {
                employee = (List<EmployeeModel>)Session["EmpList"];
            }

            var Emplist = (from N in employee
                           where N.EmpId.Equals(EmpId)
                           select N).FirstOrDefault();

            MaterialIssueGuardModel mig = new MaterialIssueGuardModel();
            mig.EmpId = Emplist.EmpId;
            mig.BranchId = Emplist.BranchId;
            mig.EmpName = Emplist.EmployeeName;
            mig.TicketCode = Emplist.TicketCode;
            return View(mig);
        }

        public ViewResult GuardRecruitedDetails(int EmpId, int brId)
        {
            List<EmployeeModel> employee = new List<EmployeeModel>();
            if (Session["EmpList"] != null)
            {
                employee = (List<EmployeeModel>)Session["EmpList"];
            }

            var Emplist = (from N in employee
                           where N.EmpId.Equals(EmpId)
                           select N).FirstOrDefault();

            GuardInfoModel mig = new GuardInfoModel();
            GuardDetailsModel dt = new GuardDetailsModel();
            mig.BranchId = Emplist.BranchId;
            mig.EmpFName = Emplist.EmployeeName;
            mig.TicketCode = Emplist.TicketCode;
            mig.age = Emplist.Age;
            mig.Designation = Emplist.Designation;
            mig.strGender = Emplist.Gender;
            mig.GuardDt = dt;
            mig.GuardDt.EmpId = EmpId;

            return View(mig);
        }
        public ViewResult MomentOrder(int EmpId, int brId)
        {
            List<EmployeeModel> employee = new List<EmployeeModel>();
            if (Session["EmpList"] != null)
            {
                employee = (List<EmployeeModel>)Session["EmpList"];
            }

            var Emplist = (from N in employee
                           where N.EmpId.Equals(EmpId)
                           select N).FirstOrDefault();

            MomentOrderModel mig = new MomentOrderModel();
            mig.EmpId = Emplist.EmpId;
            mig.BranchId = Emplist.BranchId;
            mig.EmpName = Emplist.EmployeeName;
            mig.TicketCode = Emplist.TicketCode;
            return View(mig);
        }
        public ViewResult MaterialReturnGuard(int EmpId, int brId)
        {
            MaterialReturnGuardModel mrg = new MaterialReturnGuardModel();
            mrg.EmpId = EmpId;
            //mrg.EmpName = EmpName;
            mrg.BranchId = brId;
            return View(mrg);
        }

        [HttpGet]
        public async Task<JsonResult> AllGuardList()
        {
            JsonResult jResult;
            List<EmployeeModel> employee = new List<EmployeeModel>();
            try
            {
                if (Session["EmpList"] == null)
                {
                    string _url = url + "/employee/getemployee/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                    employee = await Common.AsyncWebCalls.GetAsync<List<EmployeeModel>>(client, _url, CancellationToken.None);
                    if (employee != null)
                    {
                        Session["EmpList"] = employee;
                        jResult = Json(Session["EmpList"], JsonRequestBehavior.AllowGet);
                    }
                    else
                        jResult = Json(employee, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jResult = Json(Session["EmpList"], JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllGuardList :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> RefreshGuardList()
        {
            JsonResult jResult;
            List<EmployeeModel> employee = new List<EmployeeModel>();
            try
            {
                string _url = url + "/employee/getemployee/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                employee = await Common.AsyncWebCalls.GetAsync<List<EmployeeModel>>(client, _url, CancellationToken.None);
                if (employee != null)
                {
                    Session["EmpList"] = employee;
                    jResult = Json(Session["EmpList"], JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(employee, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in RefreshGuardList :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpPost]
        public JsonResult SearchTicketCode(string ticketcode)
        {
            List<EmployeeModel> employee = new List<EmployeeModel>();
            if (Session["EmpList"] != null)
            {
                employee = (List<EmployeeModel>)Session["EmpList"];
            }
            //Searching records from list using LINQ query  
            var EmpName = (from N in employee
                           where N.TicketCode.StartsWith(ticketcode)
                           select N);
            return Json(EmpName, JsonRequestBehavior.AllowGet);
        }
    }
}
