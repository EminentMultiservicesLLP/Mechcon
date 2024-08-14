using BISERP.Areas.AdminPanel.Models;
using BISERP.Common;
using BISERP.Filters;
using BISERP.Models.UserMangement;
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
using ParentMenuRights = BISERP.Models.UserMangement.ParentMenuRights;

namespace BISERP.Areas.AdminPanel.Controllers
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class RoleAccessController : Controller
    {
        readonly HttpClient _client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static readonly BISERPCommon.ILogger Logger = BISERPCommon.Logger.Register(typeof(RoleAccessController));

        public RoleAccessController()
        {
            _client = new HttpClient { BaseAddress = new Uri(url) };
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        //[HttpGet]
        //public async Task<JsonResult> GetMenuDetails()
        //{
        //    JsonResult jResult = null;
        //    Session["rolemenusession"] = null;

        //    try
        //    {
        //        var records = Session["UserMenu"] as List<MenuUserRightsModel>;
        //        if (records != null)
        //        {
        //            var allMenus =
        //                records.Select(
        //                    s =>
        //                        new ParentMenuRights
        //                        {
        //                            MenuName = s.MenuName,
        //                            ParentMenuId = s.ParentMenuId,
        //                            MenuId = s.MenuId,
        //                            State = s.State,
        //                            PageName = s.PageName,
        //                            Access = s.Access,
        //                            UserId = s.UserId
        //                        }).ToList();
        //            Session["rolemenusession"] = allMenus;

        //            records = records.Where(m => m.ParentMenuId == 0).ToList();
        //            jResult = Json(records, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError("Error in GetMenuDetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
        //        jResult = Json("Error");
        //    }
        //    return jResult;
        //}
        //[HttpGet]
        //public async Task<JsonResult> GetSubMenuDetails(int menuid)
        //{
        //    JsonResult jResult;

        //    try
        //    {

        //        var records = Session["UserMenu"] as List<MenuUserRightsModel>;
        //        records = records.Where(m => m.ParentMenuId == menuid).ToList();
        //        records.ForEach(m => m.SubParentMenuId = menuid);
        //        jResult = Json(records, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError("Error in GetSubMenuDetails :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
        //        jResult = Json("Error");
        //    }
        //    return jResult;
        //}

        //[HttpPost]
        //public async Task<JsonResult> SaveRoleAccess(MenuUserRightsModel accessData)
        //{
        //    string _url = "";
        //    bool _isSuccess = true;
        //    var records = Session["rolemenusession"] as List<ParentMenuRights>;
        //    accessData.Parent = records.Where(m => m.State == true).ToList();
        //    accessData.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
        //    accessData.InsertedON = System.DateTime.Now;
        //    accessData.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
        //    accessData.InsertedMacID = BISERP.Common.Constants.MacId;
        //    accessData.InsertedMacName = BISERP.Common.Constants.MacName;

        //    _url = url + "/RoleAccess/SaveRoleAccess" + Common.Constants.JsonTypeResult;
        //    var result = _client.PostAsync(_url, accessData, new JsonMediaTypeFormatter()).Result;
        //    if (result.StatusCode.ToString() == "BadRequest")
        //    {
        //        _isSuccess = false;
        //        accessData = JsonConvert.DeserializeObject<MenuUserRightsModel>(result.Content.ReadAsStringAsync().Result);
        //    }

        //    if (!_isSuccess)
        //        return Json(new { success = false, Message = accessData.Message });
        //    else
        //        return Json(new { success = true });
        //}

        //[HttpPost]
        //public async Task<JsonResult> SaveUserAccess(MenuUserRightsModel accessData)
        //{
        //    string _url = "";
        //    bool _isSuccess = true;
        //    var records = Session["rolemenusession"] as List<ParentMenuRights>;
        //    accessData.Parent = records.Where(m => m.State == true).ToList();
        //    accessData.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
        //    accessData.InsertedON = System.DateTime.Now;
        //    accessData.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
        //    accessData.InsertedMacID = BISERP.Common.Constants.MacId;
        //    accessData.InsertedMacName = BISERP.Common.Constants.MacName;

        //    _url = url + "/RoleAccess/SaveUserAccess" + Common.Constants.JsonTypeResult;
        //    var result = _client.PostAsync(_url, accessData, new JsonMediaTypeFormatter()).Result;
        //    if (result.StatusCode.ToString() == "BadRequest")
        //    {
        //        _isSuccess = false;
        //        accessData = JsonConvert.DeserializeObject<MenuUserRightsModel>(result.Content.ReadAsStringAsync().Result);
        //    }

        //    if (!_isSuccess)
        //        return Json(new { success = false, Message = accessData.Message });
        //    else
        //        return Json(new { success = true });
        //}

        //[HttpGet]
        //public async Task<JsonResult> GetRoleList()
        //{
        //    JsonResult jResult;
        //    try
        //    {
        //        string _url = url + "/RoleAccess/GetRoleList" + Common.Constants.JsonTypeResult;
        //        var records = await Common.AsyncWebCalls.GetAsync<List<BISERP.Models.UserMangement.Role>>(_client, _url, CancellationToken.None);
        //        jResult = records != null ? Json(new { data = records }, JsonRequestBehavior.AllowGet) : Json("Error");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError("Error in GetRoleList :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
        //        jResult = Json("Error");
        //    }
        //    return jResult;
        //}

        //[HttpGet]
        //public async Task<JsonResult> GetMenuByRole(int RoleId)
        //{
        //    Session["rolemenusession"] = null;

        //    JsonResult jResult;
        //    string _url = url + "/RoleAccess/GetMenuByRole/" + RoleId + Common.Constants.JsonTypeResult;
        //    var records = await Common.AsyncWebCalls.GetAsync<List<ParentMenuRights>>(_client, _url, CancellationToken.None);
        //    Session["rolemenusession"] = records;
        //    var distinctRoleAccess1 = records;

        //    var distinctRoleAccess = distinctRoleAccess1.Where(m => m.ParentMenuId == 0).ToList();
        //    jResult = distinctRoleAccess != null ? Json(new { data = distinctRoleAccess }, JsonRequestBehavior.AllowGet) : Json("Error");

        //    return jResult;
        //}

        //[HttpGet]
        //public async Task<JsonResult> GetMenuRoleByparentid(int menuid, bool state)
        //{
        //    JsonResult jResult;
        //    var records = Session["rolemenusession"] as List<ParentMenuRights>;
        //    var sessionRecords = records;
        //    records = records.Where(m => m.ParentMenuId == menuid).ToList();

        //    sessionRecords.Where(m => m.MenuId == menuid).ToList()[0].State = state;
        //    Session["rolemenusession"] = sessionRecords;
        //    jResult = Json(records, JsonRequestBehavior.AllowGet);

        //    return jResult;
        //}

        //[HttpPost]
        //public async Task<JsonResult> DeleteRole(int roleId)
        //{
        //    string _url = "";
        //    bool _isSuccess = true;
        //    _url = url + "/RoleAccess/DeleteRole/" + roleId + Common.Constants.JsonTypeResult;
        //    var result = _client.PostAsync(_url, _client, new JsonMediaTypeFormatter()).Result;
        //    if (result.StatusCode.ToString() == "BadRequest")
        //    {
        //        _isSuccess = false;
        //    }

        //    if (!_isSuccess)
        //        return Json(new { success = false });
        //    else
        //        return Json(new { success = true });
        //}

        [HttpGet]
        public async Task<JsonResult> GetUsers()
        {
            JsonResult jResult;
            Session["usermenusession"] = null;
            try
            {
                string _url = url + "/RoleAccess/GetUsers" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<MenuUserRightsModel>>(_client, _url, CancellationToken.None);
                // Session["usermenusession"] = records;
                jResult = records != null ? Json(new { data = records }, JsonRequestBehavior.AllowGet) : Json("Error");
                Session["prefixrecords"] = records;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetUsers :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        //[HttpGet]
        //public async Task<JsonResult> GetMenuByUser(int UserId)
        //{
        //    JsonResult jResult;
        //    Session["rolemenusession"] = null;

        //    string _url = url + "/RoleAccess/GetMenuByUser/" + UserId + Common.Constants.JsonTypeResult;
        //    var records = await Common.AsyncWebCalls.GetAsync<List<ParentMenuRights>>(_client, _url, CancellationToken.None);
        //    Session["rolemenusession"] = records;
        //    var distinctRoleAccess1 = records;

        //    var distinctRoleAccess = distinctRoleAccess1.Where(m => m.ParentMenuId == 0).ToList();
        //    jResult = distinctRoleAccess != null ? Json(new { data = distinctRoleAccess }, JsonRequestBehavior.AllowGet) : Json("Error");

        //    return jResult;
        //}

        //[HttpPost]
        //public Task<JsonResult> clearSession()
        //{
        //    JsonResult jResult;
        //    Session["usermenusession"] = null;
        //    Session["rolemenusession"] = null;
        //    return null;
        //}

        //[HttpGet]
        //public async Task<JsonResult> GetUserMenuByparentid(int menuid, bool state)
        //{
        //    JsonResult jResult;

        //    var records = Session["rolemenusession"] as List<ParentMenuRights>;
        //    var sessionRecords = records;
        //    records = records.Where(m => m.ParentMenuId == menuid).ToList();
        //    sessionRecords.Where(m => m.MenuId == menuid).ToList()[0].State = state;
        //    Session["rolemenusession"] = sessionRecords;
        //    jResult = Json(records, JsonRequestBehavior.AllowGet);
        //    return jResult;
        //}

        //---------------------------------new 2024

        #region GetParentMenu ;
        [HttpGet]
        public async Task<JsonResult> GetParentMenu()
        {
            JsonResult jResult;
            string _url = url + "/RoleAccess/GetParentMenu/" + Common.Constants.JsonTypeResult;
            var records = await Common.AsyncWebCalls.GetAsync<List<RoleAccess>>(_client, _url, CancellationToken.None);
            jResult = records != null ? Json(new { success = true, message = "", data = records }, JsonRequestBehavior.AllowGet) : Json(new { success = false, message = "Failed to retrieve Parent menu" }, JsonRequestBehavior.AllowGet);

            return jResult;
        }
        #endregion

        #region GetUserAccess ;
        [HttpGet]
        public async Task<JsonResult> GetUserAccess(int UserId)
        {
            JsonResult jResult;
            string _url = url + "/RoleAccess/GetUserAccess/" + UserId + Common.Constants.JsonTypeResult;
            var records = await Common.AsyncWebCalls.GetAsync<List<UserMenuAccess>>(_client, _url, CancellationToken.None);

            jResult = records != null ? Json(new { success = true, message = "", data = records }, JsonRequestBehavior.AllowGet) : Json(new { success = false, message = "Failed to retrieve UserAccess" }, JsonRequestBehavior.AllowGet);
            return jResult;
        }
        #endregion

        #region UserMenuAccess ;
        [HttpPost]
        public async Task<JsonResult> UserMenuAccess(UserFormAccess accessData)
        {
            string _url = "";
            bool _isSuccess = true;
            accessData.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            accessData.InsertedON = System.DateTime.Now;
            _url = url + "/RoleAccess/UserMenuAccess" + Common.Constants.JsonTypeResult;
            var result = _client.PostAsync(_url, accessData, new JsonMediaTypeFormatter()).Result;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                _isSuccess = false;
                accessData = JsonConvert.DeserializeObject<UserFormAccess>(result.Content.ReadAsStringAsync().Result);
            }

            if (!_isSuccess)
                return Json(new { success = false, message = accessData.Message });
            else
                return Json(new { success = true , message = "Menu Rights Saved Successfully" });
        }
        #endregion
    }
}
