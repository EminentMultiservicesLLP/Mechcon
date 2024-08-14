using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPService.Caching;

namespace BISERPService.Controllers.Store
{
      [RoutePrefix("api/otherterms")]
    public class OtherTermsController : ApiController
    {

        IOthersTermsMasterRepository _otherTermMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(OtherTermsController));

        public OtherTermsController(IOthersTermsMasterRepository otherTermMaster)
        {
            _otherTermMaster = otherTermMaster;
        }

        private List<OthersTermsMasterEntities> AllOtherTerms()
        {
            List<OthersTermsMasterEntities> otherterm = new List<OthersTermsMasterEntities>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.OtherTerms.ToString()))
                {
                    var list = _otherTermMaster.GetAllOtherTerms();
                    if (list != null && list.Count() > 0)
                        otherterm = list.ToList();

                    MemoryCaching.AddCacheValue(CachingKeys.OtherTerms.ToString(), otherterm);
                }
                else
                {
                    otherterm = (List<OthersTermsMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.OtherTerms.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllOtherTerms method of OtherTermsController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            return otherterm;
        }

        [Route("getallotherterms")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllOtherTerms()
        {
            List<OthersTermsMasterEntities> otherterm = new List<OthersTermsMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = AllOtherTerms();
                if (list != null && list.Count() > 0)
                    otherterm = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllOtherTermMaster method of OtherTermController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (otherterm.Any())
                return Ok(otherterm);
            else
                return Ok(otherterm);
        }

        [Route("getactive")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveTerms()
        {
            List<OthersTermsMasterEntities> otherterm = new List<OthersTermsMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = AllOtherTerms();
                if (list != null && list.Count() > 0)
                    otherterm = list.Where(t => t.Deactive == false).ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveTerms method of OtherTermController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (otherterm.Any())
                return Ok(otherterm);
            else
                return BadRequest();
        }

        [Route("getothertermbyid/{id}")]
        [AcceptVerbs("GET", "POST")]
        // GET api/values/5
        public IHttpActionResult GetOtherTermById(int id)
        {
            OthersTermsMasterEntities otherterm = new OthersTermsMasterEntities(); 
            TryCatch.Run(() =>
            {
                otherterm = _otherTermMaster.GetOtherTermsById(id);

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetOtherTermById method of OtherTermController : parameter :" + id.ToString() + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (otherterm.IsNotNull())
                return Ok(otherterm);
            else
                return NotFound();
        }

        [Route("createotherterm")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult CreateOtherTerms(OthersTermsMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _otherTermMaster.CheckDuplicateItem(entity.OthersTermCode);
                if (isDuplicate == false)
                {
                    var newID = _otherTermMaster.CreateOtherTerms(entity);
                    entity.TermID = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.OtherTerms.ToString());
                }
              
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateOthersTerm method of OthersTermController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<OthersTermsMasterEntities>(Request.RequestUri + entity.TermID.ToString(), entity);
            else
                return BadRequest();        
        }

        [Route("updateotherterm")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateOtherTerms(OthersTermsMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _otherTermMaster.CheckDuplicateupdate(entity.OthersTermCode, entity.TermID);
                if (isDuplicate == false)
                {
                    isSucecss = _otherTermMaster.UpdateOtherTerms(entity);
                    MemoryCaching.RemoveCacheValue(CachingKeys.OtherTerms.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateOtherTerm method of OtherTermController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok(Created<OthersTermsMasterEntities>(Request.RequestUri + entity.TermID.ToString(), entity));
            else
                return BadRequest();
        }
        [Route("getallbasisterms")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AllBasisTerm()
        {
            List<POPriceBasisEntity> basis = new List<POPriceBasisEntity>();
            TryCatch.Run(() =>
            {
                var list = _otherTermMaster.AllBasisTerm();
                if (list != null && list.Count() > 0)
                    basis = list.ToList();
            }).IfNotNull(ex =>
            {
               _loggger.LogError("Error in get all basis method of OtherTermController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return Ok(basis);
        }
        [Route("getallInspectionterms")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult AllInspectionTerm()
        {
            List<POInspectionModel> basis = new List<POInspectionModel>();
            TryCatch.Run(() =>
            {
                var list = _otherTermMaster.AllInspectionTerm();
                if (list != null && list.Count() > 0)
                    basis = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in get all AllInspectionTerm method of OtherTermController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return Ok(basis);
        }

        [Route("createInspectionmatser")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult CreateInspectionmatser(InspectionMasterEntity entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                 isDuplicate = _otherTermMaster.CheckDuplicateInspectionItem(entity.InspectionCode);
                if (isDuplicate == false)
                {
                    int newID = _otherTermMaster.CreateInspectionmatser(entity);
                    entity.InspectionId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.OtherTerms.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateOthersTerm method of OthersTermController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
           
             if (isSucecss)
                return Created<InspectionMasterEntity>(Request.RequestUri + entity.InspectionId.ToString(), entity);
            return BadRequest();
        }

        [Route("updateInspectionmatser")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateInspectionmatser(InspectionMasterEntity entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                 isDuplicate = _otherTermMaster.CheckDuplicateInspectionupdate(entity.InspectionCode, entity.InspectionId);
                if (isDuplicate == false)
                {
                    isSucecss = _otherTermMaster.UpdateInspectionmatser(entity);
                    MemoryCaching.RemoveCacheValue(CachingKeys.OtherTerms.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateOtherTerm method of OtherTermController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
           
             if (isSucecss)
                 return Ok(Created<InspectionMasterEntity>(Request.RequestUri + entity.InspectionId.ToString(), entity));
            return BadRequest();
        }


        [Route("createBasisMaster")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult CreateBasisMaster(BasisMasterEntity entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _otherTermMaster.CheckDuplicateBasisItem(entity.BasisCode);
                if (isDuplicate == false)
                {
                    int newID = _otherTermMaster.CreateBasisMaster(entity);
                    entity.BasisID = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.OtherTerms.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateOthersTerm method of OthersTermController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Created<BasisMasterEntity>(Request.RequestUri + entity.BasisID.ToString(), entity);
            return BadRequest();
        }


        [Route("updateBasisMaster")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateBasisMaster(BasisMasterEntity entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _otherTermMaster.CheckDuplicateBasisMasterupdate(entity.BasisCode, entity.BasisID);
                if (isDuplicate == false)
                {
                    isSucecss = _otherTermMaster.UpdateBasisMaster(entity);
                    MemoryCaching.RemoveCacheValue(CachingKeys.OtherTerms.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateOtherTerm method of OtherTermController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });

            if (isSucecss)
                return Ok(Created<BasisMasterEntity>(Request.RequestUri + entity.BasisID.ToString(), entity));
            return BadRequest();
        }

    }
}
