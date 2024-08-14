using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPBusinessLayer.Entities.Training;
using BISERPService.Caching;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/subject")]
    public class SubjectMasterController : ApiController
    {
        ISubjectMasterRepository _Subject;
        private static readonly ILogger _loggger = Logger.Register(typeof(SubjectMasterController));

        public SubjectMasterController(ISubjectMasterRepository Subject)
        {
            _Subject = Subject;
        }

         private List<SubjectEntity> AllSubject()
         {
             List<SubjectEntity> type = new List<SubjectEntity>();
             TryCatch.Run(() =>
             {
                 if (!MemoryCaching.CacheKeyExist(CachingKeys.AllSubject.ToString()))
                 {
                     var list = _Subject.GetAllSubject();
                     if (list != null && list.Count() > 0)
                         type = list.ToList();
                     MemoryCaching.AddCacheValue(CachingKeys.AllSubject.ToString(), type);
                 }
                 else
                 {
                     type = (List<SubjectEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllSubject.ToString()));
                 }
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in AllSubject method of SubjectController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             return type;
         }

        [Route("GetAllSubject")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllSubject()
        {
            IEnumerable<SubjectEntity> Subject = null;
            TryCatch.Run(() =>
            {
                Subject = AllSubject();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllSubject method of SubjectController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (Subject == null)
                 return BadRequest();
                return Ok(Subject.ToList());
        }

        [Route("CreateSubject")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateSubject(SubjectEntity Subject)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _Subject.CheckDuplicateItem(Subject.SubjectCode);
                if (isDuplicate == false)
                {
                    var newID = _Subject.CreateSubject(Subject);
                    Subject.SubjectId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllSubject.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateSubject method of SubjectController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<SubjectEntity>(Request.RequestUri + Subject.SubjectId.ToString(), Subject);
            else
                return BadRequest();
        }
        [Route("UpdateSubject")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateSubject(SubjectEntity Subject)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _Subject.CheckDuplicateupdate(Subject.SubjectCode, Subject.SubjectId);
                if (isDuplicate == false)
                {
                    isSucecss = _Subject.UpdateSubject(Subject);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllSubject.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateSubject method of SubjectController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactiveSubject")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetActiveSubject()
        {
            List<SubjectEntity> units = new List<SubjectEntity>();
            TryCatch.Run(() =>
            {
                var list = AllSubject();
                if (list != null && list.Count() > 0)
                    units = list.Where(m => m.Deactive == false).ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveSubject method of SubjectController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
