using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
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
    [RoutePrefix("api/vendormi")]
    public class VendorMaterialIssueController : ApiController
    {
        IVendorMaterialIssueRepository _ivmi;
        IVendorMaterialIssueDtRepository _ivmidt;
        IVendorMaterialIssueCommonRepository _ivmicommon;
        private static readonly ILogger _loggger = Logger.Register(typeof(VendorMaterialIssueController));

        public VendorMaterialIssueController(IVendorMaterialIssueRepository ivmi, IVendorMaterialIssueDtRepository ivmidt, IVendorMaterialIssueCommonRepository ivmicommon)
        {
            _ivmi = ivmi;
            _ivmidt = ivmidt;
            _ivmicommon = ivmicommon;
        }

        [Route("createissue")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateVendorMI(VendorMaterialIssueEntity entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                entity = _ivmicommon.SaveMaterialIssue(entity);
                if (entity.IssueId == 0)
                {
                    isSucecss = false;
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateVendorMI method of VendorMaterialIssueController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<VendorMaterialIssueEntity>(Request.RequestUri + entity.IssueId.ToString(), entity);
            else
                return BadRequest();
        }

        [Route("issueforauth/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetVendorMaterialIssue(int UserId)
        {
            List<VendorMaterialIssueEntity> materialissue = new List<VendorMaterialIssueEntity>();
            TryCatch.Run(() =>
            {
                var list = _ivmi.GetVendorMaterialIssue(UserId);
                if (list != null && list.Count() > 0)
                    materialissue = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveMaterialIssue method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materialissue.IsNull())
                return Ok();
            else if (materialissue.Any())
                return Ok(materialissue);
            else
                return Ok(materialissue);
        }

        [Route("issue/{UserId}/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetVendorMaterialIssues(int UserId, int StoreId)
        {
            List<VendorMaterialIssueEntity> materialissue = null;
            TryCatch.Run(() =>
            {
                var list = _ivmi.GetVendorMaterialIssuestore(UserId, StoreId);
                if (list.IsNotNull() && list.Any())
                    materialissue = list.ToList();
            }).IfNotNull(ex =>
            {
                materialissue = null;
                _loggger.LogError("Error in GetActiveMaterialIssue method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
               
            });
            if (materialissue.IsNull())
                return Ok();
            if (materialissue.IsNotNull())
                return Ok(materialissue);
           // return BadRequest();
            return InternalServerError();
        }

        [Route("getById/{IssueId}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult VendorMaterialIssueById(int IssueId)
        {
            VendorMaterialIssueEntity materialIssue = null;
            TryCatch.Run(() =>
            {
                materialIssue = _ivmi.GetByIssueId(IssueId);
                materialIssue.VendorMaterialIssueDt = _ivmidt.GetDetailByIssueID(IssueId).ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in VendorMaterialIssueById method of VendorMaterialIssueController : parameter :" + IssueId.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materialIssue.IsNull())
                return Ok();
            else if (materialIssue.IsNotNull())
                return Ok(materialIssue);
            else
                return Ok(materialIssue);
        }

        [Route("authcancelmaterialissue")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthCancelMaterialIssue(VendorMaterialIssueEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _ivmicommon.AuthCancelVendorMaterialIssue(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AuthCancelMaterialIssue method of VendorMaterialIssueController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("getVenById/{IssueId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GRNVendorMaterialIssueById(int IssueId)
        {
            List<VendorMaterialIssueDtEntity> miredt = new List<VendorMaterialIssueDtEntity>();
            TryCatch.Run(() =>
            {
                var list = _ivmidt.GetDetailByIssueIDGRN(IssueId);
                if (list != null && list.Count() > 0)
                    miredt = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetPrById method of PurchaseReturnController : parameter :" + IssueId.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (miredt.IsNotNull())
                return Ok(miredt);
            else
                return NotFound();
        }
    }
}
