using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using System.Net.Http;
using System.Threading.Tasks;
using BISERP.Areas.Training.Models;
using System.Threading;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using BISERP.Models.UserMangement;
using BISERP.Areas.Training.Models;
namespace BISERP.Areas.Training.Controllers
{
    public class TrainingController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(TrainingController));

        public TrainingController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        public ActionResult Training(int MenuId)
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
            }).Where(m => m.ParentMenuId.Equals(8)).ToList();
            if (MenuId > 8)
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
            }).Where(m => m.ParentMenuId.Equals(8)).ToList();
            if (MenuId > 8)
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

        public ActionResult TrainingType()
        {
            return PartialView();
        }

        public ActionResult BatchType()
        {
            return PartialView();
        }

        public ActionResult Subject()
        {
            return PartialView();
        }
        public ActionResult SubjectTopic()
        {
            return PartialView();
        }
        public ActionResult Trainer()
        {
            return PartialView();
        }
        public ActionResult BatchMaster()
        {
            return PartialView();
        }

        public ActionResult OrderAcceptance()
        {
            return PartialView();
        }

        public ActionResult TrainingSchedule()
        {
            return PartialView();
        }
        public ActionResult PettyCashRegister()
        {
            return PartialView();
        }

        public ActionResult TemplateMaster()
        {
            return PartialView();
        }

        public ActionResult Day()
        {
            return PartialView();
        }

        public ActionResult TrainingCategory()  
        {
            return PartialView();
        }
        public ActionResult Rating()
        {
            return PartialView();
        }
        //public ActionResult Slot() 
        //{
        //    return PartialView();
        //}

        public ActionResult MonthlyExpenditure()
        {
            return PartialView();
        }

        //[HttpGet]
        //public ActionResult SlotTemplate(int SlotId)
        //{
        //    TrainingTempHeaderModel model = new TrainingTempHeaderModel();
        //    model.BatchId = SlotId;
        //    return PartialView(model);
        //}

        public ActionResult BudgetHeads()
        {
            return PartialView();
        }
        public ActionResult TrainingTemplate() 
        {
            return PartialView();
        }

        public ActionResult GeneralBudgetProposal()
        {
            return PartialView();
        }

        public ActionResult TrainingDaillyUpdates()
        {
            return PartialView();
        }

        public ActionResult GeneralBudgetAuthorization()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllBatch()
        {
            List<BatchModel> records = new List<BatchModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/batch/GetAllBatch" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<BatchModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllBatch :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
      
        [HttpGet]
        public async Task<JsonResult> GetAllTrainingCentre()
        {
            List<TrainingCentreModel> records = new List<TrainingCentreModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/TrainingCent/GetAllTrainingCentre/" + Session["AppUserId"].ToString() + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<TrainingCentreModel>>(client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllTrainingCentre :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
        
