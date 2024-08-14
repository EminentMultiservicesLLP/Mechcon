using BISEPRService.Controllers;
using BISERPBusinessLayer.Repositories;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.Repositories.Branch.Interface;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/guardDetails")]
    public class GuardDetailsController : ApiController
    {
        IGuardDetailsRepository _Guard;
        private static readonly ILogger _loggger = Logger.Register(typeof(GuardDetailsController));

        public GuardDetailsController(IGuardDetailsRepository Guard)
        {
            _Guard = Guard;
        }

        [Route("createguard")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateGuardInfo(GuardInfoEntity Info)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _Guard.CreateGuardInfo(Info);
                //tax.=_fuelDetail.CreateFuelDetails(tax,)
                Info.GuardId = newID;
                Info.GuardDt.GuardId = newID;
                Info.GuardDt.InsertedBy = Info.InsertedBy;
                Info.GuardDt.InsertedON = Info.InsertedON;
                Info.GuardDt.InsertedIPAddress = Info.InsertedIPAddress;
                Info.GuardDt.InsertedMacName = Info.InsertedMacName;
                Info.GuardDt.InsertedMacID = Info.InsertedMacID;
                Info.GuardDt.Id = _Guard.CreateGuardDetails(Info.GuardDt);

                isSucecss = true;
                //}
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateGuardInfo method of GuardDetailsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<GuardInfoEntity>(Request.RequestUri + Info.GuardId.ToString(), Info);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("createRecruitedguard")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateGuardRecruitedDetails(GuardDetailsEntity dt)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newID = _Guard.CreateGuardDetails(dt);
                dt.Id = newID;
                isSucecss = true;
                //}
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateGuardRecruitedDetails method of GuardDetailsController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<GuardDetailsEntity>(Request.RequestUri + dt.Id.ToString(), dt);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("GetAllGuardInfo/{BranchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllGuardInfo(int BranchId)
        {
            List<GuardInfoEntity> GuardInfo = new List<GuardInfoEntity>();
            TryCatch.Run(() =>
            {
                var list = _Guard.GetAllGuardInfo(BranchId);
                if (list != null && list.Count() > 0)
                    GuardInfo = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllGuardInfo method of GuardDetailsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (GuardInfo.Any())
                return Ok(GuardInfo);
            else
                return Ok(GuardInfo);
        }
    }
}
