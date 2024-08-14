using BISERPBusinessLayer.Entities.UserManagement;
using BISERPBusinessLayer.Repositories.UserManagement.Interface;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPService.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/user")]
    public class UserPermissionController : ApiController
    {
        IUserMenuRightsRepository _userMenu;
        IUserDeparment _userDept;
        private static readonly ILogger _loggger = Logger.Register(typeof(UserPermissionController));

        public UserPermissionController(IUserMenuRightsRepository userMenu, IUserDeparment userDept)
        {
            _userMenu = userMenu;
            _userDept = userDept;
        }

        [Route("getusermenu/{UserId}/{ParentMenuId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMenuRights(int UserId, int ParentMenuId)
        {
            List<UserMenuRightsEntity> menu = new List<UserMenuRightsEntity>();
            TryCatch.Run(() =>
            {
                var list = _userMenu.GetAllMenuRights(UserId, ParentMenuId);
                if (list != null && list.Count() > 0)
                    menu = list.ToList();

                //var list = GetAllMenuRights();
                //menu = list.Where(w => w.UserId == UserId && w.ParentMenuId == ParentMenuId).ToList();
                //menu = list.Where(w => w.ParentMenuId > 0 && ).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMenuRights method of UserPermissionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (menu.Any())
                return Ok(menu);
            else
                return Ok(menu);
        }

        private List<UserMenuRightsEntity> GetAllMenuRights()
        {
            List<UserMenuRightsEntity> rights = new List<UserMenuRightsEntity>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.AllUserMenuRights.ToString()))
                {
                    var list = _userMenu.GetAllMenuRights();
                    if (list != null && list.Count() > 0)
                        rights = list.ToList();

                    MemoryCaching.AddCacheValue(CachingKeys.AllUserMenuRights.ToString(), rights);
                }
                else
                {
                    rights = (List<UserMenuRightsEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllUserMenuRights.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMenuRights method of ItemMasterController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return rights;
        }

        [Route("getuserbranch/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetUserAccessBranch(int UserId)
        {
            List<UserDepartment> dept = new List<UserDepartment>();
            TryCatch.Run(() =>
            {
                var list = _userDept.GetUserAccessBranch(UserId);
                if (list != null && list.Count() > 0)
                    dept = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetUserAccessBranch method of UserPermissionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (dept.Any())
                return Ok(dept);
            else
                return BadRequest();
        }

        [Route("getuserstore/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetUserAccessStore(int UserId)
        {
            List<UserDepartment> dept = new List<UserDepartment>();
            TryCatch.Run(() =>
            {
                var list = _userDept.GetUserAccessStore(UserId);
                if (list != null && list.Count() > 0)
                    dept = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetUserAccessStore method of UserPermissionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (dept.Any())
                return Ok(dept);
            else
                return BadRequest();
        }

        [Route("getuserunitstore/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetUserAccessUnitStore(int UserId)
        {
            List<UserDepartment> dept = new List<UserDepartment>();
            TryCatch.Run(() =>
            {
                var list = _userDept.GetUserAccessUnitStore(UserId);
                if (list != null && list.Count() > 0)
                    dept = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetUserAccessUnitStore method of UserPermissionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (dept.Any())
                return Ok(dept);
            else
                return BadRequest();
        }

        [Route("getuserreports/{UserId}/{ReportGroupId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetReportList(int UserId, int ReportGroupId)
        {
            List<ReportListEntity> reports = new List<ReportListEntity>();
            TryCatch.Run(() =>
            {
                var list = _userMenu.GetReportList(UserId, ReportGroupId);
                if (list != null && list.Count() > 0)
                    reports = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMenuRights method of UserPermissionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (reports.Any())
                return Ok(reports);
            else
                return Ok(reports);
        }

        [Route("saveManuSettings")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult saveManuSettings(UserMenuRightsEntity Items)
        {
            bool isSuccess = false, isDuplicate = false; ;
            TryCatch.Run(() =>
            {
                if (isDuplicate == false)
                {
                    var list = _userMenu.saveManuSettings(Items);
                    isSuccess = true;
                }

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in saveManuSettings method of UserPermissionController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Unit Store Already Linked To Other Store");
            else if (isSuccess)
                return Created<UserMenuRightsEntity>(Request.RequestUri + Items.ToString(), Items);
            else
                return BadRequest();
        }
    }
}
