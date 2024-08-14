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
using BISERPBusinessLayer.Entities.Training;

namespace BISERPService.Controllers
{
    [RoutePrefix("api/TrainingCent")]
    public class TrainingCentreController : ApiController
    {
        ITrainingCentreRepository _Training;
        private static readonly ILogger _loggger = Logger.Register(typeof(TrainingCentreController));

        public TrainingCentreController(ITrainingCentreRepository Training)
        {
            _Training = Training;
        }


        [Route("GetAllTrainingCentre/{UserId}")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllTrainingCentre(int UserId)
        {
            List<TrainingCentreEntity> TrainingCentre = new List<TrainingCentreEntity>();
            TryCatch.Run(() =>
            {
                var list = _Training.GetAllTrainingCentre(UserId);
                if (list != null && list.Count() > 0)
                    TrainingCentre = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllTrainingCentre method of TrainingCentreController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (TrainingCentre.Any())
                return Ok(TrainingCentre);
            else
                return Ok(TrainingCentre);
        }
    }
}
