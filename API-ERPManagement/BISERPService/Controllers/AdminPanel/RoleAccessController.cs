using BISERPBusinessLayer.Entities.UserManagement;
using BISERPBusinessLayer.Repositories.AdminPanel.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.AdminPanel
{
    [RoutePrefix("api/RoleAccess")]
    public class RoleAccessController : ApiController
    {
        IRoleAccessRepository _RoleAccess;
        private static readonly ILogger _loggger = Logger.Register(typeof(RoleAccessController));
        public RoleAccessController(IRoleAccessRepository RoleAccess)
        {
            _RoleAccess = RoleAccess;

        }

        //[Route("SaveRoleAccess")]
        //[AcceptVerbs("GET", "POST")]
        //public IHttpActionResult SaveRoleAccess(UserMenuRightsEntity Items)
        //{
        //    bool isSuccess = false, isDuplicate = false; ;
        //    TryCatch.Run(() =>
        //    {
        //        isDuplicate = _RoleAccess.CheckDuplicateItem(Items.RoleId, Items.RoleName);
        //        if (isDuplicate == false)
        //        {
        //            var list = _RoleAccess.SaveRoleAccess(Items);
        //            isSuccess = true;
        //        }


        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in SaveUser method of RoleAccessController :" + Environment.NewLine + ex.StackTrace);
        //        return InternalServerError();
        //    });
        //    if (isDuplicate)
        //        return BadRequest("Role Name Already Exists");
        //    else if (isSuccess)
        //        return Created<UserMenuRightsEntity>(Request.RequestUri + Items.ToString(), Items);
        //    else
        //        return BadRequest();
        //}
        //[Route("SaveUserAccess")]
        //[AcceptVerbs("GET", "POST")]
        //public IHttpActionResult SaveUserAccess(UserMenuRightsEntity Items)
        //{
        //    bool isSuccess = false, isDuplicate = false; ;
        //    TryCatch.Run(() =>
        //    {
        //        if (isDuplicate == false)
        //        {
        //            var list = _RoleAccess.SaveUserAccess(Items);
        //            isSuccess = true;
        //        }


        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in SaveUser method of RoleAccessController :" + Environment.NewLine + ex.StackTrace);
        //        return InternalServerError();
        //    });
        //    if (isDuplicate)
        //        return BadRequest("Unit Store Already Linked To Other Store");
        //    else if (isSuccess)
        //        return Created<UserMenuRightsEntity>(Request.RequestUri + Items.ToString(), Items);
        //    else
        //        return BadRequest();
        //}

        //[Route("DeleteRole/{roleId}")]
        //[AcceptVerbs("POST")]
        //public IHttpActionResult DeleteRole(int roleId)
        //{
        //    bool isSuccess = false, isDuplicate = false; ;
        //    TryCatch.Run(() =>
        //    {
        //        var list = _RoleAccess.DeleteRole(roleId);
        //        isSuccess = true;
        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in DeleteUser method of RoleAccessController :" + Environment.NewLine + ex.StackTrace);
        //        return InternalServerError();
        //    });

        //    if (isSuccess)
        //        return Ok();
        //    else
        //        return BadRequest();
        //}

        //[Route("GetRoleList")]
        //[AcceptVerbs("GET")]
        //public IHttpActionResult GetRoleList()
        //{
        //    List<Role> result = new List<Role>();
        //    TryCatch.Run(() =>
        //    {
        //        var list = _RoleAccess.GetRoleList();
        //        if (list != null)
        //            result = list.ToList();
        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in GetRoleList method of RoleAccessController :" + Environment.NewLine +
        //                          ex.StackTrace);
        //        return InternalServerError();
        //    });
        //    return Ok(result.ToList());
        //}

        //[Route("GetMenuByRole/{RoleId}")]
        //[AcceptVerbs("GET")]
        //public IHttpActionResult GetMenuByRole(int RoleId)
        //{
        //    List<ParentMenuRights> result = new List<ParentMenuRights>();
        //    TryCatch.Run(() =>
        //    {
        //        var list = _RoleAccess.GetMenuByRole(RoleId);
        //        if (list != null)
        //            result = list.ToList();
        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in GetMenuByRole method of RoleAccessController :" + Environment.NewLine +
        //                          ex.StackTrace);
        //        return InternalServerError();
        //    });
        //    return Ok(result.ToList());
        //}

        [Route("GetUsers")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetUsers()
        {
            List<MenuRole> result = new List<MenuRole>();
            TryCatch.Run(() =>
            {
                var list = _RoleAccess.GetUsers();
                if (list != null)
                    result = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetUsers method of RoleAccessController :" + Environment.NewLine +
                                  ex.StackTrace);
                return InternalServerError();
            });
            return Ok(result.ToList());
        }

        //[Route("GetMenuByUser/{UserId}")]
        //[AcceptVerbs("GET")]
        //public IHttpActionResult GetMenuByUser(int UserId)
        //{
        //    List<ParentMenuRights> result = new List<ParentMenuRights>();
        //    TryCatch.Run(() =>
        //    {
        //        var list = _RoleAccess.GetMenuByUser(UserId);
        //        if (list != null)
        //            result = list.ToList();
        //    }).IfNotNull(ex =>
        //    {
        //        _loggger.LogError("Error in GetMenuByUser method of RoleAccessController :" + Environment.NewLine +
        //                          ex.StackTrace);
        //        return InternalServerError();
        //    });
        //    return Ok(result.ToList());
        //}

        //---------------------------------new 2024

        [Route("GetParentMenu")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetParentMenu()
        {
            List<RoleAccess> result = new List<RoleAccess>();
            TryCatch.Run(() =>
            {
                var list = _RoleAccess.GetParentMenu();
                if (list != null)
                    result = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetParentMenu method of RoleAccessController :" + Environment.NewLine +
                                  ex.StackTrace);
                return InternalServerError();
            });
            return Ok(result.ToList());
        }

        [Route("GetUserAccess/{UserId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetUserAccess(int UserId)
        {
            List<UserMenuAccess> result = new List<UserMenuAccess>();
            TryCatch.Run(() =>
            {
                var list = _RoleAccess.GetUserAccess(UserId);
                if (list != null)
                    result = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetUserAccess method of RoleAccessController :" + Environment.NewLine +
                                  ex.StackTrace);
                return InternalServerError();
            });
            return Ok(result.ToList());
        }

        [Route("UserMenuAccess")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult UserMenuAccess(UserFormAccess Items)
        {
            bool isSuccess = false, isDuplicate = false; ;
            TryCatch.Run(() =>
            {
                if (isDuplicate == false)
                {
                    var list = _RoleAccess.UserMenuAccess(Items);
                    isSuccess = true;
                }

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UserMenuAccess method of RoleAccessController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Unit Store Already Linked To Other Store");
            else if (isSuccess)
                return Created<UserFormAccess>(Request.RequestUri + Items.ToString(), Items);
            else
                return BadRequest();
        }
    }
}
