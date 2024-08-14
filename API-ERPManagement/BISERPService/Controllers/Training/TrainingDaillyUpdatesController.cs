using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPService.Controllers.Training
{
    [RoutePrefix("api/TrainingDaillyUpdates")]
    public class TrainingDaillyUpdatesController : ApiController
    {
        readonly ITrainingDaillyUpdatesRepository _daillyUpdates;
        private static readonly ILogger Loggger = Logger.Register(typeof(TrainingDaillyUpdatesController));

        public TrainingDaillyUpdatesController(ITrainingDaillyUpdatesRepository daillyUpdates)
        {
            _daillyUpdates = daillyUpdates;
        }

        [Route("CreateTrainingDaillyUpdates")]
        [AcceptVerbs("POST")]

        public IHttpActionResult CreateTrainingDaillyUpdates(TrainingDaillyUpdatesHdrEntity entity)
        {
            bool isSucecss = false;
            TryCatch.Run(() =>
            {

                _daillyUpdates.CreateTrainingDaillyUpdates(entity);

                isSucecss = true;
            }).IfNotNull(ex =>
            {
                Loggger.LogError(
                    "Error in CreateTrainingDaillyUpdates method of TrainingDaillyUpdateeController : parameter :" +
                    Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            if (isSucecss)
                return Ok(entity); //new CustomeResponse("Training Daily updates saved successfully", entity);
            else
                return BadRequest("Unknown Error! Failed to save Record, Please contact system Administrator");
        }


        [Route("GuardTrainee")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetGuardTrainee()
        {
            IEnumerable<TraineeEntity> result = null;
            TryCatch.Run(() =>
            {
                result = _daillyUpdates.GetGuardTrainee();

            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetGuardTrainee method of TrainingDaillyUpdatesController :" +
                                 Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            var traineeEntities = result as IList<TraineeEntity> ?? result.ToList();
            if (traineeEntities.IsNotNull() && traineeEntities.Any())
                return Ok(traineeEntities);
            else
                return BadRequest();
        }



        [Route("TraniningDay/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetTraniningDay(int id)
        {
            IEnumerable<TrainingDaysEntity> getTraniningDay = null;
            TryCatch.Run(() =>
            {
                getTraniningDay = _daillyUpdates.GetTraniningDay(id);

            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetTraniningDay method of TrainingDaillyUpdatesController :" +
                                 Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            var trainingDaysEntities = getTraniningDay as IList<TrainingDaysEntity> ?? getTraniningDay.ToList();
            if (trainingDaysEntities.IsNotNull() && trainingDaysEntities.Any())
                return Ok(trainingDaysEntities);
            else
                return BadRequest();
        }


        [Route("TraniningTemplatePeriod/{trainingtempdtId}/{noOfperiod}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetTraniningTemplatePeriod(int trainingtempdtId, int noOfperiod)
        {
            IEnumerable<TrainingTemplatePeriodDetails> getTraniningTemplate = null;
            TryCatch.Run(() =>
            {
                getTraniningTemplate = _daillyUpdates.GetTraniningTemplatePeriod(trainingtempdtId, noOfperiod);

            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetTraniningTemplatePeriod method of TrainingDaillyUpdatesController :" +
                                 Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            var trainingTemplatePeriodDetailses = getTraniningTemplate as IList<TrainingTemplatePeriodDetails> ??
                                                  getTraniningTemplate.ToList();
            if (trainingTemplatePeriodDetailses.IsNotNull() && trainingTemplatePeriodDetailses.Any())
                return Ok(trainingTemplatePeriodDetailses);
            else
                return BadRequest();
        }


        [Route("TraniningTemplatePeriodRecord/{trainingtempdtId}/{noOfperiod}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetTraniningTemplatePeriodRecord(int trainingtempdtId, int noOfperiod)
        {
            IEnumerable<TraniningTemplatePeriodRecordEntity> result = null;
            TryCatch.Run(() =>
            {
                result = _daillyUpdates.GetTraniningTemplatePeriodRecord(trainingtempdtId, noOfperiod);

            }).IfNotNull(ex =>
            {
                Loggger.LogError(
                    "Error in GetTraniningTemplatePeriodRecord method of TrainingDaillyUpdatesController :" +
                    Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });

            var traniningTemplatePeriodRecordEntities = result as IList<TraniningTemplatePeriodRecordEntity> ??
                                                        result.ToList();
            if (result != null && traniningTemplatePeriodRecordEntities.Any())
                return Ok(traniningTemplatePeriodRecordEntities);
            else
                return BadRequest();
        }

        [Route("TraniningTemplateSlot/{trainingTypeId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetTrainingTemplate(int trainingTypeId)
        {
            IEnumerable<TrainingEntity> getTraniningTemplate = null;
            TryCatch.Run(() =>
            {
                getTraniningTemplate = _daillyUpdates.GetTrainingTemplate(trainingTypeId);

            }).IfNotNull(ex =>
            {
                Loggger.LogError("Error in GetTrainingTemplate method of TrainingDaillyUpdatesController :" +
                                 Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            var traniningTemplate = getTraniningTemplate as IList<TrainingEntity> ?? getTraniningTemplate.ToArray();
            if (traniningTemplate.IsNotNull() && traniningTemplate.Any())
                return Ok(traniningTemplate);
            else
                return BadRequest();
        }
    }


}
