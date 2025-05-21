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
    [System.Web.Http.RoutePrefix("api/MasterListOfProject")]
    public class MasterListOfProjectController : ApiController
    {
        IMasterListOfProjectRepository _masterListOfProject;
        private static readonly ILogger _loggger = Logger.Register(typeof(MasterListOfProjectController));

        public MasterListOfProjectController(IMasterListOfProjectRepository masterListOfProject)
        {
            _masterListOfProject = masterListOfProject;
        }

        [Route("getMasterListOfProject/{financialYearID}/{pending}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetMasterListOfProject(int financialYearID,int pending)
        {
            List<MasterListOfProjectEntity> Dtl = new List<MasterListOfProjectEntity>();
            TryCatch.Run(() =>
            {
                var list = _masterListOfProject.GetMasterListOfProject(financialYearID, pending);
                if (list.IsNotNull() && list.Any())
                    Dtl = list.ToList();
            }).IfNotNull(ex =>
            {
                Dtl = null;
                _loggger.LogError("Error in GetMasterListOfProject method of MasterListOfProjectController :" + Environment.NewLine + ex.StackTrace);

            });
            if (Dtl.IsNotNull())
                return Ok(Dtl);
            return InternalServerError();
        }

        [Route("getProjectCostingSummary/{financialYearID}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetProjectCostingSummary(int financialYearID)
        {
            List<ProjectCostingSummaryEntity> Dtl = new List<ProjectCostingSummaryEntity>();
            TryCatch.Run(() =>
            {
                var list = _masterListOfProject.GetProjectCostingSummary(financialYearID);
                if (list.IsNotNull() && list.Any())
                    Dtl = list.ToList();
            }).IfNotNull(ex =>
            {
                Dtl = null;
                _loggger.LogError("Error in GetProjectCostingSummary method of MasterListOfProjectController :" + Environment.NewLine + ex.StackTrace);

            });
            if (Dtl.IsNotNull())
                return Ok(Dtl);
            return InternalServerError();
        }
    }
}