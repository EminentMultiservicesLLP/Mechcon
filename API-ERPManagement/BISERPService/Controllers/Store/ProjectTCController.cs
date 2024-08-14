using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPService.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Store
{
    [RoutePrefix("api/projectTC")]
    public class ProjectTCController : ApiController
    {
        IProjectTCMasterRepository _projectTCMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(ProjectTCController));

        public ProjectTCController(IProjectTCMasterRepository projectTCMaster)
        {
            _projectTCMaster = projectTCMaster;
        }

        private List<ProjectTCMasterEntities> AllProjectTC()
        {
            List<ProjectTCMasterEntities> projectTC = new List<ProjectTCMasterEntities>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.ProjectTC.ToString()))
                {
                    var list = _projectTCMaster.GetAllProjectTC();
                    if (list != null && list.Count() > 0)
                        projectTC = list.ToList();

                    MemoryCaching.AddCacheValue(CachingKeys.ProjectTC.ToString(), projectTC);
                }
                else
                {
                    projectTC = (List<ProjectTCMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.ProjectTC.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllProjectTC method of ProjectTCController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return projectTC;
        }

        [Route("getallprojectTC")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllProjectTC()
        {
            List<ProjectTCMasterEntities> projectTC = new List<ProjectTCMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = AllProjectTC();
                if (list != null && list.Count() > 0)
                    projectTC = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllProjectTC method of ProjectTCController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (projectTC.Any())
                return Ok(projectTC);
            else
                return Ok(projectTC);
        }

        [Route("createProjectTC")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult CreateProjectTC(ProjectTCMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _projectTCMaster.CheckDuplicateItem(entity.ProjectTCCode);
                if (isDuplicate == false)
                {
                    var newID = _projectTCMaster.CreateProjectTC(entity);
                    entity.ProjectTCID = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.ProjectTC.ToString());
                }

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateUnit method of  ProjectTCController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<ProjectTCMasterEntities>(Request.RequestUri + entity.ProjectTCID.ToString(), entity);
            else
                return BadRequest();
        }

        [Route("updateProjectTC")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateProjectTC(ProjectTCMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _projectTCMaster.CheckDuplicateupdate(entity.ProjectTCCode, entity.ProjectTCID);
                if (isDuplicate == false)
                {
                    isSucecss = _projectTCMaster.UpdateProjectTC(entity);
                    MemoryCaching.RemoveCacheValue(CachingKeys.ProjectTC.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateProjectTC method of ProjectTCController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok(Created<ProjectTCMasterEntities>(Request.RequestUri + entity.ProjectTCID.ToString(), entity));
            else
                return BadRequest();
        }

    }
}
