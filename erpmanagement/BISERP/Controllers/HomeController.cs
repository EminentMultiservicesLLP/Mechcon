using BISERP.Common;
using BISERP.Filters;
using BISERP.Models.UserMangement;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class HomeController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(HomeController));

        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpGet]
        public async Task<ActionResult> Index(string SearchName, string SearchCode)
        {
            Session["EmpList"] = null;
            if (Session["AppUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<MenuUserRightsModel> records = new List<MenuUserRightsModel>();
            try
            {
                string _url = url + "/user/getusermenu/" + Session["AppUserId"].ToString() + "/0" + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<MenuUserRightsModel>>(client, _url, CancellationToken.None);
                //records = records.Where(m => m.Access.Equals(true)).ToList();
                Session["UserMenu"] = records;
                List<MenuUserRightsModel> menurights = (List<MenuUserRightsModel>)Session["UserMenu"];
                records = menurights.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Index of HomeController :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
            }
            return View(records);
        }

        public ActionResult Home()
        {
            return PartialView();
        }

        public ActionResult DashBoard(int UserId)
        {
            ModuleModel model = new ModuleModel();

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Keepalive()
        {
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        //new----------------2024

        #region UserMenuAccess ;
        [HttpPost]
        public async Task<JsonResult> SaveManuSettings(string headerCss, string sideBarCSS)
        {
            string _url = "";
            bool _isSuccess = true;
            MenuUserRightsModel uInfo = new MenuUserRightsModel()
            {
                UserHeaderCss = headerCss.Replace(".navbar-fixed-top", ""),
                UserSideBarMenuCSS = sideBarCSS.Replace(".app-sidebar", ""),
                UserId = Convert.ToInt32(Session["AppUserId"].ToString())
            };

            _url = url + "/user/saveManuSettings/" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, uInfo, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                uInfo = JsonConvert.DeserializeObject<MenuUserRightsModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, message = uInfo.Message });
            else
                return Json(new { success = true, message = "Changed Successfully" });
        }
        #endregion
    }
}
