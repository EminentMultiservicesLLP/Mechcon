using BISERPBusinessLayer.Entities.Store;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BISERPCommon.Extensions;
using System.Net;
using BISERPBusinessLayer.Entities.Store.Reports;
using BISERPBusinessLayer.Repositories.Store.Interfaces;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/materialissue")]
    public class MaterialIssueController :ApiController
    {
        IMaterialIssueRepository _materialIssue;
        IMaterialIssueDetailRepository _materialdetailIssue;
        IMaterialIssueCommonRepository _materialIssuecommon;
        private static readonly ILogger _loggger = Logger.Register(typeof(MaterialIssueController));

        public MaterialIssueController(IMaterialIssueRepository materialIssue, IMaterialIssueDetailRepository materialdetailIssue,
                IMaterialIssueCommonRepository materialIssuecommon)
        {
            _materialIssue = materialIssue;
            _materialdetailIssue = materialdetailIssue;
            _materialIssuecommon = materialIssuecommon;
        }

        [Route("getAll/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMaterialIssue(int UserId)
        {
            List<MaterialIssueEntity> materialIssue = null;
            TryCatch.Run(() =>
            {
                var list = _materialIssue.GetAllList(UserId);
                if (list != null && list.Count() > 0)
                    materialIssue = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialIssue method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materialIssue.IsNull())
                return Ok();
            else if (materialIssue.Any())
                return Ok(materialIssue);
            else
                return BadRequest();
        }



        [Route("getallmiFileDownload/{UserId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllMaterialIssueFileDownload(int UserId)
        {
            IEnumerable<MaterialIssueEntity> materialIssue = null;
            TryCatch.Run(() =>
            {
                materialIssue = _materialIssue.GetAllMaterialIssueFileDownload(UserId);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialIssueFileDownload method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
       
            if (materialIssue == null)
                return BadRequest();
            return Ok(materialIssue.ToList());
        }

        [Route("getmissue/{StoreId}/{fromdate}/{todate}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetMaterialIssue(int StoreId, DateTime fromdate, DateTime todate)
        {
            List<MaterialIssueEntity> materialIssue = null;
            TryCatch.Run(() =>
            {
                var list = _materialIssue.GetMaterialIssue(StoreId,fromdate, todate);
                if (list != null && list.Count() > 0)
                    materialIssue = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMaterialIssue method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materialIssue.IsNull())
                return Ok();
            else if (materialIssue.Any())
                return Ok(materialIssue);
            else
                return BadRequest();
        }

        [Route("GetAllmirptList/{StoreId}/{fromdate}/{todate}/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllmirptList(int StoreId, DateTime fromdate, DateTime todate, int UserId)
        {
            IEnumerable<MaterialIssueEntity> materialIssue = null;
            TryCatch.Run(() =>
            {
                materialIssue = _materialIssue.GetAllmirptList(StoreId, fromdate, todate, UserId);
              
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllmirptList method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
         
            if (materialIssue == null)
                return BadRequest();
            return Ok(materialIssue.ToList());
        }

        [Route("getunacceptedauthorized")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetUnAcceptedAuthorized()
        {
            List<MaterialIssueEntity> materialIssue = null;
            TryCatch.Run(() =>
            {
                var list = _materialIssue.GetUnAcceptedAuthorized();
                if (list != null && list.Count() > 0)
                    materialIssue = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetUnAcceptedAuthorized method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materialIssue.IsNull())
                return Ok();
            else if (materialIssue.Any())
                return Ok(materialIssue);
            else
                return BadRequest();
        }

        [Route("getActive/{userId}/{LocId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveMaterialIssue(int userId, int LocId)
        {
            List<MaterialIndentEntities> materilaIndent = null;
            TryCatch.Run(() =>
            {
                var list = _materialIssue.GetActiveList(userId, LocId);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveMaterialIssue method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.Any())
                return Ok(materilaIndent);
            else
                return BadRequest();
        }

        [Route("getById/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetMaterialIssueById(int id)
        {
            MaterialIndentEntities materilaIndent = null;
            TryCatch.Run(() =>
            {
                materilaIndent = _materialIssue.GetDetailByID(id);

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMaterialIssueById method of MaterialIssueController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.IsNotNull())
                return Ok(materilaIndent);
            else
                return NotFound();
        }

        [Route("getmidetailbyid/{IssueId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetMaterialIssueDetailsByIssueId(int IssueId)
        {
            List<MaterialIssueDetailsEntity> midt = null;
            TryCatch.Run(() =>
            {
                var list = _materialdetailIssue.GetDetailByIssueID(IssueId);
                if (list != null && list.Count() > 0)
                    midt = list.ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMaterialIssueDetailsByIssueId method of MaterialIssueController : parameter :" + IssueId.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (midt.IsNull())
                return Ok();
            else if (midt.IsNotNull())
                return Ok(midt);
            else
                return NotFound();
        }

        [Route("createIssue")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateMaterialIssue(MaterialIssueEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                var newEntity = _materialIssuecommon.SaveMaterialIssue(entity);
                entity = newEntity;
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateMaterialIssue method of MaterialIssueController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<MaterialIssueEntity>(Request.RequestUri + entity.Indent_Id.ToString(), entity);
            else
                return BadRequest("Server Error! Please Contact Administrator!");
        }

        [Route("updateIssue")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateMaterialIssue(MaterialIssueEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _materialIssue.UpdateEntry(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateMaterialIssue method of ItemMasterController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("authcancel")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AuthCancelMaterialIssue(MaterialIssueEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _materialIssuecommon.AuthCancelMaterialIssue(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AuthCancelMaterialIssue method of MaterialIssueController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }
        [Route("getallmlist/{userId}/{LocId}/{indType}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllMaterialList(int userId, int LOCId, string indType)
        {
            List<MaterialIssueEntity> materialIssue = null;
            TryCatch.Run(() =>
            {
                var list = _materialIssue.GetIssuenoforReturn(userId, LOCId, indType);
                if (list != null && list.Count() > 0)
                    materialIssue = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialList of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materialIssue.IsNull())
                return Ok();
            else if (materialIssue.Any())
                return Ok(materialIssue);
            else
                return BadRequest();
        }

        [Route("issueacceptance")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AcceptMaterialIssue(MaterialIssueEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {
                isSucecss = _materialIssuecommon.AcceptMaterialIssue(entity);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AcceptMaterialIssue method of MaterialIssueController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }

        [Route("mireport/{IssueId}/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetMaterialIssue(int IssueId, int UserId)
        {
            MaterialIssueEntity materialIssue = new MaterialIssueEntity();
            TryCatch.Run(() =>
            {
                materialIssue = _materialIssuecommon.GetMaterialIssue(IssueId, UserId);
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetMaterialIssue method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materialIssue.IsNull())
                return Ok();
            else if (materialIssue.IsNotNull())
                return Ok(materialIssue);
            else
                return NotFound();
        }
        [Route("getallmidsrpt/{Fromdate}/{todate}/{fStoreId}/{tStoreId}")]
        [Route("getallmidsrpt/{Fromdate}/{todate}/{tStoreId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllMaterialIssuedtRPT(DateTime Fromdate, DateTime todate, int tStoreId, int? fStoreId= null )
        {
            List<MaterialIssueEntity> materilaissue = new List<MaterialIssueEntity>();
            TryCatch.Run(() =>
            {
                var list = _materialIssuecommon.GetMaterialIssueRpt(Fromdate, todate, fStoreId, tStoreId);
                if (list != null && list.Count() > 0)
                    materilaissue = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllMaterialIssuedtRPT method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaissue.IsNull())
                return Ok();
            else if (materilaissue.Any())
                return Ok(materilaissue);
            else
                return Ok(materilaissue);
        }
        [Route("getallpmidsrpt/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllpendingMaterialIssuedtRPT( int UserId)
        {
            List<MaterialIssueEntity> materilaIndent = new List<MaterialIssueEntity>();
            TryCatch.Run(() =>
            {
                var list = _materialIssuecommon.GetPendingMaterialRpt(UserId);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllpendingMaterialIssuedtRPT method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.Any())
                return Ok(materilaIndent);
            else
                return Ok(materilaIndent);
        }

        [Route("getallcmidsrpt/{Fromdate}/{todate}/{StoreId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllCancelMaterialIssuedtRPT(DateTime Fromdate, DateTime todate, int StoreId)
        {
            List<MaterialIssueEntity> materilaIndent = new List<MaterialIssueEntity>();
            TryCatch.Run(() =>
            {
                var list = _materialIssuecommon.GetCancelledMaterialRpt(Fromdate, todate, StoreId);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllCancelMaterialIssuedtRPT method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.Any())
                return Ok(materilaIndent);
            else
                return Ok(materilaIndent);
        }

        [Route("miitemwiserpt/{Fromdate}/{todate}/{ItemId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult MaterialIssueItemwise(DateTime fromdate, DateTime todate, int ItemId)
        {
            List<MaterialIssueEntity> materilaIndent = new List<MaterialIssueEntity>();
            TryCatch.Run(() =>
            {
                var list = _materialIssuecommon.MaterialIssueItemwise(fromdate, todate, ItemId);
                if (list != null && list.Count() > 0)
                    materilaIndent = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in MaterialIssueItemwise method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materilaIndent.IsNull())
                return Ok();
            else if (materilaIndent.Any())
                return Ok(materilaIndent);
            else
                return Ok(materilaIndent);
        }

        
        [Route("MaterialIssueAllBranch/{Fromdate}/{todate}")]
        [Route("MaterialIssueAllBranch")]
        [AcceptVerbs("GET")]
        public IHttpActionResult MaterialIssueDetailsAllBranch(DateTime? fromdate = null, DateTime? todate = null)
        {
           IEnumerable<MaterialIssueAllBranchEntity> materialIssue = null;
            TryCatch.Run(() =>
            {
                materialIssue = _materialIssuecommon.MaterialIssueDetailsAllBranch(fromdate, todate);
                //if (list != null && list.Any())
                //    materialIssue = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in MaterialIssueDetailsAllBranch method of MaterialIssueController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (materialIssue == null)
                return BadRequest();
            return Ok(materialIssue.ToList());
        }
    }
}