using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using System.Net.Http;
using System.Net;
using BISERPBusinessLayer.Entities.Transport;

namespace BISERPService.Controllers.Training
{

    [RoutePrefix("api/trainingtemplate")]
    public class TrainingTemplateController : ApiController 
    {
        readonly ITrainingTemplateRepository _trainingTemplate;  
        private static readonly ILogger Loggger = Logger.Register(typeof(TrainingTemplateController));
        public TrainingTemplateController(ITrainingTemplateRepository trainingTemplate) 
        {
            _trainingTemplate = trainingTemplate;
        }

       
        [Route("CreateTrainingTemplate")]
        [AcceptVerbs("POST")]
        public IHttpActionResult CreateTrainingTemplate(TrainingTempHeaderModel entity )
        {
           
            bool isSucecss = false; 
            TryCatch.Run(() =>
            {
               entity = _trainingTemplate.CreateTrainingTemplate(entity);
                isSucecss = true;
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in CreateDetails method of TrainingTemplateController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isSucecss)
                return Created<TrainingTempHeaderModel>(Request.RequestUri + entity.TrainingTemplateId.ToString(), entity);
            else
                return BadRequest("Unknown Error! Failed to save TrainingTemplate, Please contact system Administrator");
        }

        //[Route("UpdateTrainingTemplatedt")]
        //[AcceptVerbs("PUT")]
        //public HttpResponseMessage UpdateTrainingTemplatedt(TrainingTempHeaderModel entity)
        //{
        //    bool isSucecss = false;
        //    TryCatch.Run(() =>
        //    {   
        //        foreach (var item in entity.TrainingTempDaywiseModel)
        //        {
        //            item.UpdatedBy = entity.InsertedBy;
        //            item.UpdatedIPAddress = entity.InsertedIPAddress;
        //            item.UpdatedMacID = entity.InsertedMacID;
        //            item.UpdatedMacName = entity.InsertedMacName;
        //            item.UpdatedOn = entity.UpdatedOn;
        //            item.TrainingTemplateId = entity.TrainingTemplateId;
        //             isSucecss = _trainingTemplate.UpdateTrainingTempDaywise(item);
        //        }
              
        //    }).IfNotNull(ex =>
        //    {
        //        Loggger.LogError("Error in UpdateTrainingTemplate method of TrainingTemplateController : parameter :" + Environment.NewLine + ex.StackTrace);
        //        return new HttpResponseException(HttpStatusCode.InternalServerError);
        //    });
        //    if (!isSucecss)
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Failed to Update TrainingTemplate, Please contact system Administrator" };
        //    }
        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}



        [Route("gettrainingtemplatehdr")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetTrainingTemplateHdr()
        {
            IEnumerable<TrainingTempHeaderModel> Training = null;
            TryCatch.Run(() =>
            {
                Training = _trainingTemplate.GetTrainingTemplateHdr();
                
            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetTrainingTemplateHdr method of TrainingTemplateController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (Training == null)
                return BadRequest();
            return Ok(Training.ToList());
        }



        [Route("GetTrainingTempDaywise/{TrainingTemplateId}/{day}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetTrainingTempDaywise(int TrainingTemplateId, string day)
        {
            IEnumerable<TrainingTempDaywiseModel> Daywise = null;
            TryCatch.Run(() =>
            {
                Daywise = _trainingTemplate.GetTrainingTempDaywise(TrainingTemplateId,day); 

            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetTrainingTempDaywise method of TrainingTemplateController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (Daywise == null)
                return BadRequest();
            return Ok(Daywise.ToList());

        }
    }
}
