using BISERPBusinessLayer.Entities.AdminPanel;
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
    [RoutePrefix("api/GroupMaster")]
    public class GroupMasterController : ApiController
    {
        IGroupMasterRepository _GroupMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(GroupMasterController));

        public GroupMasterController(IGroupMasterRepository GroupMaster)
        {
            _GroupMaster = GroupMaster;
        }

      
        [Route("saveGroupMaster")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult SaveGroupMaster(GroupMaster model)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var GroupMaster = _GroupMaster.SaveGroupMaster(model);
                model = GroupMaster;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in Save GroupMaster method of GroupMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<GroupMaster>(Request.RequestUri + model.GroupID.ToString(), model);
            else
                return BadRequest();
        }

        [Route("getGroupMaster")]
        [Route("getGroupMaster/{statusID}")]
        [HttpGet]
        public IHttpActionResult GetGroupMaster(int? statusID = null)
        {
            try
            {
                var data = _GroupMaster.GetGroupMaster(statusID);

                if (data == null)
                {
                    return NotFound(); // or return BadRequest("No data found for the specified GroupMaster ID.");
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetGroupMaster of GroupMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

        [Route("getGroupUserMapping")]
        [Route("getGroupUserMapping/{GroupID}")]
        [HttpGet]
        public IHttpActionResult GetGroupUserMapping(int? GroupID = null)
        {
            try
            {
                var data = _GroupMaster.GetGroupUserMapping(GroupID);

                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetGroupUserMapping of GroupMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }


        [Route("getUserByGroups")]
        [Route("getUserByGroups/{GroupIDs}")]
        [HttpGet]
        public IHttpActionResult GetUserByGroups(string GroupIDs)
        {
            try
            {
                var data = _GroupMaster.GetUserByGroups(GroupIDs);

                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                _loggger.LogError("Error in GetUserByGroups of GroupMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError(ex);
            }
        }

    }
}