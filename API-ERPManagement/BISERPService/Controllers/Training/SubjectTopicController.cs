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
    [RoutePrefix("api/subjecttopic")]
    public class SubjectTopicController : ApiController
    {
        ISubjectTopicRepository _subjectTopic;
        private static readonly ILogger _loggger = Logger.Register(typeof(SubjectTopicController));

        public SubjectTopicController(ISubjectTopicRepository SubjectTopic)
        {
            _subjectTopic = SubjectTopic;
        }

         private List<SubjectTopicEntity> AllSubjectTopics()
         {
             List<SubjectTopicEntity> type = new List<SubjectTopicEntity>();
             TryCatch.Run(() =>
             {
                 if (!MemoryCaching.CacheKeyExist(CachingKeys.AllSubjectTopics.ToString()))
                 {
                     var list = _subjectTopic.GetAllSubjectTopic();
                     if (list != null && list.Count() > 0)
                         type = list.ToList();
                     MemoryCaching.AddCacheValue(CachingKeys.AllSubjectTopics.ToString(), type);
                 }
                 else
                 {
                     type = (List<SubjectTopicEntity>)(MemoryCaching.GetCacheValue(CachingKeys.AllSubjectTopics.ToString()));
                 }
             }).IfNotNull(ex =>
             {
                 _loggger.LogError("Error in AllSubjectTopics method of SubjectTopicController :" + Environment.NewLine + ex.StackTrace);
                 return InternalServerError();
             });
             return type;
         }

        [Route("allsubjecttopics")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllSubjectTopics()
        {
            IEnumerable<SubjectTopicEntity> Subject = null;
            TryCatch.Run(() =>
            {
                Subject = AllSubjectTopics();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllSubjectTopics method of SubjectTopicController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            }); ;

            if (Subject == null)
                 return BadRequest();
           return Ok(Subject.ToList());
         
        }

        [Route("CreateSubjectTopic")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateSubjectTopic(SubjectTopicEntity Subject)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _subjectTopic.CheckDuplicateItem(Subject.TopicCode);
                if (isDuplicate == false)
                {
                    var newID = _subjectTopic.CreateSubjectTopic(Subject);
                    Subject.SubjectId = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllSubjectTopics.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateSubjectTopic method of SubjectTopicController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<SubjectTopicEntity>(Request.RequestUri + Subject.SubjectId.ToString(), Subject);
            else
                return BadRequest();
        }
        [Route("UpdateSubjectTopic")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateSubjectTopic(SubjectTopicEntity Subject)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _subjectTopic.CheckDuplicateupdate(Subject.TopicCode, Subject.TopicId);
                if (isDuplicate == false)
                {
                    isSucecss = _subjectTopic.UpdateSubjectTopic(Subject);
                    MemoryCaching.RemoveCacheValue(CachingKeys.AllSubjectTopics.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateSubjectTopic method of SubjectTopicController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok();
            else
                return BadRequest();
        }


        [Route("getactiveSubjectTopic/{SubjectId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetActiveSubjectTopics(int SubjectId)
        {
            List<SubjectTopicEntity> units = new List<SubjectTopicEntity>();
            TryCatch.Run(() =>
            {
                var list = AllSubjectTopics();
                if (list != null && list.Count() > 0)
                    units = list.Where(m => m.Deactive == false && m.SubjectId == SubjectId).ToList();

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetActiveSubjectTopics method of SubjectTopicController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (units.Any())
                return Ok(units);
            else
                return BadRequest();
        }
    }
}
