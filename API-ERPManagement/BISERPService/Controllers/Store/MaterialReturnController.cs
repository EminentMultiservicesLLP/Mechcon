using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BISERPBusinessLayer.Repositories.Store;
using BISERPBusinessLayer.Entities.Store;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
namespace BISERPService.Controllers
{
    [RoutePrefix("api/materialreturn")]
    public class MaterialReturnController : ApiController
    {
        IMaterialReturnRepository _materialReturn;
        IMaterialReturnDetailsRepository _materialdetailReturn;
        IMaterialReturnCommonRepository _matcommon;
        IRequestStatusRepository _requestStatus;
        private static readonly ILogger _loggger = Logger.Register(typeof(MaterialReturnController));

        public MaterialReturnController(IMaterialReturnRepository materialReturn, IMaterialReturnDetailsRepository materialdetailReturn,
                                        IMaterialReturnCommonRepository matcommon, IRequestStatusRepository requestStatus)
        {
            _materialReturn = materialReturn;
            _materialdetailReturn = materialdetailReturn;
            _matcommon = matcommon;
            _requestStatus = requestStatus;
        }

        [Route("getAllmrslist")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMaterialIssueList()
        {
            List<MaterialReturnEntity> materialReturn = new List<MaterialReturnEntity>();
            TryCatch.Run(() =>
            {
                var list = _materialReturn.GetAllMaterialIssue();
                if (list != null && list.Count() > 0)
                    materialReturn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialIssueList of MaterialReturnController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materialReturn.IsNotNull())
                return Ok(materialReturn);
            else
                return BadRequest();
        }

        [Route("getAllmr/{Authorized}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMaterialReturn(int Authorized)
        {
            List<MaterialReturnEntity> materialReturn = new List<MaterialReturnEntity>();
            TryCatch.Run(() =>
            {
                var list = _materialReturn.GetAllMaterialReturn(Authorized);
                if (list != null && list.Count() > 0)
                    materialReturn = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialReturn of MaterialReturnController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materialReturn.Any())
                return Ok(materialReturn);
            else
                return Ok(materialReturn);
        }

        [Route("creatematerialReturn")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateMaterialReturn(MaterialReturnEntity entity)
        {
            bool isSucecss = true;
            TryCatch.Run(() =>
            {
                entity = _matcommon.SaveMREDetails(entity);
                if (entity.ReturnID == 0)
                {
                    isSucecss = false;
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreatematerialReturn method of materialReturnController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<MaterialReturnEntity>(Request.RequestUri + entity.ReturnID.ToString(), entity);
            else
                return BadRequest();
        }

        [Route("getmiredetailbyid/{IssueId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetMaterialIssueDetailsByIssueId(int IssueId)
        {
            List<MaterialReturnDetailsEntities> miredt = null;
            TryCatch.Run(() =>
            {
                var list = _materialdetailReturn.GetDetailByIssueID(IssueId);
                if (list != null && list.Count() > 0)
                    miredt = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMaterialIssueDetailsByIssueId method of MaterialIssueController : parameter :" + IssueId.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (miredt.IsNotNull())
                return Ok(miredt);
            else
                return NotFound();
        }


        [Route("getmaterialreturntbyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetById(int id)
        {
            List<MaterialReturnDetailsEntities> miredt = new List<MaterialReturnDetailsEntities>();
            TryCatch.Run(() =>
            {
                var list = _materialdetailReturn.MaterialReturnDetailsById(id);
                if (list != null && list.Count() > 0)
                    miredt = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMaterialIndentById method of MaterialIndentController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (miredt.IsNotNull())
                return Ok(miredt);
            else
                return NotFound();
        }

        [Route("authcancel")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthCancelMaterialReturn(MaterialReturnEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newEtity = _requestStatus.UpdateMRAuthRequestStatus(entity.ReturnID);
                foreach (var IssueDetail in entity.MaterialReturnDt)
                {
                    isSucecss = _materialReturn.UpdateMaterialReturnAuth(entity, IssueDetail);
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AuthCancelMaterialReturn method of MaterialIssueController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }
    }
}
