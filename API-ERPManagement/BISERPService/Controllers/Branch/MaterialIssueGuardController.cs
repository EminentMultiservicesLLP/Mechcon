using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.Repositories.Branch.Interface;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/missueguard")]
    public class MaterialIssueGuardController : ApiController
    {
        IMaterialIssueGuardRepository _materialIssueguard;
        IMaterialIssueGuardDtRepository _materialdetailIssueguardDt;
        IMaterialIssueGuardCommonRepository _miGuardCommon;
        private static readonly ILogger _loggger = Logger.Register(typeof(MaterialIssueGuardController));

        public MaterialIssueGuardController(IMaterialIssueGuardRepository materialIssueguard, IMaterialIssueGuardDtRepository materialdetailIssueguardDt,
                                            IMaterialIssueGuardCommonRepository miGuardCommon)
        {
            _materialIssueguard = materialIssueguard;
            _materialdetailIssueguardDt = materialdetailIssueguardDt;
            _miGuardCommon = miGuardCommon;
        }

        [Route("createIssueguard")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateMaterialIssueGuard(MaterialIssueGuardEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                entity = _miGuardCommon.SaveMaterialIssueGuard(entity);
                if (entity.IssueId == 0)
                {
                    isSucecss = false;
                }
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateMaterialIssueGuard method of MaterialIssueGuardController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<MaterialIssueGuardEntity>(Request.RequestUri + entity.IssueId.ToString(), entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("getissueguard/{StoreId}/{EmpId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetIssueList(int StoreId, int EmpId)
        {
            List<MaterialIssueGuardEntity> items = new List<MaterialIssueGuardEntity>();
            TryCatch.Run(() =>
            {
                var list = _materialIssueguard.GetIssueList(StoreId, EmpId);
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetIssueList method of MaterialIssueGuardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("getissueguard/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllGuardIssueList(int UserId)
        {
            List<MaterialIssueGuardEntity> items = new List<MaterialIssueGuardEntity>();
            TryCatch.Run(() =>
            {
                var list = _materialIssueguard.GetAllGuardIssueList(UserId);
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllGuardIssueList method of MaterialIssueGuardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }

        [Route("getissueguarddt/{IssueId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetIssueDetails(int IssueId)
        {
            List<MaterialIssueGuardDtEntity> items = new List<MaterialIssueGuardDtEntity>();
            TryCatch.Run(() =>
            {
                var list = _materialdetailIssueguardDt.GetIssueDetails(IssueId);
                if (list != null && list.Count() > 0)
                    items = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetIssueDetails method of MaterialIssueGuardController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (items.Any())
                return Ok(items);
            else
                return BadRequest();
        }



        [Route("getissueguardReceipt/{branchId}/{IssueId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMaterialIssueGuradRPT(int branchId, int IssueId)
        {
            List<MaterialIssueGuardEntity> materilaIssue = new List<MaterialIssueGuardEntity>();
            TryCatch.Run(() =>
            {
                var list = _miGuardCommon.GetGuardIssueReceipt(branchId, IssueId);
                if (list != null && list.Count() > 0)
                    materilaIssue = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialIssueGuradRPT method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIssue.IsNull())
                return Ok();
            else if (materilaIssue.Any())
                return Ok(materilaIssue);
            else
                return Ok(materilaIssue);
        }

        [Route("getissueguarddtReceipt/{Fromdate}/{todate}/{branchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMaterialIssueGuraddtRPT(DateTime fromdate, DateTime todate, int branchId)
        {
            List<MaterialIssueGuardEntity> materilaIssue = new List<MaterialIssueGuardEntity>();
            TryCatch.Run(() =>
            {
                var list = _miGuardCommon.GetGuardIssueReceiptdt(fromdate, todate,branchId);
                if (list != null && list.Count() > 0)
                    materilaIssue = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialIssueGuraddtRPT method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIssue.IsNull())
                return Ok();
            else if (materilaIssue.Any())
                return Ok(materilaIssue);
            else
                return Ok(materilaIssue);
        }


        [Route("getAllissueguard/{branchId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMaterialIssueGurad( int branchId)
        {
            List<MaterialIssueGuardEntity> materilaIssue = new List<MaterialIssueGuardEntity>();
            TryCatch.Run(() =>
            {
                var list = _materialIssueguard.AllGaurdMaterialIssue(branchId);
                if (list != null && list.Count() > 0)
                    materilaIssue = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialIssueGurad method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIssue.IsNull())
                return Ok();
            else if (materilaIssue.Any())
                return Ok(materilaIssue);
            else
                return Ok(materilaIssue);
        }


    }
}
